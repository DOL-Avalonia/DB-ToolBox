namespace AmteCreator.Controls
{
    partial class ModelItemControl
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
			this.icon = new System.Windows.Forms.PictureBox();
			this.name = new System.Windows.Forms.Label();
			this.id = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
			this.SuspendLayout();
			// 
			// icon
			// 
			this.icon.Location = new System.Drawing.Point(48, 3);
			this.icon.Name = "icon";
			this.icon.Size = new System.Drawing.Size(32, 32);
			this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.icon.TabIndex = 1;
			this.icon.TabStop = false;
			this.icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this._MouseClick);
			this.icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._MouseDoubleClick);
			this.icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.model_MouseDown);
			this.icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.model_MouseMove);
			// 
			// name
			// 
			this.name.Location = new System.Drawing.Point(0, 51);
			this.name.Name = "name";
			this.name.Size = new System.Drawing.Size(128, 13);
			this.name.TabIndex = 2;
			this.name.Text = "modelName";
			this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.name.MouseClick += new System.Windows.Forms.MouseEventHandler(this._MouseClick);
			this.name.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._MouseDoubleClick);
			this.name.MouseDown += new System.Windows.Forms.MouseEventHandler(this.model_MouseDown);
			this.name.MouseMove += new System.Windows.Forms.MouseEventHandler(this.model_MouseMove);
			// 
			// id
			// 
			this.id.Location = new System.Drawing.Point(0, 38);
			this.id.Name = "id";
			this.id.Size = new System.Drawing.Size(128, 13);
			this.id.TabIndex = 3;
			this.id.Text = "ID";
			this.id.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.id.MouseClick += new System.Windows.Forms.MouseEventHandler(this._MouseClick);
			this.id.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._MouseDoubleClick);
			this.id.MouseDown += new System.Windows.Forms.MouseEventHandler(this.model_MouseDown);
			this.id.MouseMove += new System.Windows.Forms.MouseEventHandler(this.model_MouseMove);
			// 
			// ModelItemControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.id);
			this.Controls.Add(this.name);
			this.Controls.Add(this.icon);
			this.Name = "ModelItemControl";
			this.Size = new System.Drawing.Size(128, 67);
			((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label id;
    }
}
