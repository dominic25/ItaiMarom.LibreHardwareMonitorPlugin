using SuchByte.MacroDeck.GUI.CustomControls;

namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    public partial class DialogForm1 : DialogForm
    {
        public DialogForm1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }
    }
}
