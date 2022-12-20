namespace AmteCreator.Controls
{
    partial class IconImageControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
			this.iconPicture = new System.Windows.Forms.PictureBox();
			this.iconNameLabel = new System.Windows.Forms.Label();
			this.idLabel = new System.Windows.Forms.Label();
			this.propertiesLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.iconPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// iconPicture
			// 
			this.iconPicture.BackColor = System.Drawing.Color.Transparent;
			this.iconPicture.Cursor = System.Windows.Forms.Cursors.Hand;
			this.iconPicture.ErrorImage = null;
			this.iconPicture.InitialImage = null;
			this.iconPicture.Location = new System.Drawing.Point(3, 3);
			this.iconPicture.Name = "iconPicture";
			this.iconPicture.Size = new System.Drawing.Size(32, 32);
			this.iconPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.iconPicture.TabIndex = 0;
			this.iconPicture.TabStop = false;
			// 
			// iconNameLabel
			// 
			this.iconNameLabel.AutoSize = true;
			this.iconNameLabel.Location = new System.Drawing.Point(37, 0);
			this.iconNameLabel.Name = "iconNameLabel";
			this.iconNameLabel.Size = new System.Drawing.Size(54, 13);
			this.iconNameLabel.TabIndex = 1;
			this.iconNameLabel.Text = "Iconname";
			// 
			// idLabel
			// 
			this.idLabel.AutoSize = true;
			this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.idLabel.Location = new System.Drawing.Point(38, 23);
			this.idLabel.Name = "idLabel";
			this.idLabel.Size = new System.Drawing.Size(18, 12);
			this.idLabel.TabIndex = 2;
			this.idLabel.Text = "ID:";
			// 
			// propertiesLabel
			// 
			this.propertiesLabel.AutoSize = true;
			this.propertiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.propertiesLabel.Location = new System.Drawing.Point(38, 12);
			this.propertiesLabel.Name = "propertiesLabel";
			this.propertiesLabel.Size = new System.Drawing.Size(8, 12);
			this.propertiesLabel.TabIndex = 4;
			this.propertiesLabel.Text = "-";
			// 
			// IconImageControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.Controls.Add(this.propertiesLabel);
			this.Controls.Add(this.idLabel);
			this.Controls.Add(this.iconNameLabel);
			this.Controls.Add(this.iconPicture);
			this.MinimumSize = new System.Drawing.Size(185, 0);
			this.Name = "IconImageControl";
			this.Size = new System.Drawing.Size(185, 38);
			((System.ComponentModel.ISupportInitialize)(this.iconPicture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconPicture;
        private System.Windows.Forms.Label iconNameLabel;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label propertiesLabel;
    }
}
