using System;
using System.Text;
using System.Web;

namespace AmteCreator.Internal
{
    public class MobXLootTemplate
    {
        private bool _dirty;
        public bool AlreadyInDB;

        public string ID;
        public string lootTemplateName;

        public MobXLootTemplate()
        {
            AlreadyInDB = false;
        	ID = null;
            lootTemplateName = "";
            MobName = "New Mob";
            DropCount = 1;
            _dirty = false;
        }

        public MobXLootTemplate(dynamic tmpl)
        {
            AlreadyInDB = true;
            ID = tmpl.MobXLootTemplate_ID;
            lootTemplateName = tmpl.LootTemplateName;
            MobName = tmpl.MobName;
            DropCount = int.Parse(tmpl.DropCount);
            _dirty = false;
        }

        private string _mobName;
        public string MobName
        {
            get { return _mobName; }
            set { _dirty = _mobName != value; _mobName = value; }
        }

        private int _dropCount;
        public int DropCount
        {
            get { return _dropCount; }
            set { _dirty = _dropCount != value; _dropCount = value; }
        }

        private string _GetItemUpdateString()
        {
            string fields = new StringBuilder()
                .Append("LootTemplateName='").Append(lootTemplateName).Append("',")
                .Append("MobName='").Append(MobName).Append("',")
                .Append("DropCount='").Append(DropCount.ToString()).Append("'")
                .ToString();

			return "?action=UPDATE&table=mobdroptemplate&where=" +
                   HttpUtility.UrlEncode("ID = " + Server.EscapeSql(ID), Encoding.UTF8) +
                   "&upfields=" +
                   HttpUtility.UrlEncode(fields, Encoding.UTF8);
        }

        private string _GetItemInsertString()
        {
            var values = Server.EscapeSql(lootTemplateName) + "," +
                         Server.EscapeSql(MobName) + "," +
                         "'" + DropCount + "'";
			return "?action=INSERT&table=mobdroptemplate&fields=" +
                   HttpUtility.UrlEncode("`LootTemplateName`,`MobName`,`DropCount`", Encoding.UTF8) +
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
			else if (ID == null)
				throw new Exception("Impossible de modifier ce loot, l'id est invalide (-1).");
            else
                resp = Server.Query(_GetItemUpdateString());
            _dirty = resp.error != null;
            return resp;
        }
    }
}
