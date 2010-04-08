using System;
using System.Linq;
using System.Windows.Forms;
using XMLDBViewer.EventArguments;

namespace XMLDBViewer
{
	public partial class MainForm : Form
	{
		private readonly Worker _worker;

		public MainForm()
		{
			DiscardEvents = true;
			InitializeComponent();
			toolStripButtonStandardOpenDatabase.Image = imageList.Images[5];
			toolStripButtonStandardSaveTable.Image = imageList.Images[6];
			toolStripButtonStandardRefreshTable.Image = imageList.Images[7];
			toolStripButtonStandardAdvancedSearch.Image = imageList.Images[8];
			toolStripMenuItemMainDatabaseOpen.Image = imageList.Images[5];
			toolStripMenuItemMainTableSave.Image = imageList.Images[6];
			toolStripMenuItemMainTableRefresh.Image = imageList.Images[7];
			toolStripMenuItemMainTableSearchAdvanced.Image = imageList.Images[8];
			toolStripButtonQueryOpen.Image = imageList.Images[9];
			toolStripButtonQuerySave.Image = imageList.Images[10];
			toolStripButtonQueryCheck.Image = imageList.Images[11];
			toolStripButtonQueryExecute.Image = imageList.Images[12];
			toolStripMenuItemMainQueryOpen.Image = imageList.Images[9];
			toolStripMenuItemMainQuerySave.Image = imageList.Images[10];
			toolStripMenuItemMainQueryCheck.Image = imageList.Images[11];
			toolStripMenuItemMainQueryExecute.Image = imageList.Images[12];
			DiscardEvents = false;
			SelectedTreeNode = null;
			_worker = new Worker(this);
			Text += " (build " + _worker.GetBuildTag() + ")";
			_worker.SaveTableEnabled += _worker_SaveTableEnabled;
			_worker.TableSelected += _worker_TableSelected;
			_worker.TableLastModifiedTimeChanged += _worker_TableLastModifiedTimeChanged;
            _worker.DatabaseRelationSetChanged += _worker_DatabaseRelationSetChanged;
        }

		#region Properties

		private TreeNode SelectedTreeNode { get; set; }

		public bool DiscardEvents { get; set; }

		public ToolStripContainer ToolStripContainer
		{
			get { return toolStripContainerMain; }
		}

		public new MenuStrip MainMenuStrip
		{
			get { return menuStripMain; }
		}

		public ToolStrip StandardToolStrip
		{
			get { return toolStripStandard; }
		}

		public ToolStrip QueryToolStrip
		{
			get { return toolStripQuery; }
		}

		public SplitContainer MainSplitter
		{
			get { return splitContainerMain; }
		}

		public SplitContainer TableSplitter
		{
			get { return splitContainerTable; }
		}

		public SplitContainer QuerySplitter
		{
			get { return splitContainerQuery; }
		}

		public TreeView DatabaseTreeView
		{
			get { return treeViewDatabase; }
		}

		public DataGridView TableDataGridView
		{
			get { return dataGridViewTable; }
		}

		public DataGridView QueryDataGridView
		{
			get { return dataGridViewQuery; }
		}

		public ListView TableDetailsListView
		{
			get { return listViewTableDetails; }
		}

		public ContextMenuStrip TableContextMenu
		{
			get { return contextMenuStripTable; }
		}

		public ContextMenuStrip MagicSearchContextMenu
		{
			get { return contextMenuStripMagicSearch; }
		}

		public TabControl TabControl
		{
			get { return tabControl; }
		}

		#endregion

		#region Custom event handlers

