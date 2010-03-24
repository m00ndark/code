namespace XMLDBViewer
{
	partial class DatabaseRelationSetAssociationForm
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
			this.textBoxDatabasePath = new System.Windows.Forms.TextBox();
			this.comboBoxRelationSets = new System.Windows.Forms.ComboBox();
			this.labelDatabase = new System.Windows.Forms.Label();
			this.labelRelationSet = new System.Windows.Forms.Label();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxDatabasePath
			// 
			this.textBoxDatabasePath.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDatabasePath.Location = new System.Drawing.Point(84, 9);
			this.textBoxDatabasePath.Name = "textBoxDatabasePath";
			this.textBoxDatabasePath.ReadOnly = true;
			this.textBoxDatabasePath.Size = new System.Drawing.Size(359, 20);
			this.textBoxDatabasePath.TabIndex = 2;
			// 
			// comboBoxRelationSets
			// 
			this.comboBoxRelationSets.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxRelationSets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRelationSets.FormattingEnabled = true;
			this.comboBoxRelationSets.Location = new System.Drawing.Point(84, 35);
			this.comboBoxRelationSets.Name = "comboBoxRelationSets";
			this.comboBoxRelationSets.Size = new System.Drawing.Size(359, 21);
			this.comboBoxRelationSets.TabIndex = 0;
			this.comboBoxRelationSets.SelectedIndexChanged += new System.EventHandler(this.comboBoxRelationSets_SelectedIndexChanged);
			// 
			// labelDatabase
			// 
			this.labelDatabase.AutoSize = true;
			this.labelDatabase.Location = new System.Drawing.Point(12, 12);
			this.labelDatabase.Name = "labelDatabase";
			this.labelDatabase.Size = new System.Drawing.Size(56, 13);
			this.labelDatabase.TabIndex = 3;
			this.labelDatabase.Text = "Database:";
			// 
			// labelRelationSet
			// 
			this.labelRelationSet.AutoSize = true;
			this.labelRelationSet.Location = new System.Drawing.Point(12, 38);
			this.labelRelationSet.Name = "labelRelationSet";
			this.labelRelationSet.Size = new System.Drawing.Size(66, 13);
			this.labelRelationSet.TabIndex = 4;
			this.labelRelationSet.Text = "Relation set:";
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
			this.tableLayoutPanel.Location = new System.Drawing.Point(12, 73);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(431, 29);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(137, 3);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(218, 3);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// DatabaseRelationSetAssociationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(455, 114);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.labelRelationSet);
			this.Controls.Add(this.labelDatabase);
			this.Controls.Add(this.comboBoxRelationSets);
			this.Controls.Add(this.textBoxDatabasePath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DatabaseRelationSetAssociationForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Relation Set Association";
			this.Load += new System.EventHandler(this.DatabaseRelationSetAssociationForm_Load);
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDatabasePath;
		private System.Windows.Forms.ComboBox comboBoxRelationSets;
		private System.Windows.Forms.Label labelDatabase;
		private System.Windows.Forms.Label labelRelationSet;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
	}
}