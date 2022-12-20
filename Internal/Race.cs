using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AmteCreator.Internal
{
	[Serializable]
	public class Race
	{
		private bool _dirty = false;
		public bool AlreadyInDB;

		public string Race_ID { get; set; } = "newrace";
		public int ID { get; set; } = -1;
		public string Name { get; set; } = "New race";
		public int ResistBody { get; set; } = 0;
		public int ResistCold { get; set; } = 0;
		public int ResistCrush { get; set; } = 0;
		public int ResistEnergy { get; set; } = 0;
		public int ResistHeat { get; set; } = 0;
		public int ResistMatter { get; set; } = 0;
		public int ResistNatural { get; set; } = 0;
		public int ResistSlash { get; set; } = 0;
		public int ResistSpirit { get; set; } = 0;
		public int ResistThrust { get; set; } = 0;
		public string LastTimeRowUpdated { get; set; } = "2000-01-01 00:00:00";

		public Race()
		{
			AlreadyInDB = false;
			_dirty = false;
			Race_ID = Guid.NewGuid().ToString();
		}

		public Race(dynamic tmpl)
		{
			AlreadyInDB = true;
			_dirty = false;

			Race_ID = tmpl.Race_ID;
			ID = int.Parse(tmpl.ID);
			Name = tmpl.Name;
			ResistBody = int.Parse(tmpl.ResistBody);
			ResistCold = int.Parse(tmpl.ResistCold);
			ResistCrush = int.Parse(tmpl.ResistCrush);
			ResistEnergy = int.Parse(tmpl.ResistEnergy);
			ResistHeat = int.Parse(tmpl.ResistHeat);
			ResistMatter = int.Parse(tmpl.ResistMatter);
			ResistNatural = int.Parse(tmpl.ResistNatural);
			ResistSlash = int.Parse(tmpl.ResistSlash);
			ResistSpirit = int.Parse(tmpl.ResistSpirit);
			ResistThrust = int.Parse(tmpl.ResistThrust);
			LastTimeRowUpdated = tmpl.LastTimeRowUpdated;
		}

		public List<Tuple<string, object>> GetFieldList()
		{
			var fields = new List<Tuple<string, object>>();
			fields.Add(Tuple.Create<string, object>("Race_ID", Race_ID));
			fields.Add(Tuple.Create<string, object>("ID", ID));
			fields.Add(Tuple.Create<string, object>("ResistBody", ResistBody));
			fields.Add(Tuple.Create<string, object>("ResistCold", ResistCold));
			fields.Add(Tuple.Create<string, object>("ResistCrush", ResistCrush));
			fields.Add(Tuple.Create<string, object>("ResistEnergy", ResistEnergy));
			fields.Add(Tuple.Create<string, object>("ResistHeat", ResistHeat));
			fields.Add(Tuple.Create<string, object>("ResistMatter", ResistMatter));
			fields.Add(Tuple.Create<string, object>("ResistNatural", ResistNatural));
			fields.Add(Tuple.Create<string, object>("ResistSlash", ResistSlash));
			fields.Add(Tuple.Create<string, object>("ResistSpirit", ResistSpirit));
			fields.Add(Tuple.Create<string, object>("ResistThrust", ResistThrust));
			fields.Add(Tuple.Create<string, object>("LastTimeRowUpdated", LastTimeRowUpdated));
			return fields;
		}

		private string _GetItemUpdateString()
		{
			var fields = GetFieldList().Select(tup => $"`{tup.Item1}` = {Server.EscapeSql(tup.Item2)}");

			return "?action=UPDATE&table=race&where=" +
				HttpUtility.UrlEncode("ID = " + Server.EscapeSql(ID), Encoding.UTF8) +
				"&upfields=" +
				HttpUtility.UrlEncode(string.Join(",", fields), Encoding.UTF8);
		}

		private string _GetItemInsertString()
		{
			var fields = GetFieldList();
			var fieldList = string.Join(",", fields.Select(tup => $"`{tup.Item1}`"));
			var fieldValues = string.Join(",", fields.Select(tup => Server.EscapeSql(tup.Item2)));
			return "?action=INSERT&table=race&fields=" +
				HttpUtility.UrlEncode(fieldList, Encoding.UTF8) +
				"&values=" + HttpUtility.UrlEncode(fieldValues, Encoding.UTF8);
		}

		public override string ToString()
		{
			return string.Join("\r\n", GetFieldList().Select(tup => $"{tup.Item1}: {tup.Item2}"));
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
				throw new Exception("Impossible de modifier cette race, l'ID est invalide (-1).");
			else
				resp = Server.Query(_GetItemUpdateString());
			_dirty = resp.error != null;
			return resp;
		}
	}
}
