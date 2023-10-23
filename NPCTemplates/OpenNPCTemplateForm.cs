using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AmteCreator.Internal;
using System.Linq;

namespace AmteCreator
{
    public partial class OpenNPCTemplateForm : Form
    {
        private readonly IList<object> _templates = new BindingList<object>();
        private bool _inLoading = false;
        private string FieldID;
        private string FieldName;
        private string orderBy;

        public NPCTemplate _item { get; set; }
        public NPCTemplate selectedItem
        {
            get { return _item; }
            set { _item = value; }
        }

        public DBDQRewardQTemplate SelectedQuest
        {
            get;
            set;
        }


        public string ItemName { get; set; }

        public string OrderBy
        {
            get => this.orderBy;

            set
            {
                this.orderBy = value;
            }
        }

        public string DataTableName { get; set; }

        public OpenNPCTemplateForm(string dataTableName = null, string fieldID = null, string fieldName = null)
        {
            _inLoading = true;
            InitializeComponent();
            itemPerPages.SelectedIndex = 0;
            DataTableName = dataTableName != null ? dataTableName : "npctemplate";
            this.FieldID = fieldID != null ? fieldID : "TemplateId";
            this.FieldName = fieldName != null ? fieldName : "Name";
            this.orderBy = this.FieldID;          

            this.itemName.DataBindings.Add(new Binding("Text", this, nameof(this.ItemName)));
          //  this.itemId.DataBindings.Add(new Binding("Text", this, nameof(this.id)))

            dataGridView1.ClearSelection();
            _inLoading = false;
            LoadItems();
        }

        private string _GetWhereClause()
        {
            var where = new List<string>();
            if (!string.IsNullOrEmpty(itemId.Text))
                where.Add((this.FieldID) + " like " + Server.EscapeSql(itemId.Text.Replace('*', '%')));
            if (!string.IsNullOrEmpty(itemName.Text))
                where.Add(this.FieldName + " like " + Server.EscapeSql(itemName.Text.Replace('*', '%')));
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
            try
            {

                _inLoading = true;
                dynamic data = Server.QuerySelect(this.DataTableName, _GetWhereClause(), _GetLimitClause(), OrderBy);
                if (data is string)
                {
                    _templates.Clear();
                    _inLoading = false;
                    return;
                }

                if (data.error != null)
                    throw new Exception("Erreur du serveur:\r\n" + data.error);

                int oldCount = _templates.Count;
                _templates.Clear();
                foreach (var item in data.content)
                {
                    if (this.DataTableName == "npctemplate")
                    {
                        _templates.Add(new NPCTemplate(item));
                    }
                    else if (this.DataTableName == "dataquestjson")
                    {
                        DBDQRewardQTemplate quest = DBDQRewardQTemplate.GetQuestFromJson(item);
                        _templates.Add(quest);
                    }
                }

                if (this.DataTableName == "dataquestjson")
                {
                    this.Text = "Chercher une QuestReward...";
                }

                if (oldCount != _templates.Count)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _templates;
                }
                else
                    dataGridView1.Refresh();
                resultCount.Text = "Résultat: " + data.contentCount + " templates";
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
			selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as NPCTemplate;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{

            if (DataTableName == "npctemplate")
            {
                selectedItem = dataGridView1.SelectedRows[0].DataBoundItem as NPCTemplate;
            }
            else
            {
                SelectedQuest = dataGridView1.SelectedRows[0].DataBoundItem as DBDQRewardQTemplate;
            }
		
			DialogResult = DialogResult.OK;
			Close();
		}

		private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			OrderBy = dataGridView1.Columns[e.ColumnIndex].Name;
			LoadItems();
		}

		private void ItemName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				LoadItems();
		}
	}
}
