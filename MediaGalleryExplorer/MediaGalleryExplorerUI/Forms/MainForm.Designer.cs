namespace MediaGalleryExplorerUI.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItemGallery = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGalleryNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGalleryOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemGalleryClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemGalleryAddSource = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSource = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSourceScan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSourceRescan = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemGallery,
            this.toolStripMenuItemSource});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuStrip.Size = new System.Drawing.Size(1084, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip";
			// 
			// toolStripMenuItemGallery
			// 
			this.toolStripMenuItemGallery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemGalleryNew,
            this.toolStripMenuItemGalleryOpen,
            this.toolStripMenuItemGalleryClose,
            this.toolStripSeparator1,
            this.toolStripMenuItemGalleryAddSource});
			this.toolStripMenuItemGallery.Name = "toolStripMenuItemGallery";
			this.toolStripMenuItemGallery.Size = new System.Drawing.Size(55, 20);
			this.toolStripMenuItemGallery.Text = "Gallery";
			// 
			// toolStripMenuItemGalleryNew
			// 
			this.toolStripMenuItemGalleryNew.Name = "toolStripMenuItemGalleryNew";
			this.toolStripMenuItemGalleryNew.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemGalleryNew.Text = "New...";
			this.toolStripMenuItemGalleryNew.Click += new System.EventHandler(this.toolStripMenuItemGalleryNew_Click);
			// 
			// toolStripMenuItemGalleryOpen
			// 
			this.toolStripMenuItemGalleryOpen.Name = "toolStripMenuItemGalleryOpen";
			this.toolStripMenuItemGalleryOpen.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemGalleryOpen.Text = "Open...";
			// 
			// toolStripMenuItemGalleryClose
			// 
			this.toolStripMenuItemGalleryClose.Name = "toolStripMenuItemGalleryClose";
			this.toolStripMenuItemGalleryClose.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemGalleryClose.Text = "Close";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// toolStripMenuItemGalleryAddSource
			// 
			this.toolStripMenuItemGalleryAddSource.Name = "toolStripMenuItemGalleryAddSource";
			this.toolStripMenuItemGalleryAddSource.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemGalleryAddSource.Text = "Add Source...";
			// 
			// toolStripMenuItemSource
			// 
			this.toolStripMenuItemSource.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSourceScan,
            this.toolStripMenuItemSourceRescan});
			this.toolStripMenuItemSource.Name = "toolStripMenuItemSource";
			this.toolStripMenuItemSource.Size = new System.Drawing.Size(55, 20);
			this.toolStripMenuItemSource.Text = "Source";
			// 
			// toolStripMenuItemSourceScan
			// 
			this.toolStripMenuItemSourceScan.Name = "toolStripMenuItemSourceScan";
			this.toolStripMenuItemSourceScan.Size = new System.Drawing.Size(116, 22);
			this.toolStripMenuItemSourceScan.Text = "Scan";
			// 
			// toolStripMenuItemSourceRescan
			// 
			this.toolStripMenuItemSourceRescan.Name = "toolStripMenuItemSourceRescan";
			this.toolStripMenuItemSourceRescan.Size = new System.Drawing.Size(116, 22);
			this.toolStripMenuItemSourceRescan.Text = "Re-scan";
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 690);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
			this.statusStrip.Size = new System.Drawing.Size(1084, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Location = new System.Drawing.Point(5, 27);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1075, 660);
			this.tabControl1.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1084, 712);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Media Gallery Explorer";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGallery;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGalleryNew;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGalleryOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGalleryClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSource;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemGalleryAddSource;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSourceScan;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSourceRescan;
	}
}

