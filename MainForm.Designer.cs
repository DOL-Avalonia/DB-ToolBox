namespace AmteCreator
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._TabControl = new System.Windows.Forms.TabControl();
			this._TabGeneral = new System.Windows.Forms.TabPage();
			this.generalTab1 = new AmteCreator.GeneralTab();
			this._TabBonus = new System.Windows.Forms.TabPage();
			this.bonusTab1 = new AmteCreator.BonusTab();
			this._TabLoot = new System.Windows.Forms.TabPage();
			this.lootTab1 = new AmteCreator.LootTab();
			this._TabSummarize = new System.Windows.Forms.TabPage();
			this._debugTB = new System.Windows.Forms.TextBox();
			this.baseXMLData = new System.Data.DataSet();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabItemTemplates = new System.Windows.Forms.TabPage();
			this.tabNPCTemplates = new System.Windows.Forms.TabPage();
			this.npcTemplates1 = new AmteCreator.Controls.NPCTemplates();
            this.tabDataQuestRewardQuests = new System.Windows.Forms.TabPage();
            this.DataQuestRewardQuests = new DataQuestRewardQuests.DataQuestRewardQuests();
			this.toolStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this._TabControl.SuspendLayout();
			this._TabGeneral.SuspendLayout();
			this._TabBonus.SuspendLayout();
			this._TabLoot.SuspendLayout();
			this._TabSummarize.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.baseXMLData)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabItemTemplates.SuspendLayout();
			this.tabNPCTemplates.SuspendLayout();
            this.tabDataQuestRewardQuests.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStrip1.CanOverflow = false;
			this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip1.Location = new System.Drawing.Point(3, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip1.Size = new System.Drawing.Size(869, 23);
			this.toolStrip1.TabIndex = 4;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = global::AmteCreator.Properties.Resources.newToolStripButton_Image;
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 20);
			this.newToolStripButton.Text = "&Nouveau";
			this.newToolStripButton.Click += new System.EventHandler(this._NewTemplate);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = global::AmteCreator.Properties.Resources.openToolStripButton_Image;
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 20);
			this.openToolStripButton.Text = "&Ouvrir";
			this.openToolStripButton.Click += new System.EventHandler(this._OpenTemplate);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = global::AmteCreator.Properties.Resources.saveToolStripButton_Image;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 20);
			this.saveToolStripButton.Text = "Enregi&strer";
			this.saveToolStripButton.Click += new System.EventHandler(this._SaveTemplate);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.toolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(883, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fichierToolStripMenuItem
			// 
			this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitterToolStripMenuItem});
			this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
			this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.fichierToolStripMenuItem.Text = "&Fichier";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this._DisplayOptions);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
			// 
			// quitterToolStripMenuItem
			// 
			this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
			this.quitterToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.quitterToolStripMenuItem.Text = "&Quitter";
			this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
			this.toolStripMenuItem1.Text = "&?";
			// 
			// aProposToolStripMenuItem
			// 
			this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
			this.aProposToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.aProposToolStripMenuItem.Text = "&A propos";
			this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
			// 
			// _TabControl
			// 
			this._TabControl.Controls.Add(this._TabGeneral);
			this._TabControl.Controls.Add(this._TabBonus);
			this._TabControl.Controls.Add(this._TabLoot);
			this._TabControl.Controls.Add(this._TabSummarize);
			this._TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._TabControl.Location = new System.Drawing.Point(3, 26);
			this._TabControl.Name = "_TabControl";
			this._TabControl.Padding = new System.Drawing.Point(3, 3);
			this._TabControl.SelectedIndex = 0;
			this._TabControl.Size = new System.Drawing.Size(869, 575);
			this._TabControl.TabIndex = 6;
			this._TabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this._TabControl_Selecting);
			// 
			// _TabGeneral
			// 
			this._TabGeneral.Controls.Add(this.generalTab1);
			this._TabGeneral.Location = new System.Drawing.Point(4, 22);
			this._TabGeneral.Name = "_TabGeneral";
			this._TabGeneral.Padding = new System.Windows.Forms.Padding(3);
			this._TabGeneral.Size = new System.Drawing.Size(861, 549);
			this._TabGeneral.TabIndex = 0;
			this._TabGeneral.Text = "Général";
			this._TabGeneral.UseVisualStyleBackColor = true;
			// 
			// generalTab1
			// 
			this.generalTab1.currentItem = null;
			this.generalTab1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.generalTab1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.generalTab1.Location = new System.Drawing.Point(3, 3);
			this.generalTab1.Name = "generalTab1";
			this.generalTab1.Size = new System.Drawing.Size(855, 543);
			this.generalTab1.TabIndex = 0;
			// 
			// _TabBonus
			// 
			this._TabBonus.Controls.Add(this.bonusTab1);
			this._TabBonus.Location = new System.Drawing.Point(4, 22);
			this._TabBonus.Name = "_TabBonus";
			this._TabBonus.Padding = new System.Windows.Forms.Padding(3);
			this._TabBonus.Size = new System.Drawing.Size(861, 549);
			this._TabBonus.TabIndex = 1;
			this._TabBonus.Text = "Bonus";
			this._TabBonus.UseVisualStyleBackColor = true;
			// 
			// bonusTab1
			// 
			this.bonusTab1.currentItem = null;
			this.bonusTab1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bonusTab1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.bonusTab1.Location = new System.Drawing.Point(3, 3);
			this.bonusTab1.Name = "bonusTab1";
			this.bonusTab1.Size = new System.Drawing.Size(855, 543);
			this.bonusTab1.TabIndex = 0;
			// 
			// _TabLoot
			// 
			this._TabLoot.Controls.Add(this.lootTab1);
			this._TabLoot.Location = new System.Drawing.Point(4, 22);
			this._TabLoot.Name = "_TabLoot";
			this._TabLoot.Size = new System.Drawing.Size(861, 549);
			this._TabLoot.TabIndex = 2;
			this._TabLoot.Text = "Loot";
			this._TabLoot.UseVisualStyleBackColor = true;
			// 
			// lootTab1
			// 
			this.lootTab1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lootTab1.Location = new System.Drawing.Point(0, 0);
			this.lootTab1.Name = "lootTab1";
			this.lootTab1.Size = new System.Drawing.Size(861, 549);
			this.lootTab1.TabIndex = 0;
			// 
			// _TabSummarize
			// 
			this._TabSummarize.Controls.Add(this._debugTB);
			this._TabSummarize.Location = new System.Drawing.Point(4, 22);
			this._TabSummarize.Name = "_TabSummarize";
			this._TabSummarize.Padding = new System.Windows.Forms.Padding(3);
			this._TabSummarize.Size = new System.Drawing.Size(861, 549);
			this._TabSummarize.TabIndex = 4;
			this._TabSummarize.Text = "Summarize (Debug)";
			this._TabSummarize.UseVisualStyleBackColor = true;
			// 
			// _debugTB
			// 
			this._debugTB.Dock = System.Windows.Forms.DockStyle.Fill;
			this._debugTB.Location = new System.Drawing.Point(3, 3);
			this._debugTB.Multiline = true;
			this._debugTB.Name = "_debugTB";
			this._debugTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._debugTB.Size = new System.Drawing.Size(855, 543);
			this._debugTB.TabIndex = 0;
			// 
			// baseXMLData
			// 
			this.baseXMLData.DataSetName = "NewDataSet";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabItemTemplates);
			this.tabControl1.Controls.Add(this.tabNPCTemplates);
            this.tabControl1.Controls.Add(this.tabDataQuestRewardQuests);
			this.tabControl1.Location = new System.Drawing.Point(0, 27);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(0, 0);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(883, 633);
			this.tabControl1.TabIndex = 7;
			// 
			// tabItemTemplates
			// 
			this.tabItemTemplates.Controls.Add(this._TabControl);
			this.tabItemTemplates.Controls.Add(this.toolStrip1);
			this.tabItemTemplates.Location = new System.Drawing.Point(4, 25);
			this.tabItemTemplates.Name = "tabItemTemplates";
			this.tabItemTemplates.Padding = new System.Windows.Forms.Padding(3);
			this.tabItemTemplates.Size = new System.Drawing.Size(875, 604);
			this.tabItemTemplates.TabIndex = 0;
			this.tabItemTemplates.Text = "Item templates";
			this.tabItemTemplates.UseVisualStyleBackColor = true;
			// 
			// tabNPCTemplates
			// 
			this.tabNPCTemplates.Controls.Add(this.npcTemplates1);
			this.tabNPCTemplates.Location = new System.Drawing.Point(4, 25);
			this.tabNPCTemplates.Name = "tabNPCTemplates";
			this.tabNPCTemplates.Padding = new System.Windows.Forms.Padding(3);
			this.tabNPCTemplates.Size = new System.Drawing.Size(875, 604);
			this.tabNPCTemplates.TabIndex = 1;
			this.tabNPCTemplates.Text = "NPC Templates";
			this.tabNPCTemplates.UseVisualStyleBackColor = true;

            this.tabDataQuestRewardQuests.Controls.Add(this.DataQuestRewardQuests);
            this.tabDataQuestRewardQuests.Location = new System.Drawing.Point(4, 25);
            this.tabDataQuestRewardQuests.Name = "tabDataQuestRewardQuests";
            this.tabDataQuestRewardQuests.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataQuestRewardQuests.Size = new System.Drawing.Size(875, 604);
            this.tabDataQuestRewardQuests.TabIndex = 2;
            this.tabDataQuestRewardQuests.Text = "DataQuest RewardQuests";
            this.tabDataQuestRewardQuests.UseVisualStyleBackColor = true;


            // 
            // npcTemplates1
            // 
            this.npcTemplates1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.npcTemplates1.Location = new System.Drawing.Point(3, 3);
			this.npcTemplates1.Name = "npcTemplates1";
			this.npcTemplates1.Size = new System.Drawing.Size(869, 598);
			this.npcTemplates1.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(883, 659);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.tabControl1);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(625, 572);
			this.Name = "MainForm";
			this.Text = "Avalonia Creator";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this._TabControl.ResumeLayout(false);
			this._TabGeneral.ResumeLayout(false);
			this._TabBonus.ResumeLayout(false);
			this._TabLoot.ResumeLayout(false);
			this._TabSummarize.ResumeLayout(false);
			this._TabSummarize.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.baseXMLData)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabItemTemplates.ResumeLayout(false);
			this.tabItemTemplates.PerformLayout();
			this.tabNPCTemplates.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.TabControl _TabControl;
        private System.Windows.Forms.TabPage _TabGeneral;
        private System.Windows.Forms.TabPage _TabBonus;
        private System.Windows.Forms.TabPage _TabLoot;
        private GeneralTab generalTab1;
        private BonusTab bonusTab1;
        public System.Data.DataSet baseXMLData;
        private System.Windows.Forms.TabPage _TabSummarize;
        private System.Windows.Forms.TextBox _debugTB;
        private LootTab lootTab1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabItemTemplates;
		private System.Windows.Forms.TabPage tabNPCTemplates;
		private Controls.NPCTemplates npcTemplates1;
        private System.Windows.Forms.TabPage tabDataQuestRewardQuests;
        private DataQuestRewardQuests.DataQuestRewardQuests DataQuestRewardQuests;
	}
}

