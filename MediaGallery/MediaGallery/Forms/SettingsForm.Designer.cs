namespace MediaGallery.Forms
{
	partial class SettingsForm
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
			this.buttonAdd = new System.Windows.Forms.Button();
			this.listViewSources = new System.Windows.Forms.ListView();
			this.columnHeaderSourcePath = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderImageCount = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderVideoCount = new System.Windows.Forms.ColumnHeader();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.groupBoxApplication = new System.Windows.Forms.GroupBox();
			this.buttonBrowseDatabase = new System.Windows.Forms.Button();
			this.labelDatabaseLocation = new System.Windows.Forms.Label();
			this.textBoxDatabaseLocation = new System.Windows.Forms.TextBox();
			this.groupBoxGallery = new System.Windows.Forms.GroupBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.groupBoxThumbnails = new System.Windows.Forms.GroupBox();
			this.buttonBrowseVideoThumbnailsMakerPreset = new System.Windows.Forms.Button();
			this.textBoxVideoThumbnailsMakerPreset = new System.Windows.Forms.TextBox();
			this.labelVideoThumbnailsMakerPreset = new System.Windows.Forms.Label();
			this.buttonBrowseVideoThumbnailsMaker = new System.Windows.Forms.Button();
			this.textBoxVideoThumbnailsMaker = new System.Windows.Forms.TextBox();
			this.labelVideoThumbnailsMaker = new System.Windows.Forms.Label();
			this.buttonBrowserWorkingDirectory = new System.Windows.Forms.Button();
			this.labelWorkingDirectory = new System.Windows.Forms.Label();
			this.textBoxWorkingDirectory = new System.Windows.Forms.TextBox();
			this.groupBoxApplication.SuspendLayout();
			this.groupBoxGallery.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.groupBoxThumbnails.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAdd.Location = new System.Drawing.Point(537, 19);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 3;
			this.buttonAdd.Text = "Add...";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// listViewSources
			// 
			this.listViewSources.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewSources.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSourcePath,
            this.columnHeaderImageCount,
            this.columnHeaderVideoCount});
			this.listViewSources.FullRowSelect = true;
			this.listViewSources.HideSelection = false;
			this.listViewSources.Location = new System.Drawing.Point(10, 19);
			this.listViewSources.Name = "listViewSources";
			this.listViewSources.Size = new System.Drawing.Size(521, 199);
			this.listViewSources.TabIndex = 2;
			this.listViewSources.UseCompatibleStateImageBehavior = false;
			this.listViewSources.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderSourcePath
			// 
			this.columnHeaderSourcePath.Text = "Source";
			this.columnHeaderSourcePath.Width = 300;
			// 
			// columnHeaderImageCount
			// 
			this.columnHeaderImageCount.Text = "Images";
			// 
			// columnHeaderVideoCount
			// 
			this.columnHeaderVideoCount.Text = "Videos";
			// 
			// buttonRemove
			// 
			this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemove.Location = new System.Drawing.Point(537, 48);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(75, 23);
			this.buttonRemove.TabIndex = 4;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// groupBoxApplication
			// 
			this.groupBoxApplication.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxApplication.Controls.Add(this.buttonBrowserWorkingDirectory);
			this.groupBoxApplication.Controls.Add(this.labelWorkingDirectory);
			this.groupBoxApplication.Controls.Add(this.textBoxWorkingDirectory);
			this.groupBoxApplication.Controls.Add(this.buttonBrowseDatabase);
			this.groupBoxApplication.Controls.Add(this.labelDatabaseLocation);
			this.groupBoxApplication.Controls.Add(this.textBoxDatabaseLocation);
			this.groupBoxApplication.Location = new System.Drawing.Point(12, 12);
			this.groupBoxApplication.Name = "groupBoxApplication";
			this.groupBoxApplication.Size = new System.Drawing.Size(621, 92);
			this.groupBoxApplication.TabIndex = 1;
			this.groupBoxApplication.TabStop = false;
			this.groupBoxApplication.Text = "Application";
			// 
			// buttonBrowseDatabase
			// 
			this.buttonBrowseDatabase.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseDatabase.Location = new System.Drawing.Point(537, 17);
			this.buttonBrowseDatabase.Name = "buttonBrowseDatabase";
			this.buttonBrowseDatabase.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseDatabase.TabIndex = 2;
			this.buttonBrowseDatabase.Text = "Browse...";
			this.buttonBrowseDatabase.UseVisualStyleBackColor = true;
			this.buttonBrowseDatabase.Click += new System.EventHandler(this.buttonBrowseDatabase_Click);
			// 
			// labelDatabaseLocation
			// 
			this.labelDatabaseLocation.AutoSize = true;
			this.labelDatabaseLocation.Location = new System.Drawing.Point(7, 22);
			this.labelDatabaseLocation.Name = "labelDatabaseLocation";
			this.labelDatabaseLocation.Size = new System.Drawing.Size(96, 13);
			this.labelDatabaseLocation.TabIndex = 1;
			this.labelDatabaseLocation.Text = "Database location:";
			// 
			// textBoxDatabaseLocation
			// 
			this.textBoxDatabaseLocation.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDatabaseLocation.Location = new System.Drawing.Point(109, 19);
			this.textBoxDatabaseLocation.Name = "textBoxDatabaseLocation";
			this.textBoxDatabaseLocation.ReadOnly = true;
			this.textBoxDatabaseLocation.Size = new System.Drawing.Size(422, 20);
			this.textBoxDatabaseLocation.TabIndex = 0;
			// 
			// groupBoxGallery
			// 
			this.groupBoxGallery.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGallery.Controls.Add(this.listViewSources);
			this.groupBoxGallery.Controls.Add(this.buttonAdd);
			this.groupBoxGallery.Controls.Add(this.buttonRemove);
			this.groupBoxGallery.Location = new System.Drawing.Point(12, 282);
			this.groupBoxGallery.Name = "groupBoxGallery";
			this.groupBoxGallery.Size = new System.Drawing.Size(621, 228);
			this.groupBoxGallery.TabIndex = 2;
			this.groupBoxGallery.TabStop = false;
			this.groupBoxGallery.Text = "Gallery";
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(273, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.buttonOK, 1, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(12, 516);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(621, 29);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// groupBoxThumbnails
			// 
			this.groupBoxThumbnails.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxThumbnails.Controls.Add(this.buttonBrowseVideoThumbnailsMakerPreset);
			this.groupBoxThumbnails.Controls.Add(this.textBoxVideoThumbnailsMakerPreset);
			this.groupBoxThumbnails.Controls.Add(this.labelVideoThumbnailsMakerPreset);
			this.groupBoxThumbnails.Controls.Add(this.buttonBrowseVideoThumbnailsMaker);
			this.groupBoxThumbnails.Controls.Add(this.textBoxVideoThumbnailsMaker);
			this.groupBoxThumbnails.Controls.Add(this.labelVideoThumbnailsMaker);
			this.groupBoxThumbnails.Location = new System.Drawing.Point(12, 110);
			this.groupBoxThumbnails.Name = "groupBoxThumbnails";
			this.groupBoxThumbnails.Size = new System.Drawing.Size(621, 166);
			this.groupBoxThumbnails.TabIndex = 4;
			this.groupBoxThumbnails.TabStop = false;
			this.groupBoxThumbnails.Text = "Thumbnails";
			// 
			// buttonBrowseVideoThumbnailsMakerPreset
			// 
			this.buttonBrowseVideoThumbnailsMakerPreset.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseVideoThumbnailsMakerPreset.Location = new System.Drawing.Point(537, 43);
			this.buttonBrowseVideoThumbnailsMakerPreset.Name = "buttonBrowseVideoThumbnailsMakerPreset";
			this.buttonBrowseVideoThumbnailsMakerPreset.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseVideoThumbnailsMakerPreset.TabIndex = 5;
			this.buttonBrowseVideoThumbnailsMakerPreset.Text = "Browser...";
			this.buttonBrowseVideoThumbnailsMakerPreset.UseVisualStyleBackColor = true;
			this.buttonBrowseVideoThumbnailsMakerPreset.Click += new System.EventHandler(this.buttonBrowseVideoThumbnailsMakerPreset_Click);
			// 
			// textBoxVideoThumbnailsMakerPreset
			// 
			this.textBoxVideoThumbnailsMakerPreset.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxVideoThumbnailsMakerPreset.Location = new System.Drawing.Point(172, 45);
			this.textBoxVideoThumbnailsMakerPreset.Name = "textBoxVideoThumbnailsMakerPreset";
			this.textBoxVideoThumbnailsMakerPreset.ReadOnly = true;
			this.textBoxVideoThumbnailsMakerPreset.Size = new System.Drawing.Size(359, 20);
			this.textBoxVideoThumbnailsMakerPreset.TabIndex = 4;
			// 
			// labelVideoThumbnailsMakerPreset
			// 
			this.labelVideoThumbnailsMakerPreset.AutoSize = true;
			this.labelVideoThumbnailsMakerPreset.Location = new System.Drawing.Point(7, 48);
			this.labelVideoThumbnailsMakerPreset.Name = "labelVideoThumbnailsMakerPreset";
			this.labelVideoThumbnailsMakerPreset.Size = new System.Drawing.Size(159, 13);
			this.labelVideoThumbnailsMakerPreset.TabIndex = 3;
			this.labelVideoThumbnailsMakerPreset.Text = "Video Thumbnails Maker preset:";
			// 
			// buttonBrowseVideoThumbnailsMaker
			// 
			this.buttonBrowseVideoThumbnailsMaker.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseVideoThumbnailsMaker.Location = new System.Drawing.Point(537, 17);
			this.buttonBrowseVideoThumbnailsMaker.Name = "buttonBrowseVideoThumbnailsMaker";
			this.buttonBrowseVideoThumbnailsMaker.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowseVideoThumbnailsMaker.TabIndex = 2;
			this.buttonBrowseVideoThumbnailsMaker.Text = "Browser...";
			this.buttonBrowseVideoThumbnailsMaker.UseVisualStyleBackColor = true;
			this.buttonBrowseVideoThumbnailsMaker.Click += new System.EventHandler(this.buttonBrowseVideoThumbnailsMaker_Click);
			// 
			// textBoxVideoThumbnailsMaker
			// 
			this.textBoxVideoThumbnailsMaker.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxVideoThumbnailsMaker.Location = new System.Drawing.Point(172, 19);
			this.textBoxVideoThumbnailsMaker.Name = "textBoxVideoThumbnailsMaker";
			this.textBoxVideoThumbnailsMaker.ReadOnly = true;
			this.textBoxVideoThumbnailsMaker.Size = new System.Drawing.Size(359, 20);
			this.textBoxVideoThumbnailsMaker.TabIndex = 1;
			// 
			// labelVideoThumbnailsMaker
			// 
			this.labelVideoThumbnailsMaker.AutoSize = true;
			this.labelVideoThumbnailsMaker.Location = new System.Drawing.Point(7, 22);
			this.labelVideoThumbnailsMaker.Name = "labelVideoThumbnailsMaker";
			this.labelVideoThumbnailsMaker.Size = new System.Drawing.Size(127, 13);
			this.labelVideoThumbnailsMaker.TabIndex = 0;
			this.labelVideoThumbnailsMaker.Text = "Video Thumbnails Maker:";
			// 
			// buttonBrowserWorkingDirectory
			// 
			this.buttonBrowserWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowserWorkingDirectory.Location = new System.Drawing.Point(537, 43);
			this.buttonBrowserWorkingDirectory.Name = "buttonBrowserWorkingDirectory";
			this.buttonBrowserWorkingDirectory.Size = new System.Drawing.Size(75, 23);
			this.buttonBrowserWorkingDirectory.TabIndex = 5;
			this.buttonBrowserWorkingDirectory.Text = "Browse...";
			this.buttonBrowserWorkingDirectory.UseVisualStyleBackColor = true;
			this.buttonBrowserWorkingDirectory.Click += new System.EventHandler(this.buttonBrowserWorkingDirectory_Click);
			// 
			// labelWorkingDirectory
			// 
			this.labelWorkingDirectory.AutoSize = true;
			this.labelWorkingDirectory.Location = new System.Drawing.Point(7, 48);
			this.labelWorkingDirectory.Name = "labelWorkingDirectory";
			this.labelWorkingDirectory.Size = new System.Drawing.Size(93, 13);
			this.labelWorkingDirectory.TabIndex = 4;
			this.labelWorkingDirectory.Text = "Working directory:";
			// 
			// textBoxWorkingDirectory
			// 
			this.textBoxWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxWorkingDirectory.Location = new System.Drawing.Point(109, 45);
			this.textBoxWorkingDirectory.Name = "textBoxWorkingDirectory";
			this.textBoxWorkingDirectory.ReadOnly = true;
			this.textBoxWorkingDirectory.Size = new System.Drawing.Size(422, 20);
			this.textBoxWorkingDirectory.TabIndex = 3;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(645, 550);
			this.Controls.Add(this.groupBoxThumbnails);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.groupBoxGallery);
			this.Controls.Add(this.groupBoxApplication);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Media Gallery Settings";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			this.groupBoxApplication.ResumeLayout(false);
			this.groupBoxApplication.PerformLayout();
			this.groupBoxGallery.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.groupBoxThumbnails.ResumeLayout(false);
			this.groupBoxThumbnails.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ListView listViewSources;
		private System.Windows.Forms.ColumnHeader columnHeaderSourcePath;
		private System.Windows.Forms.ColumnHeader columnHeaderImageCount;
		private System.Windows.Forms.ColumnHeader columnHeaderVideoCount;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.GroupBox groupBoxApplication;
		private System.Windows.Forms.GroupBox groupBoxGallery;
		private System.Windows.Forms.Label labelDatabaseLocation;
		private System.Windows.Forms.TextBox textBoxDatabaseLocation;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonBrowseDatabase;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.GroupBox groupBoxThumbnails;
		private System.Windows.Forms.Label labelVideoThumbnailsMaker;
		private System.Windows.Forms.Button buttonBrowseVideoThumbnailsMakerPreset;
		private System.Windows.Forms.TextBox textBoxVideoThumbnailsMakerPreset;
		private System.Windows.Forms.Label labelVideoThumbnailsMakerPreset;
		private System.Windows.Forms.Button buttonBrowseVideoThumbnailsMaker;
		private System.Windows.Forms.TextBox textBoxVideoThumbnailsMaker;
		private System.Windows.Forms.Button buttonBrowserWorkingDirectory;
		private System.Windows.Forms.Label labelWorkingDirectory;
		private System.Windows.Forms.TextBox textBoxWorkingDirectory;
	}
}