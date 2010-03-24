using System.Windows.Forms;

namespace XMLDBViewer
{
	public partial class AboutForm : Form
	{
		public AboutForm(Worker worker)
		{
			InitializeComponent();
			labelBuild.Text = "Build " + worker.GetBuildTag();
			labelStarts.Text = "Application started " + worker.ApplicationStarts + " time"
				+ (worker.ApplicationStarts == 1 ? string.Empty : "s");
			labelExceptions.Text = "Application generated exceptions " + worker.ApplicationExceptions + " time"
				+ (worker.ApplicationExceptions == 1 ? string.Empty : "s");
		}

		#region GUI event handlers

		private void AboutForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
				Close();
		}

		private void AboutForm_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void labelBuild_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void labelCredits_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void labelStarts_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void labelExceptions_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		#endregion
	}
}
