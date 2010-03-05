namespace ProcessController
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
            this.listViewApplications = new System.Windows.Forms.ListView();
            this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderGroup = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPath = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderArguments = new System.Windows.Forms.ColumnHeader();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxSelectedApplication = new System.Windows.Forms.GroupBox();
            this.textBoxArguments = new System.Windows.Forms.TextBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelSets = new System.Windows.Forms.Label();
            this.labelHeaderSets = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelHeaderGroup = new System.Windows.Forms.Label();
            this.labelHeaderArguments = new System.Windows.Forms.Label();
            this.labelHeaderPath = new System.Windows.Forms.Label();
            this.labelHeaderName = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemSystemTrayStartSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSystemTrayStartBySet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSystemTrayStopSingle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSystemTrayStopBySet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSystemTrayOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSystemTrayRestoreMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSystemTrayExit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripApplicationList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemApplicationListStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemApplicationListStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemApplicationListEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemApplicationListRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxSets = new System.Windows.Forms.ComboBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.groupBoxSelectedApplication.SuspendLayout();
            this.contextMenuStripSystemTray.SuspendLayout();
            this.contextMenuStripApplicationList.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewApplications
            // 
            this.listViewApplications.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewApplications.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderStatus,
            this.columnHeaderGroup,
            this.columnHeaderName,
            this.columnHeaderPath,
            this.columnHeaderArguments});
            this.listViewApplications.FullRowSelect = true;
            this.listViewApplications.HideSelection = false;
            this.listViewApplications.Location = new System.Drawing.Point(12, 41);
            this.listViewApplications.Name = "listViewApplications";
            this.listViewApplications.Size = new System.Drawing.Size(716, 192);
            this.listViewApplications.SmallImageList = this.imageList;
            this.listViewApplications.TabIndex = 0;
            this.listViewApplications.UseCompatibleStateImageBehavior = false;
            this.listViewApplications.View = System.Windows.Forms.View.Details;
            this.listViewApplications.SelectedIndexChanged += new System.EventHandler(this.listViewApplications_SelectedIndexChanged);
            this.listViewApplications.DoubleClick += new System.EventHandler(this.listViewApplications_DoubleClick);
            this.listViewApplications.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewApplications_MouseUp);
            this.listViewApplications.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewApplications_ColumnClick);
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "";
            this.columnHeaderStatus.Width = 25;
            // 
            // columnHeaderGroup
            // 
            this.columnHeaderGroup.Text = "Group";
            this.columnHeaderGroup.Width = 100;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 120;
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "Path";
            this.columnHeaderPath.Width = 330;
            // 
            // columnHeaderArguments
            // 
            this.columnHeaderArguments.Text = "Arguments";
            this.columnHeaderArguments.Width = 120;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "running-16.ico");
            this.imageList.Images.SetKeyName(1, "stopped-16.ico");
            // 
            // groupBoxSelectedApplication
            // 
            this.groupBoxSelectedApplication.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectedApplication.Controls.Add(this.textBoxArguments);
            this.groupBoxSelectedApplication.Controls.Add(this.textBoxPath);
            this.groupBoxSelectedApplication.Controls.Add(this.labelSets);
            this.groupBoxSelectedApplication.Controls.Add(this.labelHeaderSets);
            this.groupBoxSelectedApplication.Controls.Add(this.labelGroup);
            this.groupBoxSelectedApplication.Controls.Add(this.labelName);
            this.groupBoxSelectedApplication.Controls.Add(this.labelHeaderGroup);
            this.groupBoxSelectedApplication.Controls.Add(this.labelHeaderArguments);
            this.groupBoxSelectedApplication.Controls.Add(this.labelHeaderPath);
            this.groupBoxSelectedApplication.Controls.Add(this.labelHeaderName);
            this.groupBoxSelectedApplication.Location = new System.Drawing.Point(12, 239);
            this.groupBoxSelectedApplication.Name = "groupBoxSelectedApplication";
            this.groupBoxSelectedApplication.Size = new System.Drawing.Size(716, 119);
            this.groupBoxSelectedApplication.TabIndex = 1;
            this.groupBoxSelectedApplication.TabStop = false;
            this.groupBoxSelectedApplication.Text = "Selected Application";
            // 
            // textBoxArguments
            // 
            this.textBoxArguments.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxArguments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxArguments.Location = new System.Drawing.Point(75, 57);
            this.textBoxArguments.Name = "textBoxArguments";
            this.textBoxArguments.ReadOnly = true;
            this.textBoxArguments.Size = new System.Drawing.Size(635, 13);
            this.textBoxArguments.TabIndex = 11;
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPath.Location = new System.Drawing.Point(75, 38);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(635, 13);
            this.textBoxPath.TabIndex = 10;
            // 
            // labelSets
            // 
            this.labelSets.AutoSize = true;
            this.labelSets.Location = new System.Drawing.Point(72, 95);
            this.labelSets.Margin = new System.Windows.Forms.Padding(3);
            this.labelSets.Name = "labelSets";
            this.labelSets.Size = new System.Drawing.Size(0, 13);
            this.labelSets.TabIndex = 9;
            // 
            // labelHeaderSets
            // 
            this.labelHeaderSets.AutoSize = true;
            this.labelHeaderSets.Location = new System.Drawing.Point(6, 95);
            this.labelHeaderSets.Margin = new System.Windows.Forms.Padding(3);
            this.labelHeaderSets.Name = "labelHeaderSets";
            this.labelHeaderSets.Size = new System.Drawing.Size(31, 13);
            this.labelHeaderSets.TabIndex = 8;
            this.labelHeaderSets.Text = "Sets:";
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(72, 76);
            this.labelGroup.Margin = new System.Windows.Forms.Padding(3);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(0, 13);
            this.labelGroup.TabIndex = 7;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(72, 19);
            this.labelName.Margin = new System.Windows.Forms.Padding(3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 13);
            this.labelName.TabIndex = 4;
            // 
            // labelHeaderGroup
            // 
            this.labelHeaderGroup.AutoSize = true;
            this.labelHeaderGroup.Location = new System.Drawing.Point(6, 76);
            this.labelHeaderGroup.Margin = new System.Windows.Forms.Padding(3);
            this.labelHeaderGroup.Name = "labelHeaderGroup";
            this.labelHeaderGroup.Size = new System.Drawing.Size(39, 13);
            this.labelHeaderGroup.TabIndex = 3;
            this.labelHeaderGroup.Text = "Group:";
            // 
            // labelHeaderArguments
            // 
            this.labelHeaderArguments.AutoSize = true;
            this.labelHeaderArguments.Location = new System.Drawing.Point(6, 57);
            this.labelHeaderArguments.Margin = new System.Windows.Forms.Padding(3);
            this.labelHeaderArguments.Name = "labelHeaderArguments";
            this.labelHeaderArguments.Size = new System.Drawing.Size(60, 13);
            this.labelHeaderArguments.TabIndex = 2;
            this.labelHeaderArguments.Text = "Arguments:";
            // 
            // labelHeaderPath
            // 
            this.labelHeaderPath.AutoSize = true;
            this.labelHeaderPath.Location = new System.Drawing.Point(6, 38);
            this.labelHeaderPath.Margin = new System.Windows.Forms.Padding(3);
            this.labelHeaderPath.Name = "labelHeaderPath";
            this.labelHeaderPath.Size = new System.Drawing.Size(32, 13);
            this.labelHeaderPath.TabIndex = 1;
            this.labelHeaderPath.Text = "Path:";
            // 
            // labelHeaderName
            // 
            this.labelHeaderName.AutoSize = true;
            this.labelHeaderName.Location = new System.Drawing.Point(6, 19);
            this.labelHeaderName.Margin = new System.Windows.Forms.Padding(3);
            this.labelHeaderName.Name = "labelHeaderName";
            this.labelHeaderName.Size = new System.Drawing.Size(38, 13);
            this.labelHeaderName.TabIndex = 0;
            this.labelHeaderName.Text = "Name:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(734, 40);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(734, 69);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(734, 335);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripSystemTray;
            this.notifyIcon.Icon = ((System.Drawing.Icon) (resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Process Controller";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseUp);
            // 
            // contextMenuStripSystemTray
            // 
            this.contextMenuStripSystemTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSystemTrayStartSingle,
            this.toolStripMenuItemSystemTrayStartBySet,
            this.toolStripSeparator3,
            this.toolStripMenuItemSystemTrayStopSingle,
            this.toolStripMenuItemSystemTrayStopBySet,
            this.toolStripSeparator2,
            this.toolStripMenuItemSystemTrayOptions,
            this.toolStripSeparator4,
            this.toolStripMenuItemSystemTrayRestoreMinimize,
            this.toolStripMenuItemSystemTrayExit});
            this.contextMenuStripSystemTray.Name = "contextMenuStripSystemTray";
            this.contextMenuStripSystemTray.Size = new System.Drawing.Size(134, 176);
            // 
            // toolStripMenuItemSystemTrayStartSingle
            // 
            this.toolStripMenuItemSystemTrayStartSingle.Name = "toolStripMenuItemSystemTrayStartSingle";
            this.toolStripMenuItemSystemTrayStartSingle.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayStartSingle.Text = "Start Single";
            // 
            // toolStripMenuItemSystemTrayStartBySet
            // 
            this.toolStripMenuItemSystemTrayStartBySet.Name = "toolStripMenuItemSystemTrayStartBySet";
            this.toolStripMenuItemSystemTrayStartBySet.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayStartBySet.Text = "Start By Set";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripMenuItemSystemTrayStopSingle
            // 
            this.toolStripMenuItemSystemTrayStopSingle.Name = "toolStripMenuItemSystemTrayStopSingle";
            this.toolStripMenuItemSystemTrayStopSingle.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayStopSingle.Text = "Stop Single";
            // 
            // toolStripMenuItemSystemTrayStopBySet
            // 
            this.toolStripMenuItemSystemTrayStopBySet.Name = "toolStripMenuItemSystemTrayStopBySet";
            this.toolStripMenuItemSystemTrayStopBySet.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayStopBySet.Text = "Stop By Set";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripMenuItemSystemTrayOptions
            // 
            this.toolStripMenuItemSystemTrayOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows});
            this.toolStripMenuItemSystemTrayOptions.Name = "toolStripMenuItemSystemTrayOptions";
            this.toolStripMenuItemSystemTrayOptions.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayOptions.Text = "Options";
            // 
            // toolStripMenuItemSystemTrayOptionsStartWithWindows
            // 
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Name = "toolStripMenuItemSystemTrayOptionsStartWithWindows";
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Text = "Start With Windows";
            this.toolStripMenuItemSystemTrayOptionsStartWithWindows.Click += new System.EventHandler(this.toolStripMenuItemSystemTrayOptionsStartWithWindows_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripMenuItemSystemTrayRestoreMinimize
            // 
            this.toolStripMenuItemSystemTrayRestoreMinimize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItemSystemTrayRestoreMinimize.Name = "toolStripMenuItemSystemTrayRestoreMinimize";
            this.toolStripMenuItemSystemTrayRestoreMinimize.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayRestoreMinimize.Text = "Restore";
            this.toolStripMenuItemSystemTrayRestoreMinimize.Click += new System.EventHandler(this.toolStripMenuItemSystemTrayRestoreMinimize_Click);
            // 
            // toolStripMenuItemSystemTrayExit
            // 
            this.toolStripMenuItemSystemTrayExit.Name = "toolStripMenuItemSystemTrayExit";
            this.toolStripMenuItemSystemTrayExit.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItemSystemTrayExit.Text = "Exit";
            this.toolStripMenuItemSystemTrayExit.Click += new System.EventHandler(this.toolStripMenuItemSystemTrayExit_Click);
            // 
            // contextMenuStripApplicationList
            // 
            this.contextMenuStripApplicationList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemApplicationListStart,
            this.toolStripMenuItemApplicationListStop,
            this.toolStripSeparator1,
            this.toolStripMenuItemApplicationListEdit,
            this.toolStripMenuItemApplicationListRemove});
            this.contextMenuStripApplicationList.Name = "contextMenuStripApplicationList";
            this.contextMenuStripApplicationList.Size = new System.Drawing.Size(118, 98);
            // 
            // toolStripMenuItemApplicationListStart
            // 
            this.toolStripMenuItemApplicationListStart.Name = "toolStripMenuItemApplicationListStart";
            this.toolStripMenuItemApplicationListStart.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemApplicationListStart.Text = "Start";
            this.toolStripMenuItemApplicationListStart.Click += new System.EventHandler(this.toolStripMenuItemApplicationListStart_Click);
            // 
            // toolStripMenuItemApplicationListStop
            // 
            this.toolStripMenuItemApplicationListStop.Name = "toolStripMenuItemApplicationListStop";
            this.toolStripMenuItemApplicationListStop.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemApplicationListStop.Text = "Stop";
            this.toolStripMenuItemApplicationListStop.Click += new System.EventHandler(this.toolStripMenuItemApplicationListStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(114, 6);
            // 
            // toolStripMenuItemApplicationListEdit
            // 
            this.toolStripMenuItemApplicationListEdit.Name = "toolStripMenuItemApplicationListEdit";
            this.toolStripMenuItemApplicationListEdit.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemApplicationListEdit.Text = "Edit";
            this.toolStripMenuItemApplicationListEdit.Click += new System.EventHandler(this.toolStripMenuItemApplicationListEdit_Click);
            // 
            // toolStripMenuItemApplicationListRemove
            // 
            this.toolStripMenuItemApplicationListRemove.Name = "toolStripMenuItemApplicationListRemove";
            this.toolStripMenuItemApplicationListRemove.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemApplicationListRemove.Text = "Remove";
            this.toolStripMenuItemApplicationListRemove.Click += new System.EventHandler(this.toolStripMenuItemApplicationListRemove_Click);
            // 
            // comboBoxSets
            // 
            this.comboBoxSets.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSets.FormattingEnabled = true;
            this.comboBoxSets.Location = new System.Drawing.Point(12, 13);
            this.comboBoxSets.Name = "comboBoxSets";
            this.comboBoxSets.Size = new System.Drawing.Size(716, 21);
            this.comboBoxSets.TabIndex = 5;
            this.comboBoxSets.SelectedIndexChanged += new System.EventHandler(this.comboBoxSets_SelectedIndexChanged);
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Location = new System.Drawing.Point(734, 211);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 7;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.Location = new System.Drawing.Point(734, 182);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(75, 23);
            this.buttonImport.TabIndex = 6;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 370);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.comboBoxSets);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxSelectedApplication);
            this.Controls.Add(this.listViewApplications);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Controller";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.groupBoxSelectedApplication.ResumeLayout(false);
            this.groupBoxSelectedApplication.PerformLayout();
            this.contextMenuStripSystemTray.ResumeLayout(false);
            this.contextMenuStripApplicationList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewApplications;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderPath;
        private System.Windows.Forms.ColumnHeader columnHeaderArguments;
        private System.Windows.Forms.GroupBox groupBoxSelectedApplication;
        private System.Windows.Forms.Label labelHeaderGroup;
        private System.Windows.Forms.Label labelHeaderArguments;
        private System.Windows.Forms.Label labelHeaderPath;
        private System.Windows.Forms.Label labelHeaderName;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayRestoreMinimize;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayExit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripApplicationList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemApplicationListStart;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemApplicationListStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemApplicationListEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemApplicationListRemove;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ComboBox comboBoxSets;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayStartSingle;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayStartBySet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayStopSingle;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayStopBySet;
        private System.Windows.Forms.Label labelSets;
        private System.Windows.Forms.Label labelHeaderSets;
        private System.Windows.Forms.ColumnHeader columnHeaderGroup;
        private System.Windows.Forms.TextBox textBoxArguments;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSystemTrayOptionsStartWithWindows;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImport;
    }
}

