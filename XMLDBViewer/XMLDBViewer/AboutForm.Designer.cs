namespace XMLDBViewer
{
	partial class AboutForm
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
			this.labelCredits = new System.Windows.Forms.Label();
			this.labelBuild = new System.Windows.Forms.Label();
			this.labelStarts = new System.Windows.Forms.Label();
			this.labelExceptions = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelCredits
			// 
			this.labelCredits.AutoSize = true;
			this.labelCredits.BackColor = System.Drawing.Color.Transparent;
			this.labelCredits.ForeColor = System.Drawing.Color.White;
			this.labelCredits.Location = new System.Drawing.Point(53, 142);
			this.labelCredits.Name = "labelCredits";
			this.labelCredits.Size = new System.Drawing.Size(120, 13);
			this.labelCredits.TabIndex = 2;
			this.labelCredits.Text = "Original code by Mattias";
			this.labelCredits.Click += new System.EventHandler(this.labelCredits_Click);
			// 
			// labelBuild
			// 
			this.labelBuild.AutoSize = true;
			this.labelBuild.BackColor = System.Drawing.Color.Transparent;
			this.labelBuild.ForeColor = System.Drawing.Color.White;
			this.labelBuild.Location = new System.Drawing.Point(53, 124);
			this.labelBuild.Name = "labelBuild";
			this.labelBuild.Size = new System.Drawing.Size(163, 13);
			this.labelBuild.TabIndex = 4;
			this.labelBuild.Text = "Build x.x.xxxx.xxxx @ nnnn-nn-nn";
			this.labelBuild.Click += new System.EventHandler(this.labelBuild_Click);
			// 
			// labelStarts
			// 
			this.labelStarts.AutoSize = true;
			this.labelStarts.BackColor = System.Drawing.Color.Transparent;
			this.labelStarts.ForeColor = System.Drawing.Color.White;
			this.labelStarts.Location = new System.Drawing.Point(53, 173);
			this.labelStarts.Name = "labelStarts";
			this.labelStarts.Size = new System.Drawing.Size(134, 13);
			this.labelStarts.TabIndex = 5;
			this.labelStarts.Text = "Application started xx times";
			this.labelStarts.Click += new System.EventHandler(this.labelStarts_Click);
			// 
			// labelExceptions
			// 
			this.labelExceptions.AutoSize = true;
			this.labelExceptions.BackColor = System.Drawing.Color.Transparent;
			this.labelExceptions.ForeColor = System.Drawing.Color.White;
			this.labelExceptions.Location = new System.Drawing.Point(53, 191);
			this.labelExceptions.Name = "labelExceptions";
			this.labelExceptions.Size = new System.Drawing.Size(204, 13);
			this.labelExceptions.TabIndex = 6;
			this.labelExceptions.Text = "Application generated exceptions xx times";
			this.labelExceptions.Click += new System.EventHandler(this.labelExceptions_Click);
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::XMLDBViewer.Properties.Resources.about;
			this.ClientSize = new System.Drawing.Size(419, 253);
			this.Controls.Add(this.labelExceptions);
			this.Controls.Add(this.labelStarts);
			this.Controls.Add(this.labelBuild);
			this.Controls.Add(this.labelCredits);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.TransparencyKey = System.Drawing.SystemColors.Control;
			this.Click += new System.EventHandler(this.AboutForm_Click);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyDown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelCredits;
		private System.Windows.Forms.Label labelBuild;
		private System.Windows.Forms.Label labelStarts;
		private System.Windows.Forms.Label labelExceptions;
	}
}