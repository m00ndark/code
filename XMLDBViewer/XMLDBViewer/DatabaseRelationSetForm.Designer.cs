namespace XMLDBViewer
{
	partial class DatabaseRelationSetForm
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
			this.listViewRelations = new System.Windows.Forms.ListView();
			this.columnHeaderSource = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderDestination = new System.Windows.Forms.ColumnHeader();
			this.listBoxSourceTable = new System.Windows.Forms.ListBox();
			this.listBoxSourceColumn = new System.Windows.Forms.ListBox();
			this.listBoxDestinationColumn = new System.Windows.Forms.ListBox();
			this.listBoxDestinationTable = new System.Windows.Forms.ListBox();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.labelName = new System.Windows.Forms.Label();
			this.buttonAuto = new System.Windows.Forms.Button();
			this.pictureBoxArrow = new System.Windows.Forms.PictureBox();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxArrow)).BeginInit();
			this.SuspendLayout();
			// 
			// listViewRelations
			// 
			this.listViewRelations.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewRelations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSource,
            this.columnHeaderDestination});
			this.listViewRelations.FullRowSelect = true;
			this.listViewRelations.HideSelection = false;
			this.listViewRelations.Location = new System.Drawing.Point(0, 31);
			this.listViewRelations.Name = "listViewRelations";
			this.listViewRelations.Size = new System.Drawing.Size(722, 201);
			this.listViewRelations.TabIndex = 0;
			this.listViewRelations.UseCompatibleStateImageBehavior = false;
			this.listViewRelations.View = System.Windows.Forms.View.Details;
			this.listViewRelations.SelectedIndexChanged += new System.EventHandler(this.listViewRelations_SelectedIndexChanged);
			this.listViewRelations.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewRelations_ColumnClick);
			// 
			// columnHeaderSource
			// 
			this.columnHeaderSource.Text = "Source";
			this.columnHeaderSource.Width = 350;
			// 
			// columnHeaderDestination
			// 
			this.columnHeaderDestination.Text = "Destination";
			this.columnHeaderDestination.Width = 350;
			// 
			// listBoxSourceTable
			// 
			this.listBoxSourceTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxSourceTable.FormattingEnabled = true;
			this.listBoxSourceTable.Location = new System.Drawing.Point(0, 0);
			this.listBoxSourceTable.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.listBoxSourceTable.Name = "listBoxSourceTable";
			this.listBoxSourceTable.Size = new System.Drawing.Size(155, 212);
			this.listBoxSourceTable.TabIndex = 2;
			this.listBoxSourceTable.SelectedIndexChanged += new System.EventHandler(this.listBoxSourceTable_SelectedIndexChanged);
			// 
			// listBoxSourceColumn
			// 
			this.listBoxSourceColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxSourceColumn.FormattingEnabled = true;
			this.listBoxSourceColumn.Location = new System.Drawing.Point(161, 0);
			this.listBoxSourceColumn.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.listBoxSourceColumn.Name = "listBoxSourceColumn";
			this.listBoxSourceColumn.Size = new System.Drawing.Size(155, 212);
			this.listBoxSourceColumn.TabIndex = 3;
			this.listBoxSourceColumn.SelectedIndexChanged += new System.EventHandler(this.listBoxSourceColumn_SelectedIndexChanged);
			// 
			// listBoxDestinationColumn
			// 
			this.listBoxDestinationColumn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxDestinationColumn.FormattingEnabled = true;
			this.listBoxDestinationColumn.Location = new System.Drawing.Point(563, 0);
			this.listBoxDestinationColumn.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.listBoxDestinationColumn.Name = "listBoxDestinationColumn";
			this.listBoxDestinationColumn.Size = new System.Drawing.Size(159, 212);
			this.listBoxDestinationColumn.TabIndex = 5;
			this.listBoxDestinationColumn.SelectedIndexChanged += new System.EventHandler(this.listBoxDestinationColumn_SelectedIndexChanged);
			// 
			// listBoxDestinationTable
			// 
			this.listBoxDestinationTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxDestinationTable.FormattingEnabled = true;
			this.listBoxDestinationTable.Location = new System.Drawing.Point(402, 0);
			this.listBoxDestinationTable.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
			this.listBoxDestinationTable.Name = "listBoxDestinationTable";
			this.listBoxDestinationTable.Size = new System.Drawing.Size(155, 212);
			this.listBoxDestinationTable.TabIndex = 4;
			this.listBoxDestinationTable.SelectedIndexChanged += new System.EventHandler(this.listBoxDestinationTable_SelectedIndexChanged);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAdd.Location = new System.Drawing.Point(731, 31);
			this.buttonAdd.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonAdd.TabIndex = 6;
			this.buttonAdd.Text = "Add";
			this.buttonAdd.UseVisualStyleBackColor = true;
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRemove.Location = new System.Drawing.Point(731, 60);
			this.buttonRemove.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(75, 23);
			this.buttonRemove.TabIndex = 7;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(12, 12);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.buttonAuto);
			this.splitContainer.Panel1.Controls.Add(this.labelName);
			this.splitContainer.Panel1.Controls.Add(this.textBoxName);
			this.splitContainer.Panel1.Controls.Add(this.buttonRemove);
			this.splitContainer.Panel1.Controls.Add(this.listViewRelations);
			this.splitContainer.Panel1.Controls.Add(this.buttonAdd);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanel);
			this.splitContainer.Panel2.Controls.Add(this.buttonCancel);
			this.splitContainer.Panel2.Controls.Add(this.buttonOK);
			this.splitContainer.Size = new System.Drawing.Size(806, 456);
			this.splitContainer.SplitterDistance = 240;
			this.splitContainer.TabIndex = 8;
			// 
			// textBoxName
			// 
			this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxName.Location = new System.Drawing.Point(44, 0);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(678, 20);
			this.textBoxName.TabIndex = 8;
			this.textBoxName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxName_KeyUp);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.ColumnCount = 5;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
			this.tableLayoutPanel.Controls.Add(this.listBoxSourceTable, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.listBoxSourceColumn, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.listBoxDestinationTable, 3, 0);
			this.tableLayoutPanel.Controls.Add(this.listBoxDestinationColumn, 4, 0);
			this.tableLayoutPanel.Controls.Add(this.pictureBoxArrow, 2, 0);
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(722, 214);
			this.tableLayoutPanel.TabIndex = 8;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(731, 189);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 10;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(731, 160);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 9;
			this.buttonOK.Text = "Save";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(0, 3);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(38, 13);
			this.labelName.TabIndex = 9;
			this.labelName.Text = "Name:";
			// 
			// buttonAuto
			// 
			this.buttonAuto.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAuto.Location = new System.Drawing.Point(731, 209);
			this.buttonAuto.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
			this.buttonAuto.Name = "buttonAuto";
			this.buttonAuto.Size = new System.Drawing.Size(75, 23);
			this.buttonAuto.TabIndex = 10;
			this.buttonAuto.Text = "Auto";
			this.buttonAuto.UseVisualStyleBackColor = true;
			this.buttonAuto.Click += new System.EventHandler(this.buttonAuto_Click);
			// 
			// pictureBoxArrow
			// 
			this.pictureBoxArrow.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxArrow.Image = global::XMLDBViewer.Properties.Resources.left_right_arrow;
			this.pictureBoxArrow.Location = new System.Drawing.Point(335, 19);
			this.pictureBoxArrow.Margin = new System.Windows.Forms.Padding(19);
			this.pictureBoxArrow.Name = "pictureBoxArrow";
			this.pictureBoxArrow.Size = new System.Drawing.Size(48, 176);
			this.pictureBoxArrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxArrow.TabIndex = 6;
			this.pictureBoxArrow.TabStop = false;
			// 
			// DatabaseRelationSetForm
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(830, 480);
			this.Controls.Add(this.splitContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "DatabaseRelationSetForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Relation Set";
			this.Load += new System.EventHandler(this.DatabaseRelationSetForm_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.tableLayoutPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.pictureBoxArrow)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listViewRelations;
		private System.Windows.Forms.ListBox listBoxSourceTable;
		private System.Windows.Forms.ListBox listBoxSourceColumn;
		private System.Windows.Forms.ListBox listBoxDestinationColumn;
		private System.Windows.Forms.ListBox listBoxDestinationTable;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.ColumnHeader columnHeaderSource;
		private System.Windows.Forms.ColumnHeader columnHeaderDestination;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.PictureBox pictureBoxArrow;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Button buttonAuto;
	}
}