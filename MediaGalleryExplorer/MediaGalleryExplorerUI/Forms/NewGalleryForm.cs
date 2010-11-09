using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaGalleryExplorerCore.Workers;

namespace MediaGalleryExplorerUI.Forms
{
	public partial class NewGalleryForm : Form
	{
		public NewGalleryForm()
		{
			InitializeComponent();
		}

		private void NewGalleryForm_Load(object sender, EventArgs e)
		{
			foreach (KeyValuePair<string, int> encryptionAlgorithm in CommonWorker.GetAvailableEncryptionAlgorithms())
				comboBoxEncryption.Items.Add(new ComboBoxItem(encryptionAlgorithm.Key, encryptionAlgorithm.Value));
		}
	}
}
