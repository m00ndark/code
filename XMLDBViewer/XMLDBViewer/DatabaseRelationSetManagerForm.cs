using System;
using System.Linq;
using System.Windows.Forms;
using XMLDBViewer.DataObjects;

namespace XMLDBViewer
{
	public partial class DatabaseRelationSetManagerForm : Form
	{
		private readonly Worker _worker;

		public DatabaseRelationSetManagerForm(Worker worker)
		{
			InitializeComponent();
			listViewRelationSets.ListViewItemSorter = new ListViewItemComparer(0, 1);
			listViewDatabases.ListViewItemSorter = new ListViewItemComparer(0, 1);
			_worker = worker;
		}

		#region GUI event handlers

		private void DatabaseRelationSetManagerForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitializeForm();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonRelationSetNew_Click(object sender, EventArgs e)
		{
			try
			{
				NewRelationSet();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonRelationSetEdit_Click(object sender, EventArgs e)
		{
			try
			{
				EditRelationSet();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonRelationSetDelete_Click(object sender, EventArgs e)
		{
			try
			{
				DeleteRelationSet();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

        private void buttonRelationSetCopy_Click(object sender, EventArgs e)
        {
            try
            {
                CopyRelationSet();
                EnableControls(true);
            }
            catch (Exception ex)
            {
                _worker.ShowError(ex);
            }
        }

		private void buttonAssociationAdd_Click(object sender, EventArgs e)
		{
			try
			{
				AddDatabaseAssociation();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonAssociationRemove_Click(object sender, EventArgs e)
		{
			try
			{
				RemoveDatabaseAssociation();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewRelationSets_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			try
			{
				((ListViewItemComparer) listViewRelationSets.ListViewItemSorter).SetColumn(e.Column);
				listViewRelationSets.Sort();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewDatabases_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			try
			{
				((ListViewItemComparer) listViewDatabases.ListViewItemSorter).SetColumn(e.Column);
				listViewDatabases.Sort();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewRelationSets_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				EditRelationSet();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewRelationSets_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewDatabases_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion

		#region Helpers

		private void InitializeForm()
		{
			foreach (RelationSet relationSet in _worker.RelationSets)
			{
				AddRelationSetToListView(relationSet, false);
			}
			PopulateDatabaseAssociationsListView();
		}

		private void PopulateDatabaseAssociationsListView()
		{
			listViewDatabases.Items.Clear();
			foreach (RelationSet relationSet in _worker.RelationSets)
			{
				foreach (string databasePath in relationSet.Databases)
				{
					AddDatabaseAssociationToListView(databasePath, relationSet, false);
				}
			}
		}

		private void EnableControls(bool enable)
		{
			buttonRelationSetEdit.Enabled = (enable && listViewRelationSets.SelectedItems.Count == 1);
			buttonRelationSetDelete.Enabled = (enable && listViewRelationSets.SelectedItems.Count > 0);
            buttonRelationSetCopy.Enabled = (enable && listViewRelationSets.SelectedItems.Count == 1);
            buttonAssociationAdd.Enabled = (enable && listViewRelationSets.SelectedItems.Count == 1);
			buttonAssociationRemove.Enabled = (enable && listViewDatabases.SelectedItems.Count > 0);
		}

		private void AddRelationSetToListView(RelationSet relationSet, bool select)
		{
			listViewRelationSets.SelectedItems.Clear();
			ListViewItem item = listViewRelationSets.Items.Add(relationSet.Name);
			item.SubItems.Add(relationSet.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"));
			item.SubItems.Add(relationSet.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss"));
			item.Tag = relationSet;
			if (select) item.Selected = true;
		}

        private void UpdateRelationSetListView(RelationSet relationSet)
        {
            foreach (ListViewItem item in listViewRelationSets.Items)
            {
                if (item.Tag == relationSet)
                {
                    item.Text = relationSet.Name;
                    item.SubItems[2].Text = relationSet.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
        }

		private void AddDatabaseAssociationToListView(string databasePath, RelationSet relationSet, bool select)
		{
			listViewDatabases.SelectedItems.Clear();
			ListViewItem item = listViewDatabases.Items.Add(databasePath);
			item.SubItems.Add(relationSet.Name);
			item.Tag = databasePath;
			if (select) item.Selected = true;
		}

        private void RemoveDatabaseAssociationFromListView(string databasePath)
        {
            ListViewItem itemToRemove = null;
            foreach (ListViewItem item in listViewDatabases.Items)
            {
                if (item.Text.Equals(databasePath, StringComparison.CurrentCultureIgnoreCase))
                {
                    itemToRemove = item;
                    break;
                }
            }
            listViewDatabases.Items.Remove(itemToRemove);
        }

		private void NewRelationSet()
		{
			RelationSet relationSet = new RelationSet();
			if (_worker.OpenDatabaseRelationSet(relationSet))
			{
				if (!_worker.RelationSets.Contains(relationSet))
				{
					_worker.RelationSets.Add(relationSet);
					AddRelationSetToListView(relationSet, true);
				}
				_worker.SaveRegistryRelationSets();
			}
		}

		private void EditRelationSet()
		{
			if (listViewRelationSets.SelectedItems.Count == 1)
			{
				RelationSet relationSet = (RelationSet) listViewRelationSets.SelectedItems[0].Tag;
				RelationSet relationSetClone = relationSet.Clone();
				if (_worker.OpenDatabaseRelationSet(relationSetClone))
				{
					relationSet.Adopt(relationSetClone);
                    UpdateRelationSetListView(relationSet);
					_worker.SaveRegistryRelationSets();
				}
			}
		}

        private void CopyRelationSet()
        {
            if (listViewRelationSets.SelectedItems.Count == 1)
            {
                RelationSet relationSet = (RelationSet) listViewRelationSets.SelectedItems[0].Tag;
                RelationSet relationSetCopy = relationSet.Clone();
                relationSetCopy.Databases.Clear();
                relationSetCopy.Name = _worker.GetRelationSetCopyName(relationSetCopy);
                relationSetCopy.CreateDate = DateTime.Now;
                relationSetCopy.ModifyDate = relationSetCopy.CreateDate;
                _worker.RelationSets.Add(relationSetCopy);
                AddRelationSetToListView(relationSetCopy, true);
                _worker.SaveRegistryRelationSets();
            }
        }

		private void DeleteRelationSet()
		{
			if (listViewRelationSets.SelectedItems.Count > 0)
			{
				foreach (ListViewItem selectedItem in listViewRelationSets.SelectedItems)
				{
					RelationSet relationSet = (RelationSet) selectedItem.Tag;
					if (_worker.RelationSets.Contains(relationSet))
					{
						_worker.RelationSets.Remove(relationSet);
						_worker.SaveRegistryRelationSets();
						listViewRelationSets.Items.Remove(selectedItem);
						PopulateDatabaseAssociationsListView();
					}
				}
			}
		}

		private void AddDatabaseAssociation()
		{
			if (listViewRelationSets.SelectedItems.Count == 1)
			{
				FolderBrowserDialog folderDialog = new FolderBrowserDialog();
				folderDialog.Description = "Please select a database root path...";
				folderDialog.SelectedPath = _worker.DatabaseRootFolder;
				folderDialog.ShowNewFolderButton = false;
				if (folderDialog.ShowDialog(this) == DialogResult.OK)
				{
					string selectedDatabasePath = folderDialog.SelectedPath;
                    RelationSet existingRelationSet = _worker.RelationSets.FirstOrDefault(rs => rs.Databases
                        .Any(databasePath => databasePath.Equals(selectedDatabasePath, StringComparison.CurrentCultureIgnoreCase)));
					if (existingRelationSet != null)
					{
                        string existingDatabasePath = existingRelationSet.Databases
                            .First(databasePath => databasePath.Equals(selectedDatabasePath, StringComparison.CurrentCultureIgnoreCase));
					    existingRelationSet.Databases.Remove(existingDatabasePath);
                        RemoveDatabaseAssociationFromListView(existingDatabasePath);
					}
					RelationSet relationSet = (RelationSet) listViewRelationSets.SelectedItems[0].Tag;
					relationSet.Databases.Add(selectedDatabasePath);
					_worker.SaveRegistryRelationSets();
					AddDatabaseAssociationToListView(selectedDatabasePath, relationSet, true);
				}
			}
		}

		private void RemoveDatabaseAssociation()
		{
			if (listViewDatabases.SelectedItems.Count > 0)
			{
				foreach (ListViewItem selectedItem in listViewDatabases.SelectedItems)
				{
					string selectedDatabasePath = (string) selectedItem.Tag;
					RelationSet relationSet = _worker.RelationSets.First(rs => rs.Databases
						.Any(databasePath => databasePath.Equals(selectedDatabasePath, StringComparison.CurrentCultureIgnoreCase)));
					relationSet.Databases.Remove(selectedDatabasePath);
					listViewDatabases.Items.Remove(selectedItem);
				}
				_worker.SaveRegistryRelationSets();
			}
		}

		#endregion
	}
}
