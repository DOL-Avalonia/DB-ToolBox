using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using AmteCreator.Internal;

namespace AmteCreator
{
    public partial class LootTab : UserControl
    {
        private readonly BindingSource _lootTemplates = new BindingSource();
        private readonly BindingSource _mobxlootTemplates = new BindingSource();
        private bool _inLoading;

        public LootTab()
        {
            InitializeComponent();
            // Loot template
            _lootTemplates.AllowNew = true;
            templateView.CellEndEdit += _TemplateViewCellEndEdit;
            templateView.UserDeletedRow += _TemplateViewUserDeletedRow;
            templateView.UserAddedRow += _TemplateViewUserAddedRow;

            // Mob X Loot template
            mobView.CellEndEdit += _MobViewCellEndEdit;
            mobView.UserDeletedRow += _MobViewUserDeletedRow;
        }

        public void LoadAllLoots(object sender = null, EventArgs e = null)
        {
            if (_inLoading)
                return;
            _inLoading = true;
            
            try
            {
                // Loading loottemplate
                dynamic data = Server.QuerySelect("droptemplatexitemtemplate", _GetWhereClause(), "20000");
                if (data.error != null)
                    throw new Exception("Erreur du serveur:\r\n" + data.error);
                _lootTemplates.Clear();
                foreach (var loot in data.content)
                    _lootTemplates.Add(new LootTemplate(loot));

                Dictionary<string, MobXLootTemplate> mobs = new Dictionary<string, MobXLootTemplate>();
                if (_lootTemplates.Count > 0)
                {
                    // Loading mobxloottemplate
                    data = Server.QuerySelect(table: "mobxloottemplate", where: _GetWhereMobClause(), limit: "20000");
                    if (data is string && data == "FAIL\r\n")
                        throw new Exception("Erreur inconnue du serveur. (Mauvaise requête ?!)");
                    if (data.error != null)
                        throw new Exception("Erreur du serveur:\r\n" + data.error);

                    foreach (dynamic loot in data.content)
                    {
                        if (!mobs.ContainsKey(loot.MobName))
                            mobs.Add(loot.MobName, new MobXLootTemplate(loot));
                    }
                }
                foreach (LootTemplate loot in _lootTemplates)
                {
                    if (!mobs.ContainsKey(loot.TemplateName))
                        mobs.Add(loot.TemplateName, new MobXLootTemplate { MobName = loot.TemplateName, lootTemplateName = loot.TemplateName, DropCount = 1 });
                }
                int oldCount = _mobxlootTemplates.Count;
                _mobxlootTemplates.Clear();
                foreach (var loot in mobs)
                {
                    _mobxlootTemplates.Add(loot.Value);
                }
                if (oldCount != _mobxlootTemplates.Count)
                {
                    mobView.DataSource = null;
                    mobView.DataSource = _mobxlootTemplates;
                }
                else
                    mobView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this.ParentForm, "Erreur: " + exception.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _inLoading = false;
            }
        }

        private void LoadLoots(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            MobXLootTemplate loot = mobView.Rows[e.RowIndex].DataBoundItem as MobXLootTemplate;
            if (loot != null)
            {
                _inLoading = true;
                try
                {
                    dynamic data = Server.QuerySelect("droptemplatexitemtemplate", _GetWhereClause(loot.MobName), "20000");
                    if (data.error != null)
                        throw new Exception("Erreur du serveur:\r\n" + data.error);
                    int oldCount = _lootTemplates.Count;
                    _lootTemplates.Clear();
                    foreach (var lt in data.content)
                        _lootTemplates.Add(new LootTemplate(lt));

                    if (oldCount != _lootTemplates.Count)
                    {
                        templateView.DataSource = null;
                        templateView.DataSource = _lootTemplates;
                    }
                    else
                        templateView.Refresh();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this.ParentForm, "Erreur: " + exception.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _inLoading = false;
                }
            }
        }

        private string _GetWhereMobClause()
        {
            string where = "MobName = " + _lootTemplates.Cast<LootTemplate>().GroupBy(loot => loot.TemplateName).Select(a => Server.EscapeSql(a.Key)).Aggregate((s, s1) => s + " OR MobName = " + s1);
            return where;
        }

        private string _GetWhereClause(string mobName = null)
        {
            string where = "";
            if (!string.IsNullOrEmpty(mobName))
                where = "TemplateName = " + Server.EscapeSql(mobName);
            else
            {
                if (!string.IsNullOrEmpty(searchItem.Text))
                    where += (where == "" ? "ItemTemplateID LIKE " : " AND ItemTemplateID LIKE ") + Server.EscapeSql(searchItem.Text.Replace('*', '%'));
                if (!string.IsNullOrEmpty(searchMob.Text))
                    where += (where == "" ? "TemplateName LIKE " : " AND TemplateName LIKE ") + Server.EscapeSql(searchMob.Text.Replace('*', '%'));
            }
            return where == "" ? "1" : where;
        }

        #region Edit/Add/Delete
        private void _MobViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            MobXLootTemplate loot = e.Row.DataBoundItem as MobXLootTemplate;
            if (loot != null && loot.AlreadyInDB)
            {
				if (loot.ID == null)
					throw new Exception("Impossible de modifier ce loot, l'id est invalide (-1).");
				dynamic resp = Server.Query("?action=DELETE&table=mobdroptemplate&where=" +
                                            HttpUtility.UrlEncode("ID = " + Server.EscapeSql(loot.ID), Encoding.UTF8));
                if (resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(this, "Supprimé !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _MobViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MobXLootTemplate loot = mobView.Rows[e.RowIndex].DataBoundItem as MobXLootTemplate;
            if (loot != null)
            {
                dynamic resp = loot.Save();
                if (resp != null && resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (resp != null)
                    MessageBox.Show(this, "Enregistré !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _TemplateViewUserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            LootTemplate loot = e.Row.DataBoundItem as LootTemplate;
            if (loot != null)
            {
                dynamic resp = loot.Save();
                if (resp != null && resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (resp != null)
                    MessageBox.Show(this, "Enregistré !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _TemplateViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            LootTemplate loot = e.Row.DataBoundItem as LootTemplate;
            if (loot != null && loot.AlreadyInDB)
            {
				if (loot.ID == -1)
					throw new Exception("Impossible de modifier ce loot, l'id est invalide (-1).");
				dynamic resp = Server.Query("?action=DELETE&table=droptemplatexitemtemplate&where=" +
                                            HttpUtility.UrlEncode("ID = " + Server.EscapeSql(loot.ID), Encoding.UTF8));
                if (resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(this, "Supprimé !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void _TemplateViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            LootTemplate loot = templateView.Rows[e.RowIndex].DataBoundItem as LootTemplate;
            if (loot != null)
            {
                dynamic resp = loot.Save();
                if (resp != null && resp.error != null)
                    MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (resp != null)
                    MessageBox.Show(this, "Enregistré !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
