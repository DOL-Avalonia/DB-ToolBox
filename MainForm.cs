using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using AmteCreator.Internal;
using System.Data;

namespace AmteCreator
{
	public partial class MainForm : Form
	{
		public static Dictionary<string, string> Options { get; private set; }

		private static string url;
		private static string gameUrl;
		
		public static string URL => url;
		public static string GameUrl => gameUrl;

		private ItemTemplate _template;


        private ItemTemplate _currentTemplate
		{
			get { return _template; }
			set
			{
				_template = value;
				generalTab1.currentItem = _template;
				bonusTab1.currentItem = _template;
			}
		}

		public MainForm()
		{
			InitializeComponent();
			_LoadBases();
			_LoadOptions();
			_NewTemplate();
		}

		private void _LoadBases()
		{
			if (!File.Exists("Res/base.xml"))
			{
				MessageBox.Show(this, "Impossible d'ouvrir \"Res/base.xml\"", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}

			if (!File.Exists("url.txt"))
            {
				MessageBox.Show(this, "Impossible d'ouvrir url.txt", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}

			if (!File.Exists("gameupdate-url.txt"))
			{
				MessageBox.Show(this, "Impossible d'ouvrir gameupdate-url.txt", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}


			try
			{
				url = File.ReadAllText("url.txt") + Constants.JsonAccessUrl;
				gameUrl = File.ReadAllText("gameupdate-url.txt");
				baseXMLData.ReadXmlSchema("Res/base.xsd");
				foreach (DataTable dataTable in baseXMLData.Tables) dataTable.BeginLoadData();
				baseXMLData.ReadXml("Res/base.xml");
				foreach (DataTable dataTable in baseXMLData.Tables) dataTable.EndLoadData();

				generalTab1.baseXMLData = baseXMLData;
				bonusTab1.baseXMLData = baseXMLData;
				npcTemplates1.baseXMLData = baseXMLData;

				generalTab1.LoadComboBoxes();
				bonusTab1.LoadComboBoxes();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Le fichier \"Res/base.xml\" est corrompu\r\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

		private void _LoadOptions()
		{
			if (File.Exists("options.dat"))
			{
				try
				{
					Options = File.ReadAllText("options.dat")
						.Split('\n')
						.Where(l => !string.IsNullOrWhiteSpace(l))
						.Select(l => l.Split(';'))
						.Where(p => p.Length == 2)
						.ToDictionary(l => l[0], l => l[1]);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			if (Options == null || Options.Count < 2)
				_DisplayOptions(null, null);
			dynamic resp = Server.Query("", Options);
			try
			{
				if (!resp.login)
				{
					MessageBox.Show(this, "Erreur lors de la connexion au serveur.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_DisplayOptions(null, null);
					resp = Server.Query("", Options);
					if (!resp.login)
						Close();
				}
				else if (resp.version > Constants.Version)
					MessageBox.Show(this, "Une nouvelle version est disponible !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception)
			{
				MessageBox.Show(this, "Erreur lors de la connexion au serveur.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();
			}
		}

		private static void _SaveOptions()
		{
			File.WriteAllText("options.dat", Options.Select(kv => kv.Key + ";" + kv.Value + "\n").Aggregate((l1, l2) => l1 + l2));
		}

		private void _DisplayOptions(object sender, EventArgs e)
		{
			var of = new OptionsForm(
				(login, pass, dbname) =>
				{
					var loginData = new Dictionary<string, string>
										{
											{"login", login},
								  			{"database", dbname},
											{"password", pass}
										};
					Server.Query("?disconnect");
					return Server.Query("", loginData).login;
				});
			if (of.ShowDialog(this) == DialogResult.OK)
			{
				Options = new Dictionary<string, string>
							  {
								  {"login", of.Login},
								  {"password", of.Password},
								  {"database", of.DBName},
								  {"daoc_path", of.DaocPath}
							  };
				_SaveOptions();
			}
			else if (sender == null)
				Close();
		}

		private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void _OpenTemplate(object sender = null, EventArgs e = null)
		{
			var otf = new OpenTemplateForm();
			if (otf.ShowDialog(this) == DialogResult.OK)
				_currentTemplate = otf.selectedItem;
		}

		private void _NewTemplate(object sender = null, EventArgs e = null)
		{
			_currentTemplate = new ItemTemplate();
		}

		private void _SaveTemplate(object sender = null, EventArgs e = null)
		{
			dynamic resp = Server.QuerySelect("itemtemplate", "Id_nb = " + Server.EscapeSql(_currentTemplate.id_nb));
			if (resp.error != null)
				MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if (int.Parse(resp.contentCount) <= 0)
				_SaveCurrentTemplate();
			else if (MessageBox.Show(this,
									 "Cet ID est déjà utilisé !\r\nVoulez-vous écraser l'ancien item ?!", "Information",
									 MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				_SaveCurrentTemplate(false);
		}

		private void _SaveCurrentTemplate(bool insert = true)
		{
			_currentTemplate.AlreadyInDB = !insert;
			dynamic resp = _currentTemplate.Save();
			if (resp is string)
				MessageBox.Show(this, "Erreur:\r\n" + resp, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else if (resp.error != null)
				MessageBox.Show(this, "Erreur:\r\n" + resp.error, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			else
			{
				try
				{
					Server.UpdateItem(_currentTemplate.id_nb);
					MessageBox.Show(this, "Enregistré !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception e)
				{
					MessageBox.Show(this, "L'item a été enregistré mais il n'a pas pu être mis à jour sur le serveur.\r\n" +
						"Un reboot du serveur est peut-être nécessaire.\r\n\r\nErreur: " + e.Message,
						"Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this, "Créé par Jérémy Virant (aka Dre)\r\nPour Amtenaël\r\n\r\nModifié par Christophe G pour Avalonia", "A propos de Avalonia Creator",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void _TabControl_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPage == _TabSummarize)
				_debugTB.Text = _currentTemplate.ToString();
		}
	}
}
