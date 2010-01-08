using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Application = ProcessController.DataObjects.Application;

namespace ProcessController
{
    public partial class AddApplicationForm : Form
    {
        private readonly IEnumerable<string> _groups;
        private readonly IEnumerable<string> _sets;

        public AddApplicationForm(IEnumerable<string> groups, IEnumerable<string> sets) : this(groups, sets, null) { }

        public AddApplicationForm(IEnumerable<string> groups, IEnumerable<string> sets, Application application)
        {
            InitializeComponent();
            _groups = groups;
            _sets = sets;
            Application = application;
        }

        public bool Edit
        {
            get { return (Application != null); }
        }

        public Application Application { get; private set; }

        private void AddApplicationForm_Load(object sender, EventArgs e)
        {
            if (Edit)
            {
                textBoxName.Text = Application.Name;
                textBoxPath.Text = Application.Path;
                textBoxArguments.Text = Application.Arguments;
            }
            comboBoxGroups.Items.Clear();
            comboBoxGroups.Items.Add(string.Empty);
            foreach (string group in _groups)
            {
                int index = comboBoxGroups.Items.Add(group);
                if (Edit && Application.Group != null && Application.Group.Equals(group))
                    comboBoxGroups.SelectedIndex = index;
            }
            listViewSets.Items.Clear();
            foreach (string set in _sets)
            {
                ListViewItem item = listViewSets.Items.Add(set);
                if (Edit && Application.Sets.Contains(set))
                    item.Selected = true;
            }
            EnableControls(true);
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Title = "Please select an application...";
            openFileDialog.Filter = "Applications (*.exe)|*.exe|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxPath.Text = openFileDialog.FileName;
            }
        }

        private void buttonNewGroup_Click(object sender, EventArgs e)
        {
            NewGroupForm newGroupForm = new NewGroupForm(comboBoxGroups.Items.Cast<string>().Where(group => !string.IsNullOrEmpty(group)));
            if (newGroupForm.ShowDialog(this) == DialogResult.OK && newGroupForm.GroupName != null)
            {
                int index = comboBoxGroups.Items.Add(newGroupForm.GroupName);
                comboBoxGroups.SelectedIndex = index;
            }
        }

        private void buttonNewSet_Click(object sender, EventArgs e)
        {
            NewSetForm newSetForm = new NewSetForm(listViewSets.Items.Cast<ListViewItem>().Select(item => item.Text));
            if (newSetForm.ShowDialog(this) == DialogResult.OK && newSetForm.SetName != null)
            {
                ListViewItem item = listViewSets.Items.Add(newSetForm.SetName);
                listViewSets.Sort();
                item.Selected = true;
                item.EnsureVisible();
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Application name is empty.", "Add Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPath.Text))
            {
                MessageBox.Show("Application path is empty.", "Add Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string group = (comboBoxGroups.SelectedItem != null && string.IsNullOrEmpty((string) comboBoxGroups.SelectedItem) ? null : (string) comboBoxGroups.SelectedItem);
            if (Edit)
            {
                Application.Name = textBoxName.Text;
                Application.Path = textBoxPath.Text;
                Application.Arguments = textBoxArguments.Text;
                Application.Group = group;
            }
            else
            {
                Application = new Application(textBoxName.Text, textBoxPath.Text, textBoxArguments.Text, group);
            }
            Application.Sets.Clear();
            foreach (string set in listViewSets.SelectedItems.Cast<ListViewItem>().Select(item => item.Text))
            {
                Application.Sets.Add(set);
            }
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void EnableControls(bool enable)
        {
            buttonOK.Enabled = (enable && !string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(textBoxPath.Text));
        }
    }
}
