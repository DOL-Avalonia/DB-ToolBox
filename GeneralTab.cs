using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AmteCreator.Controls;
using AmteCreator.IconsPeer;
using AmteCreator.Internal;
using AmteCreator.Libs;

namespace AmteCreator
{
	public sealed partial class GeneralTab : UserControl
	{
		private readonly Color _goodColor;
		private ItemTemplate _item;
		private bool _loadingItem = false;

		public DataSet baseXMLData;

		public ItemTemplate currentItem
		{
			get { return _item; }
			set
			{
				if (value == null) return;
				_loadingItem = true;
				_item = value;
				itemTemplateBindingSource.DataSource = value;

				// General
				try { item_realm.SelectedValue = baseXMLData.Tables["realm"].Rows.Find(_item.realm)["id"]; }
				catch { MessageBox.Show(this, "Royaume invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_slot.SelectedValue = baseXMLData.Tables["itemType"].Rows.Find(_item.item_type)["id"]; }
				catch { MessageBox.Show(this, "Slot invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_type.SelectedValue = baseXMLData.Tables["objectType"].Rows.Find(_item.object_type)["id"]; }
				catch { MessageBox.Show(this, "Type d'objet invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_hand.SelectedValue = baseXMLData.Tables["hand"].Rows.Find(_item.hand)["id"]; }
				catch { MessageBox.Show(this, "Main invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_damagetype.SelectedValue = baseXMLData.Tables["damageType"].Rows.Find(_item.type_damage)["id"]; }
				catch { MessageBox.Show(this, "Type de dégâts invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_visible_effect.SelectedValue = baseXMLData.Tables["effect"].Rows.Find(_item.effect)["id"]; }
				catch { MessageBox.Show(this, "Effet visuel invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_color.SelectedValue = baseXMLData.Tables["color"].Rows.Find(_item.color)["id"]; }
				catch { MessageBox.Show(this, "Couleur invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				try { item_extension.SelectedValue = baseXMLData.Tables["extension"].Rows.Find(_item.extension)["id"]; }
				catch { MessageBox.Show(this, "Extension invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }

				try
				{
					item_classes.SelectedIndices.Clear();
					var ids = _item.allowedClasses.Split(';');
					if (ids.Length > 0 && (ids.Length != 1 || ids[0] != "0"))
						for (var i = 0; i < item_classes.Items.Count; ++i)
						{
							var row = (DataRowView)item_classes.Items[i];
							if (ids.Any(t => t == row.Row["id"].ToString()))
								item_classes.SetSelected(i, true);
						}
					else
						item_classes.SetSelected(0, true);
				}
				catch { MessageBox.Show(this, "Classes autorisés invalides.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); }


				_loadingItem = false;
			}
		}

		public GeneralTab()
		{
			InitializeComponent();
			_goodColor = ForeColor;
			_ItemChanged(null, null);

			foreach (var ctrl in Controls)
			{
				if (ctrl is RegExTextBox)
				{
					((Control)ctrl).DataBindings[0].Parse += Utils.Parse;
					((Control)ctrl).DataBindings[0].Format += Utils.Format;
				}
			}
		}

		public void LoadComboBoxes()
		{
			item_extension.DataSource = baseXMLData;
			item_extension.DisplayMember = "extension.name";
			item_extension.Name = "item_extension";
			item_extension.ValueMember = "extension.id";
			baseXMLData.Tables["extension"].PrimaryKey = new[] { baseXMLData.Tables["extension"].Columns["id"] };

			item_color.DataSource = baseXMLData;
			item_color.DisplayMember = "color.name";
			item_color.Name = "item_color";
			item_color.ValueMember = "color.id";
			baseXMLData.Tables["color"].PrimaryKey = new[] { baseXMLData.Tables["color"].Columns["id"] };

			item_visible_effect.DataSource = baseXMLData;
			item_visible_effect.DisplayMember = "effect.name";
			item_visible_effect.Name = "item_visible_effect";
			item_visible_effect.ValueMember = "effect.id";
			baseXMLData.Tables["effect"].PrimaryKey = new[] { baseXMLData.Tables["effect"].Columns["id"] };

			item_hand.DataSource = baseXMLData;
			item_hand.DisplayMember = "hand.name";
			item_hand.Name = "item_hand";
			item_hand.ValueMember = "hand.id";
			baseXMLData.Tables["hand"].PrimaryKey = new[] { baseXMLData.Tables["hand"].Columns["id"] };

			item_damagetype.DataSource = baseXMLData;
			item_damagetype.DisplayMember = "damageType.name";
			item_damagetype.Name = "item_damagetype";
			item_damagetype.ValueMember = "damageType.id";
			baseXMLData.Tables["damageType"].PrimaryKey = new[] { baseXMLData.Tables["damageType"].Columns["id"] };

			item_classes.DataSource = baseXMLData;
			item_classes.DisplayMember = "class.name";
			item_classes.Name = "item_classes";
			item_classes.ValueMember = "class.id";
			baseXMLData.Tables["class"].PrimaryKey = new[] { baseXMLData.Tables["class"].Columns["id"] };

			item_type.DataSource = baseXMLData;
			item_type.DisplayMember = "objectType.name";
			item_type.Name = "item_type";
			item_type.ValueMember = "objectType.id";
			baseXMLData.Tables["objectType"].PrimaryKey = new[] { baseXMLData.Tables["objectType"].Columns["id"] };

			item_slot.DataSource = baseXMLData;
			item_slot.DisplayMember = "itemType.name";
			item_slot.Name = "item_slot";
			item_slot.ValueMember = "itemType.id";
			baseXMLData.Tables["itemType"].PrimaryKey = new[] { baseXMLData.Tables["itemType"].Columns["id"] };

			item_realm.DataSource = baseXMLData;
			item_realm.DisplayMember = "realm.name";
			item_realm.Name = "item_realm";
			item_realm.ValueMember = "realm.id";
			baseXMLData.Tables["realm"].PrimaryKey = new[] { baseXMLData.Tables["realm"].Columns["id"] };
		}

		private void _ItemChanged(object sender, EventArgs e)
		{
			if (_loadingItem) return;
			_loadingItem = true;
			try
			{
				if (_item != null)
                {
							_item.allowedClasses = "";
				foreach (DataRowView row in this.item_classes.SelectedItems)
					if (_item.allowedClasses == "")
						_item.allowedClasses = row.Row["id"].ToString();
					else
						_item.allowedClasses += ";" + row.Row["id"];
                }
			
				ForeColor = _goodColor;
			}
			catch
			{
				ForeColor = Color.Red;
			}
			_loadingItem = false;
		}

		private void checkKeyword_Click(object sender, EventArgs e)
		{
			dynamic resp = Server.QuerySelect("itemtemplate", $"Id_nb = {Server.EscapeSql(item_keyname.Text)}");
			if (resp.error != null)
				MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if (int.Parse(resp.contentCount) <= 0)
				MessageBox.Show(this, "Cet ID est disponible !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show(this, "Cet ID est déjà utilisé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void buttonModelIds_Click(object sender, EventArgs e)
		{
			if (!IconSelectorPeer.Initialized)
				IconSelectorPeer.Init();
			var selectForm = new ModelSelector(int.Parse(item_model_id.Text));
			if (selectForm.ShowDialog(this) == DialogResult.OK)
				item_model_id.Text = selectForm.selectedSkin.ToString();
		}

        private void CanUseInRvR_CheckedChanged(object sender, EventArgs e)
        {
			this.currentItem.canUseInRvR = this.CanUseInRvR.Checked;
        }
    }
}
