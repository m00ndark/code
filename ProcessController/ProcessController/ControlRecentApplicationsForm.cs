using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ProcessController.DataObjects;
using ProcessController.Utilities;
using Application=ProcessController.DataObjects.Application;

namespace ProcessController
{
    public partial class ControlRecentApplicationsForm : Form
    {
        private readonly Timer _timer;
        private readonly Action _openMainWindowAction;

        public ControlRecentApplicationsForm(IList<RecentUsage> recentUsages, Action openMainWindowAction)
        {
            InitializeComponent();
            _timer = new Timer() { Interval = 500 };
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _openMainWindowAction = openMainWindowAction;
            RecentUsages = recentUsages;
            Hide();
            Opacity = 0;
        }

        #region Properties

        public IList<RecentUsage> RecentUsages { get; private set; }

        #endregion

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

        #region GUI events

        private void ControlRecentApplicationsForm_Load(object sender, EventArgs e)
        {
            LayoutForm();
        }

        private void ControlRecentApplicationsForm_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabelOpen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_openMainWindowAction != null)
                _openMainWindowAction();
        }

        #endregion

        private void LayoutForm()
        {
            bool isWindowsSeven = SystemUtilities.OSIsWindowsSeven();

            ControlRecentApplicationRow row = null;
            foreach (RecentUsage recentUsage in RecentUsages.Take(Configuration.MAX_VISIBLE_RECENT_USAGE_COUNT).OrderBy(recent => recent.Name))
            {
                row = new ControlRecentApplicationRow(imageList, recentUsage.ID) { Width = flowLayoutPanel.Width };
                flowLayoutPanel.Controls.Add(row);
            }

            if (row != null)
                MinimumSize = new Size(MinimumSize.Width, Math.Max(34 + 43 + (isWindowsSeven ? 0 : -16) + RecentUsages.Count * row.Height, 100));
            else
                labelNonAvailable.Visible = true;

            linkLabelOpen.Enabled = (_openMainWindowAction != null);

            if (!isWindowsSeven)
            {
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
                flowLayoutPanel.Height += 8;
                panelBackground.BackgroundImage = null;
                panelBackground.Width += 8;
                panelBackground.Height -= 4;
                panelBackground.Location = new Point(panelBackground.Location.X, panelBackground.Location.Y + 10);
                linkLabelOpen.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                linkLabelOpen.Location = new Point(linkLabelOpen.Location.X, linkLabelOpen.Location.Y + 4);
            }

            Location = new Point(Math.Min(MousePosition.X - Size.Width / 2, Screen.PrimaryScreen.WorkingArea.Width - Size.Width - 8),
                Screen.PrimaryScreen.WorkingArea.Height - Size.Height - (isWindowsSeven ? 8 : 0));

            Opacity = 100;
            Show();

            try { Program.SetForegroundWindow(Handle); } catch { }
        }

        private void UpdateApplicationStatus()
        {
            foreach (Control control in flowLayoutPanel.Controls)
                if (control is ControlRecentApplicationRow)
                    ((ControlRecentApplicationRow) control).UpdateControl();
        }
    }
}
