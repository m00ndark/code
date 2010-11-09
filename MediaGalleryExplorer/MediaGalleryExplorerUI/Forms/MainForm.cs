using System.Windows.Forms;

namespace MediaGalleryExplorerUI.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void toolStripMenuItemGalleryNew_Click(object sender, System.EventArgs e)
		{
			NewGalleryForm newGalleryForm = new NewGalleryForm();
			newGalleryForm.ShowDialog(this);
		}
	}
}
