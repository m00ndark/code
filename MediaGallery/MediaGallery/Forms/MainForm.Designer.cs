namespace MediaGallery.Forms
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
			this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.propertyGridMediaFile = new System.Windows.Forms.PropertyGrid();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.contextMenuStripThumbnail = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemThumbnailOpenImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemThumbnailOpenPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemThumbnailPlayVideo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripComboBoxSource = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripDropDownButtonSource = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripMenuItemSourceRescan = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSourceUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.splitContainerVertical.Panel1.SuspendLayout();
			this.splitContainerVertical.Panel2.SuspendLayout();
			this.splitContainerVertical.SuspendLayout();
			this.splitContainerHorizontal.Panel1.SuspendLayout();
			this.splitContainerHorizontal.Panel2.SuspendLayout();
			this.splitContainerHorizontal.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.contextMenuStripThumbnail.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerVertical
			// 
			this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerVertical.Location = new System.Drawing.Point(0, 0);
			this.splitContainerVertical.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainerVertical.Name = "splitContainerVertical";
			// 
			// splitContainerVertical.Panel1
			// 
			this.splitContainerVertical.Panel1.Controls.Add(this.splitContainerHorizontal);
			// 
			// splitContainerVertical.Panel2
			// 
			this.splitContainerVertical.Panel2.Controls.Add(this.flowLayoutPanel);
			this.splitContainerVertical.Size = new System.Drawing.Size(1150, 789);
			this.splitContainerVertical.SplitterDistance = 317;
			this.splitContainerVertical.TabIndex = 4;
			// 
			// splitContainerHorizontal
			// 
			this.splitContainerHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerHorizontal.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainerHorizontal.Location = new System.Drawing.Point(0, 0);
			this.splitContainerHorizontal.Name = "splitContainerHorizontal";
			this.splitContainerHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerHorizontal.Panel1
			// 
			this.splitContainerHorizontal.Panel1.Controls.Add(this.treeView);
			// 
			// splitContainerHorizontal.Panel2
			// 
			this.splitContainerHorizontal.Panel2.Controls.Add(this.propertyGridMediaFile);
			this.splitContainerHorizontal.Size = new System.Drawing.Size(317, 789);
			this.splitContainerHorizontal.SplitterDistance = 509;
			this.splitContainerHorizontal.TabIndex = 1;
			// 
			// treeView
			// 
			this.treeView.BackColor = System.Drawing.Color.Black;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ForeColor = System.Drawing.Color.White;
			this.treeView.HideSelection = false;
			this.treeView.ImageKey = "folder-16";
			this.treeView.ImageList = this.imageList;
			this.treeView.LineColor = System.Drawing.Color.DimGray;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageKey = "folder-16";
			this.treeView.Size = new System.Drawing.Size(317, 509);
			this.treeView.TabIndex = 0;
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "folder-16");
			// 
			// propertyGridMediaFile
			// 
			this.propertyGridMediaFile.BackColor = System.Drawing.Color.Black;
			this.propertyGridMediaFile.CategoryForeColor = System.Drawing.Color.Silver;
			this.propertyGridMediaFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridMediaFile.HelpBackColor = System.Drawing.Color.Black;
			this.propertyGridMediaFile.HelpVisible = false;
			this.propertyGridMediaFile.LineColor = System.Drawing.Color.FromArgb(((int) (((byte) (64)))), ((int) (((byte) (64)))), ((int) (((byte) (64)))));
			this.propertyGridMediaFile.Location = new System.Drawing.Point(0, 0);
			this.propertyGridMediaFile.Name = "propertyGridMediaFile";
			this.propertyGridMediaFile.Size = new System.Drawing.Size(317, 276);
			this.propertyGridMediaFile.TabIndex = 0;
			this.propertyGridMediaFile.ToolbarVisible = false;
			this.propertyGridMediaFile.ViewBackColor = System.Drawing.Color.Black;
			this.propertyGridMediaFile.ViewForeColor = System.Drawing.Color.White;
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.AutoScroll = true;
			this.flowLayoutPanel.BackColor = System.Drawing.Color.Black;
			this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(829, 789);
			this.flowLayoutPanel.TabIndex = 0;
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.Color.DarkGray;
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
			this.statusStrip.Location = new System.Drawing.Point(0, 789);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(1150, 22);
			this.statusStrip.TabIndex = 5;
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(39, 17);
			this.toolStripStatusLabelStatus.Text = "Ready";
			this.toolStripStatusLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Controls.Add(this.statusStrip, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.splitContainerVertical, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(1150, 811);
			this.tableLayoutPanel.TabIndex = 6;
			// 
			// contextMenuStripThumbnail
			// 
			this.contextMenuStripThumbnail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemThumbnailOpenImage,
            this.toolStripMenuItemThumbnailOpenPreview,
            this.toolStripMenuItemThumbnailPlayVideo});
			this.contextMenuStripThumbnail.Name = "contextMenuStripThumbnail";
			this.contextMenuStripThumbnail.Size = new System.Drawing.Size(148, 70);
			// 
			// toolStripMenuItemThumbnailOpenImage
			// 
			this.toolStripMenuItemThumbnailOpenImage.Name = "toolStripMenuItemThumbnailOpenImage";
			this.toolStripMenuItemThumbnailOpenImage.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemThumbnailOpenImage.Text = "Open Image";
			this.toolStripMenuItemThumbnailOpenImage.Click += new System.EventHandler(this.toolStripMenuItemThumbnailOpenImage_Click);
			// 
			// toolStripMenuItemThumbnailOpenPreview
			// 
			this.toolStripMenuItemThumbnailOpenPreview.Name = "toolStripMenuItemThumbnailOpenPreview";
			this.toolStripMenuItemThumbnailOpenPreview.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemThumbnailOpenPreview.Text = "Open Preview";
			this.toolStripMenuItemThumbnailOpenPreview.Click += new System.EventHandler(this.toolStripMenuItemThumbnailOpenPreview_Click);
			// 
			// toolStripMenuItemThumbnailPlayVideo
			// 
			this.toolStripMenuItemThumbnailPlayVideo.Name = "toolStripMenuItemThumbnailPlayVideo";
			this.toolStripMenuItemThumbnailPlayVideo.Size = new System.Drawing.Size(147, 22);
			this.toolStripMenuItemThumbnailPlayVideo.Text = "Play Video";
			this.toolStripMenuItemThumbnailPlayVideo.Click += new System.EventHandler(this.toolStripMenuItemThumbnailPlayVideo_Click);
			// 
			// toolStrip
			// 
			this.toolStrip.BackColor = System.Drawing.Color.DarkGray;
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSettings,
            this.toolStripSeparator1,
            this.toolStripComboBoxSource,
            this.toolStripDropDownButtonSource});
			this.toolStrip.Location = new System.Drawing.Point(3, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(361, 25);
			this.toolStrip.TabIndex = 4;
			this.toolStrip.Text = "toolStrip";
			// 
			// toolStripButtonSettings
			// 
			this.toolStripButtonSettings.AutoToolTip = false;
			this.toolStripButtonSettings.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonSettings.Image")));
			this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonSettings.Name = "toolStripButtonSettings";
			this.toolStripButtonSettings.Size = new System.Drawing.Size(69, 22);
			this.toolStripButtonSettings.Text = "Settings";
			this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripComboBoxSource
			// 
			this.toolStripComboBoxSource.BackColor = System.Drawing.Color.DimGray;
			this.toolStripComboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolStripComboBoxSource.DropDownWidth = 200;
			this.toolStripComboBoxSource.Name = "toolStripComboBoxSource";
			this.toolStripComboBoxSource.Size = new System.Drawing.Size(200, 25);
			this.toolStripComboBoxSource.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSource_SelectedIndexChanged);
			// 
			// toolStripDropDownButtonSource
			// 
			this.toolStripDropDownButtonSource.AutoToolTip = false;
			this.toolStripDropDownButtonSource.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSourceRescan,
            this.toolStripMenuItemSourceUpdate});
			this.toolStripDropDownButtonSource.Image = ((System.Drawing.Image) (resources.GetObject("toolStripDropDownButtonSource.Image")));
			this.toolStripDropDownButtonSource.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButtonSource.Name = "toolStripDropDownButtonSource";
			this.toolStripDropDownButtonSource.Size = new System.Drawing.Size(72, 22);
			this.toolStripDropDownButtonSource.Text = "Source";
			// 
			// toolStripMenuItemSourceRescan
			// 
			this.toolStripMenuItemSourceRescan.Name = "toolStripMenuItemSourceRescan";
			this.toolStripMenuItemSourceRescan.Size = new System.Drawing.Size(116, 22);
			this.toolStripMenuItemSourceRescan.Text = "Re-scan";
			this.toolStripMenuItemSourceRescan.Click += new System.EventHandler(this.toolStripMenuItemSourceRescan_Click);
			// 
			// toolStripMenuItemSourceUpdate
			// 
			this.toolStripMenuItemSourceUpdate.Name = "toolStripMenuItemSourceUpdate";
			this.toolStripMenuItemSourceUpdate.Size = new System.Drawing.Size(116, 22);
			this.toolStripMenuItemSourceUpdate.Text = "Update";
			this.toolStripMenuItemSourceUpdate.Click += new System.EventHandler(this.toolStripMenuItemSourceUpdate_Click);
			// 
			// toolStripContainer
			// 
			this.toolStripContainer.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.tableLayoutPanel);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1150, 811);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.LeftToolStripPanelVisible = false;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.RightToolStripPanelVisible = false;
			this.toolStripContainer.Size = new System.Drawing.Size(1150, 836);
			this.toolStripContainer.TabIndex = 6;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.BackColor = System.Drawing.Color.DarkGray;
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1150, 836);
			this.Controls.Add(this.toolStripContainer);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Media Gallery";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.splitContainerVertical.Panel1.ResumeLayout(false);
			this.splitContainerVertical.Panel2.ResumeLayout(false);
			this.splitContainerVertical.ResumeLayout(false);
			this.splitContainerHorizontal.Panel1.ResumeLayout(false);
			this.splitContainerHorizontal.Panel2.ResumeLayout(false);
			this.splitContainerHorizontal.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.contextMenuStripThumbnail.ResumeLayout(false);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainerVertical;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.SplitContainer splitContainerHorizontal;
		private System.Windows.Forms.PropertyGrid propertyGridMediaFile;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripThumbnail;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemThumbnailOpenImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemThumbnailOpenPreview;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemThumbnailPlayVideo;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSource;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSource;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSourceRescan;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSourceUpdate;
		private System.Windows.Forms.ToolStripContainer toolStripContainer;
	}
}

