using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
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
            trackBar1 = new TrackBar();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            Select = new DataGridViewCheckBoxColumn();
            Sensor = new DataGridViewTextBoxColumn();
            Hardware = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // trackBar1
            // 
            trackBar1.AccessibleDescription = "in millisconds";
            trackBar1.Location = new Point(103, 13);
            trackBar1.Maximum = 10000;
            trackBar1.Minimum = 10;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(288, 56);
            trackBar1.TabIndex = 0;
            trackBar1.Value = 1000;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 29);
            label1.Name = "label1";
            label1.Size = new Size(93, 21);
            label1.TabIndex = 1;
            label1.Text = "Polling rate";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(397, 23);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(84, 27);
            textBox1.TabIndex = 2;
            textBox1.Text = "1000";
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 60);
            label2.Name = "label2";
            label2.Size = new Size(139, 21);
            label2.TabIndex = 3;
            label2.Text = "Available sensors";
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Select, Sensor, Hardware });
            dataGridView1.Location = new Point(4, 84);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Size = new Size(844, 409);
            dataGridView1.TabIndex = 4;
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
            // PluginConfig
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 497);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(trackBar1);
            Name = "PluginConfig";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGridViewCheckBoxColumn Select;
        private DataGridViewTextBoxColumn Sensor;
        private DataGridViewTextBoxColumn Hardware;
    }
}