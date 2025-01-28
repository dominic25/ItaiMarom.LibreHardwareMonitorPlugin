using Newtonsoft.Json;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TreeView = System.Windows.Forms.TreeView;

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
                poolingRateTrackBar.Value = int.Parse(strPollingRate);
                pollingRateTextBox.Text = strPollingRate;
            }

            FormClosing += new FormClosingEventHandler(SaveRequestedSensorsOnClose);
            sensorsTreeView.AfterCheck += sensorsTreeView_AfterCheck;
            UpdateSensorsList(listOfSensors);
        }

        public int getPollingRate()
        {
            return poolingRateTrackBar.Value;
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
                if (!sensorsTreeView.Nodes.ContainsKey(sensor.hardware))
                    sensorsTreeView.Nodes.Add(sensor.hardware, sensor.hardware);
                sensorsTreeView.Nodes[sensor.hardware].Nodes.Add(sensor.sensor, sensor.sensor);
                sensorsTreeView.Nodes[sensor.hardware].Nodes[sensor.sensor].Checked = chk;
            }
        }

        private void SaveRequestedSensorsOnClose(object sender, FormClosingEventArgs e)
        {
            PluginConfiguration.DeletePluginConfig(_main);
            requestedSensors.Clear();

            List<TreeNode> checkedNodes = GetCheckedNodes(sensorsTreeView);
            foreach (TreeNode node in checkedNodes)
            {
                requestedSensors.Add((node.Parent.Text, node.Text));
            }

            if (requestedSensors.Count > 0)
            {
                var serialized = JsonConvert.SerializeObject(requestedSensors);
                PluginConfiguration.SetValue(_main, "requestedSensors", serialized);
            }
            PluginConfiguration.SetValue(_main, "pollingRate", poolingRateTrackBar.Value.ToString());
        }


        // Method to get all checked nodes in a TreeView
        public static List<TreeNode> GetCheckedNodes(TreeView treeView)
        {
            List<TreeNode> checkedNodes = new List<TreeNode>();
            foreach (TreeNode node in treeView.Nodes)
            {
                GetCheckedNodes(node, checkedNodes);
            }
            return checkedNodes;
        }

        // Recursive helper method
        private static void GetCheckedNodes(TreeNode treeNode, List<TreeNode> checkedNodes)
        {
            if (treeNode.Checked)
            {
                checkedNodes.Add(treeNode);
            }

            foreach (TreeNode childNode in treeNode.Nodes)
            {
                GetCheckedNodes(childNode, checkedNodes);
            }
        }

        private void sensorsTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // Prevent recursive execution of this event
            sensorsTreeView.AfterCheck -= sensorsTreeView_AfterCheck;

            try
            {
                // Update child nodes to match the parent node's checked state
                SetChildNodesCheckedState(e.Node, e.Node.Checked);
            }
            finally
            {
                // Re-attach the event handler
                sensorsTreeView.AfterCheck += sensorsTreeView_AfterCheck;
            }
        }

        private void SetChildNodesCheckedState(TreeNode parentNode, bool isChecked)
        {
            foreach (TreeNode childNode in parentNode.Nodes)
            {
                childNode.Checked = isChecked;

                // Recursively set state for child nodes of this node
                if (childNode.Nodes.Count > 0)
                {
                    SetChildNodesCheckedState(childNode, isChecked);
                }
            }
        }

        private void pollingRateTrackBar_Scroll(object sender, System.EventArgs e)
        {
            pollingRateTextBox.Text = poolingRateTrackBar.Value.ToString();
        }

        private void deleteAllVariablesButton_Click(object sender, EventArgs e)
        {
            LibreHardwareMonitorPlugin libreHardwareMonitorPlugin = _main as LibreHardwareMonitorPlugin;
            if (libreHardwareMonitorPlugin != null)
                libreHardwareMonitorPlugin.DeleteAllVariables();
            else
                MacroDeckLogger.Error(_main, "when deleteting _main was not of type LibreHardwareMonitorPlugin");
        }
    }
}
