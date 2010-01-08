using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProcessController
{
    public partial class NewSetForm : Form
    {
        private readonly IEnumerable<string> _existingSets;

        public NewSetForm(IEnumerable<string> existingSets)
        {
            InitializeComponent();
            _existingSets = existingSets;
            SetName = null;
        }

        public string SetName { get; private set; }

        private void NewSetForm_Load(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSetName.Text))
            {
                FormUtilities.ShowError(this, "Set name is empty.");
                return;
            }
            if (_existingSets.Any(set => set.Equals(textBoxSetName.Text, StringComparison.CurrentCultureIgnoreCase)))
            {
                FormUtilities.ShowError(this, "A set with the given name already exist.");
                return;
            }
            SetName = textBoxSetName.Text;
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBoxSetName_TextChanged(object sender, EventArgs e)
        {
            EnableControls(true);
        }

        private void EnableControls(bool enable)
        {
            buttonOK.Enabled = (enable && !string.IsNullOrEmpty(textBoxSetName.Text));
        }
    }
}
