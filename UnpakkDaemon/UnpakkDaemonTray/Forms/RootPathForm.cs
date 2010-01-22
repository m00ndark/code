using System;
using System.Windows.Forms;
using UnpakkDaemon;
using UnpakkDaemon.DataObjects;
using UnpakkDaemonTray.Workers;

namespace UnpakkDaemonTray.Forms
{
	public partial class RootPathForm : Form
	{
		private readonly SettingsWorker _settingsWorker;

		public RootPath RootPath { get; private set; }

		public RootPathForm(SettingsWorker settingsWorker, RootPath rootPath)
		{
			InitializeComponent();
			_settingsWorker = settingsWorker;
			RootPath = rootPath;
		}

		private void RootPathForm_Load(object sender, EventArgs e)
		{
			if (RootPath != null)
			{
				textBoxPath.Text = RootPath.Path;
				textBoxUserName.Text = RootPath.UserName;
				textBoxPassword.Text = RootPath.Password;
			}
			EnableControls(true);
		}

		private void textBoxPath_TextChanged(object sender, EventArgs e)
		{
			EnableControls(true);
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Please select a scan root folder...";
			folderBrowserDialog.SelectedPath = (string.IsNullOrEmpty(textBoxPath.Text) ? _settingsWorker.LastBrowsedRootPath : textBoxPath.Text);
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxPath.Text = folderBrowserDialog.SelectedPath;
				_settingsWorker.LastBrowsedRootPath = textBoxPath.Text;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (RootPath != null)
			{
				RootPath.Path = textBoxPath.Text;
				RootPath.UserName = textBoxUserName.Text;
				RootPath.Password = textBoxPassword.Text;
			}
			else
			{
				RootPath newRootPath = new RootPath(textBoxPath.Text, textBoxUserName.Text, textBoxPassword.Text);
				if (EngineSettings.RootPaths.Contains(newRootPath))
				{
					FormUtilities.ShowError(this, "Root path already exist.");
					return;
				}
				RootPath = newRootPath;
			}
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void EnableControls(bool enable)
		{
			bool uncPathSelected = textBoxPath.Text.StartsWith(@"\\");
			labelUserName.Enabled = (enable && uncPathSelected);
			textBoxUserName.Enabled = (enable && uncPathSelected);
			labelPassword.Enabled = (enable && uncPathSelected);
			textBoxPassword.Enabled = (enable && uncPathSelected);
			buttonOK.Enabled = (enable && !string.IsNullOrEmpty(textBoxPath.Text));
		}
	}
}
