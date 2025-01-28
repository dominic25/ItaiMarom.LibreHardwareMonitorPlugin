using System.Drawing;
using System.Windows.Forms;

namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    partial class PluginConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            poolingRateTrackBar = new TrackBar();
            pollingRatreLabel = new Label();
            pollingRateTextBox = new TextBox();
            AvailableSensorsLabel = new Label();
            SensorsTable = new DataGridView();
            Select = new DataGridViewCheckBoxColumn();
            Sensor = new DataGridViewTextBoxColumn();
            Hardware = new DataGridViewTextBoxColumn();
            deleteAllButton = new Button();
            ((System.ComponentModel.ISupportInitialize)poolingRateTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SensorsTable).BeginInit();
            SuspendLayout();
            // 
            // poolingRateTrackBar
            // 
            poolingRateTrackBar.AccessibleDescription = "in millisconds";
            poolingRateTrackBar.Location = new Point(103, 13);
            poolingRateTrackBar.Maximum = 10000;
            poolingRateTrackBar.Minimum = 10;
            poolingRateTrackBar.Name = "poolingRateTrackBar";
            poolingRateTrackBar.Size = new Size(288, 56);
            poolingRateTrackBar.TabIndex = 0;
            poolingRateTrackBar.Value = 1000;
            poolingRateTrackBar.Scroll += pollingRateTrackBar_Scroll;
            // 
            // pollingRatreLabel
            // 
            pollingRatreLabel.AutoSize = true;
            pollingRatreLabel.Location = new Point(4, 29);
            pollingRatreLabel.Name = "pollingRatreLabel";
            pollingRatreLabel.Size = new Size(93, 21);
            pollingRatreLabel.TabIndex = 1;
            pollingRatreLabel.Text = "Polling rate";
            // 
            // pollingRateTextBox
            // 
            pollingRateTextBox.Location = new Point(397, 23);
            pollingRateTextBox.Name = "pollingRateTextBox";
            pollingRateTextBox.ReadOnly = true;
            pollingRateTextBox.Size = new Size(84, 27);
            pollingRateTextBox.TabIndex = 2;
            pollingRateTextBox.Text = "1000";
            pollingRateTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // AvailableSensorsLabel
            // 
            AvailableSensorsLabel.AutoSize = true;
            AvailableSensorsLabel.Location = new Point(4, 60);
            AvailableSensorsLabel.Name = "AvailableSensorsLabel";
            AvailableSensorsLabel.Size = new Size(139, 21);
            AvailableSensorsLabel.TabIndex = 3;
            AvailableSensorsLabel.Text = "Available sensors";
            // 
            // SensorsTable
            // 
            dataGridViewCellStyle1.ForeColor = Color.Black;
            SensorsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            SensorsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            SensorsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            SensorsTable.Columns.AddRange(new DataGridViewColumn[] { Select, Sensor, Hardware });
            SensorsTable.Location = new Point(4, 84);
            SensorsTable.Name = "SensorsTable";
            SensorsTable.RowHeadersVisible = false;
            SensorsTable.RowHeadersWidth = 51;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            SensorsTable.RowsDefaultCellStyle = dataGridViewCellStyle2;
            SensorsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SensorsTable.Size = new Size(844, 409);
            SensorsTable.TabIndex = 4;
            // 
            // Select
            // 
            Select.HeaderText = "Select";
            Select.MinimumWidth = 6;
            Select.Name = "Select";
            Select.Width = 61;
            // 
            // Sensor
            // 
            Sensor.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Sensor.HeaderText = "Sensor";
            Sensor.MinimumWidth = 6;
            Sensor.Name = "Sensor";
            Sensor.ReadOnly = true;
            Sensor.Width = 89;
            // 
            // Hardware
            // 
            Hardware.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Hardware.HeaderText = "Hardware";
            Hardware.MinimumWidth = 6;
            Hardware.Name = "Hardware";
            Hardware.ReadOnly = true;
            Hardware.Width = 111;
            // 
            // deleteAllButton
            // 
            deleteAllButton.ForeColor = Color.Black;
            deleteAllButton.Location = new Point(675, 21);
            deleteAllButton.Name = "deleteAllButton";
            deleteAllButton.Size = new Size(173, 29);
            deleteAllButton.TabIndex = 5;
            deleteAllButton.Text = "Delete all variables";
            deleteAllButton.UseVisualStyleBackColor = true;
            deleteAllButton.Click += deleteAllVariablesButton_Click;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 497);
            Controls.Add(deleteAllButton);
            Controls.Add(SensorsTable);
            Controls.Add(AvailableSensorsLabel);
            Controls.Add(pollingRateTextBox);
            Controls.Add(pollingRatreLabel);
            Controls.Add(poolingRateTrackBar);
            Name = "PluginConfig";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)poolingRateTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)SensorsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar poolingRateTrackBar;
        private System.Windows.Forms.Label pollingRatreLabel;
        private System.Windows.Forms.TextBox pollingRateTextBox;
        private System.Windows.Forms.Label AvailableSensorsLabel;
        private System.Windows.Forms.DataGridView SensorsTable;
        private DataGridViewCheckBoxColumn Select;
        private DataGridViewTextBoxColumn Sensor;
        private DataGridViewTextBoxColumn Hardware;
        private Button deleteAllButton;
    }
}