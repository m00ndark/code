using System.Drawing;
using System.Windows.Forms;
using ProcessController.DataObjects;
using ProcessController.Utilities;
using Application = ProcessController.DataObjects.Application;

namespace ProcessController
{
    public partial class ControlRecentApplicationRow : UserControl
    {
        public ControlRecentApplicationRow(ImageList imageList, string id)
        {
            InitializeComponent();
            ImageList = imageList;
            ID = id;
            Application = ApplicationControl.GetApplicationByID(ID);
            LayoutControl();
            UpdateControl();
        }

        #region Properties

        public ImageList ImageList { get; private set; }
        public string ID { get; private set; }
        public Application Application { get; private set; }
        public bool IsSingleApplication { get { return (Application != null); } }

        #endregion

        #region GUI events

        private void linkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsSingleApplication)
                ApplicationControl.StartApplication(Application);
            else
                ApplicationControl.StartApplicationsBySet(ID);
        }

        private void linkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsSingleApplication)
                ApplicationControl.StopApplication(Application);
            else
                ApplicationControl.StopApplicationsBySet(ID);
        }

        private void linkLabelRestart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsSingleApplication)
                ApplicationControl.RestartApplication(Application);
            else
                ApplicationControl.RestartApplicationsBySet(ID);
        }

        #endregion

        private void LayoutControl()
        {
            labelName.Text = RecentUsage.GetName(ID);
            if (!SystemUtilities.OSIsWindowsSeven())
            {
                linkLabelStart.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                linkLabelStop.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                linkLabelRestart.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                labelName.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }

        public void UpdateControl()
        {
            if (IsSingleApplication)
            {
                bool applicationIsRunning = Application.IsRunning;
                pictureBoxStatus.Image = ImageList.Images[applicationIsRunning ? 0 : 1];
                linkLabelStart.Links[0].Enabled = !applicationIsRunning;
                linkLabelStop.Links[0].Enabled = applicationIsRunning;
            }
            else
            {
                pictureBoxStatus.Image = ImageList.Images[2];
                linkLabelStart.Links[0].Enabled = true;
                linkLabelStop.Links[0].Enabled = true;
            }
        }
    }
}
