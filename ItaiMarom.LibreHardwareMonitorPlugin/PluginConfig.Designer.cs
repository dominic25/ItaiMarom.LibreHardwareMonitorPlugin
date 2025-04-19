using SIL.FieldWorks.Common.Controls;
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
            components = new System.ComponentModel.Container();
            poolingRateTrackBar = new TrackBar();
            pollingRatreLabel = new Label();
            pollingRateTextBox = new TextBox();
            AvailableSensorsLabel = new Label();
            deleteAllButton = new Button();
            sensorsTreeView = new TriStateTreeView();
            ((System.ComponentModel.ISupportInitialize)poolingRateTrackBar).BeginInit();
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
            poolingRateTrackBar.Scroll += PollingRateTrackBar_Scroll;
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
            pollingRateTextBox.Size = new Size(84, 27);
            pollingRateTextBox.TabIndex = 2;
            pollingRateTextBox.Text = "1000";
            pollingRateTextBox.TextAlign = HorizontalAlignment.Right;
            pollingRateTextBox.KeyPress += PollingRateTextBox_KeyPress;
            pollingRateTextBox.Leave += PollingRateTextBox_Update;
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
            // deleteAllButton
            // 
            deleteAllButton.ForeColor = Color.Black;
            deleteAllButton.Location = new Point(675, 21);
            deleteAllButton.Name = "deleteAllButton";
            deleteAllButton.Size = new Size(173, 29);
            deleteAllButton.TabIndex = 5;
            deleteAllButton.Text = "Delete all variables";
            deleteAllButton.UseVisualStyleBackColor = true;
            deleteAllButton.Click += DeleteAllVariablesButton_Click;
            // 
            // sensorsTreeView
            // 
            sensorsTreeView.ImageIndex = 1;
            sensorsTreeView.Location = new Point(4, 84);
            sensorsTreeView.Name = "sensorsTreeView";
            sensorsTreeView.SelectedImageIndex = 1;
            sensorsTreeView.Size = new Size(844, 409);
            sensorsTreeView.TabIndex = 6;
            // 
            // PluginConfig
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(852, 497);
            Controls.Add(sensorsTreeView);
            Controls.Add(deleteAllButton);
            Controls.Add(AvailableSensorsLabel);
            Controls.Add(pollingRateTextBox);
            Controls.Add(pollingRatreLabel);
            Controls.Add(poolingRateTrackBar);
            Name = "PluginConfig";
            Text = "Plugin config";
            ((System.ComponentModel.ISupportInitialize)poolingRateTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar poolingRateTrackBar;
        private System.Windows.Forms.Label pollingRatreLabel;
        private System.Windows.Forms.TextBox pollingRateTextBox;
        private System.Windows.Forms.Label AvailableSensorsLabel;
        private Button deleteAllButton;
        private TriStateTreeView sensorsTreeView;
    }
}