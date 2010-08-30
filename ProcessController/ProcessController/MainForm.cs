using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ProcessController.DataAccess;
using ProcessController.DataObjects;
using ProcessController.Utilities;
using Application = ProcessController.DataObjects.Application;
using Timer = System.Windows.Forms.Timer;

namespace ProcessController
{
    public partial class MainForm : Form
    {
        private bool _closing;
        private bool _discardEvents;
        private bool _selectingApplicationBySet;
        private ControlRecentApplicationsForm _controlRecentApplicationsForm;
        private DateTime _controlRecentApplicationsFormClosedAt;
        private readonly Configuration _config;
        private readonly Timer _timer;

        public MainForm()
        {
            InitializeComponent();
            Text += " (build " + GetBuildTag() + ")";
            _closing = false;
            _discardEvents = false;
            _selectingApplicationBySet = false;
            _controlRecentApplicationsForm = null;
            _controlRecentApplicationsFormClosedAt = DateTime.MinValue;
            _config = RegistryHandler.LoadConfiguration();
            ApplicationControl.Configuration = _config;
            _timer = new Timer() { Interval = 500 };
            _timer.Tick += _timer_Tick;
            _timer.Start();
            toolStripMenuItemSystemTrayOptionsStartWithWindows.Checked = _config.StartWithWindows;
            listViewApplications.ListViewItemSorter = new ListViewItemComparer(0, 1);
            FillApplicationList();
        }

        #region Timer events

        private void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Stop();
                UpdateApplicationStatus();
                _timer.Start();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #region GUI Events

        #region Main form

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_config.WindowVisible)
                    Restore();
                else
                    Minimize();

