using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AmteCreator.Internal;

namespace AmteCreator
{
	public partial class RaceForm : Form
	{
		private readonly IList<Race> _templates = new BindingList<Race>();
		private bool _inLoading = false;
		public Race _item { get; set; }
		public Race selectedItem
		{
			get { return _item; }
			set { _item = value; }
		}

		public string orderBy = "ID";

		public RaceForm()
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
			var where = new List<string>();
			if (!string.IsNullOrEmpty(itemId.Text))
				where.Add("ID like " + Server.EscapeSql(itemId.Text.Replace('*', '%')));
			if (!string.IsNullOrEmpty(itemName.Text))
				where.Add("Name like " + Server.EscapeSql(itemName.Text.Replace('*', '%')));
			return where.Count > 0 ? string.Join(" AND ", where) : "1";
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
			dynamic data = Server.QuerySelect("race", _GetWhereClause(), _GetLimitClause(), orderBy);
			if (data is string)
				throw new Exception("Erreur\r\n" + data);
			if (data.error != null)
				throw new Exception("Erreur du serveur:\r\n" + data.error);
			int oldCount = _templates.Count;
			_templates.Clear();
			foreach (var item in data.content)
				_templates.Add(new Race(item));
			if (oldCount != _templates.Count)
			{
				dataGridView1.DataSource = null;
				dataGridView1.DataSource = _templates;
			}
			else
				dataGridView1.Refresh();
			resultCount.Text = "Résultat: " + data.contentCount + " races";
			currentPage.Maximum = (Convert.ToInt32(data.contentCount) / Convert.ToInt32(itemPerPages.SelectedItem)) + 1;
			pageCount.Text = "sur " + currentPage.Maximum;
			_inLoading = false;
		}

		private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as Race;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as Race;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			orderBy = dataGridView1.Columns[e.ColumnIndex].Name;
			LoadItems();
		}
	}
}
