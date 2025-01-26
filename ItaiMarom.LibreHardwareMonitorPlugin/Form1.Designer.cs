using SuchByte.MacroDeck.GUI.CustomControls;

namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    partial class DialogForm1
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

        public int getPollingRate()
        {
            return trackBar1.Value;
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            trackBar1 = new System.Windows.Forms.TrackBar();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // trackBar1
            // 
            trackBar1.AccessibleDescription = "in millisconds";
            trackBar1.Location = new System.Drawing.Point(103, 13);
            trackBar1.Maximum = 10000;
            trackBar1.Minimum = 10;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new System.Drawing.Size(288, 56);
            trackBar1.TabIndex = 0;
            trackBar1.Value = 1000;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 29);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(93, 21);
            label1.TabIndex = 1;
            label1.Text = "Polling rate";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(397, 23);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(84, 27);
            textBox1.TabIndex = 2;
            textBox1.Text = trackBar1.Value.ToString();
            textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DialogForm1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(485, 215);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(trackBar1);
            Name = "DialogForm1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}