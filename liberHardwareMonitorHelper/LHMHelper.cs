using LibreHardwareMonitor.Hardware;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;

namespace liberHardwareMonitorHelper
{
    public class liberHardwareMonitorHelper
    {
        Computer computer;
        public liberHardwareMonitorHelper()
        {
            computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true
            };
            computer.Open();
        }

        ~liberHardwareMonitorHelper()
        {
            computer.Close();
        }

        public List<(String hardware, String sensor)> ListOfSensors()
        {
            var sensors = new List<(String hardware, String sensor)>();

            computer.Accept(new UpdateVisitor());//refresh sensor data

            foreach (IHardware hardware in computer.Hardware)
            {
                foreach (ISensor sensor in hardware.Sensors)
                {
                    if (sensor.Value != null)
                    {
                        String sensorName = sensor.Name + "_" + sensor.SensorType.ToString();
                        sensors.Add((hardware.Name, sensorName));
                    }
                }
            }

            return sensors;
        }

        public void DeleteAllVariables()
        {

            computer.Accept(new UpdateVisitor());//refresh sensor data

            foreach (IHardware hardware in computer.Hardware)
            {
                foreach (ISensor sensor in hardware.Sensors)
                {
                    if (sensor.Value != null)
                    {
                        String sensorName = sensor.Name + "_" + sensor.SensorType.ToString();
                        sensorName = sensorName.Replace(" ", "_").ToLower();
                        VariableManager.DeleteVariable(sensorName);
                        var test = VariableManager.Variables;
                    }
                }
            }

        }

        public void Monitor(MacroDeckPlugin Instance, List<(String hardware, String sensor)> requestedSensors)
        {
            if (requestedSensors != null)
            {
                foreach (IHardware hardware in computer.Hardware)
                {
                    if (requestedSensors.Any(tuple => tuple.hardware == hardware.Name))
                    {
                        hardware.Update();
                        foreach (ISensor sensor in hardware.Sensors)
                        {
                            if (sensor.Value != null)
                            {
                                String sensorName = sensor.Name + "_" + sensor.SensorType.ToString();
                                sensorName = sensorName.Replace(" ", "_").ToLower();
                                if (requestedSensors.Any(tuple => tuple.sensor.Replace(" ", "_").ToLower() == sensorName))
                                    VariableManager.SetValue(sensorName, sensor.Value, VariableType.Float, Instance, null);
                            }
                        }
                    }
                }
            }
        }
    }

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
