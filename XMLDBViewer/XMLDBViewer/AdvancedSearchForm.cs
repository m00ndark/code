using System;
using System.Windows.Forms;
using XMLDBViewer.EventArguments;

namespace XMLDBViewer
{
	public partial class AdvancedSearchForm : Form
	{
		private readonly Worker _worker;
		private readonly string _initialSearchValue;

		public AdvancedSearchForm(Worker worker, string searchValue)
		{
			InitializeComponent();
			_worker = worker;
			_initialSearchValue = searchValue;
			_worker.NewAdvancedSearchResultItem += _worker_NewAdvancedSearchResultItem;
			_worker.AdvancedSearchCompleted += _worker_AdvancedSearchCompleted;
			_worker.AdvancedSearchValueListUpdated += _worker_AdvancedSearchValueListUpdated;
		}

		~AdvancedSearchForm()
		{
			_worker.NewAdvancedSearchResultItem -= _worker_NewAdvancedSearchResultItem;
			_worker.AdvancedSearchCompleted -= _worker_AdvancedSearchCompleted;
			_worker.AdvancedSearchValueListUpdated -= _worker_AdvancedSearchValueListUpdated;
		}

		#region Custom event handlers

		void _worker_NewAdvancedSearchResultItem(object sender, AdvancedSearchResultItemEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => _worker_NewAdvancedSearchResultItem(sender, e)));
			}
			else
			{
				try
				{
					AddSearchResultItem(e.Value);
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		void _worker_AdvancedSearchCompleted(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => _worker_AdvancedSearchCompleted(sender, e)));
			}
			else
			{
				EnableControls(true);
			}
		}

		void _worker_AdvancedSearchValueListUpdated(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new MethodInvoker(() => _worker_AdvancedSearchValueListUpdated(sender, e)));
			}
			else
			{
				try
				{
					UpdateSearchValueList();
				}
				catch (Exception ex)
				{
					_worker.ShowError(ex);
				}
			}
		}

		#endregion

		#region GUI event handlers

		private void AdvancedSearchForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitializeForm();
				comboBoxSearchTable.SelectedItem = _worker.AdvancedSearchSelectedTable;
				comboBoxSearchMode.SelectedItem = _worker.AdvancedSearchSelectedMode;
				checkBoxMatchCase.Checked = _worker.AdvancedSearchMatchCase;
				checkBoxMatchWholeValue.Checked = _worker.AdvancedSearchMatchWholeValue;
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void AdvancedSearchForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				_worker.SaveRegistryValues();
                CloseForm(true);
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void comboBoxSearchValue_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				StartSearching();
			}
		}

		private void comboBoxSearchTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			_worker.AdvancedSearchSelectedTable = (string) comboBoxSearchTable.SelectedItem;
		}

		private void comboBoxSearchMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			_worker.AdvancedSearchSelectedMode = (string) comboBoxSearchMode.SelectedItem;
			if ((string) comboBoxSearchMode.SelectedItem == Worker.ADVANCED_SEARCH_MODE_TABLE_NAMES)
			{
				comboBoxSearchTable.SelectedItem = Worker.ADVANCED_SEARCH_TABLE_ALL;
			}
			EnableControls(true);
		}

		private void checkBoxMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			_worker.AdvancedSearchMatchCase = checkBoxMatchCase.Checked;
		}

		private void checkBoxMatchWholeValue_CheckedChanged(object sender, EventArgs e)
		{
			_worker.AdvancedSearchMatchWholeValue = checkBoxMatchWholeValue.Checked;
		}

		private void buttonFind_Click(object sender, EventArgs e)
		{
			StartSearching();
		}

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CloseForm(false);
        }

		private void listViewSearchResult_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (listViewSearchResult.SelectedItems.Count == 1)
				{
					ListViewItem selectedItem = listViewSearchResult.SelectedItems[0];
					string tableName = selectedItem.SubItems[0].Text;
					string columnName = selectedItem.SubItems[1].Text;
					string searchValue = selectedItem.SubItems[3].Text;
					int rowIndex = (!string.IsNullOrEmpty(selectedItem.SubItems[2].Text) ? int.Parse(selectedItem.SubItems[2].Text) : -1);
					if (_worker.AdvancedSearchSelectedMode == Worker.ADVANCED_SEARCH_MODE_TABLE_NAMES)
					{
						_worker.SelectTable(tableName);
					}
					else if (_worker.AdvancedSearchSelectedMode == Worker.ADVANCED_SEARCH_MODE_COLUMN_NAMES)
					{
						_worker.SelectTable(tableName);
						_worker.SelectTableColumn(columnName, true);
					}
					else if (_worker.AdvancedSearchSelectedMode == Worker.ADVANCED_SEARCH_MODE_VALUES)
					{
						_worker.FindColumnValue(tableName, columnName, searchValue, rowIndex - 1);
					}
					else
					{
						_worker.OpenTableRawFile(tableName, rowIndex);
					}
				}
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void listViewSearchResult_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listViewSearchResult.Enabled)
			{
                CloseForm(false);
			}
		}

		#endregion

		#region Helpers

		private void InitializeForm()
		{
			// adjust window size
			labelResult.Visible = false;
			listViewSearchResult.Visible = false;
			listViewSearchResult.Anchor -= (int) AnchorStyles.Bottom;
			Height = labelResult.Top + (Size.Height - ClientSize.Height);
			// populate controls
			if (!string.IsNullOrEmpty(_initialSearchValue)) comboBoxSearchValue.Text = _initialSearchValue;
			UpdateSearchValueList();
			comboBoxSearchMode.Items.Clear();
			comboBoxSearchMode.Items.Add(Worker.ADVANCED_SEARCH_MODE_VALUES);
			comboBoxSearchMode.Items.Add(Worker.ADVANCED_SEARCH_MODE_COLUMN_NAMES);
			comboBoxSearchMode.Items.Add(Worker.ADVANCED_SEARCH_MODE_TABLE_NAMES);
			comboBoxSearchMode.Items.Add(Worker.ADVANCED_SEARCH_MODE_TABLE_RAW_FILES);
			comboBoxSearchTable.Items.Clear();
			comboBoxSearchTable.Items.Add(Worker.ADVANCED_SEARCH_TABLE_ALL);
			foreach (string tableName in _worker.Tables)
			{
				comboBoxSearchTable.Items.Add(tableName);
			}
		}

        private void CloseForm(bool isClosing)
        {
            _worker.CloseAdvancedSearch(isClosing);
        }

		private void PrepareForSearch()
		{
			EnableControls(false);
			listViewSearchResult.Items.Clear();
			if (!listViewSearchResult.Visible)
			{
				int formHeight = listViewSearchResult.Top + listViewSearchResult.Height + Padding.Bottom + (Size.Height - ClientSize.Height);
				if (formHeight > Height)
				{
					Height = formHeight;
				}
				else
				{
					listViewSearchResult.Height = ClientSize.Height - listViewSearchResult.Top - Padding.Bottom;
				}
				listViewSearchResult.Anchor += (int) AnchorStyles.Bottom;
				listViewSearchResult.Visible = true;
				labelResult.Visible = true;
			}
		}

		private void EnableControls(bool enable)
		{
			comboBoxSearchValue.Enabled = enable;
			comboBoxSearchTable.Enabled = (enable && comboBoxSearchMode.SelectedIndex > -1
				&& (string) comboBoxSearchMode.SelectedItem != Worker.ADVANCED_SEARCH_MODE_TABLE_NAMES);
			comboBoxSearchMode.Enabled = enable;
			checkBoxMatchCase.Enabled = enable;
			checkBoxMatchWholeValue.Enabled = enable;
			buttonFind.Enabled = enable;
			buttonCancel.Enabled = enable;
			listViewSearchResult.Enabled = enable;
		}

		private void AddSearchResultItem(AdvancedSearchResultItem searchResultItem)
		{
			ListViewItem item = listViewSearchResult.Items.Add(searchResultItem.TableName);
			item.SubItems.Add(searchResultItem.Column);
			item.SubItems.Add(searchResultItem.Row);
			item.SubItems.Add(searchResultItem.Value);
		}

		private void UpdateSearchValueList()
		{
			string currentSearchValue = comboBoxSearchValue.Text;
			comboBoxSearchValue.Items.Clear();
			foreach (string historyItem in _worker.AdvancedSearchValueHistory)
			{
				comboBoxSearchValue.Items.Add(historyItem);
			}
			if (currentSearchValue != null)
			{
				comboBoxSearchValue.SelectedItem = currentSearchValue;
			}
		}

		private void StartSearching()
		{
			try
			{
                if (!string.IsNullOrEmpty(comboBoxSearchValue.Text))
                {
                    PrepareForSearch();
                    _worker.FindAdvanced(comboBoxSearchValue.Text);
                }
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		#endregion
	}
}
