using Newtonsoft.Json;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    public partial class PluginConfig : DialogForm
    {
        private MacroDeckPlugin _main;
        private List<(String hardware, String sensor)> requestedSensors;
        public PluginConfig(MacroDeckPlugin main, List<(String hardware, String sensor)> listOfSensors)
        {
            _main = main;
            requestedSensors = new List<(String hardware, String sensor)>();
            InitializeComponent();

            string serialized = PluginConfiguration.GetValue(_main, "requestedSensors");
            if (serialized != "")
            {
                requestedSensors = JsonConvert.DeserializeObject<List<(String hardware, String sensor)>>(serialized);
            }
            string strPollingRate = PluginConfiguration.GetValue(_main, "pollingRate");
            if (strPollingRate != "")
            {
                trackBar1.Value = int.Parse(strPollingRate);
                textBox1.Text = strPollingRate;
            }

            this.FormClosing += new FormClosingEventHandler(SaveRequestedSensorsOnClose);
            this.dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dataGridView1_CurrentCellDirtyStateChanged);
            UpdateSensorsList(listOfSensors);
        }

        public int getPollingRate()
        {
            return trackBar1.Value;
        }

        public List<(String hardware, String sensor)> getRequestedSensors()
        {
            return requestedSensors;
        }
        private void UpdateSensorsList(List<(String hardware, String sensor)> listOfSensors)
        {
            foreach (var sensor in listOfSensors)
            {
                bool chk = false;
                if (requestedSensors.Any(tuple => tuple.sensor == sensor.sensor && tuple.hardware == sensor.hardware))
                    chk = true;
                dataGridView1.Rows.Add(chk, sensor.sensor, sensor.hardware);
            }
        }

        private void SaveRequestedSensorsOnClose(object sender, FormClosingEventArgs e)
        {
            PluginConfiguration.DeletePluginConfig(_main);
            requestedSensors.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (chk.Value != null && (bool)chk.Value)
                    requestedSensors.Add((row.Cells[2].Value.ToString(), row.Cells[1].Value.ToString()));
            }

            if (requestedSensors.Count > 0)
            {
                var serialized = JsonConvert.SerializeObject(requestedSensors);
                PluginConfiguration.SetValue(_main, "requestedSensors", serialized);
            }
            PluginConfiguration.SetValue(_main, "pollingRate", trackBar1.Value.ToString());            
        }


        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LibreHardwareMonitorPlugin libreHardwareMonitorPlugin = _main as LibreHardwareMonitorPlugin;
            if (libreHardwareMonitorPlugin != null)
                libreHardwareMonitorPlugin.DeleteAllVariables();
            else
                MacroDeckLogger.Error(_main, "when deleteting _main was not of type LibreHardwareMonitorPlugin");
        }

        void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
