using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using XMLDBViewer.DataObjects;
using XMLDBViewer.EventArguments;

namespace XMLDBViewer
{
	public class Worker
	{
		public const string ADVANCED_SEARCH_TABLE_ALL = "[All Tables]";
		public const string ADVANCED_SEARCH_MODE_VALUES = "Search Values";
		public const string ADVANCED_SEARCH_MODE_COLUMN_NAMES = "Search Column Names";
		public const string ADVANCED_SEARCH_MODE_TABLE_NAMES = "Search Table Names";
		public const string ADVANCED_SEARCH_MODE_TABLE_RAW_FILES = "Search Table Raw Files";

		private const string REGISTRY_KEY_XML_DB_VIEWER = @"Software\QlikTech\XML DB Viewer";
		private const string REGISTRY_KEY_ULTRAEDIT = @"Software\IDM Computer Solutions\UltraEdit";
		private const string DATABASE_NODE_KEY = "[database]";
		private const int MAX_ADVANCED_SEARCH_VALUE_HISTORY_ITEM_COUNT = 20;
		private const int MAX_QUERY_HISTORY_ITEM_COUNT = 20;
        private const int MAX_RECENT_DATABASE_ITEM_COUNT = 20;

		public event EventHandler<BooleanEventArgs> SaveTableEnabled;
		public event EventHandler<BooleanEventArgs> TableSelected;
		public event EventHandler<StringEventArgs> TableLastModifiedTimeChanged;
        public event EventHandler<StringEventArgs> DatabaseRelationSetChanged;
        public event EventHandler<AdvancedSearchResultItemEventArgs> NewAdvancedSearchResultItem;
		public event EventHandler<EventArgs> AdvancedSearchCompleted;
		public event EventHandler<EventArgs> AdvancedSearchValueListUpdated;

		private readonly MainForm _mainForm;
	    private AdvancedSearchForm _advSearchForm;
		private readonly FileSystemWatcher _watcher;
	    private readonly IDictionary<string, DateTime> _watcherFileChanged;
		private bool _tableIsModified;
	    private bool _tableCellsHighlighted;

		public Worker(MainForm mainForm)
		{
			_mainForm = mainForm;
		    _advSearchForm = null;
			ApplicationStarts = 0;
			ApplicationExceptions = 0;
			DatabaseRootFolder = string.Empty;
            RecentDatabases = new List<string>();
			DatabaseTableSchemas = new Dictionary<string, DataTable>();
			ExpandedTables = new List<string>();
			SelectedTable = string.Empty;
			TableIsReadOnly = true;
			TableAutoReloadEnabled = false;
			TableIsModified = false;
			SelectRowsWhenSearching = false;
			ShowTableDetails = false;
			AdvancedSearchValueHistory = new List<string>();
			AdvancedSearchSelectedTable = string.Empty;
			AdvancedSearchSelectedMode = string.Empty;
			AdvancedSearchMatchCase = false;
			AdvancedSearchMatchWholeValue = false;
			RelationSets = new List<RelationSet>();
			WaitOperationRunning = false;
			WaitMessageCreated = false;
			SelectedTabView = TabView.Table;
			CurrentQuery = string.Empty;
			QueryHistory = new List<string>();
		    _tableCellsHighlighted = false;
            _watcherFileChanged = new Dictionary<string, DateTime>();
			_watcher = new FileSystemWatcher { Filter = "*.xml", NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName };
			_watcher.Changed += _watcher_Changed;
            _watcher.Created += _watcher_Changed;
            _watcher.Deleted += _watcher_Changed;
			_watcher.Renamed += _watcher_Renamed;
		}

		#region Enumerations

		public enum TabView
		{
			Table,
			Query
		}

		private enum ToolStripContainerPanel
		{
			Top,
			Bottom,
			Left,
			Right
		}

		#endregion

		#region Properties

		public int ApplicationStarts { get; private set; }

		public int ApplicationExceptions { get; private set; }

		public string DatabaseRootFolder { get; private set; }

        public IList<string> RecentDatabases { get; private set; }

		public IDictionary<string, DataTable> DatabaseTableSchemas { get; private set; }

		private IList<string> ExpandedTables { get; set; }

		public string SelectedTable { get; private set; }

		public string SelectedTablePath
		{
			get
			{
				TreeNode selectedNode = _mainForm.DatabaseTreeView.SelectedNode;
				return (selectedNode != null && selectedNode.Tag != null && selectedNode.Tag is string ? (string) selectedNode.Tag : null);
			}
		}

		public DateTime TableLastModifiedTime { get; private set; }

		public bool TableIsReadOnly { get; private set; }

		public bool TableAutoReloadEnabled { get; set; }

		public bool TableIsModified
		{
			get { return _tableIsModified; }
			set
			{
				_tableIsModified = value;
                if (_tableIsModified) ClearTableHighlights();
				OnSaveTableEnabled(_tableIsModified);
			}
		}

		public bool TableIsLoaded
		{
			get { return (_mainForm.TableDataGridView.DataSource != null); }
		}

		public IEnumerable<string> Tables
		{
			get { return DatabaseTableSchemas.Select(x => x.Key); }
		}

		public bool SelectRowsWhenSearching { get; set; }

		public bool ShowTableDetails { get; set; }

		public IList<string> AdvancedSearchValueHistory { get; private set; }

		public string AdvancedSearchSelectedTable { get; set; }

		public string AdvancedSearchSelectedMode { get; set; }

		public bool AdvancedSearchMatchCase { get; set; }

		public bool AdvancedSearchMatchWholeValue { get; set; }

		public IList<RelationSet> RelationSets { get; private set; }

		public RelationSet DatabaseRelationSet
		{
			get
			{
				return RelationSets.FirstOrDefault(relationSet
					=> relationSet.Databases.Any(databasePath
						=> databasePath.Equals(DatabaseRootFolder, StringComparison.CurrentCultureIgnoreCase)));
			}
		}

		public bool WaitMessageCreated { get; private set; }

		public bool WaitOperationRunning { get; private set; }

		public TabView SelectedTabView { get; set; }

		public string CurrentQuery { get; set; }

		public IList<string> QueryHistory { get; private set; }

		#endregion

		#region Event raisers

		private void OnSaveTableEnabled(bool enabled)
		{
			if (SaveTableEnabled != null)
			{
				SaveTableEnabled(this, new BooleanEventArgs(enabled));
			}
		}

		private void OnTableSelected(bool selected)
		{
			if (TableSelected != null)
			{
				TableSelected(this, new BooleanEventArgs(selected));
			}
		}

		private void OnTableLastModifiedTimeChanged(string modifiedTimeInfo)
		{
			if (TableLastModifiedTimeChanged != null)
			{
				TableLastModifiedTimeChanged(this, new StringEventArgs(modifiedTimeInfo));
			}
		}

        private void OnDatabaseRelationSetChanged(string relationSetName)
        {
            if (DatabaseRelationSetChanged != null)
            {
                DatabaseRelationSetChanged(this, new StringEventArgs(relationSetName));
            }
        }

		private void OnNewAdvancedSearchResultItem(AdvancedSearchResultItem resultItem)
		{
			if (NewAdvancedSearchResultItem != null)
			{
				NewAdvancedSearchResultItem(this, new AdvancedSearchResultItemEventArgs(resultItem));
			}
		}

		private void OnAdvancedSearchCompleted()
		{
			if (AdvancedSearchCompleted != null)
			{
				AdvancedSearchCompleted(this, new EventArgs());
			}
		}

		private void OnAdvancedSearchValueListUpdated()
		{
			if (AdvancedSearchValueListUpdated != null)
			{
				AdvancedSearchValueListUpdated(this, new EventArgs());
			}
		}

		#endregion

		#region Event handlers

