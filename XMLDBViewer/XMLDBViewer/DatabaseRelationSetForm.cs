using System;
using System.Data;
using System.Windows.Forms;
using XMLDBViewer.DataObjects;

namespace XMLDBViewer
{
	public partial class DatabaseRelationSetForm : Form
	{
		private readonly Worker _worker;
		private readonly RelationSet _relationSet;

		public DatabaseRelationSetForm(Worker worker, RelationSet relationSet)
		{
			InitializeComponent();
			listViewRelations.ListViewItemSorter = new ListViewItemComparer(0, 1);
			_worker = worker;
			_relationSet = relationSet;
		}

		#region GUI event handlers

		private void DatabaseRelationSetForm_Load(object sender, EventArgs e)
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

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			try
			{
				AddRelation();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonRemove_Click(object sender, EventArgs e)
		{
			try
			{
				RemoveRelation();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonAuto_Click(object sender, EventArgs e)
		{
			try
			{
				EnableControls(false);
				AutoGenerateRelations();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			_relationSet.Name = textBoxName.Text;
			_relationSet.ModifyDate = DateTime.Now;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void textBoxName_KeyUp(object sender, KeyEventArgs e)
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

		private void listViewRelations_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			try
			{
				((ListViewItemComparer) listViewRelations.ListViewItemSorter).SetColumn(e.Column);
				listViewRelations.Sort();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewRelations_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				SelectTablesAndColumns();
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listBoxSourceTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (listBoxSourceTable.SelectedIndex > -1)
				{
					PopulateColumnList(listBoxSourceColumn, (string) listBoxSourceTable.SelectedItem);
				}
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listBoxSourceColumn_SelectedIndexChanged(object sender, EventArgs e)
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

		private void listBoxDestinationTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (listBoxDestinationTable.SelectedIndex > -1)
				{
					PopulateColumnList(listBoxDestinationColumn, (string) listBoxDestinationTable.SelectedItem);
				}
				EnableControls(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listBoxDestinationColumn_SelectedIndexChanged(object sender, EventArgs e)
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
			textBoxName.Text = _relationSet.Name;
			foreach (Relation relation in _relationSet.Relations)
			{
				AddRelationToListView(relation, false);
			}
			listBoxSourceTable.Items.Clear();
			listBoxDestinationTable.Items.Clear();
			foreach (string table in _worker.Tables)
			{
				listBoxSourceTable.Items.Add(table);
				listBoxDestinationTable.Items.Add(table);
			}
		}

		private void EnableControls(bool enable)
		{
			buttonAdd.Enabled = (enable
				&& listBoxSourceTable.SelectedIndex > -1 && listBoxSourceColumn.SelectedIndex > -1
				&& listBoxDestinationTable.SelectedIndex > -1 && listBoxDestinationColumn.SelectedIndex > -1);
			buttonRemove.Enabled = (enable && listViewRelations.SelectedItems.Count > 0);
			buttonAuto.Enabled = (enable && !string.IsNullOrEmpty(_worker.DatabaseRootFolder));
			buttonOK.Enabled = (enable && !string.IsNullOrEmpty(textBoxName.Text));
		}

		private void PopulateColumnList(ListBox listBox, string selectedTableName)
		{
			listBox.Items.Clear();
			if (_worker.DatabaseTableSchemas.ContainsKey(selectedTableName))
			{
				foreach (DataColumn column in _worker.DatabaseTableSchemas[selectedTableName].Columns)
				{
					listBox.Items.Add(column.ColumnName);
				}
			}
		}

		private void AddRelation()
		{
			string sourceTable = (string) listBoxSourceTable.SelectedItem;
			string sourceColumn = (string) listBoxSourceColumn.SelectedItem;
			string destinationTable = (string) listBoxDestinationTable.SelectedItem;
			string destinationColumn = (string) listBoxDestinationColumn.SelectedItem;
			Relation relation = new Relation(sourceTable, sourceColumn, destinationTable, destinationColumn);
			if (!_relationSet.Relations.Contains(relation))
			{
				_relationSet.Relations.Add(relation);
				AddRelationToListView(relation, true);
			}
		}

		private void RemoveRelation()
		{
			if (listViewRelations.SelectedItems.Count > 0)
			{
				foreach (ListViewItem selectedItem in listViewRelations.SelectedItems)
				{
					Relation relation = (Relation) selectedItem.Tag;
					_relationSet.Relations.Remove(relation);
					listViewRelations.Items.Remove(selectedItem);
				}
			}
		}

		private void SelectTablesAndColumns()
		{
			if (listViewRelations.SelectedItems.Count == 1)
			{
				Relation relation = (Relation) listViewRelations.SelectedItems[0].Tag;
				listBoxSourceTable.SelectedItem = relation.SourceTable;
				listBoxSourceColumn.SelectedItem = relation.SourceColumn;
				listBoxDestinationTable.SelectedItem = relation.DestinationTable;
				listBoxDestinationColumn.SelectedItem = relation.DestinationColumn;
			}
		}

		private void AutoGenerateRelations()
		{
			if (_worker.ShowQuestion("You are about to auto-generate relations based on the currently open database's table and column names. "
				+ "The auto-generated relations are not necessarily exhaustive, but rather generated on a best effort basis. "
				+ "By proceeding, all current relations for this relation set will be removed.\r\n\r\nAre you sure you wish to continue?") == DialogResult.Yes)
			{
				_relationSet.Relations.Clear();
				listViewRelations.Items.Clear();
				_worker.GenerateRelations(_relationSet);
				foreach (Relation relation in _relationSet.Relations)
				{
					AddRelationToListView(relation, false);
				}
			}
		}

		private void AddRelationToListView(Relation relation, bool select)
		{
			listViewRelations.SelectedItems.Clear();
			ListViewItem item = listViewRelations.Items.Add(relation.SourceTable + "." + relation.SourceColumn);
			item.SubItems.Add(relation.DestinationTable + "." + relation.DestinationColumn);
			item.Tag = relation;
			if (select) item.Selected = true;
		}

		#endregion
	}
}
