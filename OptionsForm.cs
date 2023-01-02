using System;
using System.IO;
using System.Windows.Forms;

namespace AmteCreator
{
    public partial class OptionsForm : Form
    {
        public string Login { get { return login.Text; } }
        public string Password { get { return password.Text; } }
        public string DBName { get { return dBName.Text; } }
        public string DaocPath { get { return daocPath.Text; } }

        private readonly Func<string, string, string, bool> _testFunction;

        public OptionsForm(Func<string, string, string, bool> testFunction)
        {
            _testFunction = testFunction;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs ea)
        {
            if (!File.Exists(Path.Combine(daocPath.Text, "game.dll")))
                openFolderDaoc_Click();
            bool ok = false;
            try
            {
                if ((ok = _testFunction(login.Text, password.Text, dBName.Text)) == true)
                    MessageBox.Show(this, "Connexion réussie", "Options", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "Mauvais login ?!", "Options", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: " + ex.Message, "Options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ok)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs ea)
        {
            try
            {
                if (_testFunction(login.Text, password.Text, dBName.Text))
                    MessageBox.Show(this, "Connexion réussie", "Options", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "Mauvais login ?!", "Options", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Erreur: " + ex.Message, "Options", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openFolderDaoc_Click(object sender = null, EventArgs e = null)
        {
            var ofd = new FolderBrowserDialog();
            while (ofd.ShowDialog(this) != DialogResult.OK || !File.Exists(Path.Combine(ofd.SelectedPath, "game.dll")))
                MessageBox.Show(this, "Vous devez indiquer le répertoire de Dark age of camelot.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            daocPath.Text = ofd.SelectedPath;
        }
    }
}