		private void _watcher_Changed(object sender, FileSystemEventArgs e)
		{
			if (_mainForm.InvokeRequired)
			{
				_mainForm.Invoke(new EventHandler<FileSystemEventArgs>(_watcher_Changed), sender, e);
			}
			else
			{
                try
                {
                    Thread.Sleep(100);
                    string tableName = Path.GetFileNameWithoutExtension(e.FullPath);
                    switch (e.ChangeType)
                    {
                        case WatcherChangeTypes.Changed:
                            FileInfo fileInfo = new FileInfo(e.FullPath);
                            if (!_watcherFileChanged.ContainsKey(e.FullPath) || _watcherFileChanged[e.FullPath] < fileInfo.LastWriteTime)
                            {
                                _watcherFileChanged[e.FullPath] = fileInfo.LastWriteTime;
                                if (tableName == SelectedTable)
                                    UpdateSelectedTable();
                                else
                                    UpdateTableInTreeView(tableName, e.FullPath);
                            }
                            break;
                        case WatcherChangeTypes.Created:
                            UpdateTableInTreeView(tableName, e.FullPath);
                            break;
                        case WatcherChangeTypes.Deleted:
                            RemoveTableFromTreeView(tableName);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
			}
		}

		private void _watcher_Renamed(object sender, RenamedEventArgs e)
		{
			if (_mainForm.InvokeRequired)
			{
				_mainForm.Invoke(new EventHandler<RenamedEventArgs>(_watcher_Renamed), sender, e);
			}
			else
			{
                try
                {
                    Thread.Sleep(100);
                    string oldTableName = Path.GetFileNameWithoutExtension(e.OldFullPath);
                    UpdateTableInTreeView(oldTableName, e.FullPath);
                    if (oldTableName == SelectedTable)
                        UpdateSelectedTable();
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
		}

		#endregion

		#region Registry operations

		public void LoadRegistryValues()
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_XML_DB_VIEWER, false);
			if (key != null)
			{
				// main window
				bool windowIsMaximized = (_mainForm.WindowState == FormWindowState.Maximized);
				ApplicationStarts = (int) key.GetValue("Application Starts", ApplicationStarts) + 1;
				ApplicationExceptions = (int) key.GetValue("Application Exceptions", ApplicationExceptions);
				DatabaseRootFolder = (string) key.GetValue("Database Root Folder", string.Empty);
				SelectedTable = (string) key.GetValue("Selected Table", string.Empty);
				ExpandedTables = StringToList((string) key.GetValue("Expanded Tables", string.Empty));
				TableAutoReloadEnabled = bool.Parse((string) key.GetValue("Auto Reload Table", "False"));
				SelectRowsWhenSearching = bool.Parse((string) key.GetValue("Select Rows When Searching", "False"));
				ShowTableDetails = bool.Parse((string) key.GetValue("Show Table Details", "False"));
				SelectedTabView = (TabView) Enum.Parse(typeof(TabView), (string) key.GetValue("Selected Tab View", TabView.Table.ToString()));
				CurrentQuery = LoadMultiLineRegistryValue(key.GetValue("Current Query", string.Empty));
				QueryHistory.Clear();
				RegistryKey queryHistoryKey = key.OpenSubKey("Query History");
				if (queryHistoryKey != null)
				{
					string queryItem = LoadMultiLineRegistryValue(queryHistoryKey.GetValue("Query History 00", string.Empty));
					while (!string.IsNullOrEmpty(queryItem))
					{
						QueryHistory.Add(queryItem);
						queryItem = LoadMultiLineRegistryValue(queryHistoryKey.GetValue("Query History " + QueryHistory.Count.ToString("00"), string.Empty));
					}
					queryHistoryKey.Close();
				}
                RecentDatabases.Clear();
                RegistryKey recentDatabasesKey = key.OpenSubKey("Recent Databases");
                if (recentDatabasesKey != null)
                {
                    string recentDatabaseItem = (string) recentDatabasesKey.GetValue("Recent Database 00", string.Empty);
                    while (!string.IsNullOrEmpty(recentDatabaseItem))
                    {
                        RecentDatabases.Add(recentDatabaseItem);
                        recentDatabaseItem = (string) recentDatabasesKey.GetValue("Recent Database " + RecentDatabases.Count.ToString("00"), string.Empty);
                    }
                    recentDatabasesKey.Close();
                }
                _mainForm.Left = (int) key.GetValue("Window Location X", (windowIsMaximized ? _mainForm.RestoreBounds.Left : _mainForm.Left));
				_mainForm.Top = (int) key.GetValue("Window Location Y", (windowIsMaximized ? _mainForm.RestoreBounds.Top : _mainForm.Top));
				_mainForm.Width = (int) key.GetValue("Window Size Width", (windowIsMaximized ? _mainForm.RestoreBounds.Width : _mainForm.Width));
				_mainForm.Height = (int) key.GetValue("Window Size Height", (windowIsMaximized ? _mainForm.RestoreBounds.Height : _mainForm.Height));
				_mainForm.WindowState = (FormWindowState) Enum.Parse(typeof(FormWindowState), (string) key.GetValue("Window State", _mainForm.WindowState.ToString()));
				PositionToolStrip(_mainForm.MainMenuStrip, (ToolStripContainerPanel) Enum.Parse(typeof(ToolStripContainerPanel),
					(string) key.GetValue("Main Menu Container Panel", GetToolStripContainerPanel(_mainForm.MainMenuStrip).ToString())));
				PositionToolStrip(_mainForm.StandardToolStrip, (ToolStripContainerPanel) Enum.Parse(typeof(ToolStripContainerPanel),
					(string) key.GetValue("Standard Toolbar Container Panel", GetToolStripContainerPanel(_mainForm.StandardToolStrip).ToString())));
				_mainForm.MainMenuStrip.Left = (int) key.GetValue("Main Menu Location X", _mainForm.MainMenuStrip.Left);
				_mainForm.MainMenuStrip.Top = (int) key.GetValue("Main Menu Location Y", _mainForm.MainMenuStrip.Top);
				_mainForm.StandardToolStrip.Left = (int) key.GetValue("Standard Toolbar Location X", _mainForm.StandardToolStrip.Left);
				_mainForm.StandardToolStrip.Top = (int) key.GetValue("Standard Toolbar Location Y", _mainForm.StandardToolStrip.Top);
				_mainForm.MainSplitter.SplitterDistance = (int) key.GetValue("Main Splitter Position", _mainForm.MainSplitter.SplitterDistance);
				_mainForm.TableSplitter.SplitterDistance = (int) key.GetValue("Table Splitter Position", _mainForm.TableSplitter.SplitterDistance);
				_mainForm.QuerySplitter.SplitterDistance = (int) key.GetValue("Query Splitter Position", _mainForm.QuerySplitter.SplitterDistance);
				// advanced search window
				AdvancedSearchValueHistory.Clear();
				RegistryKey advancedSearchValueHistoryKey = key.OpenSubKey("Advanced Search Value History");
				if (advancedSearchValueHistoryKey != null)
				{
					string historyItem = (string) advancedSearchValueHistoryKey.GetValue("Advanced Search Value History 00", string.Empty);
					while (!string.IsNullOrEmpty(historyItem))
					{
						AdvancedSearchValueHistory.Add(historyItem);
						historyItem = (string) advancedSearchValueHistoryKey.GetValue("Advanced Search Value History " + AdvancedSearchValueHistory.Count.ToString("00"), string.Empty);
					}
					advancedSearchValueHistoryKey.Close();
				}
				AdvancedSearchSelectedTable = (string) key.GetValue("Advanced Search Selected Table", ADVANCED_SEARCH_TABLE_ALL);
				AdvancedSearchSelectedMode = (string) key.GetValue("Advanced Search Selected Mode", ADVANCED_SEARCH_MODE_VALUES);
				AdvancedSearchMatchCase = bool.Parse((string) key.GetValue("Advanced Search Match Case", "False"));
				AdvancedSearchMatchWholeValue = bool.Parse((string) key.GetValue("Advanced Search Match Whole Value", "False"));
				LoadRegistryRelationSets(key);
				key.Close();
			}
		}

		private void LoadRegistryRelationSets(RegistryKey key)
		{
			RelationSets.Clear();
			RegistryKey relationSetKey = key.OpenSubKey("Relation Set 00", false);
			while (relationSetKey != null)
			{
				RelationSet relationSet = new RelationSet();
				relationSet.Name = (string) relationSetKey.GetValue("Name", string.Empty);
				relationSet.CreateDate = DateTime.Parse((string) relationSetKey.GetValue("Create Date", DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")));
				relationSet.ModifyDate = DateTime.Parse((string) relationSetKey.GetValue("Modify Date", DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss")));
				string databasePath = (string) relationSetKey.GetValue("Database Path 00", string.Empty);
				while (!string.IsNullOrEmpty(databasePath))
				{
					relationSet.Databases.Add(databasePath);
					databasePath = (string) relationSetKey.GetValue("Database Path " + relationSet.Databases.Count.ToString("00"), string.Empty);
				}
				RegistryKey relationKey = relationSetKey.OpenSubKey("Relation 000", false);
				while (relationKey != null)
				{
					string sourceTable = (string) relationKey.GetValue("Source Table", string.Empty);
					string sourceColumn = (string) relationKey.GetValue("Source Column", string.Empty);
					string destinationTable = (string) relationKey.GetValue("Destination Table", string.Empty);
					string destinationColumn = (string) relationKey.GetValue("Destination Column", string.Empty);
					relationSet.Relations.Add(new Relation(sourceTable, sourceColumn, destinationTable, destinationColumn));
					relationKey.Close();
					relationKey = relationSetKey.OpenSubKey("Relation " + relationSet.Relations.Count.ToString("000"), false);
				}
				RelationSets.Add(relationSet);
				relationSetKey.Close();
				relationSetKey = key.OpenSubKey("Relation Set " + RelationSets.Count.ToString("00"), false);
			}
		}

        private static string LoadMultiLineRegistryValue(object input)
        {
            if (input is string[])
            {
                string[] requestBodyHistory = input as string[];
                return (requestBodyHistory.Length > 0 ? requestBodyHistory.Aggregate((x, y) => (x + "\n" + y)) : string.Empty);
            }
            return input as string;
        }

		public void SaveRegistryValues()
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_XML_DB_VIEWER);
			if (key != null)
			{
				// main window
				bool windowIsMaximized = (_mainForm.WindowState == FormWindowState.Maximized);
				key.SetValue("Application Starts", ApplicationStarts, RegistryValueKind.DWord);
				key.SetValue("Application Exceptions", ApplicationExceptions, RegistryValueKind.DWord);
				key.SetValue("Database Root Folder", DatabaseRootFolder, RegistryValueKind.String);
				key.SetValue("Selected Table", SelectedTable, RegistryValueKind.String);
				key.SetValue("Auto Reload Table", TableAutoReloadEnabled.ToString(), RegistryValueKind.String);
				key.SetValue("Expanded Tables", ListToString(ExpandedTables), RegistryValueKind.String);
				key.SetValue("Select Rows When Searching", SelectRowsWhenSearching.ToString(), RegistryValueKind.String);
				key.SetValue("Show Table Details", ShowTableDetails.ToString(), RegistryValueKind.String);
				key.SetValue("Selected Tab View", SelectedTabView.ToString(), RegistryValueKind.String);
                key.SetValue("Current Query", CurrentQuery.Split('\r', '\n'), RegistryValueKind.MultiString);
				RegistryKey queryHistoryKey = key.CreateSubKey("Query History");
				if (queryHistoryKey != null)
				{
					for (int i = 0; i < QueryHistory.Count; i++)
					{
                        queryHistoryKey.SetValue("Query History " + i.ToString("00"), QueryHistory[i].Split('\r', '\n'), RegistryValueKind.MultiString);
					}
					queryHistoryKey.Close();
				}
                RegistryKey recentDatabasesKey = key.CreateSubKey("Recent Databases");
                if (recentDatabasesKey != null)
                {
                    for (int i = 0; i < RecentDatabases.Count; i++)
                    {
                        recentDatabasesKey.SetValue("Recent Database " + i.ToString("00"), RecentDatabases[i], RegistryValueKind.String);
                    }
                    recentDatabasesKey.Close();
                }
                key.SetValue("Window Location X", (windowIsMaximized ? _mainForm.RestoreBounds.Left : _mainForm.Left), RegistryValueKind.DWord);
				key.SetValue("Window Location Y", (windowIsMaximized ? _mainForm.RestoreBounds.Top : _mainForm.Top), RegistryValueKind.DWord);
				key.SetValue("Window Size Width", (windowIsMaximized ? _mainForm.RestoreBounds.Width : _mainForm.Width), RegistryValueKind.DWord);
				key.SetValue("Window Size Height", (windowIsMaximized ? _mainForm.RestoreBounds.Height : _mainForm.Height), RegistryValueKind.DWord);
				key.SetValue("Window State", _mainForm.WindowState.ToString(), RegistryValueKind.String);
				key.SetValue("Main Menu Container Panel", GetToolStripContainerPanel(_mainForm.MainMenuStrip).ToString(), RegistryValueKind.String);
				key.SetValue("Standard Toolbar Container Panel", GetToolStripContainerPanel(_mainForm.StandardToolStrip).ToString(), RegistryValueKind.String);
				key.SetValue("Main Menu Location X", _mainForm.MainMenuStrip.Left, RegistryValueKind.DWord);
				key.SetValue("Main Menu Location Y", _mainForm.MainMenuStrip.Top, RegistryValueKind.DWord);
				key.SetValue("Standard Toolbar Location X", _mainForm.StandardToolStrip.Left, RegistryValueKind.DWord);
				key.SetValue("Standard Toolbar Location Y", _mainForm.StandardToolStrip.Top, RegistryValueKind.DWord);
				key.SetValue("Main Splitter Position", _mainForm.MainSplitter.SplitterDistance, RegistryValueKind.DWord);
				key.SetValue("Table Splitter Position", _mainForm.TableSplitter.SplitterDistance, RegistryValueKind.DWord);
				key.SetValue("Query Splitter Position", _mainForm.QuerySplitter.SplitterDistance, RegistryValueKind.DWord);
				// advanced search window
				RegistryKey advancedSearchValueHistoryKey = key.CreateSubKey("Advanced Search Value History");
				if (advancedSearchValueHistoryKey != null)
				{
					for (int i = 0; i < AdvancedSearchValueHistory.Count; i++)
					{
						advancedSearchValueHistoryKey.SetValue("Advanced Search Value History " + i.ToString("00"), AdvancedSearchValueHistory[i], RegistryValueKind.String);
					}
					advancedSearchValueHistoryKey.Close();
				}
				key.SetValue("Advanced Search Selected Table", AdvancedSearchSelectedTable, RegistryValueKind.String);
				key.SetValue("Advanced Search Selected Mode", AdvancedSearchSelectedMode, RegistryValueKind.String);
				key.SetValue("Advanced Search Match Case", AdvancedSearchMatchCase.ToString(), RegistryValueKind.String);
				key.SetValue("Advanced Search Match Whole Value", AdvancedSearchMatchWholeValue.ToString(), RegistryValueKind.String);
				SaveRegistryRelationSets(key);
				key.Close();
			}
		}

		public void SaveRegistryRelationSets()
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(REGISTRY_KEY_XML_DB_VIEWER);
			if (key != null)
			{
				SaveRegistryRelationSets(key);
				key.Close();
			}
		}

		private void SaveRegistryRelationSets(RegistryKey key)
		{
			string[] subKeyNames = key.GetSubKeyNames();
			foreach (string subKeyName in subKeyNames)
			{
				if (subKeyName.StartsWith("Relation Set "))
					key.DeleteSubKeyTree(subKeyName);
			}
			for (int i = 0; i < RelationSets.Count; i++)
			{
				RegistryKey relationSetKey = key.CreateSubKey("Relation Set " + i.ToString("00"));
				if (relationSetKey != null)
				{
					relationSetKey.SetValue("Name", RelationSets[i].Name, RegistryValueKind.String);
					relationSetKey.SetValue("Create Date", RelationSets[i].CreateDate.ToString("yyyy-MM-dd HH:mm:ss"), RegistryValueKind.String);
					relationSetKey.SetValue("Modify Date", RelationSets[i].ModifyDate.ToString("yyyy-MM-dd HH:mm:ss"), RegistryValueKind.String);
					for (int j = 0; j < RelationSets[i].Databases.Count; j++)
					{
						relationSetKey.SetValue("Database Path " + j.ToString("00"), RelationSets[i].Databases[j], RegistryValueKind.String);
					}
					for (int j = 0; j < RelationSets[i].Relations.Count; j++)
					{
						RegistryKey relationKey = relationSetKey.CreateSubKey("Relation " + j.ToString("000"));
						if (relationKey != null)
						{
							relationKey.SetValue("Source Table", RelationSets[i].Relations[j].SourceTable, RegistryValueKind.String);
							relationKey.SetValue("Source Column", RelationSets[i].Relations[j].SourceColumn, RegistryValueKind.String);
							relationKey.SetValue("Destination Table", RelationSets[i].Relations[j].DestinationTable, RegistryValueKind.String);
							relationKey.SetValue("Destination Column", RelationSets[i].Relations[j].DestinationColumn, RegistryValueKind.String);
							relationKey.Close();
						}
					}
					relationSetKey.Close();
				}
			}
		}

		#endregion

		#region Database tree operations

		public void OpenDatabase(string databaseRootFolder)
		{
			CloseDatabase();
			ShowWaitMessage("Opening database, please wait...");
			TreeNode databaseNode = _mainForm.DatabaseTreeView.Nodes.Add(DATABASE_NODE_KEY, databaseRootFolder, 0, 0);
			IDictionary<string, string> files = Directory.GetFiles(databaseRootFolder, "*.xml").ToDictionary(filePath => Path.GetFileNameWithoutExtension(filePath), filePath => filePath);
			List<string> tableNames = files.Select(x => x.Key).ToList();
			tableNames.Sort();
			foreach (string tableName in tableNames)
			{
				string filePath = files[tableName];
				bool writable = TableHasSchema(filePath);
				TreeNode tableNode = databaseNode.Nodes.Add(tableName, tableName, (writable ? 1 : 2), (writable ? 1 : 2));
				tableNode.Tag = filePath;
				DatabaseTableSchemas.Add(tableName, LoadTableSchema(filePath));
				AddTableColumnsToTreeView(tableNode);
				if (tableNode.Text == SelectedTable)
				{
					SelectTable(tableNode);
				}
				if (ExpandedTables.Contains(tableName))
				{
					tableNode.Expand();
				}
			}
			databaseNode.Expand();
			if (_mainForm.DatabaseTreeView.SelectedNode != null)
			{
				_mainForm.DatabaseTreeView.SelectedNode.EnsureVisible();
			}
			CreateTableContextMenu();
			DatabaseRootFolder = databaseRootFolder;
		    UpdateRecentDatabases(DatabaseRootFolder);
            _watcher.Path = DatabaseRootFolder;
			_watcher.EnableRaisingEvents = true;
		    UpdateTableLastModifiedTime();
		    RelationSet relationSet = DatabaseRelationSet;
		    OnDatabaseRelationSetChanged(relationSet != null ? relationSet.Name : string.Empty);
			CloseWaitMessage();
		}

		public void CloseDatabase()
		{
			_watcher.EnableRaisingEvents = false;
		    OnDatabaseRelationSetChanged(string.Empty);
			DatabaseTableSchemas = new Dictionary<string, DataTable>();
			_mainForm.DatabaseTreeView.Nodes.Clear();
			DatabaseRootFolder = string.Empty;
		}

		private void AddTableColumnsToTreeView(TreeNode tableNode)
		{
			string tableName = tableNode.Name;
			if (DatabaseTableSchemas.ContainsKey(tableName))
			{
				foreach (DataColumn column in DatabaseTableSchemas[tableName].Columns)
				{
					tableNode.Nodes.Add(column.ColumnName, column.ColumnName, 3, 3);
				}
			}
		}

		private void UpdateTableInTreeView(string oldTableName, string newTableFilePath)
		{
			if (_mainForm.DatabaseTreeView.Nodes.Count > 0)
			{
				string newTableName = Path.GetFileNameWithoutExtension(newTableFilePath);
				TreeNode databaseNode = _mainForm.DatabaseTreeView.Nodes[DATABASE_NODE_KEY];
				TreeNode tableNode = databaseNode.Nodes[oldTableName];
				if (tableNode == null)
				{
					List<string> tableList = Tables.ToList();
					tableList.Add(newTableName); tableList.Sort();
					bool writable = TableHasSchema(newTableFilePath);
					tableNode = databaseNode.Nodes.Insert(tableList.IndexOf(newTableName), newTableName, newTableName, (writable ? 1 : 2), (writable ? 1 : 2));
					tableNode.Tag = newTableFilePath;
					DatabaseTableSchemas.Add(newTableName, LoadTableSchema(newTableFilePath));
					AddTableColumnsToTreeView(tableNode);
				}
				else if (newTableName != oldTableName)
				{
					bool isSelected = tableNode.IsSelected;
					databaseNode.Nodes.Remove(tableNode);
					tableNode.Name = tableNode.Text = newTableName;
					tableNode.Tag = newTableFilePath;
					List<string> tableList = Tables.ToList();
					tableList.Add(newTableName); tableList.Sort();
					databaseNode.Nodes.Insert(tableList.IndexOf(newTableName), tableNode);
					if (isSelected) _mainForm.DatabaseTreeView.SelectedNode = tableNode;
				}
				DataTable dataTable = LoadDataTable(GetTableFilePath(newTableName));
				UpdateTableColumnsInTreeView(tableNode, dataTable);
			}
		}

		private void RemoveTableFromTreeView(string tableName)
		{
			if (_mainForm.DatabaseTreeView.Nodes.Count > 0)
			{
				TreeNode databaseNode = _mainForm.DatabaseTreeView.Nodes[DATABASE_NODE_KEY];
				TreeNode tableNode = databaseNode.Nodes[tableName];
				if (tableNode != null)
				{
					databaseNode.Nodes.Remove(tableNode);
					DatabaseTableSchemas.Remove(tableName);
					TableExpanded(tableName, false);
				}
			}
		}

		private void UpdateTableColumnsInTreeView()
		{
			TreeNode tableNode = _mainForm.DatabaseTreeView.SelectedNode;
			DataTable dataTable = (DataTable) _mainForm.TableDataGridView.DataSource;
			UpdateTableColumnsInTreeView(tableNode, dataTable);
		}

		private static void UpdateTableColumnsInTreeView(TreeNode tableNode, DataTable dataTable)
		{
			TreeNode lastColumnNode = null;
			foreach (DataColumn column in dataTable.Columns)
			{
				TreeNode[] existingColumnNodes = tableNode.Nodes.Find(column.ColumnName, false);
				if (existingColumnNodes.Length > 0)
				{
					lastColumnNode = existingColumnNodes[0];
				}
				else
				{
					int index = (lastColumnNode != null ? tableNode.Nodes.IndexOf(lastColumnNode) : 0);
					tableNode.Nodes.Insert(index + 1, column.ColumnName, column.ColumnName, 3, 3);
				}
			}
			List<TreeNode> columnNodesToRemove = new List<TreeNode>();
			foreach (TreeNode columnNode in tableNode.Nodes)
			{
				if (!dataTable.Columns.Contains(columnNode.Text))
					columnNodesToRemove.Add(columnNode);
			}
			foreach (TreeNode columnNode in columnNodesToRemove)
			{
				tableNode.Nodes.Remove(columnNode);
			}
		}

        private void UpdateRecentDatabases(string databasePath)
        {
            int index = RecentDatabases.Select(dbPath => dbPath.ToLower()).ToList().IndexOf(databasePath.ToLower());
            if (index > -1)
                RecentDatabases.RemoveAt(index);
            RecentDatabases.Insert(0, databasePath);
            if (RecentDatabases.Count > MAX_RECENT_DATABASE_ITEM_COUNT)
                RecentDatabases.RemoveAt(RecentDatabases.Count - 1);

            UpdateRecentDatabasesMenu();
        }

		public void OpenDatabaseRootPath()
		{
			Process process = new Process();
			process.StartInfo.FileName = DatabaseRootFolder;
			process.StartInfo.Verb = "Open";
			process.Start();
		}

		#endregion

		#region Table operations

		private static bool TableHasSchema(string filePath)
		{
			bool hasSchema = true;
			try
			{
				new DataTable().ReadXml(filePath);
			}
			catch
			{
				hasSchema = false;
			}
			return hasSchema;
		}

		private static DataTable LoadTableSchema(string filePath)
		{
			DataTable dataTable = new DataTable();
			if (TableHasSchema(filePath))
			{
				dataTable.ReadXmlSchema(filePath);
			}
			else
			{
				dataTable = LoadDataTable(filePath);
				dataTable.Rows.Clear();
			}
			return dataTable;
		}

		public bool LoadTable()
		{
			bool loaded = false;
			if (SelectedTablePath != null)
			{
				loaded = LoadTable(SelectedTablePath);
				if (loaded)
				{
					SelectedTable = _mainForm.DatabaseTreeView.SelectedNode.Text;
					SaveRegistryValues();
				}
			}
			return loaded;
		}

		private bool LoadTable(string filePath)
		{
			if (!DiscardTable()) return false;

			ShowWaitMessage("Loading table, please wait...");
			TableIsReadOnly = !TableHasSchema(filePath);
			TableLastModifiedTime = Directory.GetLastWriteTime(filePath);
			_mainForm.TableDataGridView.DataSource = LoadDataTable(filePath);
			_mainForm.TableDataGridView.AutoResizeColumns();
			_mainForm.TableDataGridView.ReadOnly = TableIsReadOnly;
			_mainForm.TableDataGridView.AllowUserToAddRows = !TableIsReadOnly;
			_mainForm.TableDataGridView.AllowUserToDeleteRows = !TableIsReadOnly;
			UpdateTableLastModifiedTime();
			UpdateTableDetails();
			UpdateTableColumnsInTreeView();
			CloseWaitMessage();
			return true;
		}

		private static DataTable LoadDataTable(string filePath)
		{
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(filePath);
			return dataSet.Tables[0];
		}

		public void SaveTable()
		{
			try
			{
				if (SelectedTablePath != null)
				{
					OnSaveTableEnabled(false);
					SaveTable(SelectedTablePath);
				}
			}
			catch
			{
				OnSaveTableEnabled(true);
				throw;
			}
		}

		private void SaveTable(string filePath)
		{
			if (!TableIsReadOnly)
			{
				File.Copy(filePath, Path.ChangeExtension(filePath, ".xmldbviewer.bak"), true);
				DataTable dataTable = (DataTable) _mainForm.TableDataGridView.DataSource;
				dataTable.WriteXml(filePath, XmlWriteMode.WriteSchema);
				TableLastModifiedTime = Directory.GetLastWriteTime(filePath);
				TableIsModified = false;
				UpdateTableLastModifiedTime();
			}
			else
			{
				throw new ReadOnlyException("Unable to save. Current table is read only.");
			}
		}

		public bool DiscardTable()
		{
			bool discarded = false;
			if (!TableIsModified || ShowQuestion("Table has been modified. Do you want to discard the changes?") == DialogResult.Yes)
			{
				_mainForm.TableDataGridView.DataSource = null;
				_mainForm.TableDetailsListView.Items.Clear();
				OnTableLastModifiedTimeChanged(string.Empty);
				TableIsModified = false;
				discarded = true;
			}
			return discarded;
		}

		public void SelectTable(string tableName)
		{
			TreeNode databaseNode = _mainForm.DatabaseTreeView.Nodes[DATABASE_NODE_KEY];
			TreeNode tableNode = databaseNode.Nodes[tableName];
			if (tableNode != null)
			{
				SelectTable(tableNode);
			}
		}

		public void SelectTable(TreeNode node)
		{
			if (node == null) SelectedTable = string.Empty;
			_mainForm.DatabaseTreeView.SelectedNode = node;
			OnTableSelected(node != null);
		}

		public void DeleteRows()
		{
			if (SelectedTabView == TabView.Query) return;

			if (TableIsLoaded && !TableIsReadOnly)
			{
				foreach (DataGridViewRow row in _mainForm.TableDataGridView.SelectedRows)
				{
					_mainForm.TableDataGridView.Rows.Remove(row);
				}
				TableIsModified = true;
			}
		}

        private void HighlightTableDifferences(DataTable oldDataTable)
        {
            if (oldDataTable != null)
            {
                DataTable currentDataTable = (DataTable) _mainForm.TableDataGridView.DataSource;
                List<DataColumn> primaryKeyColumns = currentDataTable.PrimaryKey.ToList();
                if (primaryKeyColumns.Count == 0)
                    return;

                IEnumerable<string> oldPrimaryKeyNames = oldDataTable.PrimaryKey.Select(col => col.ColumnName);
                if (primaryKeyColumns.Count != primaryKeyColumns.Where(col => oldPrimaryKeyNames.Contains(col.ColumnName)).Count())
                    return;

                for (int i = 0; i < currentDataTable.Rows.Count; i++)
                {
                    DataRow row = currentDataTable.Rows[i];
                    List<object> primaryKeyValues = new List<object>();
                    primaryKeyColumns.ForEach(col => primaryKeyValues.Add(row[col.ColumnName]));
                    DataRow oldRow = oldDataTable.Rows.Find(primaryKeyValues.ToArray());
                    if (oldRow == null)
                    {
                        foreach (DataGridViewCell cell in _mainForm.TableDataGridView.Rows[i].Cells)
                        {
                            cell.Style.BackColor = Color.FromArgb(230, 255, 230);
                            cell.ToolTipText = "- new row -";
                        }
                    }
                    else
                    {
                        foreach (DataColumn column in currentDataTable.Columns)
                        {
                            if (row[column.ColumnName].ToString() != oldRow[column.ColumnName].ToString())
                            {
                                _mainForm.TableDataGridView.Rows[i].Cells[column.ColumnName].Style.BackColor = Color.FromArgb(230, 230, 255);
                                _mainForm.TableDataGridView.Rows[i].Cells[column.ColumnName].ToolTipText = oldRow[column.ColumnName].ToString();
                            }
                        }
                    }
                }
                _tableCellsHighlighted = true;
            }
        }

        public void ClearTableHighlights()
        {
            if (_tableCellsHighlighted)
            {
                _tableCellsHighlighted = false;
                foreach (DataGridViewRow row in _mainForm.TableDataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Style.BackColor != Color.White)
                        {
                            cell.Style.BackColor = Color.White;
                            cell.ToolTipText = string.Empty;
                        }
                    }
                }
            }
        }

		private void UpdateSelectedTable()
		{
			bool tableReloaded = false;
			if (TableAutoReloadEnabled || ShowQuestion("The current table has been modified. Do you wish to reload it?") == DialogResult.Yes)
			{
			    DataTable oldDataTable = (DataTable) _mainForm.TableDataGridView.DataSource;
				if (tableReloaded = LoadTable())
				    HighlightTableDifferences(oldDataTable);
			}
			if (!tableReloaded)
			{
				TableLastModifiedTime = Directory.GetLastWriteTime(SelectedTablePath);
				UpdateTableLastModifiedTime();
				TableIsModified = true;
			}
		}

		private void UpdateTableDetails()
		{
			_mainForm.TableDetailsListView.BeginUpdate();
			_mainForm.TableDetailsListView.Items.Clear();
			DataTable dataTable = (DataTable) _mainForm.TableDataGridView.DataSource;
			foreach (DataColumn column in dataTable.Columns)
			{
				ListViewItem item = _mainForm.TableDetailsListView.Items.Add(string.Empty, (column.Unique ? 4 : -1));
				item.UseItemStyleForSubItems = false;
				AddListViewSubItem(item, false, (column.Ordinal + 1).ToString());
				AddListViewSubItem(item, true, column.ColumnName);
				AddListViewSubItem(item, false, column.DataType.ToString().Replace("System.", string.Empty));
				AddListViewSubItem(item, false, column.DefaultValue.ToString());
				AddListViewSubItem(item, false, column.MaxLength.ToString());
				AddListViewSubItem(item, false, column.AllowDBNull.ToString());
				AddListViewSubItem(item, false, column.Unique.ToString());
			}
			_mainForm.TableDetailsListView.Sort();
			AutoSizeTableListView();
			_mainForm.TableDetailsListView.EndUpdate();
		}

		private static void AddListViewSubItem(ListViewItem item, bool highlightBackground, string value)
		{
			ListViewItem.ListViewSubItem subItem = item.SubItems.Add(value);
			if (highlightBackground) subItem.BackColor = SystemColors.Control;
		}

		public void AutoSizeTableListView()
		{
			for (int i = 0; i < _mainForm.TableDetailsListView.Columns.Count; i++)
			{
				AutoSizeTableListViewColumn(i);
			}
		}

		private void AutoSizeTableListViewColumn(int columnIndex)
		{
			_mainForm.TableDetailsListView.AutoResizeColumn(columnIndex, ColumnHeaderAutoResizeStyle.HeaderSize);
			int basedOnHeader = _mainForm.TableDetailsListView.Columns[columnIndex].Width;
			_mainForm.TableDetailsListView.AutoResizeColumn(columnIndex, ColumnHeaderAutoResizeStyle.ColumnContent);
			int basedOnContent = _mainForm.TableDetailsListView.Columns[columnIndex].Width;
			_mainForm.TableDetailsListView.Columns[columnIndex].Width = Math.Max(basedOnHeader, basedOnContent);
		}

		private void UpdateTableLastModifiedTime()
		{
            if (TableLastModifiedTime != DateTime.MinValue)
    			OnTableLastModifiedTimeChanged("Table Last Modified @ " + TableLastModifiedTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
		}

		public void TableExpanded(string tableName, bool expanded)
		{
			if (expanded)
			{
				if (!ExpandedTables.Contains(tableName))
					ExpandedTables.Add(tableName);
			}
			else
			{
				if (ExpandedTables.Contains(tableName))
					ExpandedTables.Remove(tableName);
			}
		}

		private string GetTableFilePath(string tableName)
		{
			TreeNode databaseNode = _mainForm.DatabaseTreeView.Nodes[DATABASE_NODE_KEY];
			TreeNode tableNode = databaseNode.Nodes[tableName];
			if (tableNode == null)
				throw new Exception("Could not find table named " + tableName + ".");

			return (string) tableNode.Tag;
		}

		public void OpenSelectedTableRawFile(bool edit)
		{
			if (SelectedTablePath != null && File.Exists(SelectedTablePath))
			{
				OpenTableRawFile(SelectedTablePath, edit);
			}
		}

		public void OpenTableRawFile(string tableName, int row)
		{
			string tableFilePath = GetTableFilePath(tableName);
			RegistryKey key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_ULTRAEDIT, false);
			if (key != null)
			{
				key.Close();
				Process process = new Process();
				process.StartInfo.FileName = "uedit32.exe";
				process.StartInfo.Arguments = "\"" + tableFilePath + "/" + row + "\"";
				process.Start();
			}
			else
			{
				OpenTableRawFile(tableFilePath, true);
			}
		}

		private static void OpenTableRawFile(string tableFilePath, bool edit)
		{
			Process process = new Process();
			process.StartInfo.FileName = tableFilePath;
			process.StartInfo.Verb = (edit ? "Edit" : "Open");
			process.Start();
		}

		#endregion

		#region GUI operations

		public void ShowError(Exception ex)
		{
			ApplicationExceptions++;
			ShowError("An exception occurred.\r\n\r\n" + ex);
		}

		public void ShowError(string message)
		{
			ShowMessage(message, MessageBoxIcon.Error);
		}

		public void ShowWarning(string message)
		{
			ShowMessage(message, MessageBoxIcon.Warning);
		}

		public void ShowInformation(string message)
		{
			ShowMessage(message, MessageBoxIcon.Information);
		}

		public DialogResult ShowQuestion(string message)
		{
			return ShowMessage(message, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}

		private void ShowMessage(string message, MessageBoxIcon icon)
		{
			ShowMessage(message, MessageBoxButtons.OK, icon);
		}

		private DialogResult ShowMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			if (_mainForm.InvokeRequired)
			{
				return (DialogResult) _mainForm.Invoke(new MethodInvoker(() => ShowMessage(message, buttons, icon)));
			}
            CloseWaitMessage();
			return MessageBox.Show(_mainForm, message, "XML Database Viewer", buttons, icon);
		}

		public string GetBuildTag()
		{
			// Assumes that in AssemblyInfo.cs, the version is specified as 1.0.* or the like,
			// with only 2 numbers specified; the next two are generated from the date

			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
			return version + " @ " + buildDate.ToString("yyyy-MM-dd");
		}

		public void DisplayTableDetails()
		{
			_mainForm.TableSplitter.Panel2Collapsed = !ShowTableDetails;
		}

		private void CreateTableContextMenu()
		{
			ToolStripMenuItem findInItem = (ToolStripMenuItem) _mainForm.TableContextMenu.Items["toolStripMenuItemTableFindIn"];
			foreach (ToolStripMenuItem tableItem in findInItem.DropDownItems)
			{
				foreach (ToolStripMenuItem columnItem in tableItem.DropDownItems)
				{
					columnItem.Click -= _mainForm.toolStripMenuItemTableFindInTableColumn_Click;
				}
				tableItem.DropDownItems.Clear();
			}
			findInItem.DropDownItems.Clear();
			IOrderedEnumerable<string> tableNames = DatabaseTableSchemas.Keys.OrderBy(tableName => tableName);
			foreach (string tableName in tableNames)
			{
				ToolStripMenuItem tableItem = new ToolStripMenuItem(tableName);
				findInItem.DropDownItems.Add(tableItem);
				DataColumn[] columns = new DataColumn[DatabaseTableSchemas[tableName].Columns.Count];
				DatabaseTableSchemas[tableName].Columns.CopyTo(columns, 0);
				IOrderedEnumerable<DataColumn> orderedColumns = columns.OrderBy(column => column.Caption);
				foreach (DataColumn column in orderedColumns)
				{
					ToolStripMenuItem columnItem = new ToolStripMenuItem(column.Caption) { Tag = tableName };
					columnItem.Click += _mainForm.toolStripMenuItemTableFindInTableColumn_Click;
					tableItem.DropDownItems.Add(columnItem);
				}
			}
		}

		private void ShowMagicSearchContextMenu(IDictionary<string, List<string>> columnsTables)
		{
			CreateMagicSearchContextMenu(columnsTables);
			_mainForm.MagicSearchContextMenu.Items[1].Select();
			DataGridView dataGridView = (SelectedTabView == TabView.Query ? _mainForm.QueryDataGridView : _mainForm.TableDataGridView);
			Rectangle rectangle = dataGridView.GetCellDisplayRectangle(dataGridView.CurrentCell.ColumnIndex, dataGridView.CurrentCell.RowIndex, true);
			Point point = dataGridView.PointToScreen(new Point(rectangle.Left, rectangle.Bottom));
			_mainForm.MagicSearchContextMenu.Show(point);
		}

		private void CreateMagicSearchContextMenu(IDictionary<string, List<string>> columnsTables)
		{
			foreach (ToolStripMenuItem menuItem in _mainForm.MagicSearchContextMenu.Items)
			{
				menuItem.Click -= _mainForm.toolStripMenuItemTableMagicFindInTableColumn_Click;
			}
			_mainForm.MagicSearchContextMenu.Items.Clear();
			foreach (string columnName in columnsTables.Keys)
			{
				_mainForm.MagicSearchContextMenu.Items.Add(new ToolStripMenuItem("Column: " + columnName) { Enabled = false });
				foreach (string tableName in columnsTables[columnName])
				{
					ToolStripMenuItem menuItem = new ToolStripMenuItem("   " + tableName) { Tag = columnName };
					menuItem.Click += _mainForm.toolStripMenuItemTableMagicFindInTableColumn_Click;
					_mainForm.MagicSearchContextMenu.Items.Add(menuItem);
				}
			}
		}

        public void UpdateRecentDatabasesMenu()
        {
            ToolStripMenuItem databaseItem = (ToolStripMenuItem) _mainForm.MainMenuStrip.Items["toolStripMenuItemMainDatabase"];
            ToolStripMenuItem recentItem = (ToolStripMenuItem) databaseItem.DropDownItems["toolStripMenuItemMainDatabaseRecent"];
            foreach (ToolStripMenuItem recentDatabaseItem in recentItem.DropDownItems)
            {
                recentDatabaseItem.Click -= _mainForm.toolStripMenuItemMainDatabaseRecentDatabase_Click;
            }
            recentItem.DropDownItems.Clear();
            recentItem.Enabled = (RecentDatabases.Count > 0);
            foreach (string recentDatabase in RecentDatabases)
            {
                ToolStripMenuItem recentDatabaseItem = new ToolStripMenuItem(recentDatabase);
                recentDatabaseItem.Tag = recentDatabase;
                recentDatabaseItem.Click += _mainForm.toolStripMenuItemMainDatabaseRecentDatabase_Click;
                recentItem.DropDownItems.Add(recentDatabaseItem);
            }
        }

		private void UpdateAdvancedSearchValueHistory(string searchValue)
		{
			if (!string.IsNullOrEmpty(searchValue))
			{
				if (AdvancedSearchValueHistory.Contains(searchValue))
				{
					AdvancedSearchValueHistory.Remove(searchValue);
				}
				AdvancedSearchValueHistory.Insert(0, searchValue);
				if (AdvancedSearchValueHistory.Count > MAX_ADVANCED_SEARCH_VALUE_HISTORY_ITEM_COUNT)
				{
					AdvancedSearchValueHistory.RemoveAt(AdvancedSearchValueHistory.Count - 1);
				}
			}
			SaveRegistryValues();
			OnAdvancedSearchValueListUpdated();
		}

		public void UpdateQueryHistory()
		{
			_mainForm.DiscardEvents = true;
			if (!string.IsNullOrEmpty(CurrentQuery))
			{
				if (QueryHistory.Contains(CurrentQuery))
				{
					QueryHistory.Remove(CurrentQuery);
				}
				QueryHistory.Insert(0, CurrentQuery);
				if (QueryHistory.Count > MAX_QUERY_HISTORY_ITEM_COUNT)
				{
					QueryHistory.RemoveAt(QueryHistory.Count - 1);
				}
			}
			ToolStripMenuItem queryMenuItem = (ToolStripMenuItem) _mainForm.MainMenuStrip.Items["toolStripMenuItemMainQuery"];
			ToolStripMenuItem queryHistoryMenuItem = (ToolStripMenuItem) queryMenuItem.DropDownItems["toolStripMenuItemMainQueryHistory"];
			ToolStripComboBox queryHistoryComboBox = (ToolStripComboBox) _mainForm.QueryToolStrip.Items["toolStripComboBoxQueryHistory"];
			foreach (ToolStripMenuItem menuItem in queryHistoryMenuItem.DropDownItems)
			{
				menuItem.Click -= _mainForm.toolStripMenuItemMainQueryHistoryEntry_Click;
			}
			queryHistoryMenuItem.DropDownItems.Clear();
			queryHistoryComboBox.Items.Clear();

			foreach (string historyItem in QueryHistory)
			{
				string encodedHistoryItem = EncodeQueryAsHistoryItem(historyItem);
				string limitedEncodedHistoryItem = GetWidthLimitedString(_mainForm, encodedHistoryItem, 300);
				ToolStripMenuItem menuItem = new ToolStripMenuItem(limitedEncodedHistoryItem) { Tag = encodedHistoryItem };
				menuItem.Click += _mainForm.toolStripMenuItemMainQueryHistoryEntry_Click;
				queryHistoryMenuItem.DropDownItems.Add(menuItem);
				queryHistoryComboBox.Items.Add(encodedHistoryItem);
			}
			if (CurrentQuery != null)
			{
				queryHistoryComboBox.SelectedItem = EncodeQueryAsHistoryItem(CurrentQuery);
			}
			_mainForm.DiscardEvents = false;
		}

		public static string EncodeQueryAsHistoryItem(string historyItem)
		{
			return historyItem.Replace("\r", "<CR>").Replace("\n", "<LF>");
		}

		public static string DecodeQueryAsHistoryItem(string historyItem)
		{
			return historyItem.Replace("<CR>", "\r").Replace("<LF>", "\n");
		}

		private void ShowWaitMessage(string waitMessage)
		{
			WaitOperationRunning = true;
			WaitMessageCreated = false;
			new Thread(ShowWaitMessageThread).Start(waitMessage);
			while (!WaitMessageCreated)
			{
				Application.DoEvents();
				Thread.Sleep(10);
			}
		}

		private void ShowWaitMessageThread(object data)
		{
			string waitMessage = data as string;
			if (waitMessage != null)
			{
				Point waitFormLocation = new Point(_mainForm.Location.X + _mainForm.Size.Width / 2, _mainForm.Location.Y + _mainForm.Size.Height / 2);
				WaitForm waitForm = new WaitForm(this, waitFormLocation, waitMessage);
				WaitMessageCreated = true;
				waitForm.ShowDialog();
			}
		}

		private void CloseWaitMessage()
		{
			WaitOperationRunning = false;
		}

		private ToolStripContainerPanel GetToolStripContainerPanel(ToolStrip toolStrip)
		{
			if (_mainForm.ToolStripContainer.TopToolStripPanel.Controls.Contains(toolStrip))
			{
				return ToolStripContainerPanel.Top;
			}
			if (_mainForm.ToolStripContainer.BottomToolStripPanel.Controls.Contains(toolStrip))
			{
				return ToolStripContainerPanel.Bottom;
			}
			if (_mainForm.ToolStripContainer.LeftToolStripPanel.Controls.Contains(toolStrip))
			{
				return ToolStripContainerPanel.Left;
			}
			if (_mainForm.ToolStripContainer.RightToolStripPanel.Controls.Contains(toolStrip))
			{
				return ToolStripContainerPanel.Right;
			}
			throw new Exception("Tool strip '" + toolStrip.Name + "' not found in any container.");
		}

		private void PositionToolStrip(ToolStrip toolStrip, ToolStripContainerPanel containerPanel)
		{
			Point location = toolStrip.Location;
			toolStrip.Parent.Controls.Remove(toolStrip);
			switch (containerPanel)
			{
				case ToolStripContainerPanel.Top:
					_mainForm.ToolStripContainer.TopToolStripPanel.Controls.Add(toolStrip);
					break;
				case ToolStripContainerPanel.Bottom:
					_mainForm.ToolStripContainer.BottomToolStripPanel.Controls.Add(toolStrip);
					break;
				case ToolStripContainerPanel.Left:
					_mainForm.ToolStripContainer.LeftToolStripPanel.Controls.Add(toolStrip);
					break;
				case ToolStripContainerPanel.Right:
					_mainForm.ToolStripContainer.RightToolStripPanel.Controls.Add(toolStrip);
					break;
			}
			toolStrip.Location = location;
		}

		#endregion

		#region Clipboard operations

		public void CopyClipboardText(int columnIndex)
		{
			if (TableIsLoaded || SelectedTabView == TabView.Query)
			{
				DataGridView dataGridView = (SelectedTabView == TabView.Query ? _mainForm.QueryDataGridView : _mainForm.TableDataGridView);
                if (columnIndex < 0)
                {
                    string output = string.Empty;
                    DataGridViewCell[] cells = new DataGridViewCell[dataGridView.SelectedCells.Count];
                    dataGridView.SelectedCells.CopyTo(cells, 0);
                    IOrderedEnumerable<DataGridViewCell> orderedCells = cells.OrderBy(cell => cell.RowIndex).ThenBy(cell => cell.ColumnIndex);
                    int xBasePos = cells.Min(cell => cell.ColumnIndex);
                    int yBasePos = cells.Min(cell => cell.RowIndex);
                    int lastRowIndex = yBasePos;
                    int lastColumnIndex = xBasePos;
                    foreach (DataGridViewCell cell in orderedCells)
                    {
                        if (cell.RowIndex > lastRowIndex)
                        {
                            output += RepeatString(Environment.NewLine, cell.RowIndex - lastRowIndex);
                            lastRowIndex = cell.RowIndex;
                            lastColumnIndex = xBasePos;
                        }
                        if (cell.ColumnIndex > lastColumnIndex)
                        {
                            output += RepeatString("\t", cell.ColumnIndex - lastColumnIndex);
                            lastColumnIndex = cell.ColumnIndex;
                        }
                        output += cell.Value.ToString();
                    }
                    Clipboard.SetText(output);
                }
                else
                    Clipboard.SetText(dataGridView.Columns[columnIndex].HeaderText);
			}
		}

		public void PasteClipboardText()
		{
			if (SelectedTabView == TabView.Query) return;

			if (TableIsLoaded && !TableIsReadOnly && Clipboard.ContainsText())
			{
				DataGridViewCell[] cells = new DataGridViewCell[_mainForm.TableDataGridView.SelectedCells.Count];
				_mainForm.TableDataGridView.SelectedCells.CopyTo(cells, 0);
				int xBasePos = cells.Min(cell => cell.ColumnIndex);
				int yBasePos = cells.Min(cell => cell.RowIndex);
				int yPos = 0;
				string[] rowValues = Clipboard.GetText().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
				foreach (string rowValue in rowValues)
				{
					if (!string.IsNullOrEmpty(rowValue) && (yBasePos + yPos) < _mainForm.TableDataGridView.Rows.Count)
					{
						int xPos = 0;
						_mainForm.TableDataGridView.CurrentCell = _mainForm.TableDataGridView.Rows[yBasePos + yPos].Cells[xBasePos + xPos];
						string[] cellValues = rowValue.Split("\t".ToCharArray());
						foreach (string cellValue in cellValues)
						{
							if (!string.IsNullOrEmpty(cellValue) && (xBasePos + xPos) < _mainForm.TableDataGridView.Columns.Count)
							{
								_mainForm.TableDataGridView.Rows[yBasePos + yPos].Cells[xBasePos + xPos].Value = cellValue;
							}
							xPos++;
						}
					}
					yPos++;
					_mainForm.TableDataGridView.NotifyCurrentCellDirty(true);
					_mainForm.TableDataGridView.NotifyCurrentCellDirty(false);
				}
			}
		}

		public void InsertNewGuid()
		{
			if (SelectedTabView == TabView.Query) return;

			if (TableIsLoaded && !TableIsReadOnly)
			{
				foreach (DataGridViewCell cell in _mainForm.TableDataGridView.SelectedCells)
				{
					cell.Value = Guid.NewGuid();
				}
				_mainForm.TableDataGridView.NotifyCurrentCellDirty(true);
				_mainForm.TableDataGridView.NotifyCurrentCellDirty(false);
			}
		}

        public void CopyNodeNameToClipboard(TreeNode node)
        {
            Clipboard.SetText(node.Text);
        }

		#endregion

		#region Search operations

		public bool FindColumnValue(string tableName, string columnName, string searchValue)
		{
			return FindColumnValue(tableName, columnName, searchValue, -1);
		}

		public bool FindColumnValue(string tableName, string columnName, string searchValue, int rowIndex)
		{
			TreeNode[] matchingNodes = _mainForm.DatabaseTreeView.Nodes.Find(tableName, true);
			if (matchingNodes.Length != 1)
				throw new Exception("Could not find table.");

			SelectTable(matchingNodes[0]);
			_mainForm.TabControl.SelectedIndex = 0;
			_mainForm.TableDataGridView.ClearSelection();
			bool firstIsSelected = false;
			foreach (DataGridViewRow row in _mainForm.TableDataGridView.Rows)
			{
				if (rowIndex == -1 || rowIndex == row.Index)
				{
					DataGridViewCell cell = row.Cells[columnName];
					if (cell != null && cell.Value != null && cell.Value.ToString() == searchValue)
					{
						if (!firstIsSelected)
						{
							if (SelectRowsWhenSearching)
							{
								cell.OwningRow.Selected = true;
								_mainForm.TableDataGridView.FirstDisplayedScrollingRowIndex = cell.RowIndex;
							}
							else
							{
								_mainForm.TableDataGridView.CurrentCell = cell;
								cell.Selected = true;
								EnsureCurrentTableCellVisible();
							}
							firstIsSelected = true;
						}
						else
						{
							if (SelectRowsWhenSearching)
							{
								cell.OwningRow.Selected = true;
							}
							else
							{
								cell.Selected = true;
							}
						}
					}
				}
			}
			if (_mainForm.TableDataGridView.SelectedCells.Count == 0)
			{
				ShowInformation("Could not find value \"" + searchValue + "\" in column " + columnName + ".");
			}
			return (_mainForm.TableDataGridView.SelectedCells.Count > 0);
		}

		private bool TableColumnContainsValue(string tableName, string columnName, string searchValue)
		{
			DataTable dataTable = LoadDataTable(GetTableFilePath(tableName));
			foreach (DataRow row in dataTable.Rows)
			{
				if (row[columnName].ToString() == searchValue)
				{
					return true;
				}
			}
			return false;
		}

		public void FindColumnValue()
		{
			DataGridView dataGridView = (SelectedTabView == TabView.Query ? _mainForm.QueryDataGridView : _mainForm.TableDataGridView);

			if (dataGridView.SelectedCells.Count != 1)
			{
				ShowError("Unable to perform magic search. " + (dataGridView.SelectedCells.Count > 1 ? "More than one" : "No") + " cell is selected.");
				return;
			}

			string searchValue = dataGridView.CurrentCell.Value.ToString();
			string originColumnName = dataGridView.CurrentCell.OwningColumn.HeaderText;
			string originTableName = (SelectedTabView == TabView.Query ? ((DataTable) dataGridView.DataSource).TableName : SelectedTable);
			RelationSet relationSet = DatabaseRelationSet;

			if (relationSet != null)
			{
				int matchesFound = 0;
				bool tableColumnFound = false;
				IDictionary<string, List<String>> matches = new Dictionary<string, List<string>>();
				foreach (Relation relation in relationSet.Relations)
				{
					if (relation.SourceTable.Equals(originTableName, StringComparison.CurrentCultureIgnoreCase)
						&& relation.SourceColumn.Equals(originColumnName, StringComparison.CurrentCultureIgnoreCase))
					{
						tableColumnFound = true;
						if (TableColumnContainsValue(relation.DestinationTable, relation.DestinationColumn, searchValue))
						{
							matchesFound++;
							if (matches.ContainsKey(relation.DestinationColumn))
								matches[relation.DestinationColumn].Add(relation.DestinationTable);
							else
								matches.Add(relation.DestinationColumn, new List<string>() { relation.DestinationTable });
						}
					}
					if (relation.DestinationTable.Equals(originTableName, StringComparison.CurrentCultureIgnoreCase)
						&& relation.DestinationColumn.Equals(originColumnName, StringComparison.CurrentCultureIgnoreCase))
					{
						tableColumnFound = true;
						if (TableColumnContainsValue(relation.SourceTable, relation.SourceColumn, searchValue))
						{
							matchesFound++;
							if (matches.ContainsKey(relation.SourceColumn))
								matches[relation.SourceColumn].Add(relation.SourceTable);
							else
								matches.Add(relation.SourceColumn, new List<string>() { relation.SourceTable });
						}
					}
				}
				if (!tableColumnFound)
					ShowInformation("No matching table and column could be found using\r\nthe " + relationSet.Name + " relation set.");
				else if (matchesFound == 0)
					ShowInformation("Could not find value \"" + searchValue + "\" in any of the related columns specified by the " + relationSet.Name + " relation set.");
				else if (matchesFound == 1)
					FindColumnValue(matches.First().Value[0], matches.First().Key, searchValue);
				else
					ShowMagicSearchContextMenu(matches);
			}
			else
			{
				bool doMagicSearch = false;
				string soughtColumnName = null;

				if (originColumnName.Length > 2 && (originColumnName.EndsWith("ID") || originColumnName.EndsWith("Id")))
				{
					string ch = originColumnName.Substring(originColumnName.Length - 3, 1);
					if (ch == ch.ToLower()) // third char from end is lower case or not a letter
					{
						doMagicSearch = true;
						soughtColumnName = originColumnName.Substring(originColumnName.Length - 2);
						string soughtTableName = originColumnName.Substring(0, originColumnName.Length - 2);
						TreeNode[] matchingNodes = _mainForm.DatabaseTreeView.Nodes.Find(soughtTableName, true);
						if (matchingNodes.Length == 1)
						{
							FindColumnValue(soughtTableName, soughtColumnName, searchValue);
							return;
						}
					}
				}
				else if (originColumnName.Equals("ID", StringComparison.CurrentCultureIgnoreCase))
				{
					doMagicSearch = true;
					soughtColumnName = originTableName + originColumnName;
				}

				if (doMagicSearch)
				{
					IEnumerable<string> tableCandidates = DatabaseTableSchemas.Where(x => x.Value.Columns.Contains(soughtColumnName))
						.Where(x => TableColumnContainsValue(x.Key, soughtColumnName, searchValue)).Select(x => x.Key);
					if (tableCandidates.Count() == 0)
						ShowInformation("Could not find matching column " + soughtColumnName + " in any table.");
					else if (tableCandidates.Count() == 1)
						FindColumnValue(tableCandidates.First(), soughtColumnName, searchValue);
					else
						ShowMagicSearchContextMenu(new Dictionary<string, List<string>>() { { soughtColumnName, tableCandidates.ToList() } });
				}
				else
				{
					ShowInformation("Could not perform magic search for selected cell.");
				}
			}
		}

		public void SelectTableColumn(string columnName, bool selectInDetails)
		{
			// select in table data grid
			bool firstIsSelected = false;
			foreach (DataGridViewRow row in _mainForm.TableDataGridView.Rows)
			{
				DataGridViewCell cell = row.Cells[columnName];
				if (!firstIsSelected)
				{
					_mainForm.TableDataGridView.CurrentCell = cell;
					firstIsSelected = true;
				}
				else
				{
					cell.Selected = true;
				}
			}
			EnsureCurrentTableCellVisible();
			// select in table details view, if instructed to
			if (selectInDetails)
			{
				foreach (ListViewItem item in _mainForm.TableDetailsListView.Items)
				{
					if (item.SubItems[2].Text == columnName)
					{
						item.Selected = true;
						_mainForm.TableDetailsListView.EnsureVisible(item.Index);
						break;
					}
				}
			}
		}

		private void EnsureCurrentTableCellVisible()
		{
			if (_mainForm.TableDataGridView.CurrentCell != null && !_mainForm.TableDataGridView.CurrentCell.Displayed)
			{
				_mainForm.TableDataGridView.FirstDisplayedScrollingColumnIndex = _mainForm.TableDataGridView.CurrentCell.ColumnIndex;
				_mainForm.TableDataGridView.FirstDisplayedScrollingRowIndex = _mainForm.TableDataGridView.CurrentCell.RowIndex;
			}
		}

		public void OpenAdvancedSearch()
		{
            if (_advSearchForm == null)
            {
                string searchValue = (_mainForm.TableDataGridView.SelectedCells.Count == 1 ? _mainForm.TableDataGridView.SelectedCells[0].Value.ToString() : null);
                _advSearchForm = new AdvancedSearchForm(this, searchValue);
                _advSearchForm.Location = new Point(_mainForm.Location.X + _mainForm.Size.Width / 2 - _advSearchForm.Size.Width / 2,
                    _mainForm.Location.Y + _mainForm.Size.Height / 2 - _advSearchForm.Size.Height / 2);
                _advSearchForm.Show(_mainForm);
            }
            else
            {
                _advSearchForm.Focus();
            }
		}

		public void CloseAdvancedSearch(bool isClosing)
		{
		    if (!isClosing && _advSearchForm != null) _advSearchForm.Close();
		    _advSearchForm = null;
		}

		public void FindAdvanced(string searchValue)
		{
			new Thread(FindAdvancedThread).Start(searchValue);
		}

		private void FindAdvancedThread(object data)
		{
			string searchValue = data as string;
			if (searchValue != null)
			{
				IEnumerable<string> searchTables = (AdvancedSearchSelectedTable == ADVANCED_SEARCH_TABLE_ALL ? Tables : new string[] { AdvancedSearchSelectedTable });
				foreach (string tableName in searchTables)
				{
					if (AdvancedSearchSelectedMode == ADVANCED_SEARCH_MODE_TABLE_NAMES)
					{
						if (AdvancedSearchMatch(searchValue, tableName))
						{
							OnNewAdvancedSearchResultItem(new AdvancedSearchResultItem(tableName, string.Empty, string.Empty, tableName));
						}
					}
					else
					{
						string tableFilePath = GetTableFilePath(tableName);
						if (AdvancedSearchSelectedMode == ADVANCED_SEARCH_MODE_TABLE_RAW_FILES)
						{
							using (StreamReader reader = new StreamReader(tableFilePath))
							{
								string textRow;
								int rowIndex = 0;
								while ((textRow = reader.ReadLine()) != null)
								{
									if (AdvancedSearchMatch(searchValue, textRow))
									{
										OnNewAdvancedSearchResultItem(new AdvancedSearchResultItem(tableName, string.Empty, (rowIndex + 1).ToString(), textRow));
									}
									rowIndex++;
								}
							}
						}
						else
						{
							DataTable dataTable = LoadDataTable(tableFilePath);
							foreach (DataColumn column in dataTable.Columns)
							{
								if (AdvancedSearchSelectedMode == ADVANCED_SEARCH_MODE_COLUMN_NAMES)
								{
									if (AdvancedSearchMatch(searchValue, column.ColumnName))
									{
										OnNewAdvancedSearchResultItem(new AdvancedSearchResultItem(tableName, column.ColumnName, string.Empty, column.ColumnName));
									}
								}
								else
								{
									for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
									{
										string cellValue = dataTable.Rows[rowIndex][column.ColumnName].ToString();
										if (AdvancedSearchMatch(searchValue, cellValue))
										{
											OnNewAdvancedSearchResultItem(new AdvancedSearchResultItem(tableName, column.ColumnName, (rowIndex + 1).ToString(), cellValue));
										}
									}
								}
							}
						}
					}
				}
				UpdateAdvancedSearchValueHistory(searchValue);
			}
			OnAdvancedSearchCompleted();
		}

		private bool AdvancedSearchMatch(string searchFor, string searchIn)
		{
			if (AdvancedSearchMatchWholeValue)
				return searchIn.Equals(searchFor, (AdvancedSearchMatchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase));
			else
				return (searchIn.IndexOf(searchFor, (AdvancedSearchMatchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase)) != -1);
		}

		#endregion

		#region Relation operations

		public void OpenDatabaseRelationSetManager()
		{
			DatabaseRelationSetManagerForm databaseRelationSetManagerForm = new DatabaseRelationSetManagerForm(this);
			databaseRelationSetManagerForm.ShowDialog(_mainForm);
            RelationSet relationSet = DatabaseRelationSet;
            OnDatabaseRelationSetChanged(relationSet != null ? relationSet.Name : string.Empty);
        }

		public bool OpenDatabaseRelationSet(RelationSet relationSet)
		{
			DatabaseRelationSetForm databaseRelationSetForm = new DatabaseRelationSetForm(this, relationSet);
			return (databaseRelationSetForm.ShowDialog(_mainForm) == DialogResult.OK);
		}

		public bool OpenDatabaseRelationSetAssociation(string databasePath)
		{
			DatabaseRelationSetAssociationForm databaseRelationSetAssociationForm = new DatabaseRelationSetAssociationForm(this, databasePath);
			return (databaseRelationSetAssociationForm.ShowDialog(_mainForm) == DialogResult.OK);
		}

        public bool AssignDatabaseRelationSet(RelationSet relationSet, string databasePath)
        {
            RelationSet associatedRelationSet = RelationSets.FirstOrDefault(rs => rs.Databases
                .Any(dbPath => dbPath.Equals(databasePath, StringComparison.CurrentCultureIgnoreCase)));
            if (associatedRelationSet == null || ShowQuestion("Database is " + (relationSet != null ? "already " : string.Empty)
                + "assigned to the " + associatedRelationSet.Name + " relation set.\r\n\r\nWould you like to "
                + (relationSet != null ? "replace that relation set with the selected one?" : "remove this association?")) == DialogResult.Yes)
            {
                if (associatedRelationSet != null)
                {
                    string associatedDatabasePath = associatedRelationSet.Databases.First(dbPath => dbPath.Equals(databasePath, StringComparison.CurrentCultureIgnoreCase));
                    associatedRelationSet.Databases.Remove(associatedDatabasePath);
                }
                if (relationSet != null) relationSet.Databases.Add(databasePath);
                SaveRegistryRelationSets();
                OnDatabaseRelationSetChanged(relationSet != null ? relationSet.Name : string.Empty);
                return true;
            }
            return false;
        }

        public string GetRelationSetCopyName(RelationSet relationSet)
        {
            int count = 0; string copyName;
            string name = copyName = relationSet.Name + " - Copy";
            while (RelationSets.Any(rs => rs.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                name = copyName + " (" + (++count) + ")";
            }
            return name;
        }

		public void GenerateRelations(RelationSet relationSet)
		{
			IDictionary<string, DataColumn[]> dataTableColumnCache = new Dictionary<string, DataColumn[]>();
			foreach (string sourceTableName in Tables)
			{
				DataColumn[] sourceColumns = GetDataTableColumnsUsingCache(sourceTableName, dataTableColumnCache);
				if (sourceColumns.Any(column => column.ColumnName.Equals("ID", StringComparison.CurrentCultureIgnoreCase)))
				{
					DataColumn sourceColumn = sourceColumns.First(col => col.ColumnName.Equals("ID", StringComparison.CurrentCultureIgnoreCase));
					foreach (string destinationTableName in Tables)
					{
						DataColumn[] destinationColumns = GetDataTableColumnsUsingCache(destinationTableName, dataTableColumnCache);
						foreach (DataColumn column in destinationColumns.Where(column => column.ColumnName.Equals(sourceTableName + "ID") || column.ColumnName.Equals(sourceTableName + "Id")))
						{
							relationSet.Relations.Add(new Relation(sourceTableName, sourceColumn.ColumnName, destinationTableName, column.ColumnName));
						}
					}
				}
			}
		}

		private DataColumn[] GetDataTableColumnsUsingCache(string tableName, IDictionary<string, DataColumn[]> dataTableColumnCache)
		{
			DataColumn[] dataTableColumns;
			if (dataTableColumnCache.ContainsKey(tableName))
				dataTableColumns = dataTableColumnCache[tableName];
			else
			{
				DataTable dataTable = LoadDataTable(GetTableFilePath(tableName));
				dataTableColumns = new DataColumn[dataTable.Columns.Count];
				dataTable.Columns.CopyTo(dataTableColumns, 0);
				dataTableColumnCache.Add(tableName, dataTableColumns);
			}
			return dataTableColumns;
		}

		#endregion

		#region Query operations

		public string OpenQuery()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
			openFileDialog.Multiselect = false;
			openFileDialog.CheckFileExists = true;
			return (openFileDialog.ShowDialog(_mainForm) == DialogResult.OK ? File.ReadAllText(openFileDialog.FileName) : null);
		}

		public void SaveQuery(string query)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.CreatePrompt = false;
			saveFileDialog.DefaultExt = ".sql";
			if (saveFileDialog.ShowDialog(_mainForm) == DialogResult.OK)
				File.WriteAllText(saveFileDialog.FileName, query);
		}