		private void _worker_SaveTableEnabled(object sender, BooleanEventArgs e)
		{
			try
			{
				EnableToolStripItems(e.Value, toolStripButtonStandardSaveTable, toolStripMenuItemMainTableSave);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void _worker_TableSelected(object sender, BooleanEventArgs e)
		{
			try
			{
				EnableToolStripItems(e.Value, toolStripButtonStandardRefreshTable, toolStripMenuItemMainTableRefresh, toolStripMenuItemMainTableRawFile);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void _worker_TableLastModifiedTimeChanged(object sender, StringEventArgs e)
		{
			toolStripStatusLabelTableLastModified.Text = e.Value;
		}

        private void _worker_DatabaseRelationSetChanged(object sender, StringEventArgs e)
        {
            toolStripStatusLabelRelationSet.Text = e.Value;
        }

		#endregion

		#region GUI event handlers

		#region Main form

		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				EnableControls(false);
				_worker.LoadRegistryValues();
				_worker.DisplayTableDetails();
				_worker.UpdateQueryHistory();
			    _worker.UpdateRecentDatabasesMenu();
				CheckToolStripItems(_worker.ShowTableDetails, toolStripButtonStandardShowTableDetails, toolStripMenuItemMainTableShowDetails);
				CheckToolStripItems(_worker.TableAutoReloadEnabled, toolStripMenuItemMainOptionsAutoReloadModifiedTable);
				CheckToolStripItems(_worker.SelectRowsWhenSearching, toolStripMenuItemMainOptionsSelectRowsWhenSearching);
				tabControl.SelectedIndex = (_worker.SelectedTabView == Worker.TabView.Query ? 1 : 0);
				textBoxQuery.Text = _worker.CurrentQuery.Replace("\n", Environment.NewLine);
				if (!string.IsNullOrEmpty(_worker.DatabaseRootFolder))
				{
					_worker.OpenDatabase(_worker.DatabaseRootFolder);
				}
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				_worker.SaveRegistryValues();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			try
			{
				if (_worker.ShowTableDetails)
				{
					ToolStripContentChanged(toolStripStandard);
					_worker.AutoSizeTableListView();
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		protected override void WndProc(ref Message message)
		{
			const int WM_SYSCOMMAND = 0x0112;
			const int SC_MAXIMIZE = 0xf030;
			const int SC_RESTORE = 0xf120;
			if (message.Msg == WM_SYSCOMMAND)
			{
				int command = message.WParam.ToInt32() & 0xfff0;
				if (command == SC_MAXIMIZE || command == SC_RESTORE)
				{
					try
					{
						if (_worker.ShowTableDetails)
						{
							_worker.AutoSizeTableListView();
						}
					}
					catch (Exception ex)
					{
						_worker.ShowError(ex);
					}
				}
			}
			base.WndProc(ref message);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (_worker.SelectedTabView == Worker.TabView.Table)
				{
					switch (e.KeyCode)
					{
						case Keys.F5:
							{
								RefreshTable();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.F3:
							{
								MagicSearch();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.C:
							if (e.Control)
							{
								_worker.CopyClipboardText(-1);
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.V:
							if (e.Control)
							{
								_worker.PasteClipboardText();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.S:
							if (e.Control)
							{
								SaveTable();
								e.SuppressKeyPress = true;
							}
							break;
                        case Keys.Escape:
					        {
					            _worker.ClearTableHighlights();
                                e.SuppressKeyPress = true;
                            }
					        break;
					}
				}
				else if (_worker.SelectedTabView == Worker.TabView.Query)
				{
					switch (e.KeyCode)
					{
						case Keys.F5:
							{
								if (e.Control)
									CheckQuery();
								else
									ExecuteQuery();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.F3:
							{
								MagicSearch();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.S:
							if (e.Control)
							{
								SaveQuery();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.O:
							if (e.Control)
							{
								OpenQuery();
								e.SuppressKeyPress = true;
							}
							break;
						case Keys.Delete:
							if (e.Control)
							{
								ClearQuery();
								e.SuppressKeyPress = true;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void MainForm_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				switch (e.KeyCode)
				{
					case Keys.Home:
					case Keys.End:
						{
							dataGridViewTable.FirstDisplayedCell = dataGridViewTable.CurrentCell;
						}
						break;
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Database tree view

		private void treeViewDatabase_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			try
			{
				if (e.Node.Level == 2)
				{
					if (!e.Node.Parent.IsSelected)
					{
						_worker.SelectTable(e.Node.Parent);
					}
					if (e.Node.Parent.IsSelected)
					{
						_worker.SelectTableColumn(e.Node.Text, true);
					}
				}
				e.Cancel = !(e.Node.Tag != null && e.Node.Tag is string && _worker.DiscardTable());
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void treeViewDatabase_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				_worker.SelectTable(treeViewDatabase.SelectedNode);
				_worker.LoadTable();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void treeViewDatabase_AfterExpand(object sender, TreeViewEventArgs e)
		{
			try
			{
				if (e.Node.Level == 1)
				{
					_worker.TableExpanded(e.Node.Text, true);
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void treeViewDatabase_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			try
			{
				if (e.Node.Level == 1)
				{
					_worker.TableExpanded(e.Node.Text, false);
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void treeViewDatabase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.Node.Level >= 1)
			{
                toolStripSeparator9.Visible = (e.Node.Level == 1);
                toolStripMenuItemDatabaseRawFile.Visible = (e.Node.Level == 1);
                contextMenuStripDatabase.Tag = e.Node;
				contextMenuStripDatabase.Show(MousePosition);
			}
		}

		#endregion

		#region Table data grid view

		private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			_worker.ShowError(e.Exception);
		}

        private void dataGridViewTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewTable.Columns[e.ColumnIndex].ValueType.Equals(typeof(DateTime)) && e.Value != null && e.Value is DateTime)
            {
                e.Value = ((DateTime) e.Value).ToString();
                e.FormattingApplied = true;
            }
        }

		private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			_worker.TableIsModified = true;
		}

		private void dataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			_worker.TableIsModified = true;
		}

		private void dataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			_worker.TableIsModified = true;
		}

		private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right && e.RowIndex > -1)
				{
					if (e.ColumnIndex > -1)
					{
						if (!dataGridViewTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
						{
							dataGridViewTable.CurrentCell = dataGridViewTable.Rows[e.RowIndex].Cells[e.ColumnIndex];
						}
					}
					else
					{
						if (!dataGridViewTable.Rows[e.RowIndex].Selected)
						{
							dataGridViewTable.ClearSelection();
							dataGridViewTable.CurrentCell = dataGridViewTable.Rows[e.RowIndex].Cells[0];
							dataGridViewTable.Rows[e.RowIndex].Selected = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void dataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right)
				{
				    toolStripMenuItemTableCopy.Tag = (e.RowIndex < 0 ? e.ColumnIndex : -1);
					toolStripMenuItemTablePaste.Enabled = (!_worker.TableIsReadOnly && Clipboard.ContainsText());
                    toolStripMenuItemTableInsertGuid.Enabled = !_worker.TableIsReadOnly;
					toolStripMenuItemTableDelete.Enabled = !_worker.TableIsReadOnly;
                    toolStripMenuItemTablePaste.Visible = (e.RowIndex != -1);
                    toolStripMenuItemTableInsertGuid.Visible = (e.RowIndex != -1);
					toolStripSeparator1.Visible = (e.ColumnIndex == -1);
					toolStripMenuItemTableDelete.Visible = (e.ColumnIndex == -1);
                    toolStripSeparator2.Visible = (e.RowIndex != -1 && e.ColumnIndex != -1 && dataGridViewTable.SelectedCells.Count == 1);
                    toolStripMenuItemTableFindIn.Visible = (e.RowIndex != -1 && e.ColumnIndex != -1 && dataGridViewTable.SelectedCells.Count == 1);
					contextMenuStripTable.Show(MousePosition);
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Table Details list view

		private void listViewTableDetails_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (listViewTableDetails.SelectedItems.Count > 0)
				{
					string columnName = listViewTableDetails.SelectedItems[0].SubItems[2].Text;
					_worker.SelectTableColumn(columnName, false);
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Tab control

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				_worker.SelectedTabView = (tabControl.SelectedIndex == 1 ? Worker.TabView.Query : Worker.TabView.Table);
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}
		
		#endregion

		#region Query text box

		private void textBoxQuery_TextChanged(object sender, EventArgs e)
		{
			try
			{
				_worker.CurrentQuery = textBoxQuery.Text.Replace(Environment.NewLine, "\n");
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Query data grid view

		private void dataGridViewQuery_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			_worker.ShowError(e.Exception);
		}

        private void dataGridViewQuery_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewQuery.Columns[e.ColumnIndex].ValueType.Equals(typeof(DateTime)) && e.Value != null && e.Value is DateTime)
            {
                e.Value = ((DateTime) e.Value).ToString();
                e.FormattingApplied = true;
            }
        }

		private void dataGridViewQuery_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right && e.RowIndex > -1)
				{
					if (e.ColumnIndex > -1)
					{
						if (!dataGridViewQuery.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
						{
							dataGridViewQuery.CurrentCell = dataGridViewQuery.Rows[e.RowIndex].Cells[e.ColumnIndex];
						}
					}
					else
					{
						if (!dataGridViewQuery.Rows[e.RowIndex].Selected)
						{
							dataGridViewQuery.ClearSelection();
							dataGridViewQuery.CurrentCell = dataGridViewQuery.Rows[e.RowIndex].Cells[0];
							dataGridViewQuery.Rows[e.RowIndex].Selected = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void dataGridViewQuery_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Right)
				{
                    toolStripMenuItemTableCopy.Tag = (e.RowIndex < 0 ? e.ColumnIndex : -1);
                    toolStripMenuItemTablePaste.Visible = false;
					toolStripMenuItemTableInsertGuid.Visible = false;
					toolStripSeparator1.Visible = false;
					toolStripMenuItemTableDelete.Visible = false;
                    toolStripSeparator2.Visible = (e.RowIndex != -1 && e.ColumnIndex != -1 && dataGridViewQuery.SelectedCells.Count == 1);
                    toolStripMenuItemTableFindIn.Visible = (e.RowIndex != -1 && e.ColumnIndex != -1 && dataGridViewQuery.SelectedCells.Count == 1);
					contextMenuStripTable.Show(MousePosition);
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Main menu

		private void toolStripMenuItemMainDatabaseOpen_Click(object sender, EventArgs e)
		{
			OpenDatabase();
		}

        public void toolStripMenuItemMainDatabaseRecentDatabase_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem) sender;
            OpenDatabase((string) menuItem.Tag);
        }

		private void toolStripMenuItemMainDatabaseRootPathOpen_Click(object sender, EventArgs e)
		{
			OpenDatabaseRootPath();
		}

		private void toolStripMenuItemMainDatabaseClose_Click(object sender, EventArgs e)
		{
			CloseDatabase();
		}

        private void toolStripMenuItemMainDatabaseRelationsManage_Click(object sender, EventArgs e)
        {
            ManageDatabaseRelations();
        }

        private void toolStripMenuItemMainDatabaseRelationsAssociateWith_Click(object sender, EventArgs e)
        {
            AssociateDatabaseWithRelationSet();
        }

		private void toolStripMenuItemMainTableSave_Click(object sender, EventArgs e)
		{
			SaveTable();
		}

		private void toolStripMenuItemMainTableRefresh_Click(object sender, EventArgs e)
		{
			RefreshTable();
		}

		private void toolStripMenuItemMainTableSearchMagic_Click(object sender, EventArgs e)
		{
			MagicSearch();
		}

		private void toolStripMenuItemMainTableSearchAdvanced_Click(object sender, EventArgs e)
		{
			AdvancedSearch();
		}

		private void toolStripMenuItemMainTableRawFileOpen_Click(object sender, EventArgs e)
		{
			OpenRawFile(false);
		}

		private void toolStripMenuItemMainTableRawFileEdit_Click(object sender, EventArgs e)
		{
			OpenRawFile(true);
		}

		private void toolStripMenuItemMainTableShowDetails_Click(object sender, EventArgs e)
		{
			ShowTableDetails(toolStripMenuItemMainTableShowDetails.Checked);
		}

		private void toolStripMenuItemMainQueryOpen_Click(object sender, EventArgs e)
		{
			OpenQuery();
		}

		private void toolStripMenuItemMainQuerySave_Click(object sender, EventArgs e)
		{
			SaveQuery();
		}

		private void toolStripMenuItemMainQueryClear_Click(object sender, EventArgs e)
		{
			ClearQuery();
		}

		private void toolStripMenuItemMainQueryCheck_Click(object sender, EventArgs e)
		{
			CheckQuery();
		}

		private void toolStripMenuItemMainQueryExecute_Click(object sender, EventArgs e)
		{
			ExecuteQuery();
		}

		private void toolStripMenuItemMainOptionsSelectRowsWhenSearching_Click(object sender, EventArgs e)
		{
			SelectRowsWhenSearching(toolStripMenuItemMainOptionsSelectRowsWhenSearching.Checked);
		}

		private void toolStripMenuItemMainOptionsAutoReloadModifiedTable_Click(object sender, EventArgs e)
		{
			AutoReloadModifiedTable(toolStripMenuItemMainOptionsAutoReloadModifiedTable.Checked);
		}

		private void toolStripMenuItemMainAbout_Click(object sender, EventArgs e)
		{
			new AboutForm(_worker).ShowDialog(this);
		}

		public void toolStripMenuItemMainQueryHistoryEntry_Click(object sender, EventArgs e)
		{
			try
			{
				ToolStripMenuItem menuItem = (ToolStripMenuItem) sender;
				SelectQueryHistoryItem((string) menuItem.Tag);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Database context menu

        private void toolStripMenuItemDatabaseCopy_Click(object sender, EventArgs e)
        {
            CopyNodeName((TreeNode) contextMenuStripDatabase.Tag);
        }

		private void toolStripMenuItemDatabaseRawFileOpen_Click(object sender, EventArgs e)
		{
			OpenRawFile(false);
		}

		private void toolStripMenuItemDatabaseRawFileEdit_Click(object sender, EventArgs e)
		{
			OpenRawFile(true);
		}

		#endregion

		#region Table and query context menus

		private void toolStripMenuItemTableCopy_Click(object sender, EventArgs e)
		{
			try
			{
                _worker.CopyClipboardText((int) toolStripMenuItemTableCopy.Tag);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void toolStripMenuItemTablePaste_Click(object sender, EventArgs e)
		{
			try
			{
				_worker.PasteClipboardText();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void toolStripMenuItemTableInsertGuid_Click(object sender, EventArgs e)
		{
			try
			{
				_worker.InsertNewGuid();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void toolStripMenuItemTableDelete_Click(object sender, EventArgs e)
		{
			try
			{
				_worker.DeleteRows();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		public void toolStripMenuItemTableFindInTableColumn_Click(object sender, EventArgs e)
		{
			try
			{
				DataGridView dataGridView = (_worker.SelectedTabView == Worker.TabView.Query ? dataGridViewQuery : dataGridViewTable);

				if (dataGridView.SelectedCells.Count != 1)
					throw new Exception("More than one cell selected.");

				ToolStripMenuItem menuItem = (ToolStripMenuItem) sender;
				string searchValue = dataGridView.CurrentCell.Value.ToString();
				_worker.FindColumnValue((string) menuItem.Tag, menuItem.Text, searchValue);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		public void toolStripMenuItemTableMagicFindInTableColumn_Click(object sender, EventArgs e)
		{
			try
			{
				DataGridView dataGridView = (_worker.SelectedTabView == Worker.TabView.Query ? dataGridViewQuery : dataGridViewTable);

				if (dataGridView.SelectedCells.Count != 1)
					throw new Exception("More than one cell selected.");

				ToolStripMenuItem menuItem = (ToolStripMenuItem) sender;
				string searchValue = dataGridView.CurrentCell.Value.ToString();
				_worker.FindColumnValue(menuItem.Text.Trim(), (string) menuItem.Tag, searchValue);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Standard toolbar

		private void toolStripButtonStandardOpenDatabase_Click(object sender, EventArgs e)
		{
			OpenDatabase();
		}

		private void toolStripButtonStandardSaveTable_Click(object sender, EventArgs e)
		{
			SaveTable();
		}

		private void toolStripButtonStandardRefreshTable_Click(object sender, EventArgs e)
		{
			RefreshTable();
		}

		private void toolStripButtonStandardAdvancedSearch_Click(object sender, EventArgs e)
		{
			AdvancedSearch();
		}

		private void toolStripButtonStandardShowTableDetails_Click(object sender, EventArgs e)
		{
			ShowTableDetails(toolStripButtonStandardShowTableDetails.Checked);
		}

		#endregion

		#region Query toolbar

		private void toolStripButtonQueryOpen_Click(object sender, EventArgs e)
		{
			OpenQuery();
		}

		private void toolStripButtonQuerySave_Click(object sender, EventArgs e)
		{
			SaveQuery();
		}

		private void toolStripButtonQueryClear_Click(object sender, EventArgs e)
		{
			ClearQuery();
		}

		private void toolStripComboBoxQueryHistory_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectQueryHistoryItem(null);
		}

		private void toolStripButtonQueryCheck_Click(object sender, EventArgs e)
		{
			CheckQuery();
		}

		private void toolStripButtonQueryExecute_Click(object sender, EventArgs e)
		{
			ExecuteQuery();
		}

		#endregion

		#endregion

		#region Helpers

        private void OpenDatabase()
        {
            OpenDatabase(null);
        }

        private void OpenDatabase(string databasePath)
        {
            if (!DiscardEvents)
            {
                try
                {
                    if (_worker.DiscardTable())
                    {
                        bool proceed = true;
                        _worker.SelectTable((TreeNode) null);
                        _worker.CloseAdvancedSearch(false);
                        if (string.IsNullOrEmpty(databasePath))
                        {
                            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                            folderDialog.Description = "Please select a database root path...";
                            folderDialog.SelectedPath = _worker.DatabaseRootFolder;
                            folderDialog.ShowNewFolderButton = false;
                            proceed = (folderDialog.ShowDialog(this) == DialogResult.OK);
                            databasePath = folderDialog.SelectedPath;
                        }
                        if (proceed)
                        {
                            _worker.OpenDatabase(databasePath);
                            _worker.SaveRegistryValues();
                            if (_worker.RelationSets.Count > 0 && !_worker.RelationSets.SelectMany(relationSet => relationSet.Databases)
                                    .Any(dbPath => dbPath.Equals(databasePath, StringComparison.CurrentCultureIgnoreCase))
                                && _worker.ShowQuestion("Would you like to associate this database with a relation set?") == DialogResult.Yes)
                                _worker.OpenDatabaseRelationSetAssociation(databasePath);
                            EnableControls(true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _worker.ShowError(ex);
                }
            }
        }

		private void OpenDatabaseRootPath()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.OpenDatabaseRootPath();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void CloseDatabase()
		{
			if (!DiscardEvents)
			{
				try
				{
					if (_worker.DiscardTable())
					{
						_worker.SelectTable((TreeNode) null);
						_worker.CloseDatabase();
						EnableControls(true);
					}
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void ManageDatabaseRelations()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.OpenDatabaseRelationSetManager();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

        private void AssociateDatabaseWithRelationSet()
        {
            if (!DiscardEvents)
            {
                try
                {
                    _worker.OpenDatabaseRelationSetAssociation(_worker.DatabaseRootFolder);
                }
                catch (Exception ex)
                {
                    _worker.ShowError(ex);
                }
            }
        }

		private void RefreshTable()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.LoadTable();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void SaveTable()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.SaveTable();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void MagicSearch()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.FindColumnValue();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void AdvancedSearch()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.OpenAdvancedSearch();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

        private void CopyNodeName(TreeNode node)
        {
            if (!DiscardEvents)
            {
                try
                {
                    _worker.CopyNodeNameToClipboard(node);
                }
                catch (Exception ex)
                {
                    _worker.ShowError(ex);
                }
            }
        }

		private void OpenRawFile(bool edit)
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.OpenSelectedTableRawFile(edit);
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void ShowTableDetails(bool activated)
		{
			if (!DiscardEvents)
			{
				try
				{
					CheckToolStripItems(activated, toolStripButtonStandardShowTableDetails, toolStripMenuItemMainTableShowDetails);
					_worker.ShowTableDetails = activated;
					_worker.DisplayTableDetails();
					_worker.SaveRegistryValues();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void SelectRowsWhenSearching(bool activated)
		{
			if (!DiscardEvents)
			{
				try
				{
					CheckToolStripItems(activated, toolStripMenuItemMainOptionsSelectRowsWhenSearching);
					_worker.SelectRowsWhenSearching = activated;
					_worker.SaveRegistryValues();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void AutoReloadModifiedTable(bool activated)
		{
			if (!DiscardEvents)
			{
				try
				{
					CheckToolStripItems(activated, toolStripMenuItemMainOptionsAutoReloadModifiedTable);
					_worker.TableAutoReloadEnabled = activated;
					_worker.SaveRegistryValues();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void OpenQuery()
		{
			if (!DiscardEvents)
			{
				try
				{
					string query = _worker.OpenQuery();
					if (query != null) textBoxQuery.Text = query;
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void SaveQuery()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.SaveQuery(textBoxQuery.Text);
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void ClearQuery()
		{
			if (!DiscardEvents)
			{
				try
				{
					textBoxQuery.Text = string.Empty;
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void SelectQueryHistoryItem(string selectedQuery)
		{
			if (!DiscardEvents)
			{
				try
				{
					if (selectedQuery != null)
					{
						textBoxQuery.Text = Worker.DecodeQueryAsHistoryItem(selectedQuery);
						DiscardEvents = true;
						toolStripComboBoxQueryHistory.SelectedItem = selectedQuery;
						DiscardEvents = false;
					}
					else if (toolStripComboBoxQueryHistory.SelectedIndex > -1)
					{
						textBoxQuery.Text = Worker.DecodeQueryAsHistoryItem((string) toolStripComboBoxQueryHistory.SelectedItem);
					}
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void CheckQuery()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.CheckQuery(!string.IsNullOrEmpty(textBoxQuery.SelectedText) ? textBoxQuery.SelectedText : textBoxQuery.Text);
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void ExecuteQuery()
		{
			if (!DiscardEvents)
			{
				try
				{
					_worker.ExecuteQuery(!string.IsNullOrEmpty(textBoxQuery.SelectedText) ? textBoxQuery.SelectedText : textBoxQuery.Text);
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		private void EnableControls(bool enable)
		{
			EnableToolStripItems(enable && !string.IsNullOrEmpty(_worker.DatabaseRootFolder),
                toolStripMenuItemMainDatabaseClose, toolStripMenuItemMainDatabaseRootPath, toolStripMenuItemMainDatabaseRelationsAssociateWith,
                toolStripMenuItemMainTableSearch, toolStripButtonStandardAdvancedSearch);
			EnableToolStripItems(enable && !string.IsNullOrEmpty(_worker.DatabaseRootFolder) && !string.IsNullOrEmpty(_worker.SelectedTable),
				toolStripMenuItemMainTableRefresh, toolStripButtonStandardRefreshTable, toolStripMenuItemMainTableRawFile);
			EnableToolStripItems(enable && !string.IsNullOrEmpty(_worker.DatabaseRootFolder) && _worker.TableIsModified,
				toolStripMenuItemMainTableSave, toolStripButtonStandardSaveTable);
			EnableToolStripItems(enable && !string.IsNullOrEmpty(_worker.DatabaseRootFolder) && _worker.SelectedTabView == Worker.TabView.Query,
				toolStripButtonQueryCheck, toolStripButtonQueryExecute, toolStripMenuItemMainQueryCheck, toolStripMenuItemMainQueryExecute);
			EnableToolStripItems(enable && _worker.SelectedTabView == Worker.TabView.Query, toolStripButtonQueryOpen, toolStripButtonQuerySave,
				toolStripButtonQueryClear, toolStripMenuItemMainQuery);
		}

		private void CheckToolStripItems(bool check, params ToolStripItem[] items)
		{
			DiscardEvents = true;
			foreach (ToolStripItem item in items)
			{
				if (item is ToolStripButton)
				{
					((ToolStripButton) item).Checked = check;
				}
				else if (item is ToolStripMenuItem)
				{
					((ToolStripMenuItem) item).Checked = check;
				}
			}
			DiscardEvents = false;
		}

		private static void EnableToolStripItems(bool enable, params ToolStripItem[] items)
		{
			foreach (ToolStripItem item in items)
			{
				item.Enabled = enable;
			}
		}

		private static void ToolStripContentChanged(ToolStrip toolStrip)
		{
			int toolStripWidth = toolStrip.GripRectangle.Width + toolStrip.GripMargin.Size.Width + 1;
			foreach (ToolStripItem item in toolStrip.Items)
			{
				toolStripWidth += item.Width + item.Margin.Size.Width;
			}
			toolStrip.Width = toolStripWidth;
		}

		#endregion
	}
}
