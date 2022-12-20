using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AmteCreator.Libs
{
    public partial class RegExTextBox : TextBox 
    {
        private Regex _regex;
        private string _regularExpression;

        public string RegularExpression
        {
            get { return _regularExpression; }
            set
            {
                _regex = null;
                try
                {
                    _regex = new Regex(value);
                }
                catch
                {
                    MessageBox.Show("Regex invalid!");
                }
                _regularExpression = value;
            }
        }

        public event EventHandler AfterTextChanged;

        public RegExTextBox()
        {
            InitializeComponent();
        }

        public bool ValidateControl(string text)
        {
            if (text == null)
                return true;
            if (_regex == null)
            {
                MessageBox.Show("Regex invalid!");
                return false;
            }
            return _regex.IsMatch(text);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar))
            {
                var newText = base.Text.Substring(0, SelectionStart) + e.KeyChar + base.Text.Substring(SelectionStart + SelectionLength);
                if (newText != "")
                {
                    var validateCheck = ValidateControl(newText);
                    if (validateCheck == false)
                        e.Handled = true;
                }
            }
            
            base.OnKeyPress(e);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (!ValidateControl(value))
                    return;
                base.Text = value;
                if (AfterTextChanged != null)
                    AfterTextChanged(this, new EventArgs());
            }
        }
    }
}
