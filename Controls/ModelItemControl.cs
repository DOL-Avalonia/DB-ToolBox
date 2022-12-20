using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AmteCreator.Controls
{
    public partial class ModelItemControl : UserControl
    {
        public int ID;

        public ModelItemControl(int modelID, int iconID, string name_)
        {
            InitializeComponent();
            ID = modelID;
            id.Text = "ID: " + modelID + " - " + iconID;
            name.Text = name_;
            icon.Image = IconsPeer.IconSelectorPeer.GetIconImage(iconID);
        }

        private void _MouseClick(object sender, MouseEventArgs e)
        {
            OnMouseClick(e);
        }

        private void _MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        private void model_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void model_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }
    }
}
