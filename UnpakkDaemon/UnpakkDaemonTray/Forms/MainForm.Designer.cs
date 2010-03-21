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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.treeViewRecords = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.groupBoxRecordDetails = new System.Windows.Forms.GroupBox();
			this.linkLabelRecordStatus = new System.Windows.Forms.LinkLabel();
			this.labelRecordTimeValue = new System.Windows.Forms.Label();
			this.labelRecordTime = new System.Windows.Forms.Label();
			this.labelRecordStatus = new System.Windows.Forms.Label();
			this.pictureBoxRecord = new System.Windows.Forms.PictureBox();
			this.labelRecordRARSizeValue = new System.Windows.Forms.Label();
			this.labelRecordRARPartsValue = new System.Windows.Forms.Label();
			this.linkLabelRecordOpenFolder = new System.Windows.Forms.LinkLabel();
			this.labelRecordRARSize = new System.Windows.Forms.Label();
			this.labelRecordRARParts = new System.Windows.Forms.Label();
			this.textBoxRecordRARFile = new System.Windows.Forms.TextBox();
			this.labelRecordRARFile = new System.Windows.Forms.Label();
			this.textBoxRecordPath = new System.Windows.Forms.TextBox();
			this.textBoxRecordSFVFile = new System.Windows.Forms.TextBox();
			this.labelRecordSFVFile = new System.Windows.Forms.Label();
			this.labelRecordPath = new System.Windows.Forms.Label();
			this.groupBoxSubRecordDetails = new System.Windows.Forms.GroupBox();
			this.linkLabelSubRecordStatus = new System.Windows.Forms.LinkLabel();
			this.labelSubRecordTimeValue = new System.Windows.Forms.Label();
			this.labelSubRecordTime = new System.Windows.Forms.Label();
			this.labelSubRecordStatus = new System.Windows.Forms.Label();
			this.pictureBoxSubRecord = new System.Windows.Forms.PictureBox();
			this.labelSubRecordFileSizeValue = new System.Windows.Forms.Label();
			this.linkLabelSubRecordOpenFile = new System.Windows.Forms.LinkLabel();
			this.linkLabelSubRecordOpenFolder = new System.Windows.Forms.LinkLabel();
			this.labelSubRecordFileSize = new System.Windows.Forms.Label();
			this.textBoxSubRecordPath = new System.Windows.Forms.TextBox();
			this.textBoxSubRecordFile = new System.Windows.Forms.TextBox();
			this.labelSubRecordFile = new System.Windows.Forms.Label();
			this.labelSubRecordPath = new System.Windows.Forms.Label();
			this.labelSubProgress = new System.Windows.Forms.Label();
			this.labelMainProgress = new System.Windows.Forms.Label();
			this.labelSubMessage = new System.Windows.Forms.Label();
			this.labelMainMessage = new System.Windows.Forms.Label();
			this.progressBarMainProgress = new System.Windows.Forms.ProgressBar();
			this.progressBarSubProgress = new System.Windows.Forms.ProgressBar();
			this.tabPageLog = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.panelLogFilterLeastLogType = new System.Windows.Forms.Panel();
			this.comboBoxLogFilterLeastLogType = new System.Windows.Forms.ComboBox();
			this.labelLogFilterLeastLogType = new System.Windows.Forms.Label();
			this.panelLogFilterDaysBack = new System.Windows.Forms.Panel();
			this.comboBoxLogFilterDaysBack = new System.Windows.Forms.ComboBox();
			this.labelLogFilterDaysBack = new System.Windows.Forms.Label();
			this.listViewLog = new System.Windows.Forms.ListView();
			this.columnHeaderTime = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderEntry = new System.Windows.Forms.ColumnHeader();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.groupBoxScanSettings = new System.Windows.Forms.GroupBox();
			this.buttonRemoveRootPath = new System.Windows.Forms.Button();
			this.buttonAddRootPath = new System.Windows.Forms.Button();
			this.listViewRootPath = new System.Windows.Forms.ListView();
			this.columnHeaderRootPath = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderUserName = new System.Windows.Forms.ColumnHeader();
			this.groupBoxGeneralSettings = new System.Windows.Forms.GroupBox();
			this.checkBoxStartTrayAppWithWindows = new System.Windows.Forms.CheckBox();
			this.buttonBrowseApplicationDataFolder = new System.Windows.Forms.Button();
			this.labelSleepTimeMinutes = new System.Windows.Forms.Label();
			this.textBoxSleepTime = new System.Windows.Forms.TextBox();
			this.labelSleepTime = new System.Windows.Forms.Label();
			this.textBoxApplicationDataFolder = new System.Windows.Forms.TextBox();
			this.labelApplicationDataFolder = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemService = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemServiceStart = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemServicePause = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemOptionsStartWithWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemRestore = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemMinimize = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.checkBoxUseSpecificOutputFolder = new System.Windows.Forms.CheckBox();
			this.buttonBrowseOutputFolder = new System.Windows.Forms.Button();
			this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
			this.tabControl.SuspendLayout();
			this.tabPageProgress.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.groupBoxRecordDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxRecord)).BeginInit();
			this.groupBoxSubRecordDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxSubRecord)).BeginInit();
			this.tabPageLog.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.panelLogFilterLeastLogType.SuspendLayout();
			this.panelLogFilterDaysBack.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.groupBoxScanSettings.SuspendLayout();
			this.groupBoxGeneralSettings.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
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
			this.tabPageProgress.Controls.Add(this.splitContainer);
			this.tabPageProgress.Controls.Add(this.labelSubProgress);
			this.tabPageProgress.Controls.Add(this.labelMainProgress);
			this.tabPageProgress.Controls.Add(this.labelSubMessage);
			this.tabPageProgress.Controls.Add(this.labelMainMessage);
			this.tabPageProgress.Controls.Add(this.progressBarMainProgress);
			this.tabPageProgress.Controls.Add(this.progressBarSubProgress);
			this.tabPageProgress.Location = new System.Drawing.Point(4, 22);
			this.tabPageProgress.Name = "tabPageProgress";
			this.tabPageProgress.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProgress.Size = new System.Drawing.Size(791, 370);
			this.tabPageProgress.TabIndex = 0;
			this.tabPageProgress.Text = "Progress";
			this.tabPageProgress.UseVisualStyleBackColor = true;
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(6, 81);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.treeViewRecords);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.groupBoxRecordDetails);
			this.splitContainer.Panel2.Controls.Add(this.groupBoxSubRecordDetails);
			this.splitContainer.Size = new System.Drawing.Size(779, 284);
			this.splitContainer.SplitterDistance = 336;
			this.splitContainer.SplitterWidth = 6;
			this.splitContainer.TabIndex = 19;
			// 
			// treeViewRecords
			// 
			this.treeViewRecords.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewRecords.HideSelection = false;
			this.treeViewRecords.ImageIndex = 0;
			this.treeViewRecords.ImageList = this.imageList;
			this.treeViewRecords.Location = new System.Drawing.Point(0, 6);
			this.treeViewRecords.Name = "treeViewRecords";
			this.treeViewRecords.SelectedImageIndex = 0;
			this.treeViewRecords.Size = new System.Drawing.Size(336, 277);
			this.treeViewRecords.TabIndex = 6;
			this.treeViewRecords.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewRecords_AfterSelect);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "Record-16.ico");
			this.imageList.Images.SetKeyName(1, "Record-Red-16.ico");
			this.imageList.Images.SetKeyName(2, "SubRecord-16.ico");
			this.imageList.Images.SetKeyName(3, "SubRecord-Red-16.ico");
			// 
			// groupBoxRecordDetails
			// 
			this.groupBoxRecordDetails.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxRecordDetails.Controls.Add(this.linkLabelRecordStatus);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordTimeValue);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordTime);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordStatus);
			this.groupBoxRecordDetails.Controls.Add(this.pictureBoxRecord);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARSizeValue);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARPartsValue);
			this.groupBoxRecordDetails.Controls.Add(this.linkLabelRecordOpenFolder);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARSize);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARParts);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordRARFile);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordRARFile);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordPath);
			this.groupBoxRecordDetails.Controls.Add(this.textBoxRecordSFVFile);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordSFVFile);
			this.groupBoxRecordDetails.Controls.Add(this.labelRecordPath);
			this.groupBoxRecordDetails.Location = new System.Drawing.Point(0, 0);
			this.groupBoxRecordDetails.Name = "groupBoxRecordDetails";
			this.groupBoxRecordDetails.Size = new System.Drawing.Size(437, 284);
			this.groupBoxRecordDetails.TabIndex = 11;
			this.groupBoxRecordDetails.TabStop = false;
			this.groupBoxRecordDetails.Text = "Details";
			this.groupBoxRecordDetails.Visible = false;
			// 
			// linkLabelRecordStatus
			// 
			this.linkLabelRecordStatus.AutoSize = true;
			this.linkLabelRecordStatus.Location = new System.Drawing.Point(71, 178);
			this.linkLabelRecordStatus.Name = "linkLabelRecordStatus";
			this.linkLabelRecordStatus.Size = new System.Drawing.Size(0, 13);
			this.linkLabelRecordStatus.TabIndex = 25;
			this.linkLabelRecordStatus.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRecordStatus_LinkClicked);
			// 
			// labelRecordTimeValue
			// 
			this.labelRecordTimeValue.AutoSize = true;
			this.labelRecordTimeValue.Location = new System.Drawing.Point(71, 152);
			this.labelRecordTimeValue.Name = "labelRecordTimeValue";
			this.labelRecordTimeValue.Size = new System.Drawing.Size(0, 13);
			this.labelRecordTimeValue.TabIndex = 24;
			// 
			// labelRecordTime
			// 
			this.labelRecordTime.AutoSize = true;
			this.labelRecordTime.Location = new System.Drawing.Point(6, 152);
			this.labelRecordTime.Name = "labelRecordTime";
			this.labelRecordTime.Size = new System.Drawing.Size(33, 13);
			this.labelRecordTime.TabIndex = 23;
			this.labelRecordTime.Text = "Time:";
			// 
			// labelRecordStatus
			// 
			this.labelRecordStatus.AutoSize = true;
			this.labelRecordStatus.Location = new System.Drawing.Point(6, 178);
			this.labelRecordStatus.Name = "labelRecordStatus";
			this.labelRecordStatus.Size = new System.Drawing.Size(40, 13);
			this.labelRecordStatus.TabIndex = 21;
			this.labelRecordStatus.Text = "Status:";
			// 
			// pictureBoxRecord
			// 
			this.pictureBoxRecord.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBoxRecord.Location = new System.Drawing.Point(9, 225);
			this.pictureBoxRecord.Name = "pictureBoxRecord";
			this.pictureBoxRecord.Size = new System.Drawing.Size(48, 48);
			this.pictureBoxRecord.TabIndex = 20;
			this.pictureBoxRecord.TabStop = false;
			// 
			// labelRecordRARSizeValue
			// 
			this.labelRecordRARSizeValue.AutoSize = true;
			this.labelRecordRARSizeValue.Location = new System.Drawing.Point(71, 126);
			this.labelRecordRARSizeValue.Name = "labelRecordRARSizeValue";
			this.labelRecordRARSizeValue.Size = new System.Drawing.Size(0, 13);
			this.labelRecordRARSizeValue.TabIndex = 19;
			// 
			// labelRecordRARPartsValue
			// 
			this.labelRecordRARPartsValue.AutoSize = true;
			this.labelRecordRARPartsValue.Location = new System.Drawing.Point(71, 100);
			this.labelRecordRARPartsValue.Name = "labelRecordRARPartsValue";
			this.labelRecordRARPartsValue.Size = new System.Drawing.Size(0, 13);
			this.labelRecordRARPartsValue.TabIndex = 18;
			// 
			// linkLabelRecordOpenFolder
			// 
			this.linkLabelRecordOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelRecordOpenFolder.AutoSize = true;
			this.linkLabelRecordOpenFolder.LinkArea = new System.Windows.Forms.LinkArea(0, 11);
			this.linkLabelRecordOpenFolder.Location = new System.Drawing.Point(366, 260);
			this.linkLabelRecordOpenFolder.Name = "linkLabelRecordOpenFolder";
			this.linkLabelRecordOpenFolder.Size = new System.Drawing.Size(65, 13);
			this.linkLabelRecordOpenFolder.TabIndex = 17;
			this.linkLabelRecordOpenFolder.TabStop = true;
			this.linkLabelRecordOpenFolder.Text = "Open Folder";
			this.linkLabelRecordOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRecordOpenFolder_LinkClicked);
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
			// labelRecordRARParts
			// 
			this.labelRecordRARParts.AutoSize = true;
			this.labelRecordRARParts.Location = new System.Drawing.Point(6, 100);
			this.labelRecordRARParts.Name = "labelRecordRARParts";
			this.labelRecordRARParts.Size = new System.Drawing.Size(59, 13);
			this.labelRecordRARParts.TabIndex = 13;
			this.labelRecordRARParts.Text = "RAR parts:";
			// 
			// textBoxRecordRARFile
			// 
			this.textBoxRecordRARFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRecordRARFile.BackColor = System.Drawing.Color.White;
			this.textBoxRecordRARFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxRecordRARFile.Location = new System.Drawing.Point(74, 74);
			this.textBoxRecordRARFile.Name = "textBoxRecordRARFile";
			this.textBoxRecordRARFile.ReadOnly = true;
			this.textBoxRecordRARFile.Size = new System.Drawing.Size(354, 13);
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
			// textBoxRecordPath
			// 
			this.textBoxRecordPath.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRecordPath.BackColor = System.Drawing.Color.White;
			this.textBoxRecordPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxRecordPath.Location = new System.Drawing.Point(74, 22);
			this.textBoxRecordPath.Name = "textBoxRecordPath";
			this.textBoxRecordPath.ReadOnly = true;
			this.textBoxRecordPath.Size = new System.Drawing.Size(354, 13);
			this.textBoxRecordPath.TabIndex = 8;
			// 
			// textBoxRecordSFVFile
			// 
			this.textBoxRecordSFVFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRecordSFVFile.BackColor = System.Drawing.Color.White;
			this.textBoxRecordSFVFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxRecordSFVFile.Location = new System.Drawing.Point(74, 48);
			this.textBoxRecordSFVFile.Name = "textBoxRecordSFVFile";
			this.textBoxRecordSFVFile.ReadOnly = true;
			this.textBoxRecordSFVFile.Size = new System.Drawing.Size(354, 13);
			this.textBoxRecordSFVFile.TabIndex = 10;
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
			// labelRecordPath
			// 
			this.labelRecordPath.AutoSize = true;
			this.labelRecordPath.Location = new System.Drawing.Point(6, 22);
			this.labelRecordPath.Name = "labelRecordPath";
			this.labelRecordPath.Size = new System.Drawing.Size(32, 13);
			this.labelRecordPath.TabIndex = 9;
			this.labelRecordPath.Text = "Path:";
			// 
			// groupBoxSubRecordDetails
			// 
			this.groupBoxSubRecordDetails.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxSubRecordDetails.Controls.Add(this.linkLabelSubRecordStatus);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordTimeValue);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordTime);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordStatus);
			this.groupBoxSubRecordDetails.Controls.Add(this.pictureBoxSubRecord);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordFileSizeValue);
			this.groupBoxSubRecordDetails.Controls.Add(this.linkLabelSubRecordOpenFile);
			this.groupBoxSubRecordDetails.Controls.Add(this.linkLabelSubRecordOpenFolder);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordFileSize);
			this.groupBoxSubRecordDetails.Controls.Add(this.textBoxSubRecordPath);
			this.groupBoxSubRecordDetails.Controls.Add(this.textBoxSubRecordFile);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordFile);
			this.groupBoxSubRecordDetails.Controls.Add(this.labelSubRecordPath);
			this.groupBoxSubRecordDetails.Location = new System.Drawing.Point(0, 0);
			this.groupBoxSubRecordDetails.Name = "groupBoxSubRecordDetails";
			this.groupBoxSubRecordDetails.Size = new System.Drawing.Size(437, 284);
			this.groupBoxSubRecordDetails.TabIndex = 18;
			this.groupBoxSubRecordDetails.TabStop = false;
			this.groupBoxSubRecordDetails.Text = "Details";
			this.groupBoxSubRecordDetails.Visible = false;
			// 
			// linkLabelSubRecordStatus
			// 
			this.linkLabelSubRecordStatus.AutoSize = true;
			this.linkLabelSubRecordStatus.Location = new System.Drawing.Point(71, 126);
			this.linkLabelSubRecordStatus.Name = "linkLabelSubRecordStatus";
			this.linkLabelSubRecordStatus.Size = new System.Drawing.Size(0, 13);
			this.linkLabelSubRecordStatus.TabIndex = 27;
			this.linkLabelSubRecordStatus.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSubRecordStatus_LinkClicked);
			// 
			// labelSubRecordTimeValue
			// 
			this.labelSubRecordTimeValue.AutoSize = true;
			this.labelSubRecordTimeValue.Location = new System.Drawing.Point(71, 100);
			this.labelSubRecordTimeValue.Name = "labelSubRecordTimeValue";
			this.labelSubRecordTimeValue.Size = new System.Drawing.Size(0, 13);
			this.labelSubRecordTimeValue.TabIndex = 26;
			// 
			// labelSubRecordTime
			// 
			this.labelSubRecordTime.AutoSize = true;
			this.labelSubRecordTime.Location = new System.Drawing.Point(6, 100);
			this.labelSubRecordTime.Name = "labelSubRecordTime";
			this.labelSubRecordTime.Size = new System.Drawing.Size(33, 13);
			this.labelSubRecordTime.TabIndex = 25;
			this.labelSubRecordTime.Text = "Time:";
			// 
			// labelSubRecordStatus
			// 
			this.labelSubRecordStatus.AutoSize = true;
			this.labelSubRecordStatus.Location = new System.Drawing.Point(6, 126);
			this.labelSubRecordStatus.Name = "labelSubRecordStatus";
			this.labelSubRecordStatus.Size = new System.Drawing.Size(40, 13);
			this.labelSubRecordStatus.TabIndex = 23;
			this.labelSubRecordStatus.Text = "Status:";
			// 
			// pictureBoxSubRecord
			// 
			this.pictureBoxSubRecord.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBoxSubRecord.Location = new System.Drawing.Point(9, 225);
			this.pictureBoxSubRecord.Name = "pictureBoxSubRecord";
			this.pictureBoxSubRecord.Size = new System.Drawing.Size(48, 48);
			this.pictureBoxSubRecord.TabIndex = 21;
			this.pictureBoxSubRecord.TabStop = false;
			// 
			// labelSubRecordFileSizeValue
			// 
			this.labelSubRecordFileSizeValue.AutoSize = true;
			this.labelSubRecordFileSizeValue.Location = new System.Drawing.Point(71, 74);
			this.labelSubRecordFileSizeValue.Name = "labelSubRecordFileSizeValue";
			this.labelSubRecordFileSizeValue.Size = new System.Drawing.Size(0, 13);
			this.labelSubRecordFileSizeValue.TabIndex = 19;
			// 
			// linkLabelSubRecordOpenFile
			// 
			this.linkLabelSubRecordOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelSubRecordOpenFile.AutoSize = true;
			this.linkLabelSubRecordOpenFile.Location = new System.Drawing.Point(308, 260);
			this.linkLabelSubRecordOpenFile.Name = "linkLabelSubRecordOpenFile";
			this.linkLabelSubRecordOpenFile.Size = new System.Drawing.Size(52, 13);
			this.linkLabelSubRecordOpenFile.TabIndex = 18;
			this.linkLabelSubRecordOpenFile.TabStop = true;
			this.linkLabelSubRecordOpenFile.Text = "Open File";
			this.linkLabelSubRecordOpenFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSubRecordOpenFile_LinkClicked);
			// 
			// linkLabelSubRecordOpenFolder
			// 
			this.linkLabelSubRecordOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabelSubRecordOpenFolder.AutoSize = true;
			this.linkLabelSubRecordOpenFolder.Location = new System.Drawing.Point(366, 260);
			this.linkLabelSubRecordOpenFolder.Name = "linkLabelSubRecordOpenFolder";
			this.linkLabelSubRecordOpenFolder.Size = new System.Drawing.Size(65, 13);
			this.linkLabelSubRecordOpenFolder.TabIndex = 17;
			this.linkLabelSubRecordOpenFolder.TabStop = true;
			this.linkLabelSubRecordOpenFolder.Text = "Open Folder";
			this.linkLabelSubRecordOpenFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSubRecordOpenFolder_LinkClicked);
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
			this.textBoxSubRecordPath.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSubRecordPath.BackColor = System.Drawing.Color.White;
			this.textBoxSubRecordPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxSubRecordPath.Location = new System.Drawing.Point(74, 22);
			this.textBoxSubRecordPath.Name = "textBoxSubRecordPath";
			this.textBoxSubRecordPath.ReadOnly = true;
			this.textBoxSubRecordPath.Size = new System.Drawing.Size(354, 13);
			this.textBoxSubRecordPath.TabIndex = 8;
			// 
			// textBoxSubRecordFile
			// 
			this.textBoxSubRecordFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSubRecordFile.BackColor = System.Drawing.Color.White;
			this.textBoxSubRecordFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxSubRecordFile.Location = new System.Drawing.Point(74, 48);
			this.textBoxSubRecordFile.Name = "textBoxSubRecordFile";
			this.textBoxSubRecordFile.ReadOnly = true;
			this.textBoxSubRecordFile.Size = new System.Drawing.Size(354, 13);
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
			this.progressBarMainProgress.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBarMainProgress.Location = new System.Drawing.Point(6, 6);
			this.progressBarMainProgress.Name = "progressBarMainProgress";
			this.progressBarMainProgress.Size = new System.Drawing.Size(779, 15);
			this.progressBarMainProgress.TabIndex = 2;
			// 
			// progressBarSubProgress
			// 
			this.progressBarSubProgress.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBarSubProgress.Location = new System.Drawing.Point(6, 44);
			this.progressBarSubProgress.Name = "progressBarSubProgress";
			this.progressBarSubProgress.Size = new System.Drawing.Size(779, 15);
			this.progressBarSubProgress.TabIndex = 1;
			// 
			// tabPageLog
			// 
			this.tabPageLog.Controls.Add(this.tableLayoutPanel);
			this.tabPageLog.Controls.Add(this.listViewLog);
			this.tabPageLog.Location = new System.Drawing.Point(4, 22);
			this.tabPageLog.Name = "tabPageLog";
			this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageLog.Size = new System.Drawing.Size(791, 370);
			this.tabPageLog.TabIndex = 2;
			this.tabPageLog.Text = "Log";
			this.tabPageLog.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.panelLogFilterLeastLogType, 2, 0);
			this.tableLayoutPanel.Controls.Add(this.panelLogFilterDaysBack, 0, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(6, 6);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(779, 21);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// panelLogFilterLeastLogType
			// 
			this.panelLogFilterLeastLogType.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.panelLogFilterLeastLogType.Controls.Add(this.comboBoxLogFilterLeastLogType);
			this.panelLogFilterLeastLogType.Controls.Add(this.labelLogFilterLeastLogType);
			this.panelLogFilterLeastLogType.Location = new System.Drawing.Point(399, 0);
			this.panelLogFilterLeastLogType.Margin = new System.Windows.Forms.Padding(0);
			this.panelLogFilterLeastLogType.Name = "panelLogFilterLeastLogType";
			this.panelLogFilterLeastLogType.Size = new System.Drawing.Size(380, 21);
			this.panelLogFilterLeastLogType.TabIndex = 1;
			// 
			// comboBoxLogFilterLeastLogType
			// 
			this.comboBoxLogFilterLeastLogType.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxLogFilterLeastLogType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLogFilterLeastLogType.FormattingEnabled = true;
			this.comboBoxLogFilterLeastLogType.Location = new System.Drawing.Point(122, 0);
			this.comboBoxLogFilterLeastLogType.Name = "comboBoxLogFilterLeastLogType";
			this.comboBoxLogFilterLeastLogType.Size = new System.Drawing.Size(258, 21);
			this.comboBoxLogFilterLeastLogType.TabIndex = 3;
			this.comboBoxLogFilterLeastLogType.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogFilterLeastLogType_SelectedIndexChanged);
			// 
			// labelLogFilterLeastLogType
			// 
			this.labelLogFilterLeastLogType.AutoSize = true;
			this.labelLogFilterLeastLogType.Location = new System.Drawing.Point(1, 3);
			this.labelLogFilterLeastLogType.Name = "labelLogFilterLeastLogType";
			this.labelLogFilterLeastLogType.Size = new System.Drawing.Size(115, 13);
			this.labelLogFilterLeastLogType.TabIndex = 4;
			this.labelLogFilterLeastLogType.Text = "Least log type severity:";
			// 
			// panelLogFilterDaysBack
			// 
			this.panelLogFilterDaysBack.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.panelLogFilterDaysBack.Controls.Add(this.comboBoxLogFilterDaysBack);
			this.panelLogFilterDaysBack.Controls.Add(this.labelLogFilterDaysBack);
			this.panelLogFilterDaysBack.Location = new System.Drawing.Point(0, 0);
			this.panelLogFilterDaysBack.Margin = new System.Windows.Forms.Padding(0);
			this.panelLogFilterDaysBack.Name = "panelLogFilterDaysBack";
			this.panelLogFilterDaysBack.Size = new System.Drawing.Size(379, 21);
			this.panelLogFilterDaysBack.TabIndex = 0;
			// 
			// comboBoxLogFilterDaysBack
			// 
			this.comboBoxLogFilterDaysBack.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxLogFilterDaysBack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLogFilterDaysBack.FormattingEnabled = true;
			this.comboBoxLogFilterDaysBack.Location = new System.Drawing.Point(94, 0);
			this.comboBoxLogFilterDaysBack.Name = "comboBoxLogFilterDaysBack";
			this.comboBoxLogFilterDaysBack.Size = new System.Drawing.Size(258, 21);
			this.comboBoxLogFilterDaysBack.TabIndex = 1;
			this.comboBoxLogFilterDaysBack.SelectedIndexChanged += new System.EventHandler(this.comboBoxLogFilterDaysBack_SelectedIndexChanged);
			// 
			// labelLogFilterDaysBack
			// 
			this.labelLogFilterDaysBack.AutoSize = true;
			this.labelLogFilterDaysBack.Location = new System.Drawing.Point(0, 3);
			this.labelLogFilterDaysBack.Name = "labelLogFilterDaysBack";
			this.labelLogFilterDaysBack.Size = new System.Drawing.Size(88, 13);
			this.labelLogFilterDaysBack.TabIndex = 2;
			this.labelLogFilterDaysBack.Text = "Show log entries:";
			// 
			// listViewLog
			// 
			this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTime,
            this.columnHeaderType,
            this.columnHeaderEntry});
			this.listViewLog.FullRowSelect = true;
			this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewLog.HideSelection = false;
			this.listViewLog.Location = new System.Drawing.Point(6, 33);
			this.listViewLog.Name = "listViewLog";
			this.listViewLog.Size = new System.Drawing.Size(779, 331);
			this.listViewLog.TabIndex = 0;
			this.listViewLog.UseCompatibleStateImageBehavior = false;
			this.listViewLog.View = System.Windows.Forms.View.Details;
			this.listViewLog.DoubleClick += new System.EventHandler(this.listViewLog_DoubleClick);
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
			// tabPageSettings
			// 
			this.tabPageSettings.Controls.Add(this.groupBoxScanSettings);
			this.tabPageSettings.Controls.Add(this.groupBoxGeneralSettings);
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
			this.groupBoxScanSettings.Size = new System.Drawing.Size(782, 224);
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
            this.columnHeaderRootPath,
            this.columnHeaderUserName});
			this.listViewRootPath.FullRowSelect = true;
			this.listViewRootPath.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewRootPath.HideSelection = false;
			this.listViewRootPath.Location = new System.Drawing.Point(6, 19);
			this.listViewRootPath.Name = "listViewRootPath";
			this.listViewRootPath.Size = new System.Drawing.Size(689, 199);
			this.listViewRootPath.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listViewRootPath.TabIndex = 0;
			this.listViewRootPath.UseCompatibleStateImageBehavior = false;
			this.listViewRootPath.View = System.Windows.Forms.View.Details;
			this.listViewRootPath.SelectedIndexChanged += new System.EventHandler(this.listViewRootPath_SelectedIndexChanged);
			this.listViewRootPath.DoubleClick += new System.EventHandler(this.listViewRootPath_DoubleClick);
			// 
			// columnHeaderRootPath
			// 
			this.columnHeaderRootPath.Text = "Root Path";
			this.columnHeaderRootPath.Width = 545;
			// 
			// columnHeaderUserName
			// 
			this.columnHeaderUserName.Text = "User Name";
			this.columnHeaderUserName.Width = 120;
			// 
			// groupBoxGeneralSettings
			// 
			this.groupBoxGeneralSettings.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxGeneralSettings.Controls.Add(this.buttonBrowseOutputFolder);
			this.groupBoxGeneralSettings.Controls.Add(this.textBoxOutputFolder);
			this.groupBoxGeneralSettings.Controls.Add(this.checkBoxUseSpecificOutputFolder);
			this.groupBoxGeneralSettings.Controls.Add(this.checkBoxStartTrayAppWithWindows);
			this.groupBoxGeneralSettings.Controls.Add(this.buttonBrowseApplicationDataFolder);
			this.groupBoxGeneralSettings.Controls.Add(this.labelSleepTimeMinutes);
			this.groupBoxGeneralSettings.Controls.Add(this.textBoxSleepTime);
			this.groupBoxGeneralSettings.Controls.Add(this.labelSleepTime);
			this.groupBoxGeneralSettings.Controls.Add(this.textBoxApplicationDataFolder);
			this.groupBoxGeneralSettings.Controls.Add(this.labelApplicationDataFolder);
			this.groupBoxGeneralSettings.Location = new System.Drawing.Point(3, 6);
			this.groupBoxGeneralSettings.Name = "groupBoxGeneralSettings";
			this.groupBoxGeneralSettings.Size = new System.Drawing.Size(782, 131);
			this.groupBoxGeneralSettings.TabIndex = 2;
			this.groupBoxGeneralSettings.TabStop = false;
			this.groupBoxGeneralSettings.Text = "General Settings";
			// 
			// checkBoxStartTrayAppWithWindows
			// 
			this.checkBoxStartTrayAppWithWindows.AutoSize = true;
			this.checkBoxStartTrayAppWithWindows.Location = new System.Drawing.Point(9, 99);
			this.checkBoxStartTrayAppWithWindows.Name = "checkBoxStartTrayAppWithWindows";
			this.checkBoxStartTrayAppWithWindows.Size = new System.Drawing.Size(191, 17);
			this.checkBoxStartTrayAppWithWindows.TabIndex = 7;
			this.checkBoxStartTrayAppWithWindows.Text = "Start tray application with Windows";
			this.checkBoxStartTrayAppWithWindows.UseVisualStyleBackColor = true;
			this.checkBoxStartTrayAppWithWindows.CheckedChanged += new System.EventHandler(this.checkBoxStartTrayAppWithWindows_CheckedChanged);
			// 
			// buttonBrowseApplicationDataFolder
			// 
			this.buttonBrowseApplicationDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseApplicationDataFolder.Location = new System.Drawing.Point(744, 17);
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
			this.labelSleepTimeMinutes.Location = new System.Drawing.Point(246, 48);
			this.labelSleepTimeMinutes.Name = "labelSleepTimeMinutes";
			this.labelSleepTimeMinutes.Size = new System.Drawing.Size(43, 13);
			this.labelSleepTimeMinutes.TabIndex = 4;
			this.labelSleepTimeMinutes.Text = "minutes";
			// 
			// textBoxSleepTime
			// 
			this.textBoxSleepTime.Location = new System.Drawing.Point(164, 45);
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
			this.textBoxApplicationDataFolder.Location = new System.Drawing.Point(164, 19);
			this.textBoxApplicationDataFolder.Name = "textBoxApplicationDataFolder";
			this.textBoxApplicationDataFolder.Size = new System.Drawing.Size(574, 20);
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
			this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDown);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemService,
            this.toolStripMenuItemOptions,
            this.toolStripSeparator,
            this.toolStripMenuItemRestore,
            this.toolStripMenuItemMinimize,
            this.toolStripMenuItemClose});
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size(124, 120);
			// 
			// toolStripMenuItemService
			// 
			this.toolStripMenuItemService.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemServiceStart,
            this.toolStripMenuItemServicePause});
			this.toolStripMenuItemService.Name = "toolStripMenuItemService";
			this.toolStripMenuItemService.Size = new System.Drawing.Size(123, 22);
			this.toolStripMenuItemService.Text = "Service";
			// 
			// toolStripMenuItemServiceStart
			// 
			this.toolStripMenuItemServiceStart.Name = "toolStripMenuItemServiceStart";
			this.toolStripMenuItemServiceStart.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemServiceStart.Text = "Start";
			this.toolStripMenuItemServiceStart.Click += new System.EventHandler(this.toolStripMenuItemServiceStart_Click);
			// 
			// toolStripMenuItemServicePause
			// 
			this.toolStripMenuItemServicePause.Name = "toolStripMenuItemServicePause";
			this.toolStripMenuItemServicePause.Size = new System.Drawing.Size(105, 22);
			this.toolStripMenuItemServicePause.Text = "Pause";
			this.toolStripMenuItemServicePause.Click += new System.EventHandler(this.toolStripMenuItemServicePause_Click);
			// 
			// toolStripMenuItemOptions
			// 
			this.toolStripMenuItemOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOptionsStartWithWindows});
			this.toolStripMenuItemOptions.Name = "toolStripMenuItemOptions";
			this.toolStripMenuItemOptions.Size = new System.Drawing.Size(123, 22);
			this.toolStripMenuItemOptions.Text = "Options";
			// 
			// toolStripMenuItemOptionsStartWithWindows
			// 
			this.toolStripMenuItemOptionsStartWithWindows.Name = "toolStripMenuItemOptionsStartWithWindows";
			this.toolStripMenuItemOptionsStartWithWindows.Size = new System.Drawing.Size(178, 22);
			this.toolStripMenuItemOptionsStartWithWindows.Text = "Start With Windows";
			this.toolStripMenuItemOptionsStartWithWindows.Click += new System.EventHandler(this.toolStripMenuItemOptionsStartWithWindows_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(120, 6);
			// 
			// toolStripMenuItemRestore
			// 
			this.toolStripMenuItemRestore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.toolStripMenuItemRestore.Name = "toolStripMenuItemRestore";
			this.toolStripMenuItemRestore.Size = new System.Drawing.Size(123, 22);
			this.toolStripMenuItemRestore.Text = "Restore";
			this.toolStripMenuItemRestore.Click += new System.EventHandler(this.toolStripMenuItemRestore_Click);
			// 
			// toolStripMenuItemMinimize
			// 
			this.toolStripMenuItemMinimize.Name = "toolStripMenuItemMinimize";
			this.toolStripMenuItemMinimize.Size = new System.Drawing.Size(123, 22);
			this.toolStripMenuItemMinimize.Text = "Minimize";
			this.toolStripMenuItemMinimize.Click += new System.EventHandler(this.toolStripMenuItemMinimize_Click);
			// 
			// toolStripMenuItemClose
			// 
			this.toolStripMenuItemClose.Name = "toolStripMenuItemClose";
			this.toolStripMenuItemClose.Size = new System.Drawing.Size(123, 22);
			this.toolStripMenuItemClose.Text = "Close";
			this.toolStripMenuItemClose.Click += new System.EventHandler(this.toolStripMenuItemClose_Click);
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 100;
			this.toolTip.AutoPopDelay = 5000;
			this.toolTip.InitialDelay = 100;
			this.toolTip.ReshowDelay = 20;
			// 
			// checkBoxUseSpecificOutputFolder
			// 
			this.checkBoxUseSpecificOutputFolder.AutoSize = true;
			this.checkBoxUseSpecificOutputFolder.Location = new System.Drawing.Point(9, 73);
			this.checkBoxUseSpecificOutputFolder.Name = "checkBoxUseSpecificOutputFolder";
			this.checkBoxUseSpecificOutputFolder.Size = new System.Drawing.Size(149, 17);
			this.checkBoxUseSpecificOutputFolder.TabIndex = 8;
			this.checkBoxUseSpecificOutputFolder.Text = "Use specific output folder:";
			this.checkBoxUseSpecificOutputFolder.UseVisualStyleBackColor = true;
			this.checkBoxUseSpecificOutputFolder.CheckedChanged += new System.EventHandler(this.checkBoxUseSpecificOutputFolder_CheckedChanged);
			// 
			// buttonBrowseOutputFolder
			// 
			this.buttonBrowseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseOutputFolder.Location = new System.Drawing.Point(744, 69);
			this.buttonBrowseOutputFolder.Name = "buttonBrowseOutputFolder";
			this.buttonBrowseOutputFolder.Size = new System.Drawing.Size(32, 23);
			this.buttonBrowseOutputFolder.TabIndex = 10;
			this.buttonBrowseOutputFolder.Text = "...";
			this.buttonBrowseOutputFolder.UseVisualStyleBackColor = true;
			this.buttonBrowseOutputFolder.Click += new System.EventHandler(this.buttonBrowseOutputFolder_Click);
			// 
			// textBoxOutputFolder
			// 
			this.textBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxOutputFolder.Location = new System.Drawing.Point(164, 71);
			this.textBoxOutputFolder.Name = "textBoxOutputFolder";
			this.textBoxOutputFolder.Size = new System.Drawing.Size(574, 20);
			this.textBoxOutputFolder.TabIndex = 9;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(817, 414);
			this.Controls.Add(this.tabControl);
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
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.groupBoxRecordDetails.ResumeLayout(false);
			this.groupBoxRecordDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxRecord)).EndInit();
			this.groupBoxSubRecordDetails.ResumeLayout(false);
			this.groupBoxSubRecordDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxSubRecord)).EndInit();
			this.tabPageLog.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			this.panelLogFilterLeastLogType.ResumeLayout(false);
			this.panelLogFilterLeastLogType.PerformLayout();
			this.panelLogFilterDaysBack.ResumeLayout(false);
			this.panelLogFilterDaysBack.PerformLayout();
			this.tabPageSettings.ResumeLayout(false);
			this.groupBoxScanSettings.ResumeLayout(false);
			this.groupBoxGeneralSettings.ResumeLayout(false);
			this.groupBoxGeneralSettings.PerformLayout();
			this.contextMenuStrip.ResumeLayout(false);
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
		private System.Windows.Forms.GroupBox groupBoxGeneralSettings;
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
		private System.Windows.Forms.Label labelRecordRARParts;
		private System.Windows.Forms.TextBox textBoxRecordRARFile;
		private System.Windows.Forms.Label labelRecordRARFile;
		private System.Windows.Forms.Label labelRecordRARSize;
		private System.Windows.Forms.LinkLabel linkLabelRecordOpenFolder;
		private System.Windows.Forms.GroupBox groupBoxSubRecordDetails;
		private System.Windows.Forms.LinkLabel linkLabelSubRecordOpenFile;
		private System.Windows.Forms.LinkLabel linkLabelSubRecordOpenFolder;
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
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Label labelRecordRARPartsValue;
		private System.Windows.Forms.Label labelRecordRARSizeValue;
		private System.Windows.Forms.Label labelSubRecordFileSizeValue;
		private System.Windows.Forms.PictureBox pictureBoxSubRecord;
		private System.Windows.Forms.PictureBox pictureBoxRecord;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label labelSubRecordStatus;
		private System.Windows.Forms.Label labelRecordStatus;
		private System.Windows.Forms.Label labelRecordTimeValue;
		private System.Windows.Forms.Label labelRecordTime;
		private System.Windows.Forms.Label labelSubRecordTimeValue;
		private System.Windows.Forms.Label labelSubRecordTime;
		private System.Windows.Forms.LinkLabel linkLabelRecordStatus;
		private System.Windows.Forms.LinkLabel linkLabelSubRecordStatus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label labelLogFilterDaysBack;
		private System.Windows.Forms.ComboBox comboBoxLogFilterDaysBack;
		private System.Windows.Forms.Panel panelLogFilterLeastLogType;
		private System.Windows.Forms.Panel panelLogFilterDaysBack;
		private System.Windows.Forms.ComboBox comboBoxLogFilterLeastLogType;
		private System.Windows.Forms.Label labelLogFilterLeastLogType;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMinimize;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOptionsStartWithWindows;
		private System.Windows.Forms.CheckBox checkBoxStartTrayAppWithWindows;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemService;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemServiceStart;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemServicePause;
		private System.Windows.Forms.ColumnHeader columnHeaderUserName;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Button buttonBrowseOutputFolder;
		private System.Windows.Forms.TextBox textBoxOutputFolder;
		private System.Windows.Forms.CheckBox checkBoxUseSpecificOutputFolder;
	}
}