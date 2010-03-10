using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ProcessController.DataAccess;
using ProcessController.DataObjects;
using Application = ProcessController.DataObjects.Application;
using Timer=System.Windows.Forms.Timer;

namespace ProcessController
{
    public partial class MainForm : Form
    {
        private bool _closing;
        private bool _discardEvents;
        private bool _discardNextMouseClick;
        private bool _selectingApplicationBySet;
        private readonly Configuration _config;
        private readonly Timer _timer;

        public MainForm()
        {
            InitializeComponent();
            Text += " (build " + GetBuildTag() + ")";
            _closing = false;
            _discardEvents = false;
            _discardNextMouseClick = false;
            _selectingApplicationBySet = false;
            _config = RegistryHandler.LoadConfiguration();
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
                    _config.WindowVisible = Visible;
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

        #region System tray icon

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _discardNextMouseClick = true;
                if (Visible)
                    Minimize();
                else
                    Restore();
            }
            catch (Exception ex)
            {
                FormUtilities.ShowError(this, ex);
            }
        }

        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !_discardNextMouseClick)
            {
                // ShowContextMenu method is private.. have to use reflection to call it
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
            _discardNextMouseClick = false;
        }

        #endregion

        #region System tray context menu

        private void StartBySet_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                StartApplicationsBySet((string) item.Tag);
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
                RestartApplicationsBySet((string) item.Tag);
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
                StopApplicationsBySet((string) item.Tag);
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
                StartApplication((Application) item.Tag);
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
                RestartApplication((Application) item.Tag);
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
                StopApplication((Application) item.Tag);
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
                if (Visible)
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
                    StartApplications(stoppedApplications);
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
                    StopApplications(runningApplications);
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
                    _config.Applications.Remove((Application) item.Tag);
                    listViewApplications.Items.Remove(item);
                }
                RegistryHandler.SaveConfiguration(_config);
                _discardEvents = false;
                UpdateSetList();
                EnableControls(true);
            }
        }

        private static void StartApplication(Application application)
        {
            StartApplication(application, false);
        }

        private static void StartApplication(Application application, bool waitUntilStopped)
        {
            if (waitUntilStopped)
            {
                DateTime timeout = DateTime.Now.AddSeconds(30);
                while (DateTime.Now < timeout && application.IsRunning)
                {
                    Thread.Sleep(100);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            application.Start();
        }

        private static void StartApplications(IEnumerable<Application> applications)
        {
            foreach (Application application in applications)
                StartApplication(application);
        }

        private void StartApplicationsBySet(string set)
        {
            StartApplicationsBySet(set, false);
        }

        private void StartApplicationsBySet(string set, bool waitUntilStopped)
        {
            foreach (Application application in _config.Applications.Where(app => (app.Sets.Contains(set))))
                StartApplication(application, waitUntilStopped);
        }

        private static void RestartApplication(Application application)
        {
            StopApplication(application);
            StartApplication(application, true);
        }

        private void RestartApplicationsBySet(string set)
        {
            StopApplicationsBySet(set);
            StartApplicationsBySet(set, true);
        }

        private static void StopApplication(Application application)
        {
            application.Stop();
        }

        private static void StopApplications(IEnumerable<Application> applications)
        {
            foreach (Application application in applications)
                StopApplication(application);
        }

        private void StopApplicationsBySet(string set)
        {
            foreach (Application application in _config.Applications.Where(app => (app.Sets.Contains(set))))
                StopApplication(application);
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
                {
                    _config.Applications.Add(application);
                }
                RegistryHandler.SaveConfiguration(_config);
                FillApplicationList();
                FormUtilities.ShowInformation(this, "Successfully imported " + importedConfig.Applications.Count + " applications.");
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
                FormUtilities.ShowInformation(this, "Successfully exported " + _config.Applications.Count + " applications to" + Environment.NewLine + saveFileDialog.FileName);
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
            Opacity = 100;
            Show();
            WindowState = FormWindowState.Normal;
            Refresh();
            toolStripMenuItemSystemTrayRestoreMinimize.Text = "Minimize";
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
                ToolStripItem startItem = toolStripMenuItemSystemTrayStartBySet.DropDownItems.Add(set);
                ToolStripItem restartItem = toolStripMenuItemSystemTrayRestartBySet.DropDownItems.Add(set);
                ToolStripItem stopItem = toolStripMenuItemSystemTrayStopBySet.DropDownItems.Add(set);
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

            List<ToolStripMenuItem> groupItemsToRemove;
            // remove "start single" groups and sub items
            groupItemsToRemove = toolStripMenuItemSystemTrayStartSingle.DropDownItems.Cast<ToolStripMenuItem>()
                .Where(item => (item.Tag is string && !stoppedGroups.Contains((string) item.Tag))).ToList();
            foreach (ToolStripMenuItem item in groupItemsToRemove)
            {
                List<ToolStripItem> itemsToRemove = item.DropDownItems.Cast<ToolStripItem>().ToList();
                itemsToRemove.ForEach(x => { x.Click -= StartSingle_Click; item.DropDownItems.Remove(x); });
            }
            groupItemsToRemove.ForEach(item => toolStripMenuItemSystemTrayStartSingle.DropDownItems.Remove(item));
            // remove "restart single" groups and sub items
            groupItemsToRemove = toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Cast<ToolStripMenuItem>()
                .Where(item => (item.Tag is string && !allGroups.Contains((string) item.Tag))).ToList();
            foreach (ToolStripMenuItem item in groupItemsToRemove)
            {
                List<ToolStripItem> itemsToRemove = item.DropDownItems.Cast<ToolStripItem>().ToList();
                itemsToRemove.ForEach(x => { x.Click -= RestartSingle_Click; item.DropDownItems.Remove(x); });
            }
            groupItemsToRemove.ForEach(item => toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Remove(item));
            // remove "stop single" groups and sub items
            groupItemsToRemove = toolStripMenuItemSystemTrayStopSingle.DropDownItems.Cast<ToolStripMenuItem>()
                .Where(item => (item.Tag is string && !runningGroups.Contains((string) item.Tag))).ToList();
            foreach (ToolStripMenuItem item in groupItemsToRemove)
            {
                List<ToolStripItem> itemsToRemove = item.DropDownItems.Cast<ToolStripItem>().ToList();
                itemsToRemove.ForEach(x => { x.Click -= StopSingle_Click; item.DropDownItems.Remove(x); });
            }
            groupItemsToRemove.ForEach(item => toolStripMenuItemSystemTrayStopSingle.DropDownItems.Remove(item));


            List<ToolStripItem> appItemsToRemove;
            // remove "start single" items (having no group or when its group should not be removed)
            appItemsToRemove = toolStripMenuItemSystemTrayStartSingle.DropDownItems.Cast<ToolStripItem>()
                .Where(item => (item.Tag is Application && !stoppedApplications.Contains((Application) item.Tag))).ToList();
            appItemsToRemove.ForEach(item => { item.Click -= StartSingle_Click; });
            appItemsToRemove.ForEach(item => toolStripMenuItemSystemTrayStartSingle.DropDownItems.Remove(item));
            foreach (ToolStripMenuItem groupItem in toolStripMenuItemSystemTrayStartSingle.DropDownItems.Cast<ToolStripMenuItem>().Where(item => (item.Tag is string)))
            {
                appItemsToRemove = groupItem.DropDownItems.Cast<ToolStripItem>()
                    .Where(item => (item.Tag is Application && !stoppedApplications.Contains((Application) item.Tag))).ToList();
                appItemsToRemove.ForEach(item => { item.Click -= StartSingle_Click; });
                appItemsToRemove.ForEach(item => groupItem.DropDownItems.Remove(item));
            }
            // remove "restart single" items (having no group or when its group should not be removed)
            appItemsToRemove = toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Cast<ToolStripItem>()
                .Where(item => (item.Tag is Application && !allApplications.Contains((Application) item.Tag))).ToList();
            appItemsToRemove.ForEach(item => { item.Click -= RestartSingle_Click; });
            appItemsToRemove.ForEach(item => toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Remove(item));
            foreach (ToolStripMenuItem groupItem in toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Cast<ToolStripMenuItem>().Where(item => (item.Tag is string)))
            {
                appItemsToRemove = groupItem.DropDownItems.Cast<ToolStripItem>()
                    .Where(item => (item.Tag is Application && !allApplications.Contains((Application) item.Tag))).ToList();
                appItemsToRemove.ForEach(item => { item.Click -= RestartSingle_Click; });
                appItemsToRemove.ForEach(item => groupItem.DropDownItems.Remove(item));
            }
            // remove "stop single" items (having no group or when its group should not be removed)
            appItemsToRemove = toolStripMenuItemSystemTrayStopSingle.DropDownItems.Cast<ToolStripItem>()
                .Where(item => (item.Tag is Application && !runningApplications.Contains((Application) item.Tag))).ToList();
            appItemsToRemove.ForEach(item => { item.Click -= StopSingle_Click; });
            appItemsToRemove.ForEach(item => toolStripMenuItemSystemTrayStopSingle.DropDownItems.Remove(item));
            foreach (ToolStripMenuItem groupItem in toolStripMenuItemSystemTrayStopSingle.DropDownItems.Cast<ToolStripMenuItem>().Where(item => (item.Tag is string)))
            {
                appItemsToRemove = groupItem.DropDownItems.Cast<ToolStripItem>()
                    .Where(item => (item.Tag is Application && !runningApplications.Contains((Application) item.Tag))).ToList();
                appItemsToRemove.ForEach(item => { item.Click -= StopSingle_Click; });
                appItemsToRemove.ForEach(item => groupItem.DropDownItems.Remove(item));
            }

            // create and update "start single" items
            foreach (Application application in stoppedApplications)
            {
                ToolStripMenuItem startGroupItem = null;
                if (application.Group != null)
                {
                    IEnumerable<ToolStripItem> startGroupItems = toolStripMenuItemSystemTrayStartSingle.DropDownItems.Cast<ToolStripItem>().Where(item => (item.Tag is string));
                    startGroupItem = (ToolStripMenuItem) startGroupItems.FirstOrDefault(item => (item.Tag is string && (string) item.Tag == application.Group));
                    if (startGroupItem == null)
                    {
                        startGroupItem = (ToolStripMenuItem) toolStripMenuItemSystemTrayStartSingle.DropDownItems.Add(application.Group);
                        startGroupItem.Tag = application.Group;
                    }
                }
                ToolStripMenuItem parentStartItem = (application.Group != null ? startGroupItem : toolStripMenuItemSystemTrayStartSingle);
                ToolStripItem startItem = parentStartItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Tag == application));
                if (startItem == null)
                {
                    startItem = parentStartItem.DropDownItems.Add(application.Name);
                    startItem.Click += StartSingle_Click;
                    startItem.Tag = application;
                }
                else if (startItem.Text != application.Name)
                {
                    startItem.Text = application.Name;
                }
            }
            // create and update "restart single" items
            foreach (Application application in allApplications)
            {
                ToolStripMenuItem restartGroupItem = null;
                if (application.Group != null)
                {
                    IEnumerable<ToolStripItem> restartGroupItems = toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Cast<ToolStripItem>().Where(item => (item.Tag is string));
                    restartGroupItem = (ToolStripMenuItem) restartGroupItems.FirstOrDefault(item => (item.Tag is string && (string) item.Tag == application.Group));
                    if (restartGroupItem == null)
                    {
                        restartGroupItem = (ToolStripMenuItem) toolStripMenuItemSystemTrayRestartSingle.DropDownItems.Add(application.Group);
                        restartGroupItem.Tag = application.Group;
                    }
                }
                ToolStripMenuItem parentRestartItem = (application.Group != null ? restartGroupItem : toolStripMenuItemSystemTrayRestartSingle);
                ToolStripItem restartItem = parentRestartItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Tag == application));
                if (restartItem == null)
                {
                    restartItem = parentRestartItem.DropDownItems.Add(application.Name);
                    restartItem.Click += RestartSingle_Click;
                    restartItem.Tag = application;
                }
                else if (restartItem.Text != application.Name)
                {
                    restartItem.Text = application.Name;
                }
            }
            // create and update "stop single" items
            foreach (Application application in runningApplications)
            {
                ToolStripMenuItem stopGroupItem = null;
                if (application.Group != null)
                {
                    IEnumerable<ToolStripItem> stopGroupItems = toolStripMenuItemSystemTrayStopSingle.DropDownItems.Cast<ToolStripItem>().Where(item => (item.Tag is string));
                    stopGroupItem = (ToolStripMenuItem) stopGroupItems.FirstOrDefault(item => (item.Tag is string && (string) item.Tag == application.Group));
                    if (stopGroupItem == null)
                    {
                        stopGroupItem = (ToolStripMenuItem) toolStripMenuItemSystemTrayStopSingle.DropDownItems.Add(application.Group);
                        stopGroupItem.Tag = application.Group;
                    }
                }
                ToolStripMenuItem parentStopItem = (application.Group != null ? stopGroupItem : toolStripMenuItemSystemTrayStopSingle);
                ToolStripItem stopItem = parentStopItem.DropDownItems.Cast<ToolStripItem>().FirstOrDefault(item => (item.Tag == application));
                if (stopItem == null)
                {
                    stopItem = parentStopItem.DropDownItems.Add(application.Name);
                    stopItem.Click += StopSingle_Click;
                    stopItem.Tag = application;
                }
                else if (stopItem.Text != application.Name)
                {
                    stopItem.Text = application.Name;
                }
            }
        }

        #endregion
    }
}
