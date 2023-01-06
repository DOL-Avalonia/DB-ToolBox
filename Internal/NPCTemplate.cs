using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AmteCreator.Internal
{
	[Serializable]
	public class NPCTemplate
	{
		public bool AlreadyInDB { get { return _savedTemplate != null && _savedTemplate.TemplateId == TemplateId;  } }

		public string NpcTemplate_ID { get; set; } = "newtemplate";
		public int TemplateId { get; set; } = -1;
		public string Name { get; set; } = "New NPC Template";
		public string GuildName { get; set; }
		public string Model { get; set; } = "480";
		public string Size { get; set; } = "50";
		public int MaxSpeed { get; set; } = 0;
		public string EquipmentTemplateID { get; set; }
		public int Flags { get; set; } = 0;
		public int MeleeDamageType { get; set; } = 0;
		public int ParryChance { get; set; } = 0;
		public int EvadeChance { get; set; } = 0;
		public int BlockChance { get; set; } = 0;
		public int LeftHandSwingChance { get; set; } = 0;
		public string Spells { get; set; }
		public string Styles { get; set; }
		public int Strength { get; set; } = 0;
		public int Constitution { get; set; } = 0;
		public int Dexterity { get; set; } = 0;
		public int Quickness { get; set; } = 0;
		public int Intelligence { get; set; } = 0;
		public int Piety { get; set; } = 0;
		public int Charisma { get; set; } = 0;
		public int Empathy { get; set; } = 0;
		public string Abilities { get; set; }
		public int AggroLevel { get; set; } = 0;
		public int AggroRange { get; set; } = 0;
		public string Level { get; set; } = "1";
		public int Race { get; set; } = 0;
		public int BodyType { get; set; } = 0;
		public int MaxDistance { get; set; } = 0;
		public int TetherRange { get; set; } = 0;
		public string PackageID { get; set; }
		public int VisibleWeaponSlots { get; set; } = 0;
		public bool ReplaceMobValues { get; set; } = false;
		public string ItemsListTemplateID { get; set; }
		public string TranslationId { get; set; }
		public string Suffix { get; set; }
		public string ExamineArticle { get; set; }
		public string MessageArticle { get; set; }
		public int Gender { get; set; } = 0;
		public string LastTimeRowUpdated { get; set; } = "2000-01-01 00:00:00";

		public int DPS { get; set; } = 0;
		public int SPD { get; set; } = 0;
		public int AF { get; set; } = 0;
		public int ABS { get; set; } = 0;

		private NPCTemplate _savedTemplate = null;

		public NPCTemplate()
		{
			NpcTemplate_ID = Guid.NewGuid().ToString();
		}

		public NPCTemplate(dynamic tmpl)
		{
			NpcTemplate_ID = tmpl.NpcTemplate_ID;
			TemplateId = int.Parse(tmpl.TemplateId);
			Name = tmpl.Name;
			GuildName = tmpl.GuildName;
			Model = tmpl.Model;
			Size = tmpl.Size;
			MaxSpeed = int.Parse(tmpl.MaxSpeed);
			EquipmentTemplateID = tmpl.EquipmentTemplateID;
			Flags = int.Parse(tmpl.Flags);
			MeleeDamageType = int.Parse(tmpl.MeleeDamageType);
			ParryChance = int.Parse(tmpl.ParryChance);
			EvadeChance = int.Parse(tmpl.EvadeChance);
			BlockChance = int.Parse(tmpl.BlockChance);
			LeftHandSwingChance = int.Parse(tmpl.LeftHandSwingChance);
			Spells = tmpl.Spells;
			Styles = tmpl.Styles;
			Strength = int.Parse(tmpl.Strength);
			Constitution = int.Parse(tmpl.Constitution);
			Dexterity = int.Parse(tmpl.Dexterity);
			Quickness = int.Parse(tmpl.Quickness);
			Intelligence = int.Parse(tmpl.Intelligence);
			Piety = int.Parse(tmpl.Piety);
			Charisma = int.Parse(tmpl.Charisma);
			Empathy = int.Parse(tmpl.Empathy);
			Abilities = tmpl.Abilities;
			AggroLevel = int.Parse(tmpl.AggroLevel);
			AggroRange = int.Parse(tmpl.AggroRange);
			Level = tmpl.Level;
			Race = int.Parse(tmpl.Race);
			BodyType = int.Parse(tmpl.BodyType);
			MaxDistance = int.Parse(tmpl.MaxDistance);
			TetherRange = int.Parse(tmpl.TetherRange);
			PackageID = tmpl.PackageID;
			VisibleWeaponSlots = int.Parse(tmpl.VisibleWeaponSlots);
			ReplaceMobValues = tmpl.ReplaceMobValues == "1";
			ItemsListTemplateID = tmpl.ItemsListTemplateID;
			TranslationId = tmpl.TranslationId;
			Suffix = tmpl.Suffix;
			ExamineArticle = tmpl.ExamineArticle;
			MessageArticle = tmpl.MessageArticle;
			Gender = int.Parse(tmpl.Gender);
			LastTimeRowUpdated = tmpl.LastTimeRowUpdated;
			DPS = int.Parse(tmpl.WeaponDps);
			SPD = int.Parse(tmpl.WeaponSpd);
			AF = int.Parse(tmpl.ArmorFactor);
			ABS = int.Parse(tmpl.ArmorAbsorb);

			_savedTemplate = Clone();
		}

		public List<Tuple<string, object>> GetFieldList()
		{
			var fields = new List<Tuple<string, object>>();
			fields.Add("NpcTemplate_ID", NpcTemplate_ID);
			fields.Add("TemplateId", TemplateId);
			fields.Add("ReplaceMobValues", ReplaceMobValues ? 1 : 0);

			fields.Add("Name", Name);
			fields.Add("GuildName", GuildName);
			fields.Add("Model", Model);
			fields.Add("Size", Size);
			fields.Add("Suffix", Suffix);
			fields.Add("Gender", Gender);

			fields.Add("MaxSpeed", MaxSpeed);
			fields.Add("Flags", Flags);

			fields.Add("Level", Level);
			fields.Add("Race", Race);
			fields.Add("BodyType", BodyType);
			fields.Add("EquipmentTemplateID", EquipmentTemplateID);
			fields.Add("ItemsListTemplateID", ItemsListTemplateID);
			fields.Add("VisibleWeaponSlots", VisibleWeaponSlots.ToString());

			fields.Add("MeleeDamageType", MeleeDamageType);
			fields.Add("ParryChance", ParryChance);
			fields.Add("EvadeChance", EvadeChance);
			fields.Add("BlockChance", BlockChance);
			fields.Add("LeftHandSwingChance", LeftHandSwingChance);

			fields.Add("Spells", Spells);
			fields.Add("Styles", Styles);
			fields.Add("Abilities", Abilities);

			fields.Add("Strength", Strength);
			fields.Add("Constitution", Constitution);
			fields.Add("Dexterity", Dexterity);
			fields.Add("Quickness", Quickness);
			fields.Add("Intelligence", Intelligence);
			fields.Add("Piety", Piety);
			fields.Add("Charisma", Charisma);
			fields.Add("Empathy", Empathy);

			fields.Add("AggroLevel", AggroLevel);
			fields.Add("AggroRange", AggroRange);
			fields.Add("MaxDistance", MaxDistance);
			fields.Add("TetherRange", TetherRange);

			fields.Add("TranslationId", TranslationId);
			fields.Add("ExamineArticle", ExamineArticle);
			fields.Add("MessageArticle", MessageArticle);
			fields.Add("PackageID", PackageID);
			fields.Add("LastTimeRowUpdated", LastTimeRowUpdated);
			fields.Add("WeaponDps", DPS.ToString());
			fields.Add("WeaponSpd", SPD.ToString());
			fields.Add("ArmorFactor", AF.ToString());
			fields.Add("ArmorAbsorb", ABS.ToString());


			return fields;
		}

		private string _GetUpdateString()
		{
			var fields = GetFieldList().Select(tup => $"`{tup.Item1}` = {Server.EscapeSql(tup.Item2)}");

			return "?action=UPDATE&table=npctemplate&where=" +
				HttpUtility.UrlEncode("TemplateId = " + Server.EscapeSql(TemplateId), Encoding.UTF8) +
				"&upfields=" +
				HttpUtility.UrlEncode(string.Join(",", fields), Encoding.UTF8);
		}

		private string _GetInsertString()
		{
			NpcTemplate_ID = Name;
			var fields = GetFieldList();
			var fieldList = string.Join(",", fields.Select(tup => $"`{tup.Item1}`"));
			var fieldValues = string.Join(",", fields.Select(tup => Server.EscapeSql(tup.Item2)));
			return "?action=INSERT&table=npctemplate&fields=" +
				HttpUtility.UrlEncode(fieldList, Encoding.UTF8) +
				"&values=" + HttpUtility.UrlEncode(fieldValues, Encoding.UTF8);
		}

		public override string ToString()
		{
			return string.Join("\r\n", GetFieldList().Select(tup => $"{tup.Item1}: {tup.Item2}"));
		}

		public dynamic Save()
		{
			if (_savedTemplate != null && this == _savedTemplate)
				return new { error = "Le template est identique. Il n'est pas sauvegardé" };
			dynamic resp;
			if (TemplateId <= -1)
				throw new Exception("Impossible de modifier ce template, le TemplateId est invalide (-1).");
			else if (AlreadyInDB)
				resp = Server.Query(_GetUpdateString());
			else
				resp = Server.Query(_GetInsertString());
			if (resp != null && resp.error == null)
				_savedTemplate = Clone();
			return resp;
		}

		public NPCTemplate Clone()
		{
			var tmpl = new NPCTemplate();
			tmpl.NpcTemplate_ID = NpcTemplate_ID;
			tmpl.TemplateId = TemplateId;
			tmpl.Name = Name;
			tmpl.GuildName = GuildName;
			tmpl.Model = Model;
			tmpl.Size = Size;
			tmpl.MaxSpeed = MaxSpeed;
			tmpl.EquipmentTemplateID = EquipmentTemplateID;
			tmpl.Flags = Flags;
			tmpl.MeleeDamageType = MeleeDamageType;
			tmpl.ParryChance = ParryChance;
			tmpl.EvadeChance = EvadeChance;
			tmpl.BlockChance = BlockChance;
			tmpl.LeftHandSwingChance = LeftHandSwingChance;
			tmpl.Spells = Spells;
			tmpl.Styles = Styles;
			tmpl.Strength = Strength;
			tmpl.Constitution = Constitution;
			tmpl.Dexterity = Dexterity;
			tmpl.Quickness = Quickness;
			tmpl.Intelligence = Intelligence;
			tmpl.Piety = Piety;
			tmpl.Charisma = Charisma;
			tmpl.Empathy = Empathy;
			tmpl.Abilities = Abilities;
			tmpl.AggroLevel = AggroLevel;
			tmpl.AggroRange = AggroRange;
			tmpl.Level = Level;
			tmpl.Race = Race;
			tmpl.BodyType = BodyType;
			tmpl.MaxDistance = MaxDistance;
			tmpl.TetherRange = TetherRange;
			tmpl.PackageID = PackageID;
			tmpl.VisibleWeaponSlots = VisibleWeaponSlots;
			tmpl.ReplaceMobValues = ReplaceMobValues;
			tmpl.ItemsListTemplateID = ItemsListTemplateID;
			tmpl.TranslationId = TranslationId;
			tmpl.Suffix = Suffix;
			tmpl.ExamineArticle = ExamineArticle;
			tmpl.MessageArticle = MessageArticle;
			tmpl.Gender = Gender;
			tmpl.LastTimeRowUpdated = LastTimeRowUpdated;
			tmpl.DPS = DPS;
			tmpl.SPD = SPD;
			tmpl.AF = AF;
			tmpl.ABS = ABS;
			return tmpl;
		}

		public static bool operator !=(NPCTemplate lhs, NPCTemplate rhs) => !(lhs == rhs);
		public static bool operator ==(NPCTemplate lhs, NPCTemplate rhs) =>
			ReferenceEquals(lhs, rhs) ||
			(!ReferenceEquals(lhs, null) && !ReferenceEquals(rhs, null)
			&& lhs.NpcTemplate_ID == rhs.NpcTemplate_ID
			&& lhs.TemplateId == rhs.TemplateId
			&& lhs.Name == rhs.Name
			&& lhs.GuildName == rhs.GuildName
			&& lhs.Model == rhs.Model
			&& lhs.Size == rhs.Size
			&& lhs.MaxSpeed == rhs.MaxSpeed
			&& lhs.EquipmentTemplateID == rhs.EquipmentTemplateID
			&& lhs.Flags == rhs.Flags
			&& lhs.MeleeDamageType == rhs.MeleeDamageType
			&& lhs.ParryChance == rhs.ParryChance
			&& lhs.EvadeChance == rhs.EvadeChance
			&& lhs.BlockChance == rhs.BlockChance
			&& lhs.LeftHandSwingChance == rhs.LeftHandSwingChance
			&& lhs.Spells == rhs.Spells
			&& lhs.Styles == rhs.Styles
			&& lhs.Strength == rhs.Strength
			&& lhs.Constitution == rhs.Constitution
			&& lhs.Dexterity == rhs.Dexterity
			&& lhs.Quickness == rhs.Quickness
			&& lhs.Intelligence == rhs.Intelligence
			&& lhs.Piety == rhs.Piety
			&& lhs.Charisma == rhs.Charisma
			&& lhs.Empathy == rhs.Empathy
			&& lhs.Abilities == rhs.Abilities
			&& lhs.AggroLevel == rhs.AggroLevel
			&& lhs.AggroRange == rhs.AggroRange
			&& lhs.Level == rhs.Level
			&& lhs.Race == rhs.Race
			&& lhs.BodyType == rhs.BodyType
			&& lhs.MaxDistance == rhs.MaxDistance
			&& lhs.TetherRange == rhs.TetherRange
			&& lhs.PackageID == rhs.PackageID
			&& lhs.VisibleWeaponSlots == rhs.VisibleWeaponSlots
			&& lhs.ReplaceMobValues == rhs.ReplaceMobValues
			&& lhs.ItemsListTemplateID == rhs.ItemsListTemplateID
			&& lhs.TranslationId == rhs.TranslationId
			&& lhs.Suffix == rhs.Suffix
			&& lhs.ExamineArticle == rhs.ExamineArticle
			&& lhs.MessageArticle == rhs.MessageArticle
			&& lhs.Gender == rhs.Gender
			&& lhs.LastTimeRowUpdated == rhs.LastTimeRowUpdated
			&& lhs.ABS == rhs.ABS
			&& lhs.AF == rhs.AF
			&& lhs.DPS == rhs.DPS
			&& lhs.SPD == rhs.SPD);
	}
}
