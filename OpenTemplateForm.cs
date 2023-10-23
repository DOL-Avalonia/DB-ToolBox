using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AmteCreator.Internal;

namespace AmteCreator
{
	public partial class OpenTemplateForm : Form
	{
		private readonly IList<ItemTemplate> _templates = new BindingList<ItemTemplate>();
		private bool _inLoading = false;
		public ItemTemplate selectedItem
		{
			get { return _item; }
			set { _item = value; }
		}

		public string orderBy = "Id_nb";

		public OpenTemplateForm()
		{
			_inLoading = true;
			InitializeComponent();
			itemPerPages.SelectedIndex = 0;
			dataGridView1.DataSource = _templates;
			dataGridView1.ClearSelection();
			_inLoading = false;
			LoadItems();
		}

		private string _GetWhereClause()
		{
			string where = "";
			if (!string.IsNullOrEmpty(itemId.Text))
				where += (where == "" ? "Id_nb like " : " AND Id_nb like ") + Server.EscapeSql(itemId.Text.Replace('*', '%'));
			if (!string.IsNullOrEmpty(itemName.Text))
				where += (where == "" ? "Name like " : " AND Name like ") + Server.EscapeSql(itemName.Text.Replace('*', '%'));
			return where == "" ? "1" : where;
		}

		private string _GetLimitClause()
		{
			return (((int)currentPage.Value - 1) * Convert.ToInt32(itemPerPages.SelectedItem)) + "," + itemPerPages.SelectedItem;
		}

		public void LoadItems(object sender = null, EventArgs e = null)
		{
			if (_inLoading)
				return;
			_inLoading = true;
			try
            {
                dynamic data = Server.QuerySelect("itemtemplate", _GetWhereClause(), _GetLimitClause(), orderBy);
                if (data is string)
                    throw new Exception("Erreur\r\n" + data);
                if (data.error != null)
                    throw new Exception("Erreur du serveur:\r\n" + data.error);
                int oldCount = _templates.Count;
                _templates.Clear();
                foreach (var item in data.content)
                    _templates.Add(new ItemTemplate(item));
                if (oldCount != _templates.Count)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _templates;
                }
                else
                    dataGridView1.Refresh();
                resultCount.Text = "Résultat: " + data.contentCount + " items";
                currentPage.Maximum = (Convert.ToInt32(data.contentCount) / Convert.ToInt32(itemPerPages.SelectedItem)) + 1;
                pageCount.Text = "sur " + currentPage.Maximum;
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

		private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as ItemTemplate;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as ItemTemplate;
			DialogResult = DialogResult.OK;
			Close();
		}

		public ItemTemplate _item { get; set; }

		private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			orderBy = dataGridView1.Columns[e.ColumnIndex].Name;
			LoadItems();
		}

		private void ItemId_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				LoadItems();
		}
	}
}
