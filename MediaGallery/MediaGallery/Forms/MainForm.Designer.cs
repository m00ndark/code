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
			this.buttonScan = new System.Windows.Forms.Button();
			this.buttonSettings = new System.Windows.Forms.Button();
			this.comboBoxSources = new System.Windows.Forms.ComboBox();
			this.splitContainerVertical = new System.Windows.Forms.SplitContainer();
			this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.propertyGridMediaFile = new System.Windows.Forms.PropertyGrid();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.pictureBoxHeader = new System.Windows.Forms.PictureBox();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelHeader = new System.Windows.Forms.Panel();
			this.contextMenuStripThumbnail = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemThumbnailOpenImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemThumbnailOpenPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemThumbnailPlayVideo = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainerVertical.Panel1.SuspendLayout();
			this.splitContainerVertical.Panel2.SuspendLayout();
			this.splitContainerVertical.SuspendLayout();
			this.splitContainerHorizontal.Panel1.SuspendLayout();
			this.splitContainerHorizontal.Panel2.SuspendLayout();
			this.splitContainerHorizontal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxHeader)).BeginInit();
			this.statusStrip.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelHeader.SuspendLayout();
			this.contextMenuStripThumbnail.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonScan
			// 
			this.buttonScan.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonScan.Location = new System.Drawing.Point(1063, 39);
			this.buttonScan.Name = "buttonScan";
			this.buttonScan.Size = new System.Drawing.Size(75, 23);
			this.buttonScan.TabIndex = 1;
			this.buttonScan.Text = "Scan";
			this.buttonScan.UseVisualStyleBackColor = true;
			this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
			// 
			// buttonSettings
			// 
			this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonSettings.Location = new System.Drawing.Point(982, 39);
			this.buttonSettings.Name = "buttonSettings";
			this.buttonSettings.Size = new System.Drawing.Size(75, 23);
			this.buttonSettings.TabIndex = 2;
			this.buttonSettings.Text = "Settings";
			this.buttonSettings.UseVisualStyleBackColor = true;
			this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
			// 
			// comboBoxSources
			// 
			this.comboBoxSources.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxSources.BackColor = System.Drawing.Color.DimGray;
			this.comboBoxSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.comboBoxSources.ForeColor = System.Drawing.Color.White;
			this.comboBoxSources.FormattingEnabled = true;
			this.comboBoxSources.Location = new System.Drawing.Point(906, 12);
			this.comboBoxSources.Name = "comboBoxSources";
			this.comboBoxSources.Size = new System.Drawing.Size(232, 21);
			this.comboBoxSources.TabIndex = 3;
			this.comboBoxSources.SelectedIndexChanged += new System.EventHandler(this.comboBoxSources_SelectedIndexChanged);
			// 
			// splitContainerVertical
			// 
			this.splitContainerVertical.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerVertical.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainerVertical.Location = new System.Drawing.Point(0, 120);
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
			this.splitContainerVertical.Size = new System.Drawing.Size(1150, 694);
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
			this.splitContainerHorizontal.Size = new System.Drawing.Size(317, 694);
			this.splitContainerHorizontal.SplitterDistance = 456;
			this.splitContainerHorizontal.TabIndex = 1;
			// 
			// treeView
			// 
			this.treeView.BackColor = System.Drawing.Color.Black;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ForeColor = System.Drawing.Color.White;
			this.treeView.ImageKey = "folder-16";
			this.treeView.ImageList = this.imageList;
			this.treeView.LineColor = System.Drawing.Color.DimGray;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageKey = "folder-16";
			this.treeView.Size = new System.Drawing.Size(317, 456);
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
			this.propertyGridMediaFile.Size = new System.Drawing.Size(317, 234);
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
			this.flowLayoutPanel.Size = new System.Drawing.Size(829, 694);
			this.flowLayoutPanel.TabIndex = 0;
			// 
			// pictureBoxHeader
			// 
			this.pictureBoxHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxHeader.Image = global::MediaGallery.Properties.Resources.header;
			this.pictureBoxHeader.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxHeader.Name = "pictureBoxHeader";
			this.pictureBoxHeader.Size = new System.Drawing.Size(1150, 120);
			this.pictureBoxHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxHeader.TabIndex = 0;
			this.pictureBoxHeader.TabStop = false;
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.Color.DarkGray;
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
			this.statusStrip.Location = new System.Drawing.Point(0, 814);
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
			this.tableLayoutPanel.Controls.Add(this.panelHeader, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.statusStrip, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.splitContainerVertical, 0, 1);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 3;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(1150, 836);
			this.tableLayoutPanel.TabIndex = 6;
			// 
			// panelHeader
			// 
			this.panelHeader.Controls.Add(this.buttonSettings);
			this.panelHeader.Controls.Add(this.buttonScan);
			this.panelHeader.Controls.Add(this.comboBoxSources);
			this.panelHeader.Controls.Add(this.pictureBoxHeader);
			this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelHeader.Location = new System.Drawing.Point(0, 0);
			this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
			this.panelHeader.Name = "panelHeader";
			this.panelHeader.Size = new System.Drawing.Size(1150, 120);
			this.panelHeader.TabIndex = 7;
			// 
			// contextMenuStripThumbnail
			// 
			this.contextMenuStripThumbnail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemThumbnailOpenImage,
            this.toolStripMenuItemThumbnailOpenPreview,
            this.toolStripMenuItemThumbnailPlayVideo});
			this.contextMenuStripThumbnail.Name = "contextMenuStripThumbnail";
			this.contextMenuStripThumbnail.Size = new System.Drawing.Size(153, 92);
			// 
			// toolStripMenuItemThumbnailOpenImage
			// 
			this.toolStripMenuItemThumbnailOpenImage.Name = "toolStripMenuItemThumbnailOpenImage";
			this.toolStripMenuItemThumbnailOpenImage.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemThumbnailOpenImage.Text = "Open Image";
			this.toolStripMenuItemThumbnailOpenImage.Click += new System.EventHandler(this.toolStripMenuItemThumbnailOpenImage_Click);
			// 
			// toolStripMenuItemThumbnailOpenPreview
			// 
			this.toolStripMenuItemThumbnailOpenPreview.Name = "toolStripMenuItemThumbnailOpenPreview";
			this.toolStripMenuItemThumbnailOpenPreview.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemThumbnailOpenPreview.Text = "Open Preview";
			this.toolStripMenuItemThumbnailOpenPreview.Click += new System.EventHandler(this.toolStripMenuItemThumbnailOpenPreview_Click);
			// 
			// toolStripMenuItemThumbnailPlayVideo
			// 
			this.toolStripMenuItemThumbnailPlayVideo.Name = "toolStripMenuItemThumbnailPlayVideo";
			this.toolStripMenuItemThumbnailPlayVideo.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItemThumbnailPlayVideo.Text = "Play Video";
			this.toolStripMenuItemThumbnailPlayVideo.Click += new System.EventHandler(this.toolStripMenuItemThumbnailPlayVideo_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(1150, 836);
			this.Controls.Add(this.tableLayoutPanel);
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
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxHeader)).EndInit();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.panelHeader.ResumeLayout(false);
			this.contextMenuStripThumbnail.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxHeader;
		private System.Windows.Forms.Button buttonScan;
		private System.Windows.Forms.Button buttonSettings;
		private System.Windows.Forms.ComboBox comboBoxSources;
		private System.Windows.Forms.SplitContainer splitContainerVertical;
		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelHeader;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.SplitContainer splitContainerHorizontal;
		private System.Windows.Forms.PropertyGrid propertyGridMediaFile;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripThumbnail;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemThumbnailOpenImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemThumbnailOpenPreview;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemThumbnailPlayVideo;
	}
}

