namespace ProcessController
{
    partial class ControlRecentApplicationsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlRecentApplicationsForm));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.labelNonAvailable = new System.Windows.Forms.Label();
            this.linkLabelOpen = new System.Windows.Forms.LinkLabel();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.panelBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "running-16.ico");
            this.imageList.Images.SetKeyName(1, "stopped-16.ico");
            this.imageList.Images.SetKeyName(2, "unknown-16.ico");
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(9, 10);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(316, 66);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // labelNonAvailable
            // 
            this.labelNonAvailable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelNonAvailable.Location = new System.Drawing.Point(6, 36);
            this.labelNonAvailable.Name = "labelNonAvailable";
            this.labelNonAvailable.Size = new System.Drawing.Size(319, 15);
            this.labelNonAvailable.TabIndex = 2;
            this.labelNonAvailable.Text = "No recently used applications available...";
            this.labelNonAvailable.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelNonAvailable.Visible = false;
            // 
            // linkLabelOpen
            // 
            this.linkLabelOpen.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelOpen.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelOpen.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.linkLabelOpen.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelOpen.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelOpen.Location = new System.Drawing.Point(13, 15);
            this.linkLabelOpen.Name = "linkLabelOpen";
            this.linkLabelOpen.Size = new System.Drawing.Size(313, 15);
            this.linkLabelOpen.TabIndex = 4;
            this.linkLabelOpen.TabStop = true;
            this.linkLabelOpen.Text = "Open Process Controller";
            this.linkLabelOpen.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabelOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelOpen_LinkClicked);
            // 
            // panelBackground
            // 
            this.panelBackground.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBackground.BackColor = System.Drawing.SystemColors.Control;
            this.panelBackground.BackgroundImage = ((System.Drawing.Image) (resources.GetObject("panelBackground.BackgroundImage")));
            this.panelBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBackground.Controls.Add(this.linkLabelOpen);
            this.panelBackground.Location = new System.Drawing.Point(-1, 86);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(337, 43);
            this.panelBackground.TabIndex = 5;
            // 
            // ControlRecentApplicationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 127);
            this.ControlBox = false;
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.labelNonAvailable);
            this.Controls.Add(this.flowLayoutPanel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(350, 143);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 143);
            this.Name = "ControlRecentApplicationsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.ControlRecentApplicationsForm_Deactivate);
            this.Load += new System.EventHandler(this.ControlRecentApplicationsForm_Load);
            this.panelBackground.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label labelNonAvailable;
        private System.Windows.Forms.LinkLabel linkLabelOpen;
        private System.Windows.Forms.Panel panelBackground;
    }
}