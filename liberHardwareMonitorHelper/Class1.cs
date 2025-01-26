using LibreHardwareMonitor.Hardware;
using SuchByte.MacroDeck.Logging;
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

        public void Monitor(MacroDeckPlugin Instance)
        {                      
            computer.Accept(new UpdateVisitor());//refresh sensor data

            foreach (IHardware hardware in computer.Hardware)
            {
                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        if (sensor.Value != null)
                        {
                            String name = sensor.Name + "_" + sensor.SensorType.ToString();
                            VariableManager.SetValue(name, sensor.Value, VariableType.Float, Instance, null);
                        }
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    if (sensor.Value != null)
                    {
                        String name = sensor.Name + "_" + sensor.SensorType.ToString();
                        VariableManager.SetValue(name, sensor.Value, VariableType.Float, Instance, null);
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
