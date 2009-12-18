namespace UnpakkDaemonTray.Forms
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageProgress = new System.Windows.Forms.TabPage();
			this.labelSubProgress = new System.Windows.Forms.Label();
			this.labelMainProgress = new System.Windows.Forms.Label();
			this.labelSubMessage = new System.Windows.Forms.Label();
			this.labelMainMessage = new System.Windows.Forms.Label();
			this.progressBarMainProgress = new System.Windows.Forms.ProgressBar();
			this.progressBarSubProgress = new System.Windows.Forms.ProgressBar();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.groupBoxScanSettings = new System.Windows.Forms.GroupBox();
			this.buttonRemoveRootPath = new System.Windows.Forms.Button();
			this.buttonAddRootPath = new System.Windows.Forms.Button();
			this.listViewRootPath = new System.Windows.Forms.ListView();
			this.columnHeaderRootPath = new System.Windows.Forms.ColumnHeader();
			this.groupBoxApplicationSettings = new System.Windows.Forms.GroupBox();
			this.buttonBrowseApplicationDataFolder = new System.Windows.Forms.Button();
			this.labelSleepTimeMinutes = new System.Windows.Forms.Label();
			this.textBoxSleepTime = new System.Windows.Forms.TextBox();
			this.labelSleepTime = new System.Windows.Forms.Label();
			this.textBoxApplicationDataFolder = new System.Windows.Forms.TextBox();
			this.labelApplicationDataFolder = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemRestore = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.treeViewRecords = new System.Windows.Forms.TreeView();
			this.labelRecordSFVFile = new System.Windows.Forms.Label();
			this.textBoxRecordPath = new System.Windows.Forms.TextBox();
			this.textBoxRecordSFVFile = new System.Windows.Forms.TextBox();
			this.labelRecordPath = new System.Windows.Forms.Label();
			this.groupBoxRecordDetails = new System.Windows.Forms.GroupBox();
			this.textBoxRecordRARFile = new System.Windows.Forms.TextBox();
			this.labelRecordRARFile = new System.Windows.Forms.Label();
			this.textBoxRecordRARParts = new System.Windows.Forms.TextBox();
			this.labelRecordRARParts = new System.Windows.Forms.Label();
			this.textBoxRecordRARSize = new System.Windows.Forms.TextBox();
			this.labelRecordRARSize = new System.Windows.Forms.Label();
			this.linkLabelRecordOpenFolder = new System.Windows.Forms.LinkLabel();
			this.groupBoxSubRecordDetails = new System.Windows.Forms.GroupBox();
			this.linkLabelSubRecordOpenFolder = new System.Windows.Forms.LinkLabel();
			this.textBoxSubRecordFileSize = new System.Windows.Forms.TextBox();
			this.labelSubRecordFileSize = new System.Windows.Forms.Label();
			this.textBoxSubRecordPath = new System.Windows.Forms.TextBox();
			this.textBoxSubRecordFile = new System.Windows.Forms.TextBox();
			this.labelSubRecordFile = new System.Windows.Forms.Label();
			this.labelSubRecordPath = new System.Windows.Forms.Label();
			this.linkLabelOpenFile = new System.Windows.Forms.LinkLabel();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.listViewLog = new System.Windows.Forms.ListView();
			this.columnHeaderTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderEntry = new System.Windows.Forms.ColumnHeader();
			this.tabControl.SuspendLayout();
			this.tabPageProgress.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.groupBoxScanSettings.SuspendLayout();
			this.groupBoxApplicationSettings.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.groupBoxRecordDetails.SuspendLayout();
			this.groupBoxSubRecordDetails.SuspendLayout();
			this.tabPageLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageProgress);
			this.tabControl.Controls.Add(this.tabPageLog);
			this.tabControl.Controls.Add(this.tabPageSettings);
			this.tabControl.Location = new System.Drawing.Point(9, 9);
			this.tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(799, 396);
			this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tabControl.TabIndex = 0;
			this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
			// 
			// tabPageProgress
			// 
			this.tabPageProgress.Controls.Add(this.groupBoxRecordDetails);
			this.tabPageProgress.Controls.Add(this.treeViewRecords);
			this.tabPageProgress.Controls.Add(this.labelSubProgress);
			this.tabPageProgress.Controls.Add(this.labelMainProgress);
			this.tabPageProgress.Controls.Add(this.labelSubMessage);
			this.tabPageProgress.Controls.Add(this.labelMainMessage);
			this.tabPageProgress.Controls.Add(this.progressBarMainProgress);
			this.tabPageProgress.Controls.Add(this.progressBarSubProgress);
			this.tabPageProgress.Controls.Add(this.groupBoxSubRecordDetails);
			this.tabPageProgress.Location = new System.Drawing.Point(4, 22);
			this.tabPageProgress.Name = "tabPageProgress";
			this.tabPageProgress.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProgress.Size = new System.Drawing.Size(791, 370);
			this.tabPageProgress.TabIndex = 0;
			this.tabPageProgress.Text = "Progress";
			this.tabPageProgress.UseVisualStyleBackColor = true;
			// 
			// labelSubProgress
			// 
			this.labelSubProgress.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSubProgress.Location = new System.Drawing.Point(665, 62);
			this.labelSubProgress.Name = "labelSubProgress";
			this.labelSubProgress.Size = new System.Drawing.Size(120, 13);
			this.labelSubProgress.TabIndex = 4;
			this.labelSubProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelMainProgress
			// 
			this.labelMainProgress.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMainProgress.Location = new System.Drawing.Point(665, 24);
			this.labelMainProgress.Name = "labelMainProgress";
			this.labelMainProgress.Size = new System.Drawing.Size(120, 13);
			this.labelMainProgress.TabIndex = 5;
			this.labelMainProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelSubMessage
			// 
			this.labelSubMessage.AutoSize = true;
			this.labelSubMessage.Location = new System.Drawing.Point(6, 62);
			this.labelSubMessage.Name = "labelSubMessage";
			this.labelSubMessage.Size = new System.Drawing.Size(0, 13);
			this.labelSubMessage.TabIndex = 4;
			// 
			// labelMainMessage
			// 
			this.labelMainMessage.AutoSize = true;
			this.labelMainMessage.Location = new System.Drawing.Point(6, 24);
			this.labelMainMessage.Name = "labelMainMessage";
			this.labelMainMessage.Size = new System.Drawing.Size(0, 13);
			this.labelMainMessage.TabIndex = 3;
			// 
			// progressBarMainProgress
			// 
			this.progressBarMainProgress.Location = new System.Drawing.Point(6, 6);
			this.progressBarMainProgress.Name = "progressBarMainProgress";
			this.progressBarMainProgress.Size = new System.Drawing.Size(779, 15);
			this.progressBarMainProgress.TabIndex = 2;
			// 
			// progressBarSubProgress
			// 
			this.progressBarSubProgress.Location = new System.Drawing.Point(6, 44);
			this.progressBarSubProgress.Name = "progressBarSubProgress";
			this.progressBarSubProgress.Size = new System.Drawing.Size(779, 15);
			this.progressBarSubProgress.TabIndex = 1;
			// 
			// tabPageSettings
			// 
			this.tabPageSettings.Controls.Add(this.groupBoxScanSettings);
			this.tabPageSettings.Controls.Add(this.groupBoxApplicationSettings);
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSettings.Size = new System.Drawing.Size(791, 370);
			this.tabPageSettings.TabIndex = 1;
			this.tabPageSettings.Text = "Settings";
			this.tabPageSettings.UseVisualStyleBackColor = true;
			// 
			// groupBoxScanSettings
			// 
			this.groupBoxScanSettings.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxScanSettings.Controls.Add(this.buttonRemoveRootPath);
			this.groupBoxScanSettings.Controls.Add(this.buttonAddRootPath);
			this.groupBoxScanSettings.Controls.Add(this.listViewRootPath);
			this.groupBoxScanSettings.Location = new System.Drawing.Point(3, 143);
			this.groupBoxScanSettings.Name = "groupBoxScanSettings";
			this.groupBoxScanSettings.Size = new System.Drawing.Size(782, 221);
			this.groupBoxScanSettings.TabIndex = 3;
			this.groupBoxScanSettings.TabStop = false;
			this.groupBoxScanSettings.Text = "Scan Settings";
			// 
			// buttonRemoveRootPath
			// 
			this.buttonRemoveRootPath.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemoveRootPath.Location = new System.Drawing.Point(701, 48);
			this.buttonRemoveRootPath.Name = "buttonRemoveRootPath";
			this.buttonRemoveRootPath.Size = new System.Drawing.Size(75, 23);
			this.buttonRemoveRootPath.TabIndex = 2;
			this.buttonRemoveRootPath.Text = "Remove";
			this.buttonRemoveRootPath.UseVisualStyleBackColor = true;
			this.buttonRemoveRootPath.Click += new System.EventHandler(this.buttonRemoveRootPath_Click);
			// 
			// buttonAddRootPath
			// 
			this.buttonAddRootPath.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddRootPath.Location = new System.Drawing.Point(701, 19);
			this.buttonAddRootPath.Name = "buttonAddRootPath";
			this.buttonAddRootPath.Size = new System.Drawing.Size(75, 23);
			this.buttonAddRootPath.TabIndex = 1;
			this.buttonAddRootPath.Text = "Add";
			this.buttonAddRootPath.UseVisualStyleBackColor = true;
			this.buttonAddRootPath.Click += new System.EventHandler(this.buttonAddRootPath_Click);
			// 
			// listViewRootPath
			// 
			this.listViewRootPath.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewRootPath.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRootPath});
			this.listViewRootPath.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewRootPath.HideSelection = false;
			this.listViewRootPath.Location = new System.Drawing.Point(6, 19);
			this.listViewRootPath.Name = "listViewRootPath";
			this.listViewRootPath.Size = new System.Drawing.Size(689, 196);
			this.listViewRootPath.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewRootPath.TabIndex = 0;
			this.listViewRootPath.UseCompatibleStateImageBehavior = false;
			this.listViewRootPath.View = System.Windows.Forms.View.Details;
			this.listViewRootPath.SelectedIndexChanged += new System.EventHandler(this.listViewRootPath_SelectedIndexChanged);
			// 
			// columnHeaderRootPath
			// 
			this.columnHeaderRootPath.Text = "Root Path";
			this.columnHeaderRootPath.Width = 665;
			// 
			// groupBoxApplicationSettings
			// 
			this.groupBoxApplicationSettings.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxApplicationSettings.Controls.Add(this.buttonBrowseApplicationDataFolder);
			this.groupBoxApplicationSettings.Controls.Add(this.labelSleepTimeMinutes);
			this.groupBoxApplicationSettings.Controls.Add(this.textBoxSleepTime);
			this.groupBoxApplicationSettings.Controls.Add(this.labelSleepTime);
			this.groupBoxApplicationSettings.Controls.Add(this.textBoxApplicationDataFolder);
			this.groupBoxApplicationSettings.Controls.Add(this.labelApplicationDataFolder);
			this.groupBoxApplicationSettings.Location = new System.Drawing.Point(6, 6);
			this.groupBoxApplicationSettings.Name = "groupBoxApplicationSettings";
			this.groupBoxApplicationSettings.Size = new System.Drawing.Size(779, 131);
			this.groupBoxApplicationSettings.TabIndex = 2;
			this.groupBoxApplicationSettings.TabStop = false;
			this.groupBoxApplicationSettings.Text = "Application Settings";
			// 
			// buttonBrowseApplicationDataFolder
			// 
			this.buttonBrowseApplicationDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseApplicationDataFolder.Location = new System.Drawing.Point(741, 17);
			this.buttonBrowseApplicationDataFolder.Name = "buttonBrowseApplicationDataFolder";
			this.buttonBrowseApplicationDataFolder.Size = new System.Drawing.Size(32, 23);
			this.buttonBrowseApplicationDataFolder.TabIndex = 5;
			this.buttonBrowseApplicationDataFolder.Text = "...";
			this.buttonBrowseApplicationDataFolder.UseVisualStyleBackColor = true;
			this.buttonBrowseApplicationDataFolder.Click += new System.EventHandler(this.buttonBrowseApplicationDataFolder_Click);
			// 
			// labelSleepTimeMinutes
			// 
			this.labelSleepTimeMinutes.AutoSize = true;
			this.labelSleepTimeMinutes.Location = new System.Drawing.Point(209, 48);
			this.labelSleepTimeMinutes.Name = "labelSleepTimeMinutes";
			this.labelSleepTimeMinutes.Size = new System.Drawing.Size(43, 13);
			this.labelSleepTimeMinutes.TabIndex = 4;
			this.labelSleepTimeMinutes.Text = "minutes";
			// 
			// textBoxSleepTime
			// 
			this.textBoxSleepTime.Location = new System.Drawing.Point(127, 45);
			this.textBoxSleepTime.Name = "textBoxSleepTime";
			this.textBoxSleepTime.Size = new System.Drawing.Size(76, 20);
			this.textBoxSleepTime.TabIndex = 2;
			// 
			// labelSleepTime
			// 
			this.labelSleepTime.AutoSize = true;
			this.labelSleepTime.Location = new System.Drawing.Point(6, 48);
			this.labelSleepTime.Name = "labelSleepTime";
			this.labelSleepTime.Size = new System.Drawing.Size(59, 13);
			this.labelSleepTime.TabIndex = 3;
			this.labelSleepTime.Text = "Sleep time:";
			// 
			// textBoxApplicationDataFolder
			// 
			this.textBoxApplicationDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxApplicationDataFolder.Location = new System.Drawing.Point(127, 19);
			this.textBoxApplicationDataFolder.Name = "textBoxApplicationDataFolder";
			this.textBoxApplicationDataFolder.Size = new System.Drawing.Size(608, 20);
			this.textBoxApplicationDataFolder.TabIndex = 0;
			// 
			// labelApplicationDataFolder
			// 
			this.labelApplicationDataFolder.AutoSize = true;
			this.labelApplicationDataFolder.Location = new System.Drawing.Point(6, 22);
			this.labelApplicationDataFolder.Name = "labelApplicationDataFolder";
			this.labelApplicationDataFolder.Size = new System.Drawing.Size(115, 13);
			this.labelApplicationDataFolder.TabIndex = 1;
			this.labelApplicationDataFolder.Text = "Application data folder:";
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon) (resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Unpakk Daemon";
			this.notifyIcon.Visible = true;
			this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRestore,
            this.toolStripMenuItemClose});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(119, 48);
			// 
			// toolStripMenuItemRestore
			// 
			this.toolStripMenuItemRestore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripMenuItemRestore.Name = "toolStripMenuItemRestore";
			this.toolStripMenuItemRestore.Size = new System.Drawing.Size(118, 22);
			this.toolStripMenuItemRestore.Text = "Restore";
			this.toolStripMenuItemRestore.Click += new System.EventHandler(this.toolStripMenuItemRestore_Click);
			// 
			// toolStripMenuItemClose
			// 
			this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
			this.toolStripMenuItemClose.Size = new System.Drawing.Size(118, 22);
			this.toolStripMenuItemClose.Text = "Close";
			this.toolStripMenuItemClose.Click += new System.EventHandler(this.toolStripMenuItemClose_Click);
			// 
			// treeViewRecords
			// 
			this.treeViewRecords.Location = new System.Drawing.Point(6, 87);
			this.treeViewRecords.Name = "treeViewRecords";
			this.treeViewRecords.Size = new System.Drawing.Size(336, 277);
			this.treeViewRecords.TabIndex = 6;
			// 
			// labelRecordSFVFile
			// 
			this.labelRecordSFVFile.AutoSize = true;
			this.labelRecordSFVFile.Location = new System.Drawing.Point(6, 48);
			this.labelRecordSFVFile.Name = "labelRecordSFVFile";
			this.labelRecordSFVFile.Size = new System.Drawing.Size(46, 13);
			this.labelRecordSFVFile.TabIndex = 7;
			this.labelRecordSFVFile.Text = "SFV file:";
			// 
			// textBoxRecordPath
			// 
			this.textBoxRecordPath.BackColor = System.Drawing.Color.White;
			this.textBoxRecordPath.Location = new System.Drawing.Point(93, 19);
			this.textBoxRecordPath.Name = "textBoxRecordPath";
			this.textBoxRecordPath.ReadOnly = true;
			this.textBoxRecordPath.Size = new System.Drawing.Size(335, 20);
			this.textBoxRecordPath.TabIndex = 8;
			// 
			// textBoxRecordSFVFile
			// 
			this.textBoxRecordSFVFile.BackColor = System.Drawing.Color.White;
			this.textBoxRecordSFVFile.Location = new System.Drawing.Point(93, 45);
			this.textBoxRecordSFVFile.Name = "textBoxRecordSFVFile";
			this.textBoxRecordSFVFile.ReadOnly = true;
			this.textBoxRecordSFVFile.Size = new System.Drawing.Size(335, 20);
			this.textBoxRecordSFVFile.TabIndex = 10;
			// 
			// labelRecordPath
			// 
			this.labelRecordPath.AutoSize = true;
			this.labelRecordPath.Location = new System.Drawing.Point(6, 22);
			this.labelRecordPath.Name = "labelRecordPath";
			this.labelRecordPath.Size = new System.Drawing.Size(32, 13);
			this.labelRecordPath.TabIndex = 9;
			this.labelRecordPath.Text = "Path:";
			// 
			// groupBoxRecordDetails
			// 
			this.groupBoxRecordDetails.Controls.Add(this.linkLabelRecordOpenFolder);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordRARSize);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARSize);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordRARParts);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARParts);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordRARFile);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARFile);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordPath);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordSFVFile);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordSFVFile);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordPath);
			this.groupBoxRecordDetails.Location = new System.Drawing.Point(348, 81);
			this.groupBoxRecordDetails.Name = "groupBoxRecordDetails";
			this.groupBoxRecordDetails.Size = new System.Drawing.Size(437, 284);
			this.groupBoxRecordDetails.TabIndex = 11;
			this.groupBoxRecordDetails.TabStop = false;
			this.groupBoxRecordDetails.Text = "Details";
			// 
			// textBoxRecordRARFile
			// 
			this.textBoxRecordRARFile.BackColor = System.Drawing.Color.White;
			this.textBoxRecordRARFile.Location = new System.Drawing.Point(93, 71);
			this.textBoxRecordRARFile.Name = "textBoxRecordRARFile";
			this.textBoxRecordRARFile.ReadOnly = true;
			this.textBoxRecordRARFile.Size = new System.Drawing.Size(335, 20);
			this.textBoxRecordRARFile.TabIndex = 12;
			// 
			// labelRecordRARFile
			// 
			this.labelRecordRARFile.AutoSize = true;
			this.labelRecordRARFile.Location = new System.Drawing.Point(6, 74);
			this.labelRecordRARFile.Name = "labelRecordRARFile";
			this.labelRecordRARFile.Size = new System.Drawing.Size(49, 13);
			this.labelRecordRARFile.TabIndex = 11;
			this.labelRecordRARFile.Text = "RAR file:";
			// 
			// textBoxRecordRARParts
			// 
			this.textBoxRecordRARParts.BackColor = System.Drawing.Color.White;
			this.textBoxRecordRARParts.Location = new System.Drawing.Point(93, 97);
			this.textBoxRecordRARParts.Name = "textBoxRecordRARParts";
			this.textBoxRecordRARParts.ReadOnly = true;
			this.textBoxRecordRARParts.Size = new System.Drawing.Size(335, 20);
			this.textBoxRecordRARParts.TabIndex = 14;
			// 
			// labelRecordRARParts
			// 
			this.labelRecordRARParts.AutoSize = true;
			this.labelRecordRARParts.Location = new System.Drawing.Point(6, 100);
			this.labelRecordRARParts.Name = "labelRecordRARParts";
			this.labelRecordRARParts.Size = new System.Drawing.Size(59, 13);
			this.labelRecordRARParts.TabIndex = 13;
			this.labelRecordRARParts.Text = "RAR parts:";
			// 
			// textBoxRecordRARSize
			// 
			this.textBoxRecordRARSize.BackColor = System.Drawing.Color.White;
			this.textBoxRecordRARSize.Location = new System.Drawing.Point(93, 123);
			this.textBoxRecordRARSize.Name = "textBoxRecordRARSize";
			this.textBoxRecordRARSize.ReadOnly = true;
			this.textBoxRecordRARSize.Size = new System.Drawing.Size(335, 20);
			this.textBoxRecordRARSize.TabIndex = 16;
			// 
			// labelRecordRARSize
			// 
			this.labelRecordRARSize.AutoSize = true;
			this.labelRecordRARSize.Location = new System.Drawing.Point(6, 126);
			this.labelRecordRARSize.Name = "labelRecordRARSize";
			this.labelRecordRARSize.Size = new System.Drawing.Size(54, 13);
			this.labelRecordRARSize.TabIndex = 15;
			this.labelRecordRARSize.Text = "RAR size:";
			// 
			// linkLabelRecordOpenFolder
			// 
			this.linkLabelRecordOpenFolder.AutoSize = true;
			this.linkLabelRecordOpenFolder.Location = new System.Drawing.Point(366, 260);
			this.linkLabelRecordOpenFolder.Name = "linkLabelRecordOpenFolder";
			this.linkLabelRecordOpenFolder.Size = new System.Drawing.Size(65, 13);
			this.linkLabelRecordOpenFolder.TabIndex = 17;
			this.linkLabelRecordOpenFolder.TabStop = true;
			this.linkLabelRecordOpenFolder.Text = "Open Folder";
			// 
			// groupBoxSubRecordDetails
			// 
			this.groupBoxSubRecordDetails.Controls.Add(this.linkLabelOpenFile);
			this.groupBoxSubRecordDetails.Controls.Add(this.linkLabelSubRecordOpenFolder);
			this.groupBoxSubRecordDetails.Controls.Add(this.textBoxSubRecordFileSize);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordFileSize);
			this.groupBoxSubRecordDetails.Controls.Add(this.textBoxSubRecordPath);
			this.groupBoxSubRecordDetails.Controls.Add(this.textBoxSubRecordFile);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordFile);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordPath);
			this.groupBoxSubRecordDetails.Location = new System.Drawing.Point(348, 81);
			this.groupBoxSubRecordDetails.Name = "groupBoxSubRecordDetails";
			this.groupBoxSubRecordDetails.Size = new System.Drawing.Size(437, 284);
			this.groupBoxSubRecordDetails.TabIndex = 18;
			this.groupBoxSubRecordDetails.TabStop = false;
			this.groupBoxSubRecordDetails.Text = "Details";
			// 
			// linkLabelSubRecordOpenFolder
			// 
			this.linkLabelSubRecordOpenFolder.AutoSize = true;
			this.linkLabelSubRecordOpenFolder.Location = new System.Drawing.Point(366, 260);
			this.linkLabelSubRecordOpenFolder.Name = "linkLabelSubRecordOpenFolder";
			this.linkLabelSubRecordOpenFolder.Size = new System.Drawing.Size(65, 13);
			this.linkLabelSubRecordOpenFolder.TabIndex = 17;
			this.linkLabelSubRecordOpenFolder.TabStop = true;
			this.linkLabelSubRecordOpenFolder.Text = "Open Folder";
			// 
			// textBoxSubRecordFileSize
			// 
			this.textBoxSubRecordFileSize.BackColor = System.Drawing.Color.White;
			this.textBoxSubRecordFileSize.Location = new System.Drawing.Point(93, 71);
			this.textBoxSubRecordFileSize.Name = "textBoxSubRecordFileSize";
			this.textBoxSubRecordFileSize.ReadOnly = true;
			this.textBoxSubRecordFileSize.Size = new System.Drawing.Size(335, 20);
			this.textBoxSubRecordFileSize.TabIndex = 12;
			// 
			// labelSubRecordFileSize
			// 
			this.labelSubRecordFileSize.AutoSize = true;
			this.labelSubRecordFileSize.Location = new System.Drawing.Point(6, 74);
			this.labelSubRecordFileSize.Name = "labelSubRecordFileSize";
			this.labelSubRecordFileSize.Size = new System.Drawing.Size(47, 13);
			this.labelSubRecordFileSize.TabIndex = 11;
			this.labelSubRecordFileSize.Text = "File size:";
			// 
			// textBoxSubRecordPath
			// 
			this.textBoxSubRecordPath.BackColor = System.Drawing.Color.White;
			this.textBoxSubRecordPath.Location = new System.Drawing.Point(93, 19);
			this.textBoxSubRecordPath.Name = "textBoxSubRecordPath";
			this.textBoxSubRecordPath.ReadOnly = true;
			this.textBoxSubRecordPath.Size = new System.Drawing.Size(335, 20);
			this.textBoxSubRecordPath.TabIndex = 8;
			// 
			// textBoxSubRecordFile
			// 
			this.textBoxSubRecordFile.BackColor = System.Drawing.Color.White;
			this.textBoxSubRecordFile.Location = new System.Drawing.Point(93, 45);
			this.textBoxSubRecordFile.Name = "textBoxSubRecordFile";
			this.textBoxSubRecordFile.ReadOnly = true;
			this.textBoxSubRecordFile.Size = new System.Drawing.Size(335, 20);
			this.textBoxSubRecordFile.TabIndex = 10;
			// 
			// labelSubRecordFile
			// 
			this.labelSubRecordFile.AutoSize = true;
			this.labelSubRecordFile.Location = new System.Drawing.Point(6, 48);
			this.labelSubRecordFile.Name = "labelSubRecordFile";
			this.labelSubRecordFile.Size = new System.Drawing.Size(26, 13);
			this.labelSubRecordFile.TabIndex = 7;
			this.labelSubRecordFile.Text = "File:";
			// 
			// labelSubRecordPath
			// 
			this.labelSubRecordPath.AutoSize = true;
			this.labelSubRecordPath.Location = new System.Drawing.Point(6, 22);
			this.labelSubRecordPath.Name = "labelSubRecordPath";
			this.labelSubRecordPath.Size = new System.Drawing.Size(32, 13);
			this.labelSubRecordPath.TabIndex = 9;
			this.labelSubRecordPath.Text = "Path:";
			// 
			// linkLabelOpenFile
			// 
			this.linkLabelOpenFile.AutoSize = true;
			this.linkLabelOpenFile.Location = new System.Drawing.Point(308, 260);
			this.linkLabelOpenFile.Name = "linkLabelOpenFile";
			this.linkLabelOpenFile.Size = new System.Drawing.Size(52, 13);
			this.linkLabelOpenFile.TabIndex = 18;
			this.linkLabelOpenFile.TabStop = true;
			this.linkLabelOpenFile.Text = "Open File";
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.listViewLog);
			this.tabPageLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLog.Size = new System.Drawing.Size(791, 370);
			this.tabPageLog.TabIndex = 2;
			this.tabPageLog.Text = "Log";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// listViewLog
			// 
			this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderType,
            this.columnHeaderEntry});
			this.listViewLog.FullRowSelect = true;
			this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewLog.HideSelection = false;
			this.listViewLog.Location = new System.Drawing.Point(6, 6);
			this.listViewLog.Name = "listViewLog";
			this.listViewLog.Size = new System.Drawing.Size(779, 358);
			this.listViewLog.TabIndex = 0;
			this.listViewLog.UseCompatibleStateImageBehavior = false;
			this.listViewLog.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderTime
			// 
			this.columnHeaderTime.Text = "Time";
			this.columnHeaderTime.Width = 120;
			// 
			// columnHeaderType
			// 
			this.columnHeaderType.Text = "Type";
			this.columnHeaderType.Width = 80;
			// 
			// columnHeaderEntry
			// 
			this.columnHeaderEntry.Text = "Entry";
			this.columnHeaderEntry.Width = 555;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(817, 414);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unpakk Daemon";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.tabControl.ResumeLayout(false);
			this.tabPageProgress.ResumeLayout(false);
			this.tabPageProgress.PerformLayout();
			this.tabPageSettings.ResumeLayout(false);
			this.groupBoxScanSettings.ResumeLayout(false);
			this.groupBoxApplicationSettings.ResumeLayout(false);
			this.groupBoxApplicationSettings.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
			this.groupBoxRecordDetails.ResumeLayout(false);
			this.groupBoxRecordDetails.PerformLayout();
			this.groupBoxSubRecordDetails.ResumeLayout(false);
			this.groupBoxSubRecordDetails.PerformLayout();
			this.tabPageLog.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageProgress;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.Label labelSubMessage;
		private System.Windows.Forms.Label labelMainMessage;
		private System.Windows.Forms.ProgressBar progressBarMainProgress;
		private System.Windows.Forms.ProgressBar progressBarSubProgress;
		private System.Windows.Forms.Label labelMainProgress;
		private System.Windows.Forms.Label labelSubProgress;
		private System.Windows.Forms.Label labelApplicationDataFolder;
		private System.Windows.Forms.TextBox textBoxApplicationDataFolder;
		private System.Windows.Forms.GroupBox groupBoxApplicationSettings;
		private System.Windows.Forms.Button buttonBrowseApplicationDataFolder;
		private System.Windows.Forms.Label labelSleepTimeMinutes;
		private System.Windows.Forms.TextBox textBoxSleepTime;
		private System.Windows.Forms.Label labelSleepTime;
		private System.Windows.Forms.GroupBox groupBoxScanSettings;
		private System.Windows.Forms.Button buttonRemoveRootPath;
		private System.Windows.Forms.Button buttonAddRootPath;
		private System.Windows.Forms.ListView listViewRootPath;
		private System.Windows.Forms.ColumnHeader columnHeaderRootPath;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRestore;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemClose;
		private System.Windows.Forms.TreeView treeViewRecords;
		private System.Windows.Forms.GroupBox groupBoxRecordDetails;
		private System.Windows.Forms.TextBox textBoxRecordSFVFile;
		private System.Windows.Forms.Label labelRecordPath;
		private System.Windows.Forms.TextBox textBoxRecordPath;
		private System.Windows.Forms.Label labelRecordSFVFile;
		private System.Windows.Forms.TextBox textBoxRecordRARParts;
		private System.Windows.Forms.Label labelRecordRARParts;
		private System.Windows.Forms.TextBox textBoxRecordRARFile;
		private System.Windows.Forms.Label labelRecordRARFile;
		private System.Windows.Forms.TextBox textBoxRecordRARSize;
		private System.Windows.Forms.Label labelRecordRARSize;
		private System.Windows.Forms.LinkLabel linkLabelRecordOpenFolder;
		private System.Windows.Forms.GroupBox groupBoxSubRecordDetails;
		private System.Windows.Forms.LinkLabel linkLabelOpenFile;
		private System.Windows.Forms.LinkLabel linkLabelSubRecordOpenFolder;
		private System.Windows.Forms.TextBox textBoxSubRecordFileSize;
		private System.Windows.Forms.Label labelSubRecordFileSize;
		private System.Windows.Forms.TextBox textBoxSubRecordPath;
		private System.Windows.Forms.TextBox textBoxSubRecordFile;
		private System.Windows.Forms.Label labelSubRecordFile;
		private System.Windows.Forms.Label labelSubRecordPath;
		private System.Windows.Forms.TabPage tabPageLog;
		private System.Windows.Forms.ListView listViewLog;
		private System.Windows.Forms.ColumnHeader columnHeaderTime;
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.ColumnHeader columnHeaderEntry;
	}
}