using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AmteCreator;
using MPKLib;
using LumenWorks.Framework.IO.Csv;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace AmteCreator.IconsPeer
{
	public static class IconSelectorPeer
	{
		public static bool Initialized { get; private set; }

		public static string dataMpk = "gamedata.mpk";
		public static string dataSourceIcons = "icons.csv";
		public static string dataSourceObjects = "objects.csv";
		public static string resPath = "Res";

		private static DataTable iconDataTable;
		private static DataTable objectDataTable;

		public static Icons IconData { get; private set; }
		public static Objects ObjectData { get; private set; }

		public static void Init()
		{
			if (Initialized || !MainForm.Options.ContainsKey("daoc_path")) return;

			FileInfo mpkSource = new FileInfo(Path.Combine(MainForm.Options["daoc_path"], dataMpk));
			FileInfo csvIcons = new FileInfo(Path.Combine(resPath, dataSourceIcons));
			FileInfo csvObjects = new FileInfo(Path.Combine(resPath, dataSourceObjects));

			if (!mpkSource.Exists)
				return;

			if (!csvIcons.Exists || !csvObjects.Exists)
			{
				var mpk = new MPAK();
				mpk.Load(mpkSource.FullName);
				File.WriteAllBytes(Path.Combine(resPath, dataSourceIcons), mpk.GetFile(dataSourceIcons).Data);
				File.WriteAllBytes(Path.Combine(resPath, dataSourceObjects), mpk.GetFile(dataSourceObjects).Data);
				csvIcons = new FileInfo(csvIcons.FullName);
				csvObjects = new FileInfo(csvObjects.FullName);
			}

			//Read the Data
			iconDataTable = new DataTable();
			objectDataTable = new DataTable();
			IconData = new Icons();
			ObjectData = new Objects();
			ReadCsv<Icons.IconValuesDataTable, Icons.IconValuesRow>(csvIcons, iconDataTable, IconData.IconValues);
			ReadCsv<Objects.ObjectsDataTable, Objects.ObjectsRow>(csvObjects, objectDataTable, ObjectData._Objects);

			//Get the Icon files
			ReadIconFiles();

			Initialized = true;
		}

		public static void Release()
		{
			if (iconDataTable != null)
			{
				iconDataTable.Clear();
				iconDataTable.Dispose();
				iconDataTable = null;
			}
			if (IconData != null)
			{
				IconData.Clear();
				IconData.Dispose();
				IconData = null;
			}
			Initialized = false;
		}

		public static void ReadCsv<T, T2>(FileInfo csvFile, DataTable table, T values) where T : TypedTableBase<T2> where T2 : DataRow
		{
			StreamReader streamReader = new StreamReader(csvFile.FullName);
			//Skip the first line
			streamReader.ReadLine();
			streamReader.ReadLine();

			using (CsvReader csv = new CsvReader(streamReader, false, ','))
			{
				int fieldCount = csv.FieldCount;
				string[] headers = csv.GetFieldHeaders();

				int id = 0;
				foreach (string header in headers)
				{
					table.Columns.Add(header + id++);
				}

				while (csv.ReadNextRecord())
				{
					if (string.IsNullOrWhiteSpace(csv[0]) || string.IsNullOrWhiteSpace(csv[1]))
						continue;
					DataRow row = values.NewRow();
					try
					{
						for (int i = 0; i < fieldCount; i++)
						{
							if (i < values.Columns.Count)
								row[i] = csv[i] == "" ? "-1" : csv[i];
						}
						values.Rows.Add(row);
					}
					catch (Exception e)
					{
						throw e;
					}
				}
			}
		}

		public static void ReadIconFiles()
		{
			Dictionary<int, KeyValuePair<string, string>> iconFiles = new Dictionary<int, KeyValuePair<string, string>>();
			//Spells
			iconFiles.Add(0, new KeyValuePair<string, string>("spells", "spl_0.bmp"));
			iconFiles.Add(100, new KeyValuePair<string, string>("spells", "spl_100.bmp"));
			iconFiles.Add(200, new KeyValuePair<string, string>("spells", "spl_200.bmp"));
			iconFiles.Add(300, new KeyValuePair<string, string>("spells", "spl_300.bmp"));
			iconFiles.Add(400, new KeyValuePair<string, string>("spells", "spl_400.bmp"));

			//Styles
			iconFiles.Add(500, new KeyValuePair<string, string>("styles", "cbt_500.bmp"));
			iconFiles.Add(600, new KeyValuePair<string, string>("styles", "cbt_600.bmp"));
			iconFiles.Add(700, new KeyValuePair<string, string>("styles", "cbt_700.bmp"));
			iconFiles.Add(800, new KeyValuePair<string, string>("styles", "cbt_800.bmp"));

			//Weapons
			iconFiles.Add(900, new KeyValuePair<string, string>("weapons", "wpn_900.bmp"));
			iconFiles.Add(1000, new KeyValuePair<string, string>("weapons", "wpn_1000.bmp"));
			iconFiles.Add(1100, new KeyValuePair<string, string>("weapons", "wpn_1100.bmp"));

			//Items
			iconFiles.Add(1200, new KeyValuePair<string, string>("items", "itm_1200.bmp"));
			iconFiles.Add(1300, new KeyValuePair<string, string>("items", "itm_1300.bmp"));
			iconFiles.Add(1400, new KeyValuePair<string, string>("items", "itm_1400.bmp"));
			iconFiles.Add(1500, new KeyValuePair<string, string>("items", "itm_1500.bmp"));

			//Skills
			iconFiles.Add(1600, new KeyValuePair<string, string>("skills", "skl_1600.bmp"));

			//Check if all files exist
			foreach (int id in iconFiles.Keys)
			{
				string fileName = Path.Combine(MainForm.Options["daoc_path"], "icons", iconFiles[id].Value);
				if (File.Exists(fileName))
				{
					Icons.IconFilesRow row = (Icons.IconFilesRow)IconData.IconFiles.NewRow();
					row.id = id;
					row.category = iconFiles[id].Key;
					row.file = iconFiles[id].Value;
					row.fullPath = fileName;
					IconData.IconFiles.Rows.Add(row);
				}
			}
		}

		public static Icons.IconValuesRow GetIconRow(int iconId)
		{
			if (!Initialized) throw new Exception("IconSelectorPeer not intialized");

			var iconRow = from icons in IconData.IconValues
						  where icons.id == iconId
						  select icons;

			if (iconRow.Count() > 0) return iconRow.First<Icons.IconValuesRow>();
			else return null;
		}

		public static Bitmap GetIconFromItemModel(int itemModel)
		{
			var item = from a in ObjectData._Objects
					   where a.ID == itemModel
					   select a.Icon;
			return GetIconImage(item.FirstOrDefault());
		}
		public static Bitmap GetIconImage(int iconId)
		{
			return GetIconImage(GetIconRow(iconId));
		}

		private static Dictionary<int, Bitmap> bitmapHolder = new Dictionary<int, Bitmap>();
		public static Bitmap GetIconImage(Icons.IconValuesRow row)
		{
			if (!Initialized) throw new Exception("IconSelectorPeer not intialized");
			if (row == null) return null;

			GetIconBorders();
			GetIconStylesConditions();
			GetIconSpellValues();

			//Get the base
			int iconSetFile = row.baseIconId - row.baseIconId % 100;

			var iconRow = IconData.IconFiles.FirstOrDefault(i => i.id.Equals(iconSetFile));

			if (!bitmapHolder.ContainsKey(iconSetFile))
			{
				if (iconRow == null || iconRow.fullPath == null)
				{
					return null;
				}

				bitmapHolder.Add(iconSetFile, new Bitmap(iconRow.fullPath));
			}

			//Location
			int x = (row.baseIconId % 100) % 10 * 32;
			int y = ((row.baseIconId % 100) - (row.baseIconId % 100) % 10) / 10 * 32;
			Rectangle location = new Rectangle(x, y, 32, 32);
			Bitmap baseIcon = CopyBitmap(bitmapHolder[iconSetFile], location);

			//Borders
			if (row.borderId >= 0) baseIcon = AddIconBorder(baseIcon, row.borderId);

			//Add Style conditions
			AddIconStylesConditions(baseIcon, row);

			//Add Spell stat
			if (row.spell >= 0) AddSpellValueIcon(baseIcon, row.spell);

			return baseIcon;
		}

		#region Icon Borders

		private static bool bordersInitialized = false;
		private static Bitmap iconBorders;
		private static void GetIconBorders()
		{
			if (bordersInitialized) return;
			iconBorders = Properties.Resources.icon_borders;
			bordersInitialized = true;
		}

		#endregion

		#region Icon Styles

		private static Bitmap AddIconBorder(Bitmap target, int iconBorderId)
		{
			if (iconBorderId < 0) return target;

			Rectangle borderPosition = new Rectangle(iconBorderId * 32, 0, 32, 32);
			Rectangle targetPosition = new Rectangle(0, 0, 32, 32);
			Bitmap border = CopyBitmap(iconBorders, borderPosition);

			return MergeBitmap(border, target, targetPosition);
		}

		private static bool iconStylesConditionsInitialized = false;
		private static Bitmap iconStylesConditions;
		private static void GetIconStylesConditions()
		{
			if (iconStylesConditionsInitialized) return;
			iconStylesConditions = Properties.Resources.icon_stats_styles;
			iconStylesConditionsInitialized = true;
		}

		private static Bitmap AddIconStylesConditions(Bitmap target, Icons.IconValuesRow row)
		{
			Rectangle position;
			if (row.up_left >= 0)
			{
				position = new Rectangle(0, 0, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.up_left), target, position);
			}
			if (row.up >= 0)
			{
				position = new Rectangle(11, 0, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.up), target, position);
			}
			if (row.up_right >= 0)
			{
				position = new Rectangle(22, 0, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.up_right), target, position);
			}
			if (row.right >= 0)
			{
				position = new Rectangle(22, 11, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.right), target, position);
			}
			if (row.down_right >= 0)
			{
				position = new Rectangle(22, 22, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.down_right), target, position);
			}
			if (row.down >= 0)
			{
				position = new Rectangle(11, 22, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.down), target, position);
			}
			if (row.left >= 0)
			{
				position = new Rectangle(0, 11, 10, 10);
				target = MergeBitmap(GetStyleConditionIcon(row.left), target, position);
			}


			return target;
		}

		private static Bitmap GetStyleConditionIcon(int styleConditionId)
		{
			Rectangle position = new Rectangle(styleConditionId * 10, 0, 10, 10);
			return CopyBitmap(iconStylesConditions, position);
		}

		#endregion

		#region Icon Spells values

		private static bool iconSpellValuesInitialized = false;
		private static Bitmap iconSpellValues;

		static IconSelectorPeer()
		{
			Initialized = false;
		}

		private static void GetIconSpellValues()
		{
			if (iconSpellValuesInitialized) return;
			iconSpellValues = Properties.Resources.icon_spell_values;
			iconSpellValuesInitialized = true;
		}

		private static Bitmap AddSpellValueIcon(Bitmap target, int spellStatId)
		{
			Rectangle position = new Rectangle(13, 21, 20, 12);
			Bitmap spellStatIcon = GetSpellValueIcon(spellStatId);
			target = MergeBitmap(spellStatIcon, target, position);
			spellStatIcon.Dispose();

			return target;
		}

		private static Bitmap GetSpellValueIcon(int spellStatId)
		{
			int row = (int)Math.Floor(spellStatId % 100 / 7.0);
			int column = (spellStatId % 100) % 7;

			Rectangle iconPosition = new Rectangle(column * 20, row * 12, 20, 12);
			return CopyBitmap(iconSpellValues, iconPosition);
		}

		#endregion

		#region Util

		public static Bitmap CopyBitmap(Bitmap source, Rectangle part)
		{
			return source.Clone(part, source.PixelFormat);
		}
		public static Bitmap MergeBitmap(Bitmap source, Bitmap target, Rectangle part)
		{
			using (Graphics g = Graphics.FromImage(target))
			{
				g.DrawImage(source, part);
				return target;
			}
		}

		#endregion
	}
}
