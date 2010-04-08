namespace XMLDBViewer
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
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.treeViewDatabase = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTable = new System.Windows.Forms.TabPage();
            this.splitContainerTable = new System.Windows.Forms.SplitContainer();
            this.listViewTableDetails = new System.Windows.Forms.ListView();
            this.columnHeaderImage = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderOrdinal = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumnName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDataType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDefaultValue = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMaxLength = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAllowsNull = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderUnique = new System.Windows.Forms.ColumnHeader();
            this.tabPageQuery = new System.Windows.Forms.TabPage();
            this.splitContainerQuery = new System.Windows.Forms.SplitContainer();
            this.toolStripContainerQuery = new System.Windows.Forms.ToolStripContainer();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.toolStripQuery = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonQueryOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonQuerySave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonQueryClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxQueryHistory = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonQueryCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonQueryExecute = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewQuery = new System.Windows.Forms.DataGridView();
            this.contextMenuStripTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemTableCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTablePaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTableInsertGuid = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemTableDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemTableFindIn = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMagicSearch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripDatabase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDatabaseCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemDatabaseRawFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDatabaseRawFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDatabaseRawFileEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStandard = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStandardOpenDatabase = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStandardSaveTable = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStandardRefreshTable = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStandardAdvancedSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStandardShowTableDetails = new System.Windows.Forms.ToolStripButton();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemMainDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseRootPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseRootPathOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemMainDatabaseRelations = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseRelationsManage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainDatabaseRelationsAssociateWith = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableSearchMagic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableSearchAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableRawFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableRawFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainTableRawFileEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemMainTableShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainQueryOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainQuerySave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainQueryClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainQueryHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemMainQueryCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainQueryExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMainAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainerMain = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTableLastModified = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRelationSet = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewTable)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageTable.SuspendLayout();
            this.splitContainerTable.Panel1.SuspendLayout();
            this.splitContainerTable.Panel2.SuspendLayout();
            this.splitContainerTable.SuspendLayout();
            this.tabPageQuery.SuspendLayout();
            this.splitContainerQuery.Panel1.SuspendLayout();
            this.splitContainerQuery.Panel2.SuspendLayout();
            this.splitContainerQuery.SuspendLayout();
            this.toolStripContainerQuery.ContentPanel.SuspendLayout();
            this.toolStripContainerQuery.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerQuery.SuspendLayout();
            this.toolStripQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewQuery)).BeginInit();
            this.contextMenuStripTable.SuspendLayout();
            this.contextMenuStripDatabase.SuspendLayout();
            this.toolStripStandard.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripContainerMain.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainerMain.ContentPanel.SuspendLayout();
            this.toolStripContainerMain.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridViewTable.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.ReadOnly = true;
            this.dataGridViewTable.Size = new System.Drawing.Size(1115, 436);
            this.dataGridViewTable.TabIndex = 0;
            this.dataGridViewTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridViewTable.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseUp);
            this.dataGridViewTable.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserAddedRow);
            this.dataGridViewTable.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridViewTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView_UserDeletedRow);
            this.dataGridViewTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewTable_CellFormatting);
            this.dataGridViewTable.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            // 
            // treeViewDatabase
            // 
            this.treeViewDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDatabase.HideSelection = false;
            this.treeViewDatabase.ImageIndex = 0;
            this.treeViewDatabase.ImageList = this.imageList;
            this.treeViewDatabase.Location = new System.Drawing.Point(0, 0);
            this.treeViewDatabase.Name = "treeViewDatabase";
            this.treeViewDatabase.SelectedImageIndex = 0;
            this.treeViewDatabase.Size = new System.Drawing.Size(256, 675);
            this.treeViewDatabase.TabIndex = 0;
            this.treeViewDatabase.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDatabase_AfterCollapse);
            this.treeViewDatabase.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDatabase_AfterSelect);
            this.treeViewDatabase.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDatabase_NodeMouseClick);
            this.treeViewDatabase.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewDatabase_BeforeSelect);
            this.treeViewDatabase.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDatabase_AfterExpand);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "database-16.ico");
            this.imageList.Images.SetKeyName(1, "table-16.ico");
            this.imageList.Images.SetKeyName(2, "table-locked-16.ico");
            this.imageList.Images.SetKeyName(3, "column-16.ico");
            this.imageList.Images.SetKeyName(4, "key-16.ico");
            this.imageList.Images.SetKeyName(5, "open-database-16.ico");
            this.imageList.Images.SetKeyName(6, "save-table-16.ico");
            this.imageList.Images.SetKeyName(7, "refresh-table-16.ico");
            this.imageList.Images.SetKeyName(8, "find-16.ico");
            this.imageList.Images.SetKeyName(9, "open-query-16.ico");
            this.imageList.Images.SetKeyName(10, "save-query-16.ico");
            this.imageList.Images.SetKeyName(11, "query-check-16.ico");
            this.imageList.Images.SetKeyName(12, "query-execute-16.ico");
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.treeViewDatabase);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControl);
            this.splitContainerMain.Size = new System.Drawing.Size(1383, 675);
            this.splitContainerMain.SplitterDistance = 256;
            this.splitContainerMain.TabIndex = 4;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageTable);
            this.tabControl.Controls.Add(this.tabPageQuery);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1125, 676);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageTable
            // 
            this.tabPageTable.Controls.Add(this.splitContainerTable);
            this.tabPageTable.Location = new System.Drawing.Point(4, 22);
            this.tabPageTable.Name = "tabPageTable";
            this.tabPageTable.Padding = new System.Windows.Forms.Padding(0, 2, 2, 1);
            this.tabPageTable.Size = new System.Drawing.Size(1117, 650);
            this.tabPageTable.TabIndex = 0;
            this.tabPageTable.Text = "Table";
            this.tabPageTable.UseVisualStyleBackColor = true;
            // 
            // splitContainerTable
            // 
            this.splitContainerTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTable.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerTable.Location = new System.Drawing.Point(0, 2);
            this.splitContainerTable.Name = "splitContainerTable";
            this.splitContainerTable.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTable.Panel1
            // 
            this.splitContainerTable.Panel1.Controls.Add(this.dataGridViewTable);
            // 
            // splitContainerTable.Panel2
            // 
            this.splitContainerTable.Panel2.Controls.Add(this.listViewTableDetails);
            this.splitContainerTable.Size = new System.Drawing.Size(1115, 647);
            this.splitContainerTable.SplitterDistance = 436;
            this.splitContainerTable.TabIndex = 10;
            // 
            // listViewTableDetails
            // 
            this.listViewTableDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderImage,
            this.columnHeaderOrdinal,
            this.columnHeaderColumnName,
            this.columnHeaderDataType,
            this.columnHeaderDefaultValue,
            this.columnHeaderMaxLength,
            this.columnHeaderAllowsNull,
            this.columnHeaderUnique});
            this.listViewTableDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTableDetails.FullRowSelect = true;
            this.listViewTableDetails.GridLines = true;
            this.listViewTableDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewTableDetails.HideSelection = false;
            this.listViewTableDetails.Location = new System.Drawing.Point(0, 0);
            this.listViewTableDetails.MultiSelect = false;
            this.listViewTableDetails.Name = "listViewTableDetails";
            this.listViewTableDetails.Size = new System.Drawing.Size(1115, 207);
            this.listViewTableDetails.SmallImageList = this.imageList;
            this.listViewTableDetails.TabIndex = 0;
            this.listViewTableDetails.UseCompatibleStateImageBehavior = false;
            this.listViewTableDetails.View = System.Windows.Forms.View.Details;
            this.listViewTableDetails.SelectedIndexChanged += new System.EventHandler(this.listViewTableDetails_SelectedIndexChanged);
            // 
            // columnHeaderImage
            // 
            this.columnHeaderImage.Text = "";
            this.columnHeaderImage.Width = 100;
            // 
            // columnHeaderOrdinal
            // 
            this.columnHeaderOrdinal.Text = "#";
            this.columnHeaderOrdinal.Width = 100;
            // 
            // columnHeaderColumnName
            // 
            this.columnHeaderColumnName.Text = "Column Name";
            this.columnHeaderColumnName.Width = 100;
            // 
            // columnHeaderDataType
            // 
            this.columnHeaderDataType.Text = "Data Type";
            this.columnHeaderDataType.Width = 100;
            // 
            // columnHeaderDefaultValue
            // 
            this.columnHeaderDefaultValue.Text = "Default Value";
            this.columnHeaderDefaultValue.Width = 100;
            // 
            // columnHeaderMaxLength
            // 
            this.columnHeaderMaxLength.Text = "Max Length";
            this.columnHeaderMaxLength.Width = 100;
            // 
            // columnHeaderAllowsNull
            // 
            this.columnHeaderAllowsNull.Text = "Allows Null";
            this.columnHeaderAllowsNull.Width = 100;
            // 
            // columnHeaderUnique
            // 
            this.columnHeaderUnique.Text = "Unique";
            this.columnHeaderUnique.Width = 100;
            // 
            // tabPageQuery
            // 
            this.tabPageQuery.Controls.Add(this.splitContainerQuery);
            this.tabPageQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuery.Name = "tabPageQuery";
            this.tabPageQuery.Padding = new System.Windows.Forms.Padding(0, 2, 2, 1);
            this.tabPageQuery.Size = new System.Drawing.Size(1117, 650);
            this.tabPageQuery.TabIndex = 1;
            this.tabPageQuery.Text = "Query";
            this.tabPageQuery.UseVisualStyleBackColor = true;
            // 
            // splitContainerQuery
            // 
            this.splitContainerQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerQuery.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerQuery.Location = new System.Drawing.Point(0, 2);
            this.splitContainerQuery.Name = "splitContainerQuery";
            this.splitContainerQuery.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerQuery.Panel1
            // 
            this.splitContainerQuery.Panel1.Controls.Add(this.toolStripContainerQuery);
            // 
            // splitContainerQuery.Panel2
            // 
            this.splitContainerQuery.Panel2.Controls.Add(this.dataGridViewQuery);
            this.splitContainerQuery.Size = new System.Drawing.Size(1115, 647);
            this.splitContainerQuery.SplitterDistance = 240;
            this.splitContainerQuery.TabIndex = 0;
            // 
            // toolStripContainerQuery
            // 
            this.toolStripContainerQuery.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainerQuery.ContentPanel
            // 
            this.toolStripContainerQuery.ContentPanel.Controls.Add(this.textBoxQuery);
            this.toolStripContainerQuery.ContentPanel.Size = new System.Drawing.Size(1115, 215);
            this.toolStripContainerQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerQuery.LeftToolStripPanelVisible = false;
            this.toolStripContainerQuery.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerQuery.Name = "toolStripContainerQuery";
            this.toolStripContainerQuery.RightToolStripPanelVisible = false;
            this.toolStripContainerQuery.Size = new System.Drawing.Size(1115, 240);
            this.toolStripContainerQuery.TabIndex = 1;
            this.toolStripContainerQuery.Text = "toolStripContainerQuery";
            // 
            // toolStripContainerQuery.TopToolStripPanel
            // 
            this.toolStripContainerQuery.TopToolStripPanel.Controls.Add(this.toolStripQuery);
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxQuery.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.textBoxQuery.Location = new System.Drawing.Point(0, 0);
            this.textBoxQuery.Multiline = true;
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxQuery.Size = new System.Drawing.Size(1115, 215);
            this.textBoxQuery.TabIndex = 0;
            this.textBoxQuery.TextChanged += new System.EventHandler(this.textBoxQuery_TextChanged);
            // 
            // toolStripQuery
            // 
            this.toolStripQuery.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonQueryOpen,
            this.toolStripButtonQuerySave,
            this.toolStripButtonQueryClear,
            this.toolStripSeparator5,
            this.toolStripComboBoxQueryHistory,
            this.toolStripSeparator8,
            this.toolStripButtonQueryCheck,
            this.toolStripButtonQueryExecute});
            this.toolStripQuery.Location = new System.Drawing.Point(3, 0);
            this.toolStripQuery.Name = "toolStripQuery";
            this.toolStripQuery.Size = new System.Drawing.Size(668, 25);
            this.toolStripQuery.TabIndex = 3;
            // 
            // toolStripButtonQueryOpen
            // 
            this.toolStripButtonQueryOpen.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonQueryOpen.Image")));
            this.toolStripButtonQueryOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQueryOpen.Name = "toolStripButtonQueryOpen";
            this.toolStripButtonQueryOpen.Size = new System.Drawing.Size(56, 22);
            this.toolStripButtonQueryOpen.Text = "Open";
            this.toolStripButtonQueryOpen.Click += new System.EventHandler(this.toolStripButtonQueryOpen_Click);
            // 
            // toolStripButtonQuerySave
            // 
            this.toolStripButtonQuerySave.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonQuerySave.Image")));
            this.toolStripButtonQuerySave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQuerySave.Name = "toolStripButtonQuerySave";
            this.toolStripButtonQuerySave.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonQuerySave.Text = "Save";
            this.toolStripButtonQuerySave.Click += new System.EventHandler(this.toolStripButtonQuerySave_Click);
            // 
            // toolStripButtonQueryClear
            // 
            this.toolStripButtonQueryClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonQueryClear.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonQueryClear.Image")));
            this.toolStripButtonQueryClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQueryClear.Name = "toolStripButtonQueryClear";
            this.toolStripButtonQueryClear.Size = new System.Drawing.Size(38, 22);
            this.toolStripButtonQueryClear.Text = "Clear";
            this.toolStripButtonQueryClear.Click += new System.EventHandler(this.toolStripButtonQueryClear_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBoxQueryHistory
            // 
            this.toolStripComboBoxQueryHistory.AutoSize = false;
            this.toolStripComboBoxQueryHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxQueryHistory.DropDownWidth = 300;
            this.toolStripComboBoxQueryHistory.Name = "toolStripComboBoxQueryHistory";
            this.toolStripComboBoxQueryHistory.Size = new System.Drawing.Size(300, 23);
            this.toolStripComboBoxQueryHistory.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxQueryHistory_SelectedIndexChanged);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonQueryCheck
            // 
            this.toolStripButtonQueryCheck.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonQueryCheck.Image")));
            this.toolStripButtonQueryCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQueryCheck.Name = "toolStripButtonQueryCheck";
            this.toolStripButtonQueryCheck.Size = new System.Drawing.Size(95, 22);
            this.toolStripButtonQueryCheck.Text = "Check Query";
            this.toolStripButtonQueryCheck.Click += new System.EventHandler(this.toolStripButtonQueryCheck_Click);
            // 
            // toolStripButtonQueryExecute
            // 
            this.toolStripButtonQueryExecute.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonQueryExecute.Image")));
            this.toolStripButtonQueryExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonQueryExecute.Name = "toolStripButtonQueryExecute";
            this.toolStripButtonQueryExecute.Size = new System.Drawing.Size(102, 22);
            this.toolStripButtonQueryExecute.Text = "Execute Query";
            this.toolStripButtonQueryExecute.Click += new System.EventHandler(this.toolStripButtonQueryExecute_Click);
            // 
            // dataGridViewQuery
            // 
            this.dataGridViewQuery.AllowUserToAddRows = false;
            this.dataGridViewQuery.AllowUserToDeleteRows = false;
            this.dataGridViewQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewQuery.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewQuery.Name = "dataGridViewQuery";
            this.dataGridViewQuery.ReadOnly = true;
            this.dataGridViewQuery.Size = new System.Drawing.Size(1115, 403);
            this.dataGridViewQuery.TabIndex = 0;
            this.dataGridViewQuery.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewQuery_CellMouseUp);
            this.dataGridViewQuery.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewQuery_CellMouseDown);
            this.dataGridViewQuery.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewQuery_CellFormatting);
            this.dataGridViewQuery.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewQuery_DataError);
            // 
            // contextMenuStripTable
            // 
            this.contextMenuStripTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTableCopy,
            this.toolStripMenuItemTablePaste,
            this.toolStripMenuItemTableInsertGuid,
            this.toolStripSeparator1,
            this.toolStripMenuItemTableDelete,
            this.toolStripSeparator2,
            this.toolStripMenuItemTableFindIn});
            this.contextMenuStripTable.Name = "contextMenuStrip";
            this.contextMenuStripTable.Size = new System.Drawing.Size(161, 126);
            // 
            // toolStripMenuItemTableCopy
            // 
            this.toolStripMenuItemTableCopy.Name = "toolStripMenuItemTableCopy";
            this.toolStripMenuItemTableCopy.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemTableCopy.Text = "Copy";
            this.toolStripMenuItemTableCopy.Click += new System.EventHandler(this.toolStripMenuItemTableCopy_Click);
            // 
            // toolStripMenuItemTablePaste
            // 
            this.toolStripMenuItemTablePaste.Name = "toolStripMenuItemTablePaste";
            this.toolStripMenuItemTablePaste.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemTablePaste.Text = "Paste";
            this.toolStripMenuItemTablePaste.Click += new System.EventHandler(this.toolStripMenuItemTablePaste_Click);
            // 
            // toolStripMenuItemTableInsertGuid
            // 
            this.toolStripMenuItemTableInsertGuid.Name = "toolStripMenuItemTableInsertGuid";
            this.toolStripMenuItemTableInsertGuid.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemTableInsertGuid.Text = "Insert New GUID";
            this.toolStripMenuItemTableInsertGuid.Click += new System.EventHandler(this.toolStripMenuItemTableInsertGuid_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItemTableDelete
            // 
            this.toolStripMenuItemTableDelete.Name = "toolStripMenuItemTableDelete";
            this.toolStripMenuItemTableDelete.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemTableDelete.Text = "Delete";
            this.toolStripMenuItemTableDelete.Click += new System.EventHandler(this.toolStripMenuItemTableDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // toolStripMenuItemTableFindIn
            // 
            this.toolStripMenuItemTableFindIn.Name = "toolStripMenuItemTableFindIn";
            this.toolStripMenuItemTableFindIn.Size = new System.Drawing.Size(160, 22);
            this.toolStripMenuItemTableFindIn.Text = "Find In...";
            // 
            // contextMenuStripMagicSearch
            // 
            this.contextMenuStripMagicSearch.Name = "contextMenuStripMagicSearch";
            this.contextMenuStripMagicSearch.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStripDatabase
            // 
            this.contextMenuStripDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatabaseCopy,
            this.toolStripSeparator9,
            this.toolStripMenuItemDatabaseRawFile});
            this.contextMenuStripDatabase.Name = "contextMenuStripDatabase";
            this.contextMenuStripDatabase.Size = new System.Drawing.Size(118, 54);
            // 
            // toolStripMenuItemDatabaseCopy
            // 
            this.toolStripMenuItemDatabaseCopy.Name = "toolStripMenuItemDatabaseCopy";
            this.toolStripMenuItemDatabaseCopy.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemDatabaseCopy.Text = "Copy";
            this.toolStripMenuItemDatabaseCopy.Click += new System.EventHandler(this.toolStripMenuItemDatabaseCopy_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(114, 6);
            // 
            // toolStripMenuItemDatabaseRawFile
            // 
            this.toolStripMenuItemDatabaseRawFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDatabaseRawFileOpen,
            this.toolStripMenuItemDatabaseRawFileEdit});
            this.toolStripMenuItemDatabaseRawFile.Name = "toolStripMenuItemDatabaseRawFile";
            this.toolStripMenuItemDatabaseRawFile.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemDatabaseRawFile.Text = "Raw File";
            // 
            // toolStripMenuItemDatabaseRawFileOpen
            // 
            this.toolStripMenuItemDatabaseRawFileOpen.Name = "toolStripMenuItemDatabaseRawFileOpen";
            this.toolStripMenuItemDatabaseRawFileOpen.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemDatabaseRawFileOpen.Text = "Open";
            this.toolStripMenuItemDatabaseRawFileOpen.Click += new System.EventHandler(this.toolStripMenuItemDatabaseRawFileOpen_Click);
            // 
            // toolStripMenuItemDatabaseRawFileEdit
            // 
            this.toolStripMenuItemDatabaseRawFileEdit.Name = "toolStripMenuItemDatabaseRawFileEdit";
            this.toolStripMenuItemDatabaseRawFileEdit.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemDatabaseRawFileEdit.Text = "Edit";
            this.toolStripMenuItemDatabaseRawFileEdit.Click += new System.EventHandler(this.toolStripMenuItemDatabaseRawFileEdit_Click);
            // 
            // toolStripStandard
            // 
            this.toolStripStandard.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripStandard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStandardOpenDatabase,
            this.toolStripSeparator3,
            this.toolStripButtonStandardSaveTable,
            this.toolStripButtonStandardRefreshTable,
            this.toolStripButtonStandardAdvancedSearch,
            this.toolStripButtonStandardShowTableDetails});
            this.toolStripStandard.Location = new System.Drawing.Point(3, 24);
            this.toolStripStandard.Name = "toolStripStandard";
            this.toolStripStandard.Size = new System.Drawing.Size(439, 25);
            this.toolStripStandard.TabIndex = 1;
            this.toolStripStandard.Text = "Standard";
            // 
            // toolStripButtonStandardOpenDatabase
            // 
            this.toolStripButtonStandardOpenDatabase.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonStandardOpenDatabase.Image")));
            this.toolStripButtonStandardOpenDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStandardOpenDatabase.Name = "toolStripButtonStandardOpenDatabase";
            this.toolStripButtonStandardOpenDatabase.Size = new System.Drawing.Size(107, 22);
            this.toolStripButtonStandardOpenDatabase.Text = "Open Database";
            this.toolStripButtonStandardOpenDatabase.Click += new System.EventHandler(this.toolStripButtonStandardOpenDatabase_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonStandardSaveTable
            // 
            this.toolStripButtonStandardSaveTable.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonStandardSaveTable.Image")));
            this.toolStripButtonStandardSaveTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStandardSaveTable.Name = "toolStripButtonStandardSaveTable";
            this.toolStripButtonStandardSaveTable.Size = new System.Drawing.Size(83, 22);
            this.toolStripButtonStandardSaveTable.Text = "Save Table";
            this.toolStripButtonStandardSaveTable.Click += new System.EventHandler(this.toolStripButtonStandardSaveTable_Click);
            // 
            // toolStripButtonStandardRefreshTable
            // 
            this.toolStripButtonStandardRefreshTable.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonStandardRefreshTable.Image")));
            this.toolStripButtonStandardRefreshTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStandardRefreshTable.Name = "toolStripButtonStandardRefreshTable";
            this.toolStripButtonStandardRefreshTable.Size = new System.Drawing.Size(98, 22);
            this.toolStripButtonStandardRefreshTable.Text = "Refresh Table";
            this.toolStripButtonStandardRefreshTable.Click += new System.EventHandler(this.toolStripButtonStandardRefreshTable_Click);
            // 
            // toolStripButtonStandardAdvancedSearch
            // 
            this.toolStripButtonStandardAdvancedSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStandardAdvancedSearch.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonStandardAdvancedSearch.Image")));
            this.toolStripButtonStandardAdvancedSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStandardAdvancedSearch.Name = "toolStripButtonStandardAdvancedSearch";
            this.toolStripButtonStandardAdvancedSearch.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonStandardAdvancedSearch.Text = "Advanced Search";
            this.toolStripButtonStandardAdvancedSearch.Click += new System.EventHandler(this.toolStripButtonStandardAdvancedSearch_Click);
            // 
            // toolStripButtonStandardShowTableDetails
            // 
            this.toolStripButtonStandardShowTableDetails.CheckOnClick = true;
            this.toolStripButtonStandardShowTableDetails.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStandardShowTableDetails.Image = ((System.Drawing.Image) (resources.GetObject("toolStripButtonStandardShowTableDetails.Image")));
            this.toolStripButtonStandardShowTableDetails.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStandardShowTableDetails.Name = "toolStripButtonStandardShowTableDetails";
            this.toolStripButtonStandardShowTableDetails.Size = new System.Drawing.Size(110, 22);
            this.toolStripButtonStandardShowTableDetails.Text = "Show Table Details";
            this.toolStripButtonStandardShowTableDetails.Click += new System.EventHandler(this.toolStripButtonStandardShowTableDetails_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainDatabase,
            this.toolStripMenuItemMainTable,
            this.toolStripMenuItemMainQuery,
            this.toolStripMenuItemMainOptions,
            this.toolStripMenuItemMainAbout});
            this.menuStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1389, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";
            // 
            // toolStripMenuItemMainDatabase
            // 
            this.toolStripMenuItemMainDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainDatabaseOpen,
            this.toolStripMenuItemMainDatabaseRecent,
            this.toolStripMenuItemMainDatabaseClose,
            this.toolStripMenuItemMainDatabaseRootPath,
            this.toolStripSeparator4,
            this.toolStripMenuItemMainDatabaseRelations});
            this.toolStripMenuItemMainDatabase.Name = "toolStripMenuItemMainDatabase";
            this.toolStripMenuItemMainDatabase.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItemMainDatabase.Text = "Database";
            // 
            // toolStripMenuItemMainDatabaseOpen
            // 
            this.toolStripMenuItemMainDatabaseOpen.Name = "toolStripMenuItemMainDatabaseOpen";
            this.toolStripMenuItemMainDatabaseOpen.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemMainDatabaseOpen.Text = "Open...";
            this.toolStripMenuItemMainDatabaseOpen.Click += new System.EventHandler(this.toolStripMenuItemMainDatabaseOpen_Click);
            // 
            // toolStripMenuItemMainDatabaseRecent
            // 
            this.toolStripMenuItemMainDatabaseRecent.Name = "toolStripMenuItemMainDatabaseRecent";
            this.toolStripMenuItemMainDatabaseRecent.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemMainDatabaseRecent.Text = "Recent";
            // 
            // toolStripMenuItemMainDatabaseClose
            // 
            this.toolStripMenuItemMainDatabaseClose.Name = "toolStripMenuItemMainDatabaseClose";
            this.toolStripMenuItemMainDatabaseClose.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemMainDatabaseClose.Text = "Close";
            this.toolStripMenuItemMainDatabaseClose.Click += new System.EventHandler(this.toolStripMenuItemMainDatabaseClose_Click);
            // 
            // toolStripMenuItemMainDatabaseRootPath
            // 
            this.toolStripMenuItemMainDatabaseRootPath.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainDatabaseRootPathOpen});
            this.toolStripMenuItemMainDatabaseRootPath.Name = "toolStripMenuItemMainDatabaseRootPath";
            this.toolStripMenuItemMainDatabaseRootPath.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemMainDatabaseRootPath.Text = "Root Path";
            // 
            // toolStripMenuItemMainDatabaseRootPathOpen
            // 
            this.toolStripMenuItemMainDatabaseRootPathOpen.Name = "toolStripMenuItemMainDatabaseRootPathOpen";
            this.toolStripMenuItemMainDatabaseRootPathOpen.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemMainDatabaseRootPathOpen.Text = "Open";
            this.toolStripMenuItemMainDatabaseRootPathOpen.Click += new System.EventHandler(this.toolStripMenuItemMainDatabaseRootPathOpen_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(123, 6);
            // 
            // toolStripMenuItemMainDatabaseRelations
            // 
            this.toolStripMenuItemMainDatabaseRelations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainDatabaseRelationsManage,
            this.toolStripMenuItemMainDatabaseRelationsAssociateWith});
            this.toolStripMenuItemMainDatabaseRelations.Name = "toolStripMenuItemMainDatabaseRelations";
            this.toolStripMenuItemMainDatabaseRelations.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemMainDatabaseRelations.Text = "Relations";
            // 
            // toolStripMenuItemMainDatabaseRelationsManage
            // 
            this.toolStripMenuItemMainDatabaseRelationsManage.Name = "toolStripMenuItemMainDatabaseRelationsManage";
            this.toolStripMenuItemMainDatabaseRelationsManage.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItemMainDatabaseRelationsManage.Text = "Manage...";
            this.toolStripMenuItemMainDatabaseRelationsManage.Click += new System.EventHandler(this.toolStripMenuItemMainDatabaseRelationsManage_Click);
            // 
            // toolStripMenuItemMainDatabaseRelationsAssociateWith
            // 
            this.toolStripMenuItemMainDatabaseRelationsAssociateWith.Name = "toolStripMenuItemMainDatabaseRelationsAssociateWith";
            this.toolStripMenuItemMainDatabaseRelationsAssociateWith.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItemMainDatabaseRelationsAssociateWith.Text = "Associate With...";
            this.toolStripMenuItemMainDatabaseRelationsAssociateWith.Click += new System.EventHandler(this.toolStripMenuItemMainDatabaseRelationsAssociateWith_Click);
            // 
            // toolStripMenuItemMainTable
            // 
            this.toolStripMenuItemMainTable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainTableSave,
            this.toolStripMenuItemMainTableRefresh,
            this.toolStripMenuItemMainTableSearch,
            this.toolStripMenuItemMainTableRawFile,
            this.toolStripSeparator6,
            this.toolStripMenuItemMainTableShowDetails});
            this.toolStripMenuItemMainTable.Name = "toolStripMenuItemMainTable";
            this.toolStripMenuItemMainTable.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItemMainTable.Text = "Table";
            // 
            // toolStripMenuItemMainTableSave
            // 
            this.toolStripMenuItemMainTableSave.Name = "toolStripMenuItemMainTableSave";
            this.toolStripMenuItemMainTableSave.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItemMainTableSave.Text = "Save";
            this.toolStripMenuItemMainTableSave.Click += new System.EventHandler(this.toolStripMenuItemMainTableSave_Click);
            // 
            // toolStripMenuItemMainTableRefresh
            // 
            this.toolStripMenuItemMainTableRefresh.Name = "toolStripMenuItemMainTableRefresh";
            this.toolStripMenuItemMainTableRefresh.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItemMainTableRefresh.Text = "Refresh";
            this.toolStripMenuItemMainTableRefresh.Click += new System.EventHandler(this.toolStripMenuItemMainTableRefresh_Click);
            // 
            // toolStripMenuItemMainTableSearch
            // 
            this.toolStripMenuItemMainTableSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainTableSearchMagic,
            this.toolStripMenuItemMainTableSearchAdvanced});
            this.toolStripMenuItemMainTableSearch.Name = "toolStripMenuItemMainTableSearch";
            this.toolStripMenuItemMainTableSearch.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItemMainTableSearch.Text = "Search";
            // 
            // toolStripMenuItemMainTableSearchMagic
            // 
            this.toolStripMenuItemMainTableSearchMagic.Name = "toolStripMenuItemMainTableSearchMagic";
            this.toolStripMenuItemMainTableSearchMagic.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemMainTableSearchMagic.Text = "Magic";
            this.toolStripMenuItemMainTableSearchMagic.Click += new System.EventHandler(this.toolStripMenuItemMainTableSearchMagic_Click);
            // 
            // toolStripMenuItemMainTableSearchAdvanced
            // 
            this.toolStripMenuItemMainTableSearchAdvanced.Name = "toolStripMenuItemMainTableSearchAdvanced";
            this.toolStripMenuItemMainTableSearchAdvanced.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemMainTableSearchAdvanced.Text = "Advanced...";
            this.toolStripMenuItemMainTableSearchAdvanced.Click += new System.EventHandler(this.toolStripMenuItemMainTableSearchAdvanced_Click);
            // 
            // toolStripMenuItemMainTableRawFile
            // 
            this.toolStripMenuItemMainTableRawFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainTableRawFileOpen,
            this.toolStripMenuItemMainTableRawFileEdit});
            this.toolStripMenuItemMainTableRawFile.Name = "toolStripMenuItemMainTableRawFile";
            this.toolStripMenuItemMainTableRawFile.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItemMainTableRawFile.Text = "Raw File";
            // 
            // toolStripMenuItemMainTableRawFileOpen
            // 
            this.toolStripMenuItemMainTableRawFileOpen.Name = "toolStripMenuItemMainTableRawFileOpen";
            this.toolStripMenuItemMainTableRawFileOpen.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemMainTableRawFileOpen.Text = "Open";
            this.toolStripMenuItemMainTableRawFileOpen.Click += new System.EventHandler(this.toolStripMenuItemMainTableRawFileOpen_Click);
            // 
            // toolStripMenuItemMainTableRawFileEdit
            // 
            this.toolStripMenuItemMainTableRawFileEdit.Name = "toolStripMenuItemMainTableRawFileEdit";
            this.toolStripMenuItemMainTableRawFileEdit.Size = new System.Drawing.Size(103, 22);
            this.toolStripMenuItemMainTableRawFileEdit.Text = "Edit";
            this.toolStripMenuItemMainTableRawFileEdit.Click += new System.EventHandler(this.toolStripMenuItemMainTableRawFileEdit_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(138, 6);
            // 
            // toolStripMenuItemMainTableShowDetails
            // 
            this.toolStripMenuItemMainTableShowDetails.CheckOnClick = true;
            this.toolStripMenuItemMainTableShowDetails.Name = "toolStripMenuItemMainTableShowDetails";
            this.toolStripMenuItemMainTableShowDetails.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItemMainTableShowDetails.Text = "Show Details";
            this.toolStripMenuItemMainTableShowDetails.Click += new System.EventHandler(this.toolStripMenuItemMainTableShowDetails_Click);
            // 
            // toolStripMenuItemMainQuery
            // 
            this.toolStripMenuItemMainQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainQueryOpen,
            this.toolStripMenuItemMainQuerySave,
            this.toolStripMenuItemMainQueryClear,
            this.toolStripMenuItemMainQueryHistory,
            this.toolStripSeparator7,
            this.toolStripMenuItemMainQueryCheck,
            this.toolStripMenuItemMainQueryExecute});
            this.toolStripMenuItemMainQuery.Name = "toolStripMenuItemMainQuery";
            this.toolStripMenuItemMainQuery.Size = new System.Drawing.Size(51, 20);
            this.toolStripMenuItemMainQuery.Text = "Query";
            // 
            // toolStripMenuItemMainQueryOpen
            // 
            this.toolStripMenuItemMainQueryOpen.Name = "toolStripMenuItemMainQueryOpen";
            this.toolStripMenuItemMainQueryOpen.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItemMainQueryOpen.Text = "Open...";
            this.toolStripMenuItemMainQueryOpen.Click += new System.EventHandler(this.toolStripMenuItemMainQueryOpen_Click);
            // 
            // toolStripMenuItemMainQuerySave
            // 
            this.toolStripMenuItemMainQuerySave.Name = "toolStripMenuItemMainQuerySave";
            this.toolStripMenuItemMainQuerySave.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItemMainQuerySave.Text = "Save...";
            this.toolStripMenuItemMainQuerySave.Click += new System.EventHandler(this.toolStripMenuItemMainQuerySave_Click);
            // 
            // toolStripMenuItemMainQueryClear
            // 
            this.toolStripMenuItemMainQueryClear.Name = "toolStripMenuItemMainQueryClear";
            this.toolStripMenuItemMainQueryClear.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItemMainQueryClear.Text = "Clear";
            this.toolStripMenuItemMainQueryClear.Click += new System.EventHandler(this.toolStripMenuItemMainQueryClear_Click);
            // 
            // toolStripMenuItemMainQueryHistory
            // 
            this.toolStripMenuItemMainQueryHistory.Name = "toolStripMenuItemMainQueryHistory";
            this.toolStripMenuItemMainQueryHistory.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItemMainQueryHistory.Text = "Recent";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(111, 6);
            // 
            // toolStripMenuItemMainQueryCheck
            // 
            this.toolStripMenuItemMainQueryCheck.Name = "toolStripMenuItemMainQueryCheck";
            this.toolStripMenuItemMainQueryCheck.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItemMainQueryCheck.Text = "Check";
            this.toolStripMenuItemMainQueryCheck.Click += new System.EventHandler(this.toolStripMenuItemMainQueryCheck_Click);
            // 
            // toolStripMenuItemMainQueryExecute
            // 
            this.toolStripMenuItemMainQueryExecute.Name = "toolStripMenuItemMainQueryExecute";
            this.toolStripMenuItemMainQueryExecute.Size = new System.Drawing.Size(114, 22);
            this.toolStripMenuItemMainQueryExecute.Text = "Execute";
            this.toolStripMenuItemMainQueryExecute.Click += new System.EventHandler(this.toolStripMenuItemMainQueryExecute_Click);
            // 
            // toolStripMenuItemMainOptions
            // 
            this.toolStripMenuItemMainOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching,
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable});
            this.toolStripMenuItemMainOptions.Name = "toolStripMenuItemMainOptions";
            this.toolStripMenuItemMainOptions.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItemMainOptions.Text = "Options";
            // 
            // toolStripMenuItemMainOptionsSelectRowsWhenSearching
            // 
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching.CheckOnClick = true;
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching.Name = "toolStripMenuItemMainOptionsSelectRowsWhenSearching";
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching.Text = "Select Rows When Searching";
            this.toolStripMenuItemMainOptionsSelectRowsWhenSearching.Click += new System.EventHandler(this.toolStripMenuItemMainOptionsSelectRowsWhenSearching_Click);
            // 
            // toolStripMenuItemMainOptionsAutoReloadModifiedTable
            // 
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable.CheckOnClick = true;
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable.Name = "toolStripMenuItemMainOptionsAutoReloadModifiedTable";
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable.Size = new System.Drawing.Size(225, 22);
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable.Text = "Auto Reload Modified Table";
            this.toolStripMenuItemMainOptionsAutoReloadModifiedTable.Click += new System.EventHandler(this.toolStripMenuItemMainOptionsAutoReloadModifiedTable_Click);
            // 
            // toolStripMenuItemMainAbout
            // 
            this.toolStripMenuItemMainAbout.Name = "toolStripMenuItemMainAbout";
            this.toolStripMenuItemMainAbout.Size = new System.Drawing.Size(52, 20);
            this.toolStripMenuItemMainAbout.Text = "About";
            this.toolStripMenuItemMainAbout.Click += new System.EventHandler(this.toolStripMenuItemMainAbout_Click);
            // 
            // toolStripContainerMain
            // 
            // 
            // toolStripContainerMain.BottomToolStripPanel
            // 
            this.toolStripContainerMain.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainerMain.ContentPanel
            // 
            this.toolStripContainerMain.ContentPanel.Controls.Add(this.splitContainerMain);
            this.toolStripContainerMain.ContentPanel.Size = new System.Drawing.Size(1389, 681);
            this.toolStripContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerMain.LeftToolStripPanelVisible = false;
            this.toolStripContainerMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerMain.Name = "toolStripContainerMain";
            this.toolStripContainerMain.RightToolStripPanelVisible = false;
            this.toolStripContainerMain.Size = new System.Drawing.Size(1389, 752);
            this.toolStripContainerMain.TabIndex = 0;
            this.toolStripContainerMain.Text = "toolStripContainerMain";
            // 
            // toolStripContainerMain.TopToolStripPanel
            // 
            this.toolStripContainerMain.TopToolStripPanel.Controls.Add(this.menuStripMain);
            this.toolStripContainerMain.TopToolStripPanel.Controls.Add(this.toolStripStandard);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTableLastModified,
            this.toolStripStatusLabelRelationSet});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1389, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripStatusLabelTableLastModified
            // 
            this.toolStripStatusLabelTableLastModified.Name = "toolStripStatusLabelTableLastModified";
            this.toolStripStatusLabelTableLastModified.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelRelationSet
            // 
            this.toolStripStatusLabelRelationSet.Name = "toolStripStatusLabelRelationSet";
            this.toolStripStatusLabelRelationSet.Size = new System.Drawing.Size(1374, 17);
            this.toolStripStatusLabelRelationSet.Spring = true;
            this.toolStripStatusLabelRelationSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 752);
            this.Controls.Add(this.toolStripContainerMain);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XML Database Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewTable)).EndInit();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageTable.ResumeLayout(false);
            this.splitContainerTable.Panel1.ResumeLayout(false);
            this.splitContainerTable.Panel2.ResumeLayout(false);
            this.splitContainerTable.ResumeLayout(false);
            this.tabPageQuery.ResumeLayout(false);
            this.splitContainerQuery.Panel1.ResumeLayout(false);
            this.splitContainerQuery.Panel2.ResumeLayout(false);
            this.splitContainerQuery.ResumeLayout(false);
            this.toolStripContainerQuery.ContentPanel.ResumeLayout(false);
            this.toolStripContainerQuery.ContentPanel.PerformLayout();
            this.toolStripContainerQuery.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerQuery.TopToolStripPanel.PerformLayout();
            this.toolStripContainerQuery.ResumeLayout(false);
            this.toolStripContainerQuery.PerformLayout();
            this.toolStripQuery.ResumeLayout(false);
            this.toolStripQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewQuery)).EndInit();
            this.contextMenuStripTable.ResumeLayout(false);
            this.contextMenuStripDatabase.ResumeLayout(false);
            this.toolStripStandard.ResumeLayout(false);
            this.toolStripStandard.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripContainerMain.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainerMain.BottomToolStripPanel.PerformLayout();
            this.toolStripContainerMain.ContentPanel.ResumeLayout(false);
            this.toolStripContainerMain.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerMain.TopToolStripPanel.PerformLayout();
            this.toolStripContainerMain.ResumeLayout(false);
            this.toolStripContainerMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewTable;
		private System.Windows.Forms.TreeView treeViewDatabase;
		private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripTable;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTableCopy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTablePaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTableDelete;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTableFindIn;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripMagicSearch;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTableInsertGuid;
		private System.Windows.Forms.SplitContainer splitContainerTable;
		private System.Windows.Forms.ListView listViewTableDetails;
		private System.Windows.Forms.ColumnHeader columnHeaderOrdinal;
		private System.Windows.Forms.ColumnHeader columnHeaderColumnName;
		private System.Windows.Forms.ColumnHeader columnHeaderDataType;
		private System.Windows.Forms.ColumnHeader columnHeaderDefaultValue;
		private System.Windows.Forms.ColumnHeader columnHeaderMaxLength;
		private System.Windows.Forms.ColumnHeader columnHeaderAllowsNull;
		private System.Windows.Forms.ColumnHeader columnHeaderUnique;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripDatabase;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabaseRawFile;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabaseRawFileOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabaseRawFileEdit;
		private System.Windows.Forms.ToolStrip toolStripStandard;
		private System.Windows.Forms.MenuStrip menuStripMain;
		private System.Windows.Forms.ToolStripButton toolStripButtonStandardShowTableDetails;
		private System.Windows.Forms.ToolStripButton toolStripButtonStandardOpenDatabase;
		private System.Windows.Forms.ToolStripButton toolStripButtonStandardSaveTable;
		private System.Windows.Forms.ToolStripButton toolStripButtonStandardRefreshTable;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabase;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTable;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainOptions;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableShowDetails;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainOptionsSelectRowsWhenSearching;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainOptionsAutoReloadModifiedTable;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableRefresh;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableRawFile;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableRawFileOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableSave;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableRawFileEdit;
		private System.Windows.Forms.ToolStripContainer toolStripContainerMain;
		private System.Windows.Forms.ColumnHeader columnHeaderImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableSearch;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableSearchMagic;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainTableSearchAdvanced;
		private System.Windows.Forms.ToolStripButton toolStripButtonStandardAdvancedSearch;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainAbout;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTableLastModified;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseRelations;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseRootPath;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseRootPathOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseClose;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageTable;
		private System.Windows.Forms.TabPage tabPageQuery;
		private System.Windows.Forms.SplitContainer splitContainerQuery;
		private System.Windows.Forms.TextBox textBoxQuery;
		private System.Windows.Forms.DataGridView dataGridViewQuery;
		private System.Windows.Forms.ToolStripContainer toolStripContainerQuery;
		private System.Windows.Forms.ToolStrip toolStripQuery;
		private System.Windows.Forms.ToolStripButton toolStripButtonQueryOpen;
		private System.Windows.Forms.ToolStripButton toolStripButtonQuerySave;
		private System.Windows.Forms.ToolStripButton toolStripButtonQueryClear;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton toolStripButtonQueryExecute;
		private System.Windows.Forms.ToolStripButton toolStripButtonQueryCheck;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQuery;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQueryOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQuerySave;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQueryClear;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQueryCheck;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQueryExecute;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripComboBox toolStripComboBoxQueryHistory;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainQueryHistory;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRelationSet;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDatabaseCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseRecent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseRelationsManage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMainDatabaseRelationsAssociateWith;
	}
}