		public void CheckQuery(string query)
		{
			bool distinct;
			string[] columns;
			string table, expression, order;
			if (ParseQuery(query, false, out table, out columns, out distinct, out expression, out order))
				ShowInformation("The entered query is valid.");
		}

		private bool ParseQuery(string query, bool showErrorsInsteadOfWarnings, out string table,
			out string[] columns, out bool distinct, out string expression, out string order)
		{
			bool queryOk = false;
			columns = null;
			distinct = false;
			table = expression = order = null;
			MessageBoxIcon icon = (showErrorsInsteadOfWarnings ? MessageBoxIcon.Error : MessageBoxIcon.Warning);

			if (string.IsNullOrEmpty(DatabaseRootFolder))
				ShowMessage("Unable to validate query. No database is open.", icon);
			else
			{
				query = query.Trim().Replace("\r\n", " ").Replace("\t", " ").ToLower();
				if (!query.StartsWith("select "))
					ShowMessage("Only select queries are supported. Query must begin with \"select\".", icon);
				else
				{
					if (query.IndexOf(" from ") == -1)
						ShowMessage("Query must include the \"from\" keyword.", icon);
					else
					{
						query = query.Substring("select ".Length).TrimStart();
						distinct = query.StartsWith("distinct ");
						if (distinct) query = query.Substring("distinct ".Length);
						string[] split = query.Split(new string[] {" from "}, 2, StringSplitOptions.RemoveEmptyEntries);
						if (split.Length == 1)
							ShowMessage("Query is missing information about from which table to fetch data.", icon);
						else
						{
							columns = split[0].Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
							query = split[1];
							int whereIndex = query.IndexOf(" where "), orderIndex = query.IndexOf(" order by ");
							if (orderIndex != -1 && orderIndex < whereIndex)
								ShowMessage("Query must have the \"where\" keyword before the \"order by\" keyword.", icon);
							else
							{
								split = query.Split(new string[] {" where ", " order by "}, 3, StringSplitOptions.RemoveEmptyEntries);
								table = split[0].Trim();
								if (split.Length == 2)
								{
									if (orderIndex == -1)
										expression = split[1].Trim();
									else
										order = split[1].Trim();
								}
								else if (split.Length == 3)
								{
									expression = split[1].Trim();
									order = split[2].Trim();
								}
								string tempTable = table;
								if (!Tables.Any(t => t.Equals(tempTable, StringComparison.CurrentCultureIgnoreCase)))
									ShowMessage("Table \"" + table + "\" does not exist in the current database.", icon);
								else
								{
									queryOk = true;
									DataTable dataTable = LoadDataTable(GetTableFilePath(table));
									if (columns.Length == 1 && columns[0] == "*")
									{
										DataColumn[] dataColumns = new DataColumn[dataTable.Columns.Count];
										dataTable.Columns.CopyTo(dataColumns, 0);
										columns = dataColumns.Select(x => x.ColumnName).ToArray();
									}
									else
									{
										foreach (string column in columns)
										{
											if (dataTable.Columns[column] == null)
											{
												ShowMessage("Column \"" + column + "\" does not exist in table \"" + table + "\".", icon);
												queryOk = false;
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return queryOk;
		}

		public void ExecuteQuery(string query)
		{
			bool distinct;
			string[] columns;
			string table, expression, order;
			if (ParseQuery(query, true, out table, out columns, out distinct, out expression, out order))
			{
				try
				{
					ShowWaitMessage("Executing query, please wait...");
					_mainForm.QueryDataGridView.DataSource = null;
					DataTable dataTable = LoadDataTable(GetTableFilePath(table));
					DataView dataView = new DataView(dataTable, expression, order, DataViewRowState.CurrentRows);
					_mainForm.QueryDataGridView.DataSource = dataView.ToTable(distinct, columns);
					_mainForm.QueryDataGridView.AutoResizeColumns();
					UpdateQueryHistory();
				}
				finally
				{
					CloseWaitMessage();
				}
			}
		}

		#endregion

		#region String support

		private static string RepeatString(string value, int times)
		{
			string result = string.Empty;
			for (int i = 0; i < times; i++)
			{
				result += value;
			}
			return result;
		}

		private static string ListToString(IEnumerable<string> list)
		{
			string output = string.Empty;
			foreach (string value in list)
			{
				output += value + "|";
			}
			return output.TrimEnd("|".ToCharArray());
		}

		private static List<string> StringToList(string listString)
		{
			string[] values = listString.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			return new List<string>(values);
		}

		private static string GetWidthLimitedString(Form form, string str, int maxWidth)
		{
			int originalLength = str.Length;
			Graphics graphics = Graphics.FromHwnd(form.Handle);
			float width = graphics.MeasureString(str, form.Font).Width;
			while (str.Length > 0 && width > maxWidth)
			{
				str = str.Substring(0, str.Length - 1);
				width = graphics.MeasureString(str + " ...", form.Font).Width;
			}
			return (str.Length < originalLength ? str + " ..." : str);
		}

		#endregion
	}
}
