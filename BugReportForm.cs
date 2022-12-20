using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AmteCreator
{
    public partial class BugReportForm : Form
    {
        public BugReportForm(string data)
        {
            InitializeComponent();
            textBox1.Text = data;
        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().StartInfo);
            Close();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
