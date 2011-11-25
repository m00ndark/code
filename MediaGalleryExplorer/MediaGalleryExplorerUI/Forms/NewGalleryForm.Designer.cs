namespace MediaGalleryExplorerUI.Forms
{
	partial class NewGalleryForm
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
			this.labelFilePath = new System.Windows.Forms.Label();
			this.textBoxFilePath = new System.Windows.Forms.TextBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.comboBoxEncryption = new System.Windows.Forms.ComboBox();
			this.labelEncryption = new System.Windows.Forms.Label();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.labelPassword = new System.Windows.Forms.Label();
			this.buttonBrowseFilePath = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelFilePath
			// 
			this.labelFilePath.AutoSize = true;
			this.labelFilePath.Location = new System.Drawing.Point(12, 15);
			this.labelFilePath.Name = "labelFilePath";
			this.labelFilePath.Size = new System.Drawing.Size(55, 15);
			this.labelFilePath.TabIndex = 0;
			this.labelFilePath.Text = "File path:";
			// 
			// textBoxFilePath
			// 
			this.textBoxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFilePath.Location = new System.Drawing.Point(85, 12);
			this.textBoxFilePath.Name = "textBoxFilePath";
			this.textBoxFilePath.Size = new System.Drawing.Size(368, 23);
			this.textBoxFilePath.TabIndex = 1;
			this.textBoxFilePath.TextChanged += new System.EventHandler(this.TextBoxFilePath_TextChanged);
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(85, 41);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(368, 23);
			this.textBoxName.TabIndex = 3;
			this.textBoxName.TextChanged += new System.EventHandler(this.TextBoxName_TextChanged);
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(12, 44);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(42, 15);
			this.labelName.TabIndex = 2;
			this.labelName.Text = "Name:";
			// 
			// comboBoxEncryption
			// 
			this.comboBoxEncryption.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEncryption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEncryption.FormattingEnabled = true;
			this.comboBoxEncryption.Location = new System.Drawing.Point(85, 70);
			this.comboBoxEncryption.Name = "comboBoxEncryption";
			this.comboBoxEncryption.Size = new System.Drawing.Size(368, 23);
			this.comboBoxEncryption.TabIndex = 4;
			this.comboBoxEncryption.SelectedIndexChanged += new System.EventHandler(this.ComboBoxEncryption_SelectedIndexChanged);
			// 
			// labelEncryption
			// 
			this.labelEncryption.AutoSize = true;
			this.labelEncryption.Location = new System.Drawing.Point(12, 73);
			this.labelEncryption.Name = "labelEncryption";
			this.labelEncryption.Size = new System.Drawing.Size(67, 15);
			this.labelEncryption.TabIndex = 5;
			this.labelEncryption.Text = "Encryption:";
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPassword.Location = new System.Drawing.Point(85, 99);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.Size = new System.Drawing.Size(368, 23);
			this.textBoxPassword.TabIndex = 7;
			this.textBoxPassword.TextChanged += new System.EventHandler(this.TextBoxPassword_TextChanged);
			// 
			// labelPassword
			// 
			this.labelPassword.AutoSize = true;
			this.labelPassword.Location = new System.Drawing.Point(12, 102);
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.Size = new System.Drawing.Size(60, 15);
			this.labelPassword.TabIndex = 6;
			this.labelPassword.Text = "Password:";
			// 
			// buttonBrowseFilePath
			// 
			this.buttonBrowseFilePath.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBrowseFilePath.Location = new System.Drawing.Point(459, 12);
			this.buttonBrowseFilePath.Name = "buttonBrowseFilePath";
			this.buttonBrowseFilePath.Size = new System.Drawing.Size(31, 23);
			this.buttonBrowseFilePath.TabIndex = 8;
			this.buttonBrowseFilePath.Text = "...";
			this.buttonBrowseFilePath.UseVisualStyleBackColor = true;
			this.buttonBrowseFilePath.Click += new System.EventHandler(this.ButtonBrowseFilePath_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Enabled = false;
			this.buttonOK.Location = new System.Drawing.Point(161, 0);
			this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 9;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(242, 0);
			this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 10;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.buttonCancel, 1, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(12, 134);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(478, 23);
			this.tableLayoutPanel.TabIndex = 11;
			// 
			// NewGalleryForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(502, 169);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.buttonBrowseFilePath);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.labelEncryption);
			this.Controls.Add(this.comboBoxEncryption);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.textBoxFilePath);
			this.Controls.Add(this.labelFilePath);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewGalleryForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Gallery";
			this.Load += new System.EventHandler(this.NewGalleryForm_Load);
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelFilePath;
		private System.Windows.Forms.TextBox textBoxFilePath;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.ComboBox comboBoxEncryption;
		private System.Windows.Forms.Label labelEncryption;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Button buttonBrowseFilePath;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
	}
}