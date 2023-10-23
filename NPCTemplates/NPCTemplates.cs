using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmteCreator.Internal;

namespace AmteCreator.Controls
{
	[ToolboxItem(true)]
	[DesignTimeVisible(true)]
	public partial class NPCTemplates : UserControl
	{
		NPCTemplate _currentTemplate = new NPCTemplate();
		bool _loading = false;
		private readonly Dictionary<int, int> _visibleSlot2SlotID = new Dictionary<int, int>();

		private DataSet _baseXMLData;
		public DataSet baseXMLData
		{
			get { return _baseXMLData; }
			set
			{
				_baseXMLData = value;
				_LoadComboBoxes();
			}
		}

		public NPCTemplate currentTemplate
		{
			get { return _currentTemplate; }
			set
			{
				if (value == null) return;
				_loading = true;
				_currentTemplate = value;
				bindingTemplate.DataSource = value;
				_loading = false;
			}
		}

		public NPCTemplates()
		{
			InitializeComponent();
			_LoadFlagHandlers();

			_visibleSlot2SlotID.Add(0,0);
			_visibleSlot2SlotID.Add(1,16);
			_visibleSlot2SlotID.Add(2,31);
			_visibleSlot2SlotID.Add(3,34);
			_visibleSlot2SlotID.Add(4,255);
		}

		private void OpenNPCTemplate(object sender, EventArgs e)
		{
			var form = new OpenNPCTemplateForm();
			if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
			{
				currentTemplate = form.selectedItem;
				_LoadTemplate();
			}
		}

		private void _RaceButton_Click(object sender, EventArgs e)
		{
			var form = new RaceForm();
			if (form.ShowDialog(this.ParentForm) == DialogResult.OK)
			{
				currentTemplate.Race = form.selectedItem.ID;
				_Race.Text = form.selectedItem.ID.ToString();
			}
		}

		private void _LoadFlagHandlers()
		{
			Func<CheckBox, eFlags, EventHandler> lambda = (CheckBox cb, eFlags flag) =>
				(s, e) => {
					currentTemplate.Flags = (currentTemplate.Flags & (int)(~flag)) | (cb.Checked ? (int)flag : 0);
					Console.WriteLine(currentTemplate.Flags);
				};
			_FlagsFlying.CheckedChanged += lambda(_FlagsFlying, eFlags.FLYING);
			_FlagsNoTarget.CheckedChanged += lambda(_FlagsNoTarget, eFlags.CANTTARGET);
			_FlagsHideName.CheckedChanged += lambda(_FlagsHideName, eFlags.DONTSHOWNAME);
			_FlagsStealth.CheckedChanged += lambda(_FlagsStealth, eFlags.STEALTH);
			_FlagsGhost.CheckedChanged += lambda(_FlagsGhost, eFlags.GHOST);
			_FlagsPeace.CheckedChanged += lambda(_FlagsPeace, eFlags.PEACE);
			_FlagsStatue.CheckedChanged += lambda(_FlagsStatue, eFlags.STATUE);
			_FlagsSwimming.CheckedChanged += lambda(_FlagsSwimming, eFlags.SWIMMING);
			_FlagsTorch.CheckedChanged += lambda(_FlagsTorch, eFlags.TORCH);
		}
		private void _LoadComboBoxes()
		{
			Action<ComboBox, string> load = (ctrl, tableName) =>
			{
				var table = baseXMLData.Tables[tableName];
				table.PrimaryKey = new[] { table.Columns["id"] };
				ctrl.ValueMember = tableName + ".id";
				ctrl.DisplayMember = tableName + ".name";
				ctrl.DataSource = baseXMLData;
			};

			load(_VisibleWeaponSlot, "hand");
			load(_Gender, "gender");
			load(_BodyType, "bodyType");
			load(_MeleeDamageType, "damageType");
			// TODO Races
			
		}

		private void _LoadTemplate()
		{
			var flags = currentTemplate.Flags;
			_FlagsFlying.Checked = (flags & (int)eFlags.FLYING) != 0;
			_FlagsNoTarget.Checked = (flags & (int)eFlags.CANTTARGET) != 0;
			_FlagsHideName.Checked = (flags & (int)eFlags.DONTSHOWNAME) != 0;
			_FlagsStealth.Checked = (flags & (int)eFlags.STEALTH) != 0;
			_FlagsGhost.Checked = (flags & (int)eFlags.GHOST) != 0;
			_FlagsPeace.Checked = (flags & (int)eFlags.PEACE) != 0;
			_FlagsStatue.Checked = (flags & (int)eFlags.STATUE) != 0;
			_FlagsSwimming.Checked = (flags & (int)eFlags.SWIMMING) != 0;
			_FlagsTorch.Checked = (flags & (int)eFlags.TORCH) != 0;
			//	_visibleSlot2SlotID.Add("Right", 0);

			if (currentTemplate.VisibleWeaponSlots != 0)
			{
				var val = _visibleSlot2SlotID.FirstOrDefault(v => v.Value.Equals(currentTemplate.VisibleWeaponSlots));

				if (val.Key != 0)
				{
					_VisibleWeaponSlot.SelectedIndex = val.Key;
				}
				else
				{
					_VisibleWeaponSlot.SelectedIndex = 0;
				}
			}
			else
			{
				_VisibleWeaponSlot.SelectedIndex = 0;
			}
		}

		private void _VisibleWeaponSlot_SelectedIndexChanged(object sender, EventArgs e)
		{
			int slot = 0;
			_visibleSlot2SlotID.TryGetValue(_VisibleWeaponSlot.SelectedIndex, out slot);
			currentTemplate.VisibleWeaponSlots = slot;
		}

		private void TabControl_Selected(object sender, TabControlEventArgs e)
		{
			_debugText.Text = _currentTemplate.ToString();
		}

		private void _SaveButton_Click(object sender, EventArgs e)
		{
			try
            {
                dynamic resp = Server.QuerySelect("npctemplate", "TemplateId = " + Server.EscapeSql(_currentTemplate.TemplateId));
                if (resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (int.Parse(resp.contentCount) <= 0)
                    _SaveCurrentTemplate();
                else if (!_currentTemplate.AlreadyInDB)
                    MessageBox.Show(this.ParentForm, "Cet ID est déjà utilisé, veuillez recharger le npc template ou changer l'ID.", "Erreur");
                else if (MessageBox.Show(this.ParentForm,
                                         "Cet ID est déjà utilisé !\r\nVoulez-vous écraser l'ancien item ?!", "Information",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    _SaveCurrentTemplate();
            }
			catch (Exception exception)
			{
				MessageBox.Show(this.ParentForm, "Erreur:\n" + exception.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void _SaveCurrentTemplate()
		{
			try
            {
                dynamic resp = _currentTemplate.Save();
                if (resp == null || resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + (resp?.error ?? "resp is null"), "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    try
                    {
                        Server.UpdateNpcTemplate(_currentTemplate.NpcTemplate_ID);
                        MessageBox.Show(this, "Enregistré !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(this, "Le NPCTemplate a été enregistré mais il n'a pas pu être mis à jour sur le serveur.\r\n" +
                            "Un reboot du serveur est peut-être nécessaire.\r\n\r\nErreur: " + e.Message,
                            "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this.ParentForm, "Erreur:\n" + exception.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void _NewButton_Click(object sender, EventArgs e)
		{
			_currentTemplate = new NPCTemplate();
		}
	}
}
