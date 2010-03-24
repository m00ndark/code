using System;
using System.Linq;
using System.Windows.Forms;
using XMLDBViewer.DataObjects;

namespace XMLDBViewer
{
	public partial class DatabaseRelationSetAssociationForm : Form
	{
		private readonly Worker _worker;
		private readonly string _databasePath;

		public DatabaseRelationSetAssociationForm(Worker worker, string databasePath)
		{
			InitializeComponent();
			_worker = worker;
			_databasePath = databasePath;
		}

		#region GUI event handlers

		private void DatabaseRelationSetAssociationForm_Load(object sender, EventArgs e)
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

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult = (SetRelationSet() ? DialogResult.OK : DialogResult.Cancel);
				Close();
			}
			catch (Exception ex)
			{
				_worker.ShowError(ex);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void comboBoxRelationSets_SelectedIndexChanged(object sender, EventArgs e)
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
			textBoxDatabasePath.Text = _databasePath;
            RelationSet associatedRelationSet = _worker.RelationSets.FirstOrDefault(rs => rs.Databases
                .Any(dbPath => dbPath.Equals(_databasePath, StringComparison.CurrentCultureIgnoreCase)));
            comboBoxRelationSets.Items.Add(new ComboBoxItem(string.Empty, null));
            foreach (RelationSet relationSet in _worker.RelationSets)
			{
				int index = comboBoxRelationSets.Items.Add(new ComboBoxItem(relationSet.Name, relationSet));
                if (associatedRelationSet != null && relationSet == associatedRelationSet)
                    comboBoxRelationSets.SelectedIndex = index;
			}
        }

		private void EnableControls(bool enable)
		{
			buttonOK.Enabled = (enable && !string.IsNullOrEmpty(textBoxDatabasePath.Text) && comboBoxRelationSets.SelectedIndex > -1);
		}

		private bool SetRelationSet()
		{
			return (comboBoxRelationSets.SelectedIndex > -1
                ? _worker.AssignDatabaseRelationSet((RelationSet) ((ComboBoxItem) comboBoxRelationSets.SelectedItem).Tag, _databasePath) : false);
		}

		#endregion
	}
}
