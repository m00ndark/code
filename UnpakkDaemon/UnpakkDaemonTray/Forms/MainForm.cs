using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UnpakkDaemon;
using UnpakkDaemon.DataAccess;
using UnpakkDaemon.DataObjects;
using UnpakkDaemon.EventArguments;
using UnpakkDaemon.Service.Client;
using UnpakkDaemonTray.EventArguments;
using UnpakkDaemonTray.Workers;

namespace UnpakkDaemonTray.Forms
{
	public partial class MainForm : Form
	{
		private bool _closing;
		private bool _discardEvents;
		private SettingsWorker _settingsWorker;
		private Image _statusRunning;
		private Image _statusPaused;
		private Image _statusStopped;
		private Image _record;
		private Image _recordRed;
		private Image _subRecord;
		private Image _subRecordRed;

		public MainForm()
		{
			InitializeComponent();
			_closing = false;
			_discardEvents = false;
			_settingsWorker = null;
			CommonWorker.ShowMessage += CommonWorker_ShowMessage;
		}

		#region GUI event handlers

		private void MainForm_Load(object sender, EventArgs e)
		{
			TraySettings.Load();
			if (TraySettings.WindowVisible)
				Restore();
			else
				Minimize();
			toolStripMenuItemOptionsStartWithWindows.Checked = TraySettings.StartWithWindows;
			SetStartWithWindows(TraySettings.StartWithWindows);
// ReSharper disable AssignNullToNotNullAttribute
			_statusRunning = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.Status-Running-16.png"));
			_statusPaused = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.Status-Paused-16.png"));
			_statusStopped = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.Status-Stopped-16.png"));
			_record = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.Record-48.png"));
			_recordRed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.Record-Red-48.png"));
			_subRecord = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.SubRecord-48.png"));
			_subRecordRed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("UnpakkDaemonTray.Resources.SubRecord-Red-48.png"));
// ReSharper restore AssignNullToNotNullAttribute
			pictureBoxRecord.Image = _record;
			pictureBoxSubRecord.Image = _subRecord;
			FileLogger.LogEntryRead += FileLogger_LogEntryRead;
			StatusChangedHandler statusChangedHandler = new StatusChangedHandler();
			statusChangedHandler.ProgressChanged += StatusChangedHandler_ProgressChanged;
			statusChangedHandler.SubProgressChanged += StatusChangedHandler_SubProgressChanged;
			statusChangedHandler.RecordAdded += StatusChangedHandler_RecordAdded;
			statusChangedHandler.SubRecordAdded += StatusChangedHandler_SubRecordAdded;
			statusChangedHandler.LogEntryAdded += StatusChangedHandler_LogEntryAdded;
			ObjectPool.StatusServiceHandler = new StatusServiceHandler(statusChangedHandler);
			ObjectPool.StatusServiceHandler.Start();
			ObjectPool.LogFilterDaysBack = 0;
			ObjectPool.LogFilterLeastLogType = LogType.Flow;
			SetupLogFilter();
			EngineSettings.Load();
			LoadRecords();
			LoadLog();
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

		private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				UpdateServiceStatus();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			if (TraySettings.WindowVisible)
				Minimize();
			else
				Restore();
		}

		private void toolStripMenuItemServiceStart_Click(object sender, EventArgs e)
		{
			if (ServiceHandler.ServiceIsStopped())
			{
				if (!ServiceHandler.StartService())
					FormUtilities.ShowError(this, "The Unpakk Daemon service failed to start.");
			}
			else if (ServiceHandler.ServiceIsRunning() && ObjectPool.StatusServiceHandler.EngineIsPaused())
			{
				ObjectPool.StatusServiceHandler.ResumeEngine();
			}
		}

		private void toolStripMenuItemServicePause_Click(object sender, EventArgs e)
		{
			if (ServiceHandler.ServiceIsRunning() && !ObjectPool.StatusServiceHandler.EngineIsPaused())
			{
				ObjectPool.StatusServiceHandler.PauseEngine();
			}
		}

		private void toolStripMenuItemOptionsStartWithWindows_Click(object sender, EventArgs e)
		{
			if (!_discardEvents)
				ToggleStartTrayWithWindows();
		}

		private void toolStripMenuItemRestore_Click(object sender, EventArgs e)
		{
			Restore();
		}

		private void toolStripMenuItemMinimize_Click(object sender, EventArgs e)
		{
			Minimize();
		}

		private void toolStripMenuItemClose_Click(object sender, EventArgs e)
		{
			_closing = true;
			Close();
		}

