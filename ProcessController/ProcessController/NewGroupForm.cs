using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ProcessController.Utilities;

namespace ProcessController
{
    public partial class NewGroupForm : Form
    {
        private readonly IEnumerable<string> _existingGroups;

        public NewGroupForm(IEnumerable<string> existingGroups)
        {
            InitializeComponent();
            _existingGroups = existingGroups;
            GroupName = null;
        }

        public string GroupName { get; private set; }

        private void NewSetForm_Load(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxGroupName.Text))
            {
                FormUtilities.ShowError(this, "Group name is empty.");
                return;
            }
            if (_existingGroups.Any(group => group.Equals(textBoxGroupName.Text, StringComparison.CurrentCultureIgnoreCase)))
            {
                FormUtilities.ShowError(this, "A group with the given name already exist.");
                return;
            }
            GroupName = textBoxGroupName.Text;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBoxGroupName_TextChanged(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void EnableControls(bool enable)
        {
            buttonOK.Enabled = (enable && !string.IsNullOrEmpty(textBoxGroupName.Text));
        }
    }
}
