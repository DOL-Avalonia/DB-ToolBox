using System;
using System.Text;
using System.Web;

namespace AmteCreator.Internal
{
    public class LootTemplate
    {
        private bool _dirty;
        public bool AlreadyInDB;

        public long ID;
        public string TemplateName;

        public LootTemplate()
        {
            AlreadyInDB = false;
            ID = -1;
            TemplateName = "";
            ItemTemplateID = "";
            Chance = 99;
            ItemCount = 1;
            _dirty = false;
        }

        public LootTemplate(dynamic tmpl)
        {
            AlreadyInDB = true;
            ID = long.Parse(tmpl.ID);
            TemplateName = tmpl.TemplateName;
            ItemTemplateID = tmpl.ItemTemplateID;
            Chance = int.Parse(tmpl.Chance);
            ItemCount = int.Parse(tmpl.Count);
            _dirty = false;
        }

        private string _itemTemplateID;
        public string ItemTemplateID
        {
            get { return _itemTemplateID; }
            set { _dirty = _itemTemplateID != value; _itemTemplateID = value; }
        }

        private int _chance;
        public int Chance
        {
            get { return _chance; }
            set { _dirty = _chance != value; _chance = value; }
        }

        private int _itemCount;
        public int ItemCount
        {
            get { return _itemCount; }
            set { _dirty = _itemCount != value; _itemCount = value; }
        }

        private string _GetItemUpdateString()
        {
            string fields = new StringBuilder()
                .Append("ItemTemplateID='").Append(ItemTemplateID).Append("',")
                .Append("Chance='").Append(Chance.ToString()).Append("',")
                .Append("Count='").Append(ItemCount.ToString()).Append("'")
                .ToString();

			return "?action=UPDATE&table=droptemplatexitemtemplate&where=" +
                   HttpUtility.UrlEncode("ID = " + Server.EscapeSql(ID), Encoding.UTF8) +
                   "&upfields=" +
                   HttpUtility.UrlEncode(fields, Encoding.UTF8);
        }

        private string _GetItemInsertString()
        {
            var values = Server.EscapeSql(TemplateName) + "," +
                         Server.EscapeSql(ItemTemplateID) + "," +
                         "'" + Chance + "'," +
                         "'" + ItemCount + "'";
			return "?action=INSERT&table=droptemplatexitemtemplate&fields=" +
                   HttpUtility.UrlEncode("`TemplateName`,`ItemTemplateID`,`Chance`,`Count`", Encoding.UTF8) +
                   "&values=" + HttpUtility.UrlEncode(values, Encoding.UTF8);
        }

        public dynamic Save()
        {
            if (!_dirty)
                return null;
            dynamic resp;
            if (!AlreadyInDB)
            {
                AlreadyInDB = true;
                resp = Server.Query(_GetItemInsertString());
            }
            else if (ID <= -1)
				throw new Exception("Impossible de modifier ce loot, l'id est invalide (-1).");
			else
                resp = Server.Query(_GetItemUpdateString());
            _dirty = resp.error != null;
            return resp;
        }
    }
}
