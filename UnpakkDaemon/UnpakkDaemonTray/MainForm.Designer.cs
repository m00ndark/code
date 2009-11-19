namespace UnpakkDaemonTray
{
	partial class MainForm
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageProgress = new System.Windows.Forms.TabPage();
			this.listViewLog = new System.Windows.Forms.ListView();
			this.columnHeaderLog = new System.Windows.Forms.ColumnHeader();
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.progressBarSubProgress = new System.Windows.Forms.ProgressBar();
			this.progressBarMainProgress = new System.Windows.Forms.ProgressBar();
			this.labelMainProgress = new System.Windows.Forms.Label();
			this.labelSubProgress = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tabPageProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageProgress);
			this.tabControl.Controls.Add(this.tabPageSettings);
			this.tabControl.Location = new System.Drawing.Point(9, 9);
			this.tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(799, 396);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageProgress
			// 
			this.tabPageProgress.Controls.Add(this.labelSubProgress);
			this.tabPageProgress.Controls.Add(this.labelMainProgress);
			this.tabPageProgress.Controls.Add(this.progressBarMainProgress);
			this.tabPageProgress.Controls.Add(this.progressBarSubProgress);
			this.tabPageProgress.Controls.Add(this.listViewLog);
			this.tabPageProgress.Location = new System.Drawing.Point(4, 22);
			this.tabPageProgress.Name = "tabPageProgress";
			this.tabPageProgress.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProgress.Size = new System.Drawing.Size(791, 370);
			this.tabPageProgress.TabIndex = 0;
			this.tabPageProgress.Text = "Progress";
			this.tabPageProgress.UseVisualStyleBackColor = true;
			// 
			// listViewLog
			// 
			this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLog});
			this.listViewLog.FullRowSelect = true;
			this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewLog.HideSelection = false;
			this.listViewLog.Location = new System.Drawing.Point(6, 74);
			this.listViewLog.Name = "listViewLog";
			this.listViewLog.Size = new System.Drawing.Size(779, 293);
			this.listViewLog.TabIndex = 0;
			this.listViewLog.UseCompatibleStateImageBehavior = false;
			this.listViewLog.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderLog
			// 
			this.columnHeaderLog.Text = "Log";
			this.columnHeaderLog.Width = 755;
			// 
			// tabPageSettings
			// 
			this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSettings.Name = "tabPageSettings";
			this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSettings.Size = new System.Drawing.Size(791, 330);
			this.tabPageSettings.TabIndex = 1;
			this.tabPageSettings.Text = "Settings";
			this.tabPageSettings.UseVisualStyleBackColor = true;
			// 
			// progressBarSubProgress
			// 
			this.progressBarSubProgress.Location = new System.Drawing.Point(6, 40);
			this.progressBarSubProgress.Name = "progressBarSubProgress";
			this.progressBarSubProgress.Size = new System.Drawing.Size(779, 15);
			this.progressBarSubProgress.TabIndex = 1;
			// 
			// progressBarMainProgress
			// 
			this.progressBarMainProgress.Location = new System.Drawing.Point(6, 6);
			this.progressBarMainProgress.Name = "progressBarMainProgress";
			this.progressBarMainProgress.Size = new System.Drawing.Size(779, 15);
			this.progressBarMainProgress.TabIndex = 2;
			// 
			// labelMainProgress
			// 
			this.labelMainProgress.AutoSize = true;
			this.labelMainProgress.Location = new System.Drawing.Point(6, 24);
			this.labelMainProgress.Name = "labelMainProgress";
			this.labelMainProgress.Size = new System.Drawing.Size(0, 13);
			this.labelMainProgress.TabIndex = 3;
			// 
			// labelSubProgress
			// 
			this.labelSubProgress.AutoSize = true;
			this.labelSubProgress.Location = new System.Drawing.Point(6, 58);
			this.labelSubProgress.Name = "labelSubProgress";
			this.labelSubProgress.Size = new System.Drawing.Size(0, 13);
			this.labelSubProgress.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(817, 414);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unpakk Daemon";
			this.tabControl.ResumeLayout(false);
			this.tabPageProgress.ResumeLayout(false);
			this.tabPageProgress.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageProgress;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.ListView listViewLog;
		private System.Windows.Forms.ColumnHeader columnHeaderLog;
		private System.Windows.Forms.Label labelSubProgress;
		private System.Windows.Forms.Label labelMainProgress;
		private System.Windows.Forms.ProgressBar progressBarMainProgress;
		private System.Windows.Forms.ProgressBar progressBarSubProgress;
	}
}

