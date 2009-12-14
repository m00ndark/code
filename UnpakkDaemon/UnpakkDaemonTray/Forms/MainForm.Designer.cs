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
			this.listViewLog = new System.Windows.Forms.ListView();
			this.columnHeaderLog = new System.Windows.Forms.ColumnHeader();
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
			this.tabControl.SuspendLayout();
			this.tabPageProgress.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.groupBoxScanSettings.SuspendLayout();
			this.groupBoxApplicationSettings.SuspendLayout();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageProgress);
			this.tabControl.Controls.Add(this.tabPageSettings);
			this.tabControl.Location = new System.Drawing.Point(9, 9);
			this.tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(799, 396);
			this.tabControl.TabIndex = 0;
			this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
			// 
			// tabPageProgress
			// 
			this.tabPageProgress.Controls.Add(this.labelSubProgress);
			this.tabPageProgress.Controls.Add(this.labelMainProgress);
			this.tabPageProgress.Controls.Add(this.labelSubMessage);
			this.tabPageProgress.Controls.Add(this.labelMainMessage);
			this.tabPageProgress.Controls.Add(this.progressBarMainProgress);
			this.tabPageProgress.Controls.Add(this.progressBarSubProgress);
			this.tabPageProgress.Controls.Add(this.listViewLog);
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
			// listViewLog
			// 
			this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLog});
			this.listViewLog.FullRowSelect = true;
			this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewLog.HideSelection = false;
			this.listViewLog.Location = new System.Drawing.Point(6, 82);
			this.listViewLog.Name = "listViewLog";
			this.listViewLog.Size = new System.Drawing.Size(779, 285);
			this.listViewLog.TabIndex = 0;
			this.listViewLog.UseCompatibleStateImageBehavior = false;
			this.listViewLog.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderLog
			// 
			this.columnHeaderLog.Text = "Log";
			this.columnHeaderLog.Width = 755;
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageProgress;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.ListView listViewLog;
		private System.Windows.Forms.ColumnHeader columnHeaderLog;
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
	}
}