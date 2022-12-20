using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AmteCreator.IconsPeer;

namespace AmteCreator.Controls
{
    public delegate void SelectEventHandler(object sender, EventArgs e);

    public partial class IconImageControl : UserControl
    {
        /// <summary>
        /// Gets the current assigned IconId
        /// </summary>
        public int ID
        {
            get
            {
                if (iconRow != null) return iconRow.id;
                return this._id;
            }
        }
		private int _id;

        /// <summary>
        /// Holds the Data of the Icon
        /// </summary>
        private Icons.IconValuesRow iconRow;

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public IconImageControl(int modelID, int iconID, string name)
        {
            InitializeComponent();

			this._id = modelID;
			this.BackgroundImage = Properties.Resources.icon_bg;
			this.idLabel.Text = "ID: " + modelID + " - " + iconID;
			this.iconNameLabel.Text = name;
			this.iconPicture.Image = IconsPeer.IconSelectorPeer.GetIconImage(iconID);
		}

        public void SetIconRow(int iconId)
        {
            if (!IconSelectorPeer.Initialized) return;

            SetIconRow(IconSelectorPeer.GetIconRow(iconId));
        }

        public void SetIconRow(Icons.IconValuesRow iconRow)
        {
            this.iconRow = iconRow;
            SetValues();
        }

        private void SetValues()
        {
            if (this.iconRow != null)
            {
                this.iconNameLabel.Text = iconRow.name;
                this.idLabel.Text = "ID: " + iconRow.id.ToString();
                this.iconPicture.Image = IconSelectorPeer.GetIconImage(iconRow);
                this.propertiesLabel.Text = getProperties();
            }
            else
            {
                this.iconNameLabel.Text = "Invalid Icon ID";
                this.idLabel.Text = "";
                this.iconPicture.Image = Properties.Resources.default_icon;
                this.propertiesLabel.Text = "";
            }
        }

        private string getProperties()
        {
            string properties = "";

            if (this.iconRow.borderId >= 0)
            {
                switch (this.iconRow.borderId)
                {
                    case 0: properties += "Green; "; break;
                    case 1: properties += "Blue; "; break;
                    case 2: properties += "Yellow; "; break;
                    case 3: properties += "Purple; "; break;
                    case 4: properties += "ML; "; break;
                    case 5: properties += "CL; "; break;
                    case 6: properties += "RR-Ability; "; break;
                    case 7: properties += "Relic; "; break;
                    case 8: properties += "Artifact; "; break;
                    case 9: properties += "Mythical; "; break;
                }
            }

            if (this.iconRow.spell >= 0)
            {
                int row = (int)Math.Floor(this.iconRow.spell % 100 / 7.0);

                switch (row)
                {
                    case 0: properties += "Group; "; break;
                    case 1: properties += "Resistance; "; break;
                    case 2: properties += "Self; "; break;
                    case 3: properties += "AE; "; break;
                    case 4: properties += "GT AE; "; break;
                    case 5: properties += "AE Pulse/DoT; "; break;
                    case 6: properties += "Pulse/DoT; "; break;
                    case 7: properties += "Cone Spell; "; break;
                    case 8: properties += "Group Resis.; "; break;
                    case 9: properties += "Mostly: Group Resis., GT action; "; break;
                    case 10: properties += "Mostly: AE Resis.; "; break;
                    case 11: properties += "Unknown, smth. with resis.; "; break;
                    case 12: properties += "Various: Trap, Realm AE; "; break;
                }
            }
            else properties += "Enemy/Realm; ";

            return properties;
        }

    }
}
