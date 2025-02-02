using Newtonsoft.Json;
using SIL.FieldWorks.Common.Controls;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using TreeView = System.Windows.Forms.TreeView;

namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    public partial class PluginConfig : DialogForm
    {
        private MacroDeckPlugin _main;
        private List<(String hardware, String type, String sensor)> requestedSensors;
        public PluginConfig(MacroDeckPlugin main, List<(String hardware, String type, String sensor)> listOfSensors)
        {
            _main = main;
            requestedSensors = new List<(String hardware, String type, String sensor)>();
            InitializeComponent();

            string serialized = PluginConfiguration.GetValue(_main, "requestedSensors");
            if (serialized != "")
            {
                requestedSensors = JsonConvert.DeserializeObject<List<(String hardware, String type, String sensor)>>(serialized);
            }
            string strPollingRate = PluginConfiguration.GetValue(_main, "pollingRate");
            if (strPollingRate != "")
            {
                poolingRateTrackBar.Value = int.Parse(strPollingRate);
                pollingRateTextBox.Text = strPollingRate;
            }

            FormClosing += new FormClosingEventHandler(SaveRequestedSensorsOnClose);
            UpdateSensorsTree(listOfSensors);
        }

        public int GetPollingRate()
        {
            return poolingRateTrackBar.Value;
        }

        public List<(String hardware, String type, String sensor)> GetRequestedSensors()
        {
            return requestedSensors;
        }
        private void UpdateSensorsTree(List<(String hardware, String type, String sensor)> listOfSensors)
        {
            foreach (var sensor in listOfSensors)
            {
                bool chk = false;
                if (requestedSensors.Any(tuple => tuple.sensor == sensor.sensor && tuple.hardware == sensor.hardware))
                    chk = true;
                if (!sensorsTreeView.Nodes.ContainsKey(sensor.hardware))
                    sensorsTreeView.Nodes.Add(sensor.hardware, sensor.hardware);
                if(!sensorsTreeView.Nodes[sensor.hardware].Nodes.ContainsKey(sensor.type))
                    sensorsTreeView.Nodes[sensor.hardware].Nodes.Add(sensor.type, sensor.type);
                sensorsTreeView.Nodes[sensor.hardware].Nodes[sensor.type].Nodes.Add(sensor.sensor, sensor.sensor);
                if (chk)
                {
                    TreeNode node = sensorsTreeView.Nodes[sensor.hardware].Nodes[sensor.type].Nodes[sensor.sensor];
                    sensorsTreeView.SetChecked(node, TriStateTreeView.CheckState.Checked);
                }
            }
        }

        private void SaveRequestedSensorsOnClose(object sender, FormClosingEventArgs e)
        {
            PluginConfiguration.DeletePluginConfig(_main);
            requestedSensors.Clear();

            List<TreeNode> checkedNodes = GetCheckedNodes(sensorsTreeView);
            foreach (TreeNode node in checkedNodes)
            {
                requestedSensors.Add((node.Parent.Parent.Text, node.Parent.Text, node.Text));
            }

            if (requestedSensors.Count > 0)
            {
                var serialized = JsonConvert.SerializeObject(requestedSensors);
                PluginConfiguration.SetValue(_main, "requestedSensors", serialized);
            }
            PluginConfiguration.SetValue(_main, "pollingRate", poolingRateTrackBar.Value.ToString());
        }


        // Method to get all checked nodes in a TreeView
        public List<TreeNode> GetCheckedNodes(TreeView treeView)
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
            {
                GetCheckedNodes(node, checkedNodes);
            }
            return checkedNodes;
        }

        // Recursive helper method
        private void GetCheckedNodes(TreeNode treeNode, List<TreeNode> checkedNodes)
        {

            if (sensorsTreeView.GetChecked(treeNode) == TriStateTreeView.CheckState.Checked && treeNode.Nodes.Count == 0)
            {
                checkedNodes.Add(treeNode);
            }

            foreach (TreeNode childNode in treeNode.Nodes)
            {
                GetCheckedNodes(childNode, checkedNodes);
            }
        }       

        private void PollingRateTrackBar_Scroll(object sender, System.EventArgs e)
        {
            pollingRateTextBox.Text = poolingRateTrackBar.Value.ToString();
        }

        private void DeleteAllVariablesButton_Click(object sender, EventArgs e)
        {
            if (_main is LibreHardwareMonitorPlugin libreHardwareMonitorPlugin)
                libreHardwareMonitorPlugin.DeleteAllVariables();
            else
                MacroDeckLogger.Error(_main, "when deleteting _main was not of type LibreHardwareMonitorPlugin");
        }
    }
}
