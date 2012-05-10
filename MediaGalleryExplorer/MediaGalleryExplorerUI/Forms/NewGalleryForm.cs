using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaGalleryExplorerCore.DataObjects;
using MediaGalleryExplorerCore.Workers;

namespace MediaGalleryExplorerUI.Forms
{
	public partial class NewGalleryForm : Form
	{
		public NewGalleryForm()
		{
			InitializeComponent();
		}

		#region Properties

		public Gallery Gallery
		{
			get { return new Gallery(textBoxFilePath.Text, textBoxName.Text, textBoxPassword.Text, (int) ((ComboBoxItem) comboBoxEncryption.SelectedItem).Tag); }
		}

		#endregion

		private void NewGalleryForm_Load(object sender, EventArgs e)
		{
			foreach (KeyValuePair<string, int> encryptionAlgorithm in CommonWorker.GetAvailableEncryptionAlgorithms())
				comboBoxEncryption.Items.Add(new ComboBoxItem(encryptionAlgorithm.Key, encryptionAlgorithm.Value));
			EnableControls();
		}

		private void ButtonBrowseFilePath_Click(object sender, EventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog()
				{
					CheckPathExists = true,
					CheckFileExists = false,
					CreatePrompt = false,
					OverwritePrompt = true,
					SupportMultiDottedExtensions = true,
					AddExtension = true,
					DefaultExt = "mgdb",
					Filter = "Media Gallery Database (*.mgdb)|*.mgdb",
					Title = "Media Gallery Database"
				};
			if (fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBoxFilePath.Text = fileDialog.FileName;
			}
		}

		private void TextBoxFilePath_TextChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void TextBoxName_TextChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void ComboBoxEncryption_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void TextBoxPassword_TextChanged(object sender, EventArgs e)
		{
			EnableControls();
		}

		private void EnableControls(bool enable = true)
		{
			textBoxPassword.Enabled = (enable && comboBoxEncryption.SelectedIndex > 0);
			buttonOK.Enabled = (enable && CommonWorker.PathNameIsValid(textBoxFilePath.Text) && textBoxName.Text.Length > 0
				&& (comboBoxEncryption.SelectedIndex == 0 || comboBoxEncryption.SelectedIndex > 0 && textBoxPassword.Text.Length > 0));
		}
	}
}
