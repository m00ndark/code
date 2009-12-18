using System;
using System.Windows.Forms;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Client;
using UnpakkDaemonTray.EventArguments;
using UnpakkDaemonTray.Workers;

namespace UnpakkDaemonTray.Forms
{
	public partial class MainForm : Form
	{
		private bool _closing;
		private SettingsWorker _settingsWorker;

		public MainForm()
		{
			InitializeComponent();
			_closing = false;
			_settingsWorker = null;
			CommonWorker.ShowMessage += CommonWorker_ShowMessage;
		}

		#region GUI event handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			Restore();
			StatusChangedHandler statusChangedHandler = new StatusChangedHandler();
			statusChangedHandler.ProgressChanged += StatusChangedHandler_ProgressChanged;
			statusChangedHandler.SubProgressChanged += StatusChangedHandler_SubProgressChanged;
			statusChangedHandler.LogEntryAdded += StatusChangedHandler_LogEntryAdded;
			ObjectPool.StatusServiceHandler = new StatusServiceHandler(statusChangedHandler);
			ObjectPool.StatusServiceHandler.Start();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_closing)
			{
				ObjectPool.StatusServiceHandler.Stop();
			}
			else
			{
				Minimize();
				e.Cancel = true;
			}
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
				Minimize();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			Restore();
		}

		private void toolStripMenuItemRestore_Click(object sender, EventArgs e)
		{
			Restore();
		}

		private void toolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			_closing = true;
			Close();
		}

		private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPageIndex == 1)
			{
				_settingsWorker = new SettingsWorker();
				_settingsWorker.ApplicationDataFolderUpdated += SettingsWorker_ApplicationDataFolderUpdated;
				_settingsWorker.SleepTimeUpdated += SettingsWorker_SleepTimeUpdated;
				_settingsWorker.RootPathListUpdated += SettingsWorker_RootPathListUpdated;
				_settingsWorker.Initialize();
			}
			else if (_settingsWorker != null)
			{
				if (_settingsWorker.SetSleepTime(textBoxSleepTime.Text))
				{
					_settingsWorker.Save();
					_settingsWorker.ApplicationDataFolderUpdated -= SettingsWorker_ApplicationDataFolderUpdated;
					_settingsWorker.SleepTimeUpdated -= SettingsWorker_SleepTimeUpdated;
					_settingsWorker.RootPathListUpdated -= SettingsWorker_RootPathListUpdated;
					_settingsWorker = null;
				}
				else
					e.Cancel = true;
			}
			EnableControls(true);
		}

		#region Settings tab

		private void buttonBrowseApplicationDataFolder_Click(object sender, EventArgs e)
		{
			_settingsWorker.SetApplicationDataFolder();
		}

		private void listViewRootPath_SelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControls(true);
		}

		private void buttonAddRootPath_Click(object sender, EventArgs e)
		{
			_settingsWorker.AddRootPath();
		}

		private void buttonRemoveRootPath_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewRootPath.SelectedItems)
			{
				_settingsWorker.RemoveRootPath(item.Text);
			}
		}

		#endregion

		#endregion

		#region Custom event handlers

		private void StatusChangedHandler_ProgressChanged(object sender, ProgressEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ProgressEventArgs>(StatusChangedHandler_ProgressChanged), new object[] { sender, e });
			}
			else
			{
				progressBarMainProgress.Value = (int) e.Percent;
				labelMainMessage.Text = e.Message;
				labelMainProgress.Text = (e.Current < 0 ? string.Empty : e.Current + "/" + e.Max);
			}
		}

		private void StatusChangedHandler_SubProgressChanged(object sender, ProgressEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ProgressEventArgs>(StatusChangedHandler_SubProgressChanged), new object[] { sender, e });
			}
			else
			{
				progressBarSubProgress.Value = (int) e.Percent;
				labelSubMessage.Text = e.Message;
				labelSubProgress.Text = (e.Current < 0 ? string.Empty : (int) e.Percent + "%");
			}
		}

		private void StatusChangedHandler_LogEntryAdded(object sender, LogEntryEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<LogEntryEventArgs>(StatusChangedHandler_LogEntryAdded), new object[] { sender, e });
			}
			else
			{
				ListViewItem item = listViewLog.Items.Add(e.LogTime.ToString("yyyy-MM-dd HH:mm:ss"));
				item.SubItems.Add(e.LogType.ToString());
				item.SubItems.Add(e.LogText);
			}
		}

		#endregion

		#region Worker event handlers

		private object CommonWorker_ShowMessage(object sender, MessageEventArgs e)
		{
			if (InvokeRequired)
				return Invoke(new CommonWorker.EventHandler<MessageEventArgs>(CommonWorker_ShowMessage), new object[] { sender, e });

			return FormUtilities.ShowMessage(this, e.Message, e.Buttons, e.Icon);
		}

		private void SettingsWorker_ApplicationDataFolderUpdated(object sender, StringEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<StringEventArgs>(SettingsWorker_ApplicationDataFolderUpdated), new object[] { sender, e });
				}
				else
				{
					textBoxApplicationDataFolder.Text = e.Value;
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void SettingsWorker_SleepTimeUpdated(object sender, TimeSpanEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<TimeSpanEventArgs>(SettingsWorker_SleepTimeUpdated), new object[] { sender, e });
				}
				else
				{
					textBoxSleepTime.Text = e.Value.TotalMinutes.ToString();
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		private void SettingsWorker_RootPathListUpdated(object sender, RootPathListEventArgs e)
		{
			try
			{
				if (InvokeRequired)
				{
					Invoke(new EventHandler<RootPathListEventArgs>(SettingsWorker_RootPathListUpdated), new object[] { sender, e });
				}
				else
				{
					listViewRootPath.BeginUpdate();
					listViewRootPath.Items.Clear();
					foreach (string rootPath in e.RootPaths)
					{
						listViewRootPath.Items.Add(rootPath);
					}
					listViewRootPath.EndUpdate();
				}
			}
			catch (Exception ex)
			{
				FormUtilities.ShowError(this, ex);
			}
		}

		#endregion

		#region Helpers

		private void EnableControls(bool enable)
		{
			buttonRemoveRootPath.Enabled = (enable && listViewRootPath.SelectedItems.Count > 0);
		}

		private void Minimize()
		{
			Hide();
			toolStripMenuItemRestore.Visible = true;
		}

		private void Restore()
		{
			Show();
			WindowState = FormWindowState.Normal;
			Refresh();
			toolStripMenuItemRestore.Visible = false;
		}

		#endregion
	}
}