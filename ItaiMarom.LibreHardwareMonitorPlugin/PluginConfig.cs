//using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    public partial class PluginConfig : SuchByte.MacroDeck.GUI.CustomControls.DialogForm
    {
        private List<(String hardware, String sensor)> requestedSensors;
        public PluginConfig(List<(String hardware, String sensor)> listOfSensors)
        {
            requestedSensors = new List<(String hardware, String sensor)>();
            this.FormClosing += new FormClosingEventHandler(SaveRequestedSensorsOnClose);
            InitializeComponent();
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
                dataGridView1.Rows.Add(false, sensor.sensor, sensor.hardware);
            }
        }

        private void SaveRequestedSensorsOnClose(object sender, FormClosingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (chk.Value != null &&(bool)chk.Value)
                    requestedSensors.Add((row.Cells[2].Value.ToString(), row.Cells[1].Value.ToString()));
            }
        }


        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }
    }
}
