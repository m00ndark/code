namespace ProcessController
{
    partial class ControlRecentApplicationRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabelStart = new System.Windows.Forms.LinkLabel();
            this.linkLabelStop = new System.Windows.Forms.LinkLabel();
            this.linkLabelRestart = new System.Windows.Forms.LinkLabel();
            this.labelName = new System.Windows.Forms.Label();
            this.pictureBoxStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabelStart
            // 
            this.linkLabelStart.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelStart.AutoSize = true;
            this.linkLabelStart.DisabledLinkColor = System.Drawing.Color.Gray;
            this.linkLabelStart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.linkLabelStart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelStart.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelStart.Location = new System.Drawing.Point(25, 3);
            this.linkLabelStart.Margin = new System.Windows.Forms.Padding(3);
            this.linkLabelStart.Name = "linkLabelStart";
            this.linkLabelStart.Size = new System.Drawing.Size(31, 15);
            this.linkLabelStart.TabIndex = 0;
            this.linkLabelStart.TabStop = true;
            this.linkLabelStart.Text = "Start";
            this.linkLabelStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelStart_LinkClicked);
            // 
            // linkLabelStop
            // 
            this.linkLabelStop.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelStop.AutoSize = true;
            this.linkLabelStop.DisabledLinkColor = System.Drawing.Color.Gray;
            this.linkLabelStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.linkLabelStop.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelStop.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelStop.Location = new System.Drawing.Point(55, 3);
            this.linkLabelStop.Name = "linkLabelStop";
            this.linkLabelStop.Size = new System.Drawing.Size(31, 15);
            this.linkLabelStop.TabIndex = 1;
            this.linkLabelStop.TabStop = true;
            this.linkLabelStop.Text = "Stop";
            this.linkLabelStop.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelStop_LinkClicked);
            // 
            // linkLabelRestart
            // 
            this.linkLabelRestart.ActiveLinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelRestart.AutoSize = true;
            this.linkLabelRestart.DisabledLinkColor = System.Drawing.Color.Gray;
            this.linkLabelRestart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.linkLabelRestart.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabelRestart.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.linkLabelRestart.Location = new System.Drawing.Point(85, 3);
            this.linkLabelRestart.Name = "linkLabelRestart";
            this.linkLabelRestart.Size = new System.Drawing.Size(43, 15);
            this.linkLabelRestart.TabIndex = 2;
            this.linkLabelRestart.TabStop = true;
            this.linkLabelRestart.Text = "Restart";
            this.linkLabelRestart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRestart_LinkClicked);
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.AutoEllipsis = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelName.Location = new System.Drawing.Point(134, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(135, 15);
            this.labelName.TabIndex = 3;
            // 
            // pictureBoxStatus
            // 
            this.pictureBoxStatus.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxStatus.Name = "pictureBoxStatus";
            this.pictureBoxStatus.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxStatus.TabIndex = 4;
            this.pictureBoxStatus.TabStop = false;
            // 
            // ControlRecentApplicationRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pictureBoxStatus);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.linkLabelRestart);
            this.Controls.Add(this.linkLabelStop);
            this.Controls.Add(this.linkLabelStart);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ControlRecentApplicationRow";
            this.Size = new System.Drawing.Size(272, 22);
            ((System.ComponentModel.ISupportInitialize) (this.pictureBoxStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelStart;
        private System.Windows.Forms.LinkLabel linkLabelStop;
        private System.Windows.Forms.LinkLabel linkLabelRestart;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PictureBox pictureBoxStatus;
    }
}
