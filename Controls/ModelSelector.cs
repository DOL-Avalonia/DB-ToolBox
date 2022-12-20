using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using AmteCreator.IconsPeer;

namespace AmteCreator.Controls
{
    public partial class ModelSelector : Form
    {
        public int selectedSkin;

        private int _select;

        public ModelSelector(int select = 0)
        {
            IconSelectorPeer.Init();
            if (!IconSelectorPeer.Initialized)
            {
                MessageBox.Show("Icon selector not available!");
                return;
            }
            InitializeComponent();
            Size = new Size(660, 520);
            controlList1 = new ControlList<IconImageControl> {Dock = DockStyle.Fill};
            controlList1.MouseDoubleClick += controlList1_MouseDoubleClick;
            groupBox2.Controls.Add(controlList1);
            int count = 0;
            foreach (var m in IconSelectorPeer.ObjectData._Objects)
                if (m.ID == select)
                    break;
                else
                    ++count;
            _select = count;
        }

        void controlList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (controlList1.SelectedControl == null)
                return;
            DialogResult = DialogResult.OK;
            selectedSkin = controlList1.SelectedControl.ID;
            Close();
        }

		private void ModelSelector_Load(object sender, EventArgs e)
		{
            IconSelectorPeer.Init();
            if (!IconSelectorPeer.Initialized)
			{
				MessageBox.Show("Icon selector not available!");
				Close();
				return;
			}
            foreach (var row in IconSelectorPeer.ObjectData._Objects)
            {
                var row1 = row;
                controlList1.AddControl(
                    new Lazy<IconImageControl>(() => new IconImageControl(row1.ID, row1.Icon, row1.Name))
                    );
            }
		    controlList1.SetSelected(_select);
		}
    }
}
