namespace XMLDBViewer
{
	partial class WaitForm
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
			this.labelMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelMessage
			// 
			this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelMessage.Location = new System.Drawing.Point(0, 0);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(255, 64);
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "Loading, please wait...";
			this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WaitForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(255, 64);
			this.ControlBox = false;
			this.Controls.Add(this.labelMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "WaitForm";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Please Wait...";
			this.Load += new System.EventHandler(this.WaitForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelMessage;
	}
}