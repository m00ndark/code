namespace XMLDBViewer
{
	partial class DatabaseRelationSetManagerForm
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
            this.listViewRelationSets = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreatedDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderModifiedDate = new System.Windows.Forms.ColumnHeader();
            this.labelReleationSet = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelRelationSets = new System.Windows.Forms.Panel();
            this.buttonRelationSetDelete = new System.Windows.Forms.Button();
            this.buttonRelationSetEdit = new System.Windows.Forms.Button();
            this.buttonRelationSetNew = new System.Windows.Forms.Button();
            this.panelRelationSetDatabases = new System.Windows.Forms.Panel();
            this.buttonAssociationRemove = new System.Windows.Forms.Button();
            this.buttonAssociationAdd = new System.Windows.Forms.Button();
            this.labelDatabases = new System.Windows.Forms.Label();
            this.listViewDatabases = new System.Windows.Forms.ListView();
            this.columnHeaderDatabasePath = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRelationSetName = new System.Windows.Forms.ColumnHeader();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonRelationSetCopy = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.panelRelationSets.SuspendLayout();
            this.panelRelationSetDatabases.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewRelationSets
            // 
            this.listViewRelationSets.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewRelationSets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderCreatedDate,
            this.columnHeaderModifiedDate});
            this.listViewRelationSets.FullRowSelect = true;
            this.listViewRelationSets.HideSelection = false;
            this.listViewRelationSets.Location = new System.Drawing.Point(0, 16);
            this.listViewRelationSets.Name = "listViewRelationSets";
            this.listViewRelationSets.Size = new System.Drawing.Size(555, 130);
            this.listViewRelationSets.TabIndex = 0;
            this.listViewRelationSets.UseCompatibleStateImageBehavior = false;
            this.listViewRelationSets.View = System.Windows.Forms.View.Details;
            this.listViewRelationSets.SelectedIndexChanged += new System.EventHandler(this.listViewRelationSets_SelectedIndexChanged);
            this.listViewRelationSets.DoubleClick += new System.EventHandler(this.listViewRelationSets_DoubleClick);
            this.listViewRelationSets.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewRelationSets_ColumnClick);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 308;
            // 
            // columnHeaderCreatedDate
            // 
            this.columnHeaderCreatedDate.Text = "Created";
            this.columnHeaderCreatedDate.Width = 111;
            // 
            // columnHeaderModifiedDate
            // 
            this.columnHeaderModifiedDate.Text = "Modified";
            this.columnHeaderModifiedDate.Width = 111;
            // 
            // labelReleationSet
            // 
            this.labelReleationSet.AutoSize = true;
            this.labelReleationSet.Location = new System.Drawing.Point(0, 0);
            this.labelReleationSet.Name = "labelReleationSet";
            this.labelReleationSet.Size = new System.Drawing.Size(71, 13);
            this.labelReleationSet.TabIndex = 1;
            this.labelReleationSet.Text = "Relation sets:";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.panelRelationSets, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panelRelationSetDatabases, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonOK, 0, 2);
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(636, 338);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // panelRelationSets
            // 
            this.panelRelationSets.Controls.Add(this.buttonRelationSetCopy);
            this.panelRelationSets.Controls.Add(this.buttonRelationSetDelete);
            this.panelRelationSets.Controls.Add(this.buttonRelationSetEdit);
            this.panelRelationSets.Controls.Add(this.buttonRelationSetNew);
            this.panelRelationSets.Controls.Add(this.labelReleationSet);
            this.panelRelationSets.Controls.Add(this.listViewRelationSets);
            this.panelRelationSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRelationSets.Location = new System.Drawing.Point(0, 0);
            this.panelRelationSets.Margin = new System.Windows.Forms.Padding(0);
            this.panelRelationSets.Name = "panelRelationSets";
            this.panelRelationSets.Size = new System.Drawing.Size(636, 155);
            this.panelRelationSets.TabIndex = 0;
            // 
            // buttonRelationSetDelete
            // 
            this.buttonRelationSetDelete.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRelationSetDelete.Location = new System.Drawing.Point(561, 74);
            this.buttonRelationSetDelete.Name = "buttonRelationSetDelete";
            this.buttonRelationSetDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonRelationSetDelete.TabIndex = 4;
            this.buttonRelationSetDelete.Text = "Delete";
            this.buttonRelationSetDelete.UseVisualStyleBackColor = true;
            this.buttonRelationSetDelete.Click += new System.EventHandler(this.buttonRelationSetDelete_Click);
            // 
            // buttonRelationSetEdit
            // 
            this.buttonRelationSetEdit.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRelationSetEdit.Location = new System.Drawing.Point(561, 45);
            this.buttonRelationSetEdit.Name = "buttonRelationSetEdit";
            this.buttonRelationSetEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonRelationSetEdit.TabIndex = 3;
            this.buttonRelationSetEdit.Text = "Edit";
            this.buttonRelationSetEdit.UseVisualStyleBackColor = true;
            this.buttonRelationSetEdit.Click += new System.EventHandler(this.buttonRelationSetEdit_Click);
            // 
            // buttonRelationSetNew
            // 
            this.buttonRelationSetNew.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRelationSetNew.Location = new System.Drawing.Point(561, 16);
            this.buttonRelationSetNew.Name = "buttonRelationSetNew";
            this.buttonRelationSetNew.Size = new System.Drawing.Size(75, 23);
            this.buttonRelationSetNew.TabIndex = 2;
            this.buttonRelationSetNew.Text = "New";
            this.buttonRelationSetNew.UseVisualStyleBackColor = true;
            this.buttonRelationSetNew.Click += new System.EventHandler(this.buttonRelationSetNew_Click);
            // 
            // panelRelationSetDatabases
            // 
            this.panelRelationSetDatabases.Controls.Add(this.buttonAssociationRemove);
            this.panelRelationSetDatabases.Controls.Add(this.buttonAssociationAdd);
            this.panelRelationSetDatabases.Controls.Add(this.labelDatabases);
            this.panelRelationSetDatabases.Controls.Add(this.listViewDatabases);
            this.panelRelationSetDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRelationSetDatabases.Location = new System.Drawing.Point(0, 155);
            this.panelRelationSetDatabases.Margin = new System.Windows.Forms.Padding(0);
            this.panelRelationSetDatabases.Name = "panelRelationSetDatabases";
            this.panelRelationSetDatabases.Size = new System.Drawing.Size(636, 155);
            this.panelRelationSetDatabases.TabIndex = 1;
            // 
            // buttonAssociationRemove
            // 
            this.buttonAssociationRemove.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAssociationRemove.Location = new System.Drawing.Point(561, 46);
            this.buttonAssociationRemove.Name = "buttonAssociationRemove";
            this.buttonAssociationRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonAssociationRemove.TabIndex = 5;
            this.buttonAssociationRemove.Text = "Remove";
            this.buttonAssociationRemove.UseVisualStyleBackColor = true;
            this.buttonAssociationRemove.Click += new System.EventHandler(this.buttonAssociationRemove_Click);
            // 
            // buttonAssociationAdd
            // 
            this.buttonAssociationAdd.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAssociationAdd.Location = new System.Drawing.Point(561, 17);
            this.buttonAssociationAdd.Name = "buttonAssociationAdd";
            this.buttonAssociationAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAssociationAdd.TabIndex = 4;
            this.buttonAssociationAdd.Text = "Add";
            this.buttonAssociationAdd.UseVisualStyleBackColor = true;
            this.buttonAssociationAdd.Click += new System.EventHandler(this.buttonAssociationAdd_Click);
            // 
            // labelDatabases
            // 
            this.labelDatabases.AutoSize = true;
            this.labelDatabases.Location = new System.Drawing.Point(0, 1);
            this.labelDatabases.Name = "labelDatabases";
            this.labelDatabases.Size = new System.Drawing.Size(69, 13);
            this.labelDatabases.TabIndex = 3;
            this.labelDatabases.Text = "Associations:";
            // 
            // listViewDatabases
            // 
            this.listViewDatabases.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDatabases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDatabasePath,
            this.columnHeaderRelationSetName});
            this.listViewDatabases.FullRowSelect = true;
            this.listViewDatabases.HideSelection = false;
            this.listViewDatabases.Location = new System.Drawing.Point(0, 17);
            this.listViewDatabases.Name = "listViewDatabases";
            this.listViewDatabases.Size = new System.Drawing.Size(555, 130);
            this.listViewDatabases.TabIndex = 2;
            this.listViewDatabases.UseCompatibleStateImageBehavior = false;
            this.listViewDatabases.View = System.Windows.Forms.View.Details;
            this.listViewDatabases.SelectedIndexChanged += new System.EventHandler(this.listViewDatabases_SelectedIndexChanged);
            this.listViewDatabases.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewDatabases_ColumnClick);
            // 
            // columnHeaderDatabasePath
            // 
            this.columnHeaderDatabasePath.Text = "Database";
            this.columnHeaderDatabasePath.Width = 380;
            // 
            // columnHeaderRelationSetName
            // 
            this.columnHeaderRelationSetName.Text = "Relation Set";
            this.columnHeaderRelationSetName.Width = 150;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(280, 315);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonRelationSetCopy
            // 
            this.buttonRelationSetCopy.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRelationSetCopy.Location = new System.Drawing.Point(561, 103);
            this.buttonRelationSetCopy.Name = "buttonRelationSetCopy";
            this.buttonRelationSetCopy.Size = new System.Drawing.Size(75, 23);
            this.buttonRelationSetCopy.TabIndex = 5;
            this.buttonRelationSetCopy.Text = "Copy";
            this.buttonRelationSetCopy.UseVisualStyleBackColor = true;
            this.buttonRelationSetCopy.Click += new System.EventHandler(this.buttonRelationSetCopy_Click);
            // 
            // DatabaseRelationSetManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(660, 362);
            this.Controls.Add(this.tableLayoutPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseRelationSetManagerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Database Relation Set Manager";
            this.Load += new System.EventHandler(this.DatabaseRelationSetManagerForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelRelationSets.ResumeLayout(false);
            this.panelRelationSets.PerformLayout();
            this.panelRelationSetDatabases.ResumeLayout(false);
            this.panelRelationSetDatabases.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listViewRelationSets;
		private System.Windows.Forms.Label labelReleationSet;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Panel panelRelationSets;
		private System.Windows.Forms.Panel panelRelationSetDatabases;
		private System.Windows.Forms.Label labelDatabases;
		private System.Windows.Forms.ListView listViewDatabases;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderCreatedDate;
		private System.Windows.Forms.ColumnHeader columnHeaderModifiedDate;
		private System.Windows.Forms.ColumnHeader columnHeaderDatabasePath;
		private System.Windows.Forms.Button buttonRelationSetDelete;
		private System.Windows.Forms.Button buttonRelationSetEdit;
		private System.Windows.Forms.Button buttonRelationSetNew;
		private System.Windows.Forms.Button buttonAssociationRemove;
		private System.Windows.Forms.Button buttonAssociationAdd;
		private System.Windows.Forms.ColumnHeader columnHeaderRelationSetName;
        private System.Windows.Forms.Button buttonRelationSetCopy;
	}
}