		private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPageIndex == 2)
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

		#region Progress tab

		private void treeViewRecords_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ShowRecordDetails(e.Node.Tag);
		}

		private void linkLabelRecordStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			SelectLogEntry((DateTime) linkLabelRecordStatus.Tag);
		}

		private void linkLabelRecordOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Record record = (Record) ((LinkLabel) sender).Tag;
			Process.Start(new ProcessStartInfo()
			{
				FileName = record.Folder,
				Verb = "open"
			});
		}

		private void linkLabelSubRecordStatus_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			SelectLogEntry((DateTime) linkLabelSubRecordStatus.Tag);
		}

		private void linkLabelSubRecordOpenFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			SubRecord subRecord = (SubRecord) ((LinkLabel) sender).Tag;
			Process.Start(new ProcessStartInfo()
				{
					FileName = Path.Combine(subRecord.Folder, subRecord.Name),
					Verb = "open"
				});
		}

		private void linkLabelSubRecordOpenFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			SubRecord subRecord = (SubRecord) ((LinkLabel) sender).Tag;
			Process.Start(new ProcessStartInfo()
			{
				FileName = subRecord.Folder,
				Verb = "open"
			});
		}

		#endregion

		#region Log tab

		private void comboBoxLogFilterDaysBack_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxLogFilterDaysBack.SelectedIndex > -1)
			{
				ObjectPool.LogFilterDaysBack = comboBoxLogFilterDaysBack.SelectedIndex;
				LoadLog();
			}
		}

		private void comboBoxLogFilterLeastLogType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxLogFilterLeastLogType.SelectedIndex > -1)
			{
				ObjectPool.LogFilterLeastLogType = (LogType) Enum.Parse(typeof(LogType), (string) comboBoxLogFilterLeastLogType.SelectedItem);
				LoadLog();
			}
		}

		#endregion

		#region Settings tab

		private void buttonBrowseApplicationDataFolder_Click(object sender, EventArgs e)
		{
			_settingsWorker.SetApplicationDataFolder();
		}

		private void checkBoxStartTrayAppWithWindows_CheckedChanged(object sender, EventArgs e)
		{
			if (!_discardEvents)
				ToggleStartTrayWithWindows();
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

		private void StatusChangedHandler_RecordAdded(object sender, RecordEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<RecordEventArgs>(StatusChangedHandler_RecordAdded), new object[] { sender, e });
			}
			else
			{
				AddRecord(e.Record, true);
			}
		}

		private void StatusChangedHandler_SubRecordAdded(object sender, SubRecordEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<SubRecordEventArgs>(StatusChangedHandler_SubRecordAdded), new object[] { sender, e });
			}
			else
			{
				AddSubRecord(e.ParentID, e.SubRecord, true);
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
				AddLogEntry(e.LogTime, e.LogType, e.LogText);
			}
		}

		private void FileLogger_LogEntryRead(object sender, LogEntryEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<LogEntryEventArgs>(FileLogger_LogEntryRead), new object[] { sender, e });
			}
			else
			{
				AddLogEntry(e.LogTime, e.LogType, e.LogText);
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

		private void SetupLogFilter()
		{
			comboBoxLogFilterLeastLogType.Items.Clear();
			foreach (string logType in Enum.GetNames(typeof(LogType)))
			{
				int index = comboBoxLogFilterLeastLogType.Items.Add(logType);
				if (logType == ObjectPool.LogFilterLeastLogType.ToString())
					comboBoxLogFilterLeastLogType.SelectedIndex = index;
			}
			comboBoxLogFilterDaysBack.Items.Clear();
			for (int i = 0; i <= 7; i++)
			{
				int index;
				if (i == 0)
					index = comboBoxLogFilterDaysBack.Items.Add("Today");
				else
					index = comboBoxLogFilterDaysBack.Items.Add(i + " day" + (i > 1 ? "s" : string.Empty) + " back");
				if (index == ObjectPool.LogFilterDaysBack)
					comboBoxLogFilterDaysBack.SelectedIndex = index;
			}
		}

		private void LoadRecords()
		{
			FileRecorder fileRecorder = new FileRecorder();
			foreach (Record record in fileRecorder.RecordList)
			{
				AddRecord(record, false);
				foreach (SubRecord subRecord in record.SubRecords)
				{
					AddSubRecord(record.ID, subRecord, false);
				}
			}
		}

		private void LoadLog()
		{
			listViewLog.Items.Clear();
			new FileLogger().Load(ObjectPool.LogFilterDaysBack, ObjectPool.LogFilterLeastLogType);
		}

		private void AddRecord(Record record, bool update)
		{
			TreeNode node = treeViewRecords.Nodes.Find(record.ID.ToString(), false).FirstOrDefault();
			int imageIndex = (record.Status == RecordStatus.Failure || record.SubRecordStatus == RecordStatus.Failure ? 1 : 0);

			if (node == null)
				node = treeViewRecords.Nodes.Add(record.ID.ToString(), Path.GetFileNameWithoutExtension(record.SFVName), imageIndex, imageIndex);
			else if (update)
			{
				node.Text = Path.GetFileNameWithoutExtension(record.SFVName);
				node.ImageIndex = node.SelectedImageIndex = imageIndex;
				if (node.IsSelected) ShowRecordDetails(record);
			}

			node.Tag = record;
			node.EnsureVisible();
		}

		private void AddSubRecord(Guid parentID, SubRecord subRecord, bool update)
		{
			TreeNode parentNode = treeViewRecords.Nodes.Find(parentID.ToString(), false).FirstOrDefault();
			if (parentNode != null)
			{
				TreeNode node = parentNode.Nodes.Find(subRecord.Name, false).FirstOrDefault();
				int imageIndex = (subRecord.Status == RecordStatus.Failure ? 3 : 2);

				if (node == null)
					node = parentNode.Nodes.Add(subRecord.Name, subRecord.Name, imageIndex, imageIndex);
				else if (update)
				{
					node.Text = subRecord.Name;
					node.ImageIndex = node.SelectedImageIndex = imageIndex;
					if (node.IsSelected) ShowRecordDetails(subRecord);
				}

				node.Tag = subRecord;

				Record parentRecord = (Record) parentNode.Tag;
				SubRecord existingSubRecord = parentRecord.SubRecords.FirstOrDefault(sr => (sr.Name == subRecord.Name));
				if (existingSubRecord == null)
					parentRecord.SubRecords.Add(subRecord);
				else
					existingSubRecord.CopyFrom(subRecord);

				AddRecord(parentRecord, true);
			}
		}

		private void ShowRecordDetails(object obj)
		{
			if (obj is Record)
			{
				Record record = (Record) obj;
				RecordStatus subRecordStatus = record.SubRecordStatus;
				bool success = (record.Status == RecordStatus.Success && subRecordStatus == RecordStatus.Success);
				bool failure = (record.Status == RecordStatus.Failure || subRecordStatus == RecordStatus.Failure);
				groupBoxRecordDetails.Visible = true;
				groupBoxSubRecordDetails.Visible = false;
				textBoxRecordPath.Text = record.Folder;
				textBoxRecordSFVFile.Text = record.SFVName;
				textBoxRecordRARFile.Text = record.RARName;
				labelRecordRARPartsValue.Text = record.RARCount.ToString();
				labelRecordRARSizeValue.Text = MakeFileSize(record.RARSize);
				labelRecordTimeValue.Text = record.Time.ToString("yyyy-MM-dd HH:mm:ss");
				linkLabelRecordStatus.Text = MakeStatus(success ? RecordStatus.Success : (failure ? RecordStatus.Failure : RecordStatus.InProgress))
					+ (subRecordStatus == RecordStatus.Failure ? " (sub record failed)" : string.Empty);
				linkLabelRecordStatus.LinkArea = new LinkArea(0, record.Status == RecordStatus.Failure ? linkLabelRecordStatus.Text.Length : 0);
				linkLabelRecordStatus.Tag = record.Time;
				pictureBoxRecord.Image = (failure ? _recordRed : _record);
				linkLabelRecordOpenFolder.Tag = record;
				toolTip.SetToolTip(linkLabelRecordOpenFolder, record.Folder);
			}
			else if (obj is SubRecord)
			{
				SubRecord subRecord = (SubRecord) obj;
				groupBoxRecordDetails.Visible = false;
				groupBoxSubRecordDetails.Visible = true;
				textBoxSubRecordPath.Text = subRecord.Folder;
				textBoxSubRecordFile.Text = subRecord.Name;
				labelSubRecordFileSizeValue.Text = MakeFileSize(subRecord.Size);
				labelSubRecordTimeValue.Text = subRecord.Time.ToString("yyyy-MM-dd HH:mm:ss");
				linkLabelSubRecordStatus.Text = MakeStatus(subRecord.Status);
				linkLabelSubRecordStatus.LinkArea = new LinkArea(0, subRecord.Status == RecordStatus.Failure ? linkLabelSubRecordStatus.Text.Length : 0);
				linkLabelSubRecordStatus.Tag = subRecord.Time;
				pictureBoxSubRecord.Image = (subRecord.Status == RecordStatus.Failure ? _subRecordRed : _subRecord);
				linkLabelSubRecordOpenFile.Tag = subRecord;
				linkLabelSubRecordOpenFolder.Tag = subRecord;
				toolTip.SetToolTip(linkLabelSubRecordOpenFolder, subRecord.Folder);
			}
			else
			{
				groupBoxRecordDetails.Visible = false;
				groupBoxSubRecordDetails.Visible = false;
			}
		}

		private void AddLogEntry(DateTime logTime, LogType logType, string logText)
		{
			ListViewItem item = listViewLog.Items.Add(logTime.ToString("yyyy-MM-dd HH:mm:ss"));
			item.SubItems.Add(logType.ToString());
			item.SubItems.Add(logText);
			item.Tag = logTime;
			item.EnsureVisible();
		}

		private void SelectLogEntry(DateTime dateTime)
		{
			tabControl.SelectedIndex = 1;
			if (listViewLog.Items.Count > 0)
			{
				listViewLog.SelectedItems.Clear();
				ListViewItem itemAfter = listViewLog.Items.Cast<ListViewItem>().FirstOrDefault(x => ((DateTime) x.Tag > dateTime && (DateTime) x.Tag < dateTime.AddSeconds(10)));
				if (itemAfter != null)
				{
					ListViewItem item = listViewLog.Items[Math.Max(0, itemAfter.Index - 1)];
					item.Selected = true;
					item.EnsureVisible();
				}
			}
		}

		private void Minimize()
		{
			TraySettings.WindowVisible = false;
			TraySettings.Save(TraySettingsType.TrayWindowVisibility);
			Hide();
			Opacity = 0;
			ShowInTaskbar = false;
			AdjustTrayMenu();
		}

		private void Restore()
		{
			TraySettings.WindowVisible = true;
			TraySettings.Save(TraySettingsType.TrayWindowVisibility);
			Opacity = 1;
			ShowInTaskbar = true;
			Show();
			WindowState = FormWindowState.Normal;
			Refresh();
			AdjustTrayMenu();
		}

		private void AdjustTrayMenu()
		{
			toolStripMenuItemRestore.Enabled = !TraySettings.WindowVisible;
			toolStripMenuItemMinimize.Enabled = TraySettings.WindowVisible;
			toolStripMenuItemRestore.Font = new Font(toolStripMenuItemClose.Font, (TraySettings.WindowVisible ? FontStyle.Regular : FontStyle.Bold));
			toolStripMenuItemMinimize.Font = new Font(toolStripMenuItemClose.Font, (TraySettings.WindowVisible ? FontStyle.Bold : FontStyle.Regular));
		}

		private void ToggleStartTrayWithWindows()
		{
			TraySettings.StartWithWindows = !TraySettings.StartWithWindows;
			TraySettings.Save(TraySettingsType.TrayStartupWithWindows);
			SetStartWithWindows(TraySettings.StartWithWindows);
		}

		private void SetStartWithWindows(bool enable)
		{
			_discardEvents = true;
			checkBoxStartTrayAppWithWindows.Checked = enable;
			toolStripMenuItemOptionsStartWithWindows.Checked = enable;
			_discardEvents = false;
			RegistryHandler.SetTrayToStartWithWindows(enable ? Application.ExecutablePath : null);
		}

		private void UpdateServiceStatus()
		{
			bool serviceIsRunning = ServiceHandler.ServiceIsRunning();
			bool serviceIsStopped = ServiceHandler.ServiceIsStopped();
			bool serviceIsStoppedOrNotRunning = (serviceIsStopped || !serviceIsRunning);
			bool engineIsPaused = (serviceIsStoppedOrNotRunning || ObjectPool.StatusServiceHandler.EngineIsPaused());
			toolStripMenuItemService.Image = (serviceIsStoppedOrNotRunning ? _statusStopped : (engineIsPaused ? _statusPaused : _statusRunning));
			toolStripMenuItemServiceStart.Enabled = (serviceIsStopped || serviceIsRunning && engineIsPaused);
			toolStripMenuItemServicePause.Enabled = (serviceIsRunning && !engineIsPaused);
			//bool serviceIsRunning = ServiceHandler.ServiceIsRunning();
			//bool engineIsPaused = (serviceIsRunning ? ObjectPool.StatusServiceHandler.EngineIsPaused() : true);
			//toolStripMenuItemService.Image = (serviceIsRunning ? (engineIsPaused ? _statusPaused : _statusRunning) : _statusStopped);
			//toolStripMenuItemServiceStart.Enabled = !serviceIsRunning || engineIsPaused;
			//toolStripMenuItemServicePause.Enabled = serviceIsRunning && !engineIsPaused;
		}

		private static string MakeFileSize(long bytes)
		{
			int i = 0;
			double value = bytes;
			while (value / 1024 > 1)
			{
				value /= 1024;
				i++;
			}
			string[] units = new string[] { "bytes", "kB", "MB", "GB", "TB" };
			return value.ToString("0.0") + " " + units[i];
		}

		private static string MakeStatus(RecordStatus status)
		{
			return status.ToString().Aggregate(string.Empty, (x, y) => (x + (y >= 65 && y <= 90 ? " " + y.ToString() : y.ToString()))).Trim();
		}

		#endregion
	}
}