                EnableControls(true);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_closing)
                {
                    Minimize();
                    e.Cancel = true;
                }
                else
                {
                    _config.WindowVisible = (Opacity == 1);
                    RegistryHandler.SaveConfiguration(_config);
                }
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (WindowState == FormWindowState.Minimized)
                    Minimize();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #region Control recent applications form

        private void _controlRecentApplicationsForm_Closed(object sender, EventArgs e)
        {
            _controlRecentApplicationsFormClosedAt = DateTime.Now;
            _controlRecentApplicationsForm.Closed -= _controlRecentApplicationsForm_Closed;
            _controlRecentApplicationsForm = null;
        }

        #endregion

        #region System tray icon

        private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_controlRecentApplicationsForm != null)
                {
                    _controlRecentApplicationsFormClosedAt = DateTime.Now;
                    _controlRecentApplicationsForm.Closed -= _controlRecentApplicationsForm_Closed;
                    _controlRecentApplicationsForm = null;
                }
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_controlRecentApplicationsForm == null && _controlRecentApplicationsFormClosedAt.AddMilliseconds(500) < DateTime.Now)
                {
                    _controlRecentApplicationsForm = new ControlRecentApplicationsForm(_config.RecentUsages, Restore);
                    _controlRecentApplicationsForm.Closed += _controlRecentApplicationsForm_Closed;
                    _controlRecentApplicationsForm.Show();
                }
            }
        }

        #endregion

        #region System tray context menu

        private void StartBySet_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                ApplicationControl.StartApplicationsBySet((string) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void RestartBySet_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                ApplicationControl.RestartApplicationsBySet((string) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void StopBySet_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                ApplicationControl.StopApplicationsBySet((string) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void StartSingle_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                ApplicationControl.StartApplication((Application) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void RestartSingle_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                ApplicationControl.RestartApplication((Application) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void StopSingle_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                ApplicationControl.StopApplication((Application) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void StartRecent_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                Application application = ApplicationControl.GetApplicationByID((string) item.Tag);
                if (application != null)
                    ApplicationControl.StartApplication(application);
                else
                    ApplicationControl.StartApplicationsBySet((string) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void RestartRecent_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                Application application = ApplicationControl.GetApplicationByID((string) item.Tag);
                if (application != null)
                    ApplicationControl.RestartApplication(application);
                else
                    ApplicationControl.RestartApplicationsBySet((string) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void StopRecent_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                Application application = ApplicationControl.GetApplicationByID((string) item.Tag);
                if (application != null)
                    ApplicationControl.StopApplication(application);
                else
                    ApplicationControl.StopApplicationsBySet((string) item.Tag);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void toolStripMenuItemSystemTrayOptionsStartWithWindows_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleOptionStartWithWindows();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void toolStripMenuItemSystemTrayRestoreMinimize_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opacity == 1)
                    Minimize();
                else
                    Restore();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void toolStripMenuItemSystemTrayExit_Click(object sender, EventArgs e)
        {
            try
            {
                Exit();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #region Application list context menu

        private void toolStripMenuItemApplicationListStart_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<Application> stoppedApplications =
                    listViewApplications.SelectedItems.Cast<ListViewItem>().Select(x => (Application) x.Tag).Where(application => !application.IsRunning);
                if (stoppedApplications.Count() > 0)
                    ApplicationControl.StartApplications(stoppedApplications);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void toolStripMenuItemApplicationListStop_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<Application> runningApplications =
                    listViewApplications.SelectedItems.Cast<ListViewItem>().Select(x => (Application) x.Tag).Where(application => application.IsRunning);
                if (runningApplications.Count() > 0)
                    ApplicationControl.StopApplications(runningApplications);
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void toolStripMenuItemApplicationListEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EditApplication();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void toolStripMenuItemApplicationListRemove_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveApplications();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #region Set combo box

        private void comboBoxSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_discardEvents)
                {
                    SelectApplicationsBySet();
                    EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #region Application list view

        private void listViewApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!_discardEvents)
                {
                    if (!_selectingApplicationBySet) comboBoxSets.SelectedIndex = -1;
                    UpdateSelectedApplicationInfo();
                    EnableControls(true);
                }
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void listViewApplications_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ((ListViewItemComparer) listViewApplications.ListViewItemSorter).SetColumn(e.Column);
                listViewApplications.Sort();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void listViewApplications_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                EditApplication();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void listViewApplications_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && listViewApplications.SelectedItems.Count > 0)
                {
                    Application application = (Application) listViewApplications.SelectedItems[0].Tag;
                    toolStripMenuItemApplicationListStart.Enabled = (listViewApplications.SelectedItems.Count > 1 || !application.IsRunning);
                    toolStripMenuItemApplicationListStop.Enabled = (listViewApplications.SelectedItems.Count > 1 || application.IsRunning);
                    toolStripMenuItemApplicationListEdit.Enabled = (listViewApplications.SelectedItems.Count == 1);
                    contextMenuStripApplicationList.Show(MousePosition);
                }
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #region Buttons

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddApplication();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveApplications();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveAllApplications();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                ImportApplications();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExportApplications();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                Exit();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        #endregion

        #endregion

        #region Actions

        private void AddApplication()
        {
            AddApplicationForm addApplicationForm = new AddApplicationForm(_config.Groups, _config.Sets);
            if (addApplicationForm.ShowDialog(this) == DialogResult.OK && addApplicationForm.Application != null)
            {
                _config.Applications.Add(addApplicationForm.Application);
                RegistryHandler.SaveConfiguration(_config);
                AddApplicationToList(addApplicationForm.Application);
            }
        }

        private void EditApplication()
        {
            if (listViewApplications.SelectedItems.Count == 1)
            {
                Application application = (Application) listViewApplications.SelectedItems[0].Tag;
                AddApplicationForm addApplicationForm = new AddApplicationForm(_config.Groups, _config.Sets, application);
                if (addApplicationForm.ShowDialog(this) == DialogResult.OK && addApplicationForm.Application != null)
                {
                    RegistryHandler.SaveConfiguration(_config);
                    AddApplicationToList(addApplicationForm.Application);
                    UpdateSelectedApplicationInfo();
                }
            }
        }

        private void RemoveApplications()
        {
            if (listViewApplications.SelectedItems.Count > 0)
            {
                _discardEvents = true;
                foreach (ListViewItem item in listViewApplications.SelectedItems)
                {
                    Application application = (Application) item.Tag;
                    _config.Applications.Remove(application);
                    _config.RecentUsages.Where(recent => (recent.ID == application.ID.ToString()))
                        .ToList().ForEach(recent => _config.RecentUsages.Remove(recent));
                    listViewApplications.Items.Remove(item);
                }
                _config.RecentUsages.Where(recent => (ApplicationControl.GetApplicationByID(recent.ID) == null && !_config.Sets.Contains(recent.ID)))
                    .ToList().ForEach(recent => _config.RecentUsages.Remove(recent));
                RegistryHandler.SaveConfiguration(_config);
                _discardEvents = false;
                UpdateSetList();
                UpdateSelectedApplicationInfo();
                EnableControls(true);
            }
        }

        private void RemoveAllApplications()
        {
            listViewApplications.Items.Clear();
            _config.Applications.Clear();
            _config.RecentUsages.Clear();
            RegistryHandler.SaveConfiguration(_config);
            UpdateSetList();
            UpdateSelectedApplicationInfo();
            EnableControls(true);
        }

        private void ImportApplications()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.Title = "Please select a configuration file to import...";
            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Configuration importedConfig = FileHandler.Deserialize<Configuration>(openFileDialog.FileName);
                foreach (Application application in importedConfig.Applications)
                    _config.Applications.Add(application);
                foreach (RecentUsage recentUsage in importedConfig.RecentUsages)
                    _config.RecentUsages.Add(recentUsage);
                RegistryHandler.SaveConfiguration(_config);
                FillApplicationList();
                EnableControls(true);
                FormUtilities.ShowInformation(this, "Successfully imported " + importedConfig.Applications.Count
                    + " applications and " + _config.RecentUsages.Count + " recent usages.");
            }
        }

        private void ExportApplications()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Please select a target file...";
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            saveFileDialog.FileName = "processcontroller_applications.xml";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileHandler.Serialize(saveFileDialog.FileName, _config);
                FormUtilities.ShowInformation(this, "Successfully exported " + _config.Applications.Count + " applications and "
                    + _config.RecentUsages.Count + " recent usages to" + Environment.NewLine + saveFileDialog.FileName);
            }
        }

        private void ToggleOptionStartWithWindows()
        {
            _config.StartWithWindows = !_config.StartWithWindows;
            RegistryHandler.SaveConfiguration(_config);
            RegistryHandler.SetWindowsStartupTrigger(_config.StartWithWindows);
            toolStripMenuItemSystemTrayOptionsStartWithWindows.Checked = _config.StartWithWindows;
        }

        private void Minimize()
        {
            Hide();
            Opacity = 0;
            ShowInTaskbar = false;
            WindowState = FormWindowState.Normal;
            toolStripMenuItemSystemTrayRestoreMinimize.Text = "Restore";
        }

        private void Restore()
        {
            ShowInTaskbar = true;
            Opacity = 1;
            Show();
            WindowState = FormWindowState.Normal;
            Refresh();
            toolStripMenuItemSystemTrayRestoreMinimize.Text = "Minimize";
            try { Program.SetForegroundWindow(Handle); } catch { }
        }

        private void Exit()
        {
            _closing = true;
            Close();
        }

        #endregion

        #region Helpers

        public string GetBuildTag()
        {
            // Assumes that in AssemblyInfo.cs, the version is specified as 1.0.* or the like,
            // with only 2 numbers specified; the next two are generated from the date

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
            return version + " @ " + buildDate.ToString("yyyy-MM-dd");
        }

        private void EnableControls(bool enable)
        {
            buttonRemove.Enabled = (enable && listViewApplications.SelectedItems.Count > 0);
            buttonRemoveAll.Enabled = (enable && listViewApplications.Items.Count > 0);
        }

        private void UpdateSetList()
        {
            _discardEvents = true;
            string selectedSet = (string) comboBoxSets.SelectedItem;
            comboBoxSets.Items.Clear();
            foreach (string set in _config.Sets)
            {
                int index = comboBoxSets.Items.Add(set);
                if (set.Equals(selectedSet, StringComparison.CurrentCultureIgnoreCase))
                    comboBoxSets.SelectedIndex = index;
            }
            UpdateSystemTrayContextMenuSets();
            _discardEvents = false;
        }

        private void FillApplicationList()
        {
            listViewApplications.Items.Clear();
            foreach (Application application in _config.Applications)
            {
                AddApplicationToList(application);
            }
        }

        private void AddApplicationToList(Application application)
        {
            ListViewItem item = listViewApplications.Items.Cast<ListViewItem>().FirstOrDefault(x => (x.Tag == application));
            if (item == null)
            {
                item = listViewApplications.Items.Add(string.Empty);
                item.SubItems.Add(application.Group);
                item.SubItems.Add(application.Name);
                item.SubItems.Add(application.Path);
                item.SubItems.Add(application.Arguments);
                item.Tag = application;
            }
            else
            {
                item.SubItems[1].Text = application.Group;
                item.SubItems[2].Text = application.Name;
                item.SubItems[3].Text = application.Path;
                item.SubItems[4].Text = application.Arguments;
            }
            item.ImageIndex = (application.IsRunning ? 0 : 1);
            UpdateSetList();
        }

        private void UpdateApplicationStatus()
        {
            IList<Application> runningApplications = new List<Application>();
            IList<Application> stoppedApplications = new List<Application>();
            foreach (ListViewItem item in listViewApplications.Items)
            {
                int imageIndex;
                Application application = (Application) item.Tag;
                if (application.IsRunning)
                {
                    runningApplications.Add(application);
                    imageIndex = 0;
                }
                else
                {
                    stoppedApplications.Add(application);
                    imageIndex = 1;
                }
                if (item.ImageIndex != imageIndex)
                    item.ImageIndex = imageIndex;
            }
            UpdateSystemTrayContextMenuSingles(runningApplications, stoppedApplications);
            UpdateSystemTrayContextMenuRecents();
        }

        private void UpdateSelectedApplicationInfo()
        {
            if (listViewApplications.SelectedItems.Count == 1)
            {
                Application application = (Application) listViewApplications.SelectedItems[0].Tag;
                labelName.Text = application.Name;
                textBoxPath.Text = application.Path;
                textBoxArguments.Text = application.Arguments;
                labelGroup.Text = application.Group ?? string.Empty;
                labelSets.Text = StringUtilities.ListToString(application.Sets);
            }
            else
            {
                labelName.Text = string.Empty;
                textBoxPath.Text = string.Empty;
                textBoxArguments.Text = string.Empty;
                labelGroup.Text = string.Empty;
                labelSets.Text = string.Empty;
            }
        }

        private void SelectApplicationsBySet()
        {
            _selectingApplicationBySet = true;
            string selectedSet = (string) comboBoxSets.SelectedItem;
            foreach (ListViewItem item in listViewApplications.Items)
            {
                Application application = (Application) item.Tag;
                if (application.Sets != null)
                    item.Selected = application.Sets.Contains(selectedSet);
            }
            _selectingApplicationBySet = false;
            listViewApplications.Focus();
        }

        private void UpdateSystemTrayContextMenuRecents()
        {
            // make sure top [max count] recent items are in correct order
            List<RecentUsage> recentUsages = _config.RecentUsages.Take(Configuration.MAX_VISIBLE_RECENT_USAGE_COUNT).OrderBy(recent => recent.Name).ToList();
            foreach (RecentUsage recentUsage in recentUsages)
            {
                Application application = ApplicationControl.GetApplicationByID(recentUsage.ID);
                bool applicationIsRunning = (application != null ? application.IsRunning : false);
                Image image = imageList.Images[application != null ? (applicationIsRunning ? 0 : 1) : 2];
                int menuIndex = toolStripMenuItemSystemTrayRecent.DropDownItems.Cast<ToolStripItem>().Select(item => item.Text).ToList().IndexOf(recentUsage.Name);
                int correctIndex = recentUsages.IndexOf(recentUsage);
                if (menuIndex == -1)
                {
                    // recent item does not exist in context menu
                    ToolStripMenuItem recentItem = new ToolStripMenuItem(recentUsage.Name, image);
                    toolStripMenuItemSystemTrayRecent.DropDownItems.Insert(correctIndex, recentItem);
                    if (application == null || !applicationIsRunning)
                    {
                        ToolStripItem recentStartItem = new ToolStripMenuItem("Start") { Tag = recentUsage.ID };
                        recentStartItem.Click += StartRecent_Click;
                        recentItem.DropDownItems.Add(recentStartItem);
                    }
                    ToolStripItem recentRestartItem = new ToolStripMenuItem("Restart") { Tag = recentUsage.ID };
                    recentRestartItem.Click += RestartRecent_Click;
                    recentItem.DropDownItems.Add(recentRestartItem);
                    if (application == null || applicationIsRunning)
                    {
                        ToolStripItem recentStopItem = new ToolStripMenuItem("Stop") { Tag = recentUsage.ID };
                        recentStopItem.Click += StopRecent_Click;
                        recentItem.DropDownItems.Add(recentStopItem);
                    }
                }
                else
                {
                    // recent item exist in context menu, verify position and status
                    ToolStripMenuItem recentItem = (ToolStripMenuItem) toolStripMenuItemSystemTrayRecent.DropDownItems[menuIndex];
                    recentItem.Image = image;
                    if (menuIndex != correctIndex)
                    {
                        toolStripMenuItemSystemTrayRecent.DropDownItems.RemoveAt(menuIndex);
                        toolStripMenuItemSystemTrayRecent.DropDownItems.Insert(correctIndex, recentItem);
                    }
                    if (application != null)
                    {
                        ToolStripItem recentStartItem = recentItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Text == "Start"));
                        if (applicationIsRunning && recentStartItem != null)
                        {
                            recentStartItem.Click -= StartRecent_Click;
                            recentItem.DropDownItems.Remove(recentStartItem);
                        }
                        else if (!applicationIsRunning && recentStartItem == null)
                        {
                            recentStartItem = new ToolStripMenuItem("Start") { Tag = recentUsage.ID };
                            recentStartItem.Click += StartRecent_Click;
                            recentItem.DropDownItems.Insert(0, recentStartItem);
                        }
                        ToolStripItem recentStopItem = recentItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Text == "Stop"));
                        if (!applicationIsRunning && recentStopItem != null)
                        {
                            recentStopItem.Click -= StopRecent_Click;
                            recentItem.DropDownItems.Remove(recentStopItem);
                        }
                        else if (applicationIsRunning && recentStopItem == null)
                        {
                            recentStopItem = new ToolStripMenuItem("Stop") { Tag = recentUsage.ID };
                            recentStopItem.Click += StopRecent_Click;
                            recentItem.DropDownItems.Add(recentStopItem);
                        }
                    }
                }
            }
            // remove items exceeding [max count]
            for (int i = Configuration.MAX_VISIBLE_RECENT_USAGE_COUNT; i < toolStripMenuItemSystemTrayRecent.DropDownItems.Count; i++)
            {
                ToolStripMenuItem recentItem = (ToolStripMenuItem) toolStripMenuItemSystemTrayRecent.DropDownItems[Configuration.MAX_VISIBLE_RECENT_USAGE_COUNT];
                ToolStripItem recentStartItem = recentItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Text == "Start"));
                if (recentStartItem != null) recentStartItem.Click -= StartRecent_Click;
                ToolStripItem recentRestartItem = recentItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Text == "Restart"));
                if (recentRestartItem != null) recentRestartItem.Click -= RestartRecent_Click;
                ToolStripItem recentStopItem = recentItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Text == "Stop"));
                if (recentStopItem != null) recentStopItem.Click -= StopRecent_Click;
                recentItem.DropDownItems.Clear();
                toolStripMenuItemSystemTrayRecent.DropDownItems.RemoveAt(Configuration.MAX_VISIBLE_RECENT_USAGE_COUNT);
            }
        }

        private void UpdateSystemTrayContextMenuSets()
        {
            toolStripMenuItemSystemTrayStartBySet.DropDownItems.Cast<ToolStripItem>().ToList().ForEach(item => item.Click -= StartBySet_Click);
            toolStripMenuItemSystemTrayRestartBySet.DropDownItems.Cast<ToolStripItem>().ToList().ForEach(item => item.Click -= StartBySet_Click);
            toolStripMenuItemSystemTrayStopBySet.DropDownItems.Cast<ToolStripItem>().ToList().ForEach(item => item.Click -= StartBySet_Click);
            toolStripMenuItemSystemTrayStartBySet.DropDownItems.Clear();
            toolStripMenuItemSystemTrayRestartBySet.DropDownItems.Clear();
            toolStripMenuItemSystemTrayStopBySet.DropDownItems.Clear();
            foreach (string set in _config.Sets)
            {
                ToolStripItem startItem = toolStripMenuItemSystemTrayStartBySet.DropDownItems.Add(set, imageList.Images[2]);
                ToolStripItem restartItem = toolStripMenuItemSystemTrayRestartBySet.DropDownItems.Add(set, imageList.Images[2]);
                ToolStripItem stopItem = toolStripMenuItemSystemTrayStopBySet.DropDownItems.Add(set, imageList.Images[2]);
                startItem.Click += StartBySet_Click;
                restartItem.Click += RestartBySet_Click;
                stopItem.Click += StopBySet_Click;
                startItem.Tag = restartItem.Tag = stopItem.Tag = set;
            }
        }

        private void UpdateSystemTrayContextMenuSingles(IEnumerable<Application> runningApplications, IEnumerable<Application> stoppedApplications)
        {
            IEnumerable<Application> allApplications = runningApplications.Union(stoppedApplications);
            IEnumerable<string> allGroups = allApplications.Select(app => app.Group)
                .Where(group => (group != null)).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(group => group);
            IEnumerable<string> runningGroups = runningApplications.Select(app => app.Group)
                .Where(group => (group != null)).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(group => group);
            IEnumerable<string> stoppedGroups = stoppedApplications.Select(app => app.Group)
                .Where(group => (group != null)).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(group => group);

            // remove "start single" groups and sub items
            RemoveSingleGroupsAndSubItemsFromSystemTrayContextMenu(stoppedGroups, toolStripMenuItemSystemTrayStartSingle, StartSingle_Click);
            // remove "restart single" groups and sub items
            RemoveSingleGroupsAndSubItemsFromSystemTrayContextMenu(allGroups, toolStripMenuItemSystemTrayRestartSingle, RestartSingle_Click);
            // remove "stop single" groups and sub items
            RemoveSingleGroupsAndSubItemsFromSystemTrayContextMenu(runningGroups, toolStripMenuItemSystemTrayStopSingle, StopSingle_Click);

            // remove "start single" items (having no group or when its group should not be removed)
            RemoveSingleItemsFromSystemTrayContextMenu(stoppedApplications, toolStripMenuItemSystemTrayStartSingle, StartSingle_Click);
            // remove "restart single" items (having no group or when its group should not be removed)
            RemoveSingleItemsFromSystemTrayContextMenu(allApplications, toolStripMenuItemSystemTrayRestartSingle, RestartSingle_Click);
            // remove "stop single" items (having no group or when its group should not be removed)
            RemoveSingleItemsFromSystemTrayContextMenu(runningApplications, toolStripMenuItemSystemTrayStopSingle, StopSingle_Click);

            // create and update "start single" items
            CreateAndUpdateSingleItemsInSystemTrayContextMenu(stoppedApplications, toolStripMenuItemSystemTrayStartSingle, StartSingle_Click, imageList);
            // create and update "restart single" items
            CreateAndUpdateSingleItemsInSystemTrayContextMenu(allApplications, toolStripMenuItemSystemTrayRestartSingle, RestartSingle_Click, imageList);
            // create and update "stop single" items
            CreateAndUpdateSingleItemsInSystemTrayContextMenu(runningApplications, toolStripMenuItemSystemTrayStopSingle, StopSingle_Click, imageList);
        }

        private static void RemoveSingleGroupsAndSubItemsFromSystemTrayContextMenu(IEnumerable<string> groups, ToolStripDropDownItem menuItem, EventHandler singleClick)
        {
            List<ToolStripMenuItem> groupItemsToRemove = menuItem.DropDownItems.Cast<ToolStripMenuItem>()
                .Where(item => (item.Tag is string && !groups.Contains((string) item.Tag))).ToList();
            foreach (ToolStripMenuItem item in groupItemsToRemove)
            {
                List<ToolStripItem> itemsToRemove = item.DropDownItems.Cast<ToolStripItem>().ToList();
                itemsToRemove.ForEach(x => { x.Click -= singleClick; item.DropDownItems.Remove(x); });
            }
            groupItemsToRemove.ForEach(item => menuItem.DropDownItems.Remove(item));
        }

        private static void RemoveSingleItemsFromSystemTrayContextMenu(IEnumerable<Application> applications, ToolStripDropDownItem menuItem, EventHandler singleClick)
        {
            List<ToolStripItem> appItemsToRemove = menuItem.DropDownItems.Cast<ToolStripItem>()
                .Where(item => (item.Tag is Application && !applications.Contains((Application) item.Tag))).ToList();
            appItemsToRemove.ForEach(item => { item.Click -= singleClick; });
            appItemsToRemove.ForEach(item => menuItem.DropDownItems.Remove(item));
            foreach (ToolStripMenuItem groupItem in menuItem.DropDownItems.Cast<ToolStripMenuItem>().Where(item => (item.Tag is string)))
            {
                appItemsToRemove = groupItem.DropDownItems.Cast<ToolStripItem>()
                    .Where(item => (item.Tag is Application && !applications.Contains((Application) item.Tag))).ToList();
                appItemsToRemove.ForEach(item => { item.Click -= singleClick; });
                appItemsToRemove.ForEach(item => groupItem.DropDownItems.Remove(item));
            }
        }

        private static void CreateAndUpdateSingleItemsInSystemTrayContextMenu(IEnumerable<Application> applications, ToolStripMenuItem menuItem, EventHandler singleClick, ImageList imageList)
        {
            foreach (Application application in applications)
            {
                ToolStripMenuItem groupItem = null;
                if (application.Group != null)
                {
                    IEnumerable<ToolStripItem> groupItems = menuItem.DropDownItems.Cast<ToolStripItem>().Where(item => (item.Tag is string));
                    groupItem = (ToolStripMenuItem) groupItems.FirstOrDefault(item => (item.Tag is string && (string) item.Tag == application.Group));
                    if (groupItem == null)
                    {
                        groupItem = (ToolStripMenuItem) menuItem.DropDownItems.Add(application.Group);
                        groupItem.Tag = application.Group;
                    }
                }
                ToolStripMenuItem parentItem = (application.Group != null ? groupItem : menuItem);
                ToolStripItem appItem = parentItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Tag == application));
                if (appItem == null)
                {
                    List<string> existingStartItemNames = parentItem.DropDownItems.Cast<ToolStripItem>().Select(item => item.Text).ToList();
                    existingStartItemNames.Add(application.Name);
                    int index = existingStartItemNames.OrderBy(name => name).ToList().IndexOf(application.Name);
                    appItem = new ToolStripMenuItem(application.Name) { Tag = application };
                    appItem.Click += singleClick;
                    parentItem.DropDownItems.Insert(index, appItem);
                }
                else if (appItem.Text != application.Name)
                {
                    appItem.Text = application.Name;
                }
                appItem.Image = imageList.Images[application.IsRunning ? 0 : 1];
            }
        }

        #endregion
    }
}
