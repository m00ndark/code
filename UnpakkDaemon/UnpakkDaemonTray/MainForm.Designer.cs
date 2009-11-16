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
			this.tabPageSettings = new System.Windows.Forms.TabPage();
			this.listViewLog = new System.Windows.Forms.ListView();
			this.columnHeaderLog = new System.Windows.Forms.ColumnHeader();
			this.tabControl.SuspendLayout();
			this.tabPageProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageProgress);
			this.tabControl.Controls.Add(this.tabPageSettings);
			this.tabControl.Location = new System.Drawing.Point(9, 9);
			this.tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(799, 356);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageProgress
			// 
			this.tabPageProgress.Controls.Add(this.listViewLog);
			this.tabPageProgress.Location = new System.Drawing.Point(4, 22);
			this.tabPageProgress.Name = "tabPageProgress";
			this.tabPageProgress.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageProgress.Size = new System.Drawing.Size(791, 330);
			this.tabPageProgress.TabIndex = 0;
			this.tabPageProgress.Text = "Progress";
			this.tabPageProgress.UseVisualStyleBackColor = true;
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
			// listViewLog
			// 
			this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLog});
			this.listViewLog.FullRowSelect = true;
			this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listViewLog.HideSelection = false;
			this.listViewLog.Location = new System.Drawing.Point(6, 46);
			this.listViewLog.Name = "listViewLog";
			this.listViewLog.Size = new System.Drawing.Size(779, 281);
			this.listViewLog.TabIndex = 0;
			this.listViewLog.UseCompatibleStateImageBehavior = false;
			this.listViewLog.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderLog
			// 
			this.columnHeaderLog.Text = "Log";
			this.columnHeaderLog.Width = 755;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(817, 374);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Unpakk Daemon";
			this.tabControl.ResumeLayout(false);
			this.tabPageProgress.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageProgress;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.ListView listViewLog;
		private System.Windows.Forms.ColumnHeader columnHeaderLog;
	}
}

