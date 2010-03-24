namespace XMLDBViewer
{
	partial class AdvancedSearchForm
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
            this.labelFindWhat = new System.Windows.Forms.Label();
            this.labelLookIn = new System.Windows.Forms.Label();
            this.comboBoxSearchMode = new System.Windows.Forms.ComboBox();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeValue = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxSearchTable = new System.Windows.Forms.ComboBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.buttonFind = new System.Windows.Forms.Button();
            this.listViewSearchResult = new System.Windows.Forms.ListView();
            this.columnHeaderTable = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColumn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRow = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderValue = new System.Windows.Forms.ColumnHeader();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxSearchValue = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFindWhat
            // 
            this.labelFindWhat.AutoSize = true;
            this.labelFindWhat.Location = new System.Drawing.Point(12, 12);
            this.labelFindWhat.Name = "labelFindWhat";
            this.labelFindWhat.Size = new System.Drawing.Size(56, 13);
            this.labelFindWhat.TabIndex = 0;
            this.labelFindWhat.Text = "Find what:";
            // 
            // labelLookIn
            // 
            this.labelLookIn.AutoSize = true;
            this.labelLookIn.Location = new System.Drawing.Point(317, 0);
            this.labelLookIn.Name = "labelLookIn";
            this.labelLookIn.Size = new System.Drawing.Size(45, 13);
            this.labelLookIn.TabIndex = 2;
            this.labelLookIn.Text = "Look in:";
            // 
            // comboBoxSearchMode
            // 
            this.comboBoxSearchMode.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSearchMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchMode.FormattingEnabled = true;
            this.comboBoxSearchMode.Location = new System.Drawing.Point(3, 16);
            this.comboBoxSearchMode.Name = "comboBoxSearchMode";
            this.comboBoxSearchMode.Size = new System.Drawing.Size(308, 21);
            this.comboBoxSearchMode.TabIndex = 1;
            this.comboBoxSearchMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchMode_SelectedIndexChanged);
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(15, 107);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMatchCase.TabIndex = 2;
            this.checkBoxMatchCase.Text = "Match case";
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            this.checkBoxMatchCase.CheckedChanged += new System.EventHandler(this.checkBoxMatchCase_CheckedChanged);
            // 
            // checkBoxMatchWholeValue
            // 
            this.checkBoxMatchWholeValue.AutoSize = true;
            this.checkBoxMatchWholeValue.Location = new System.Drawing.Point(15, 130);
            this.checkBoxMatchWholeValue.Name = "checkBoxMatchWholeValue";
            this.checkBoxMatchWholeValue.Size = new System.Drawing.Size(116, 17);
            this.checkBoxMatchWholeValue.TabIndex = 3;
            this.checkBoxMatchWholeValue.Text = "Match whole value";
            this.checkBoxMatchWholeValue.UseVisualStyleBackColor = true;
            this.checkBoxMatchWholeValue.CheckedChanged += new System.EventHandler(this.checkBoxMatchWholeValue_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxSearchTable, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxSearchMode, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelLookIn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMode, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 58);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(628, 40);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // comboBoxSearchTable
            // 
            this.comboBoxSearchTable.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSearchTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchTable.FormattingEnabled = true;
            this.comboBoxSearchTable.Location = new System.Drawing.Point(317, 16);
            this.comboBoxSearchTable.Name = "comboBoxSearchTable";
            this.comboBoxSearchTable.Size = new System.Drawing.Size(308, 21);
            this.comboBoxSearchTable.TabIndex = 0;
            this.comboBoxSearchTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchTable_SelectedIndexChanged);
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Location = new System.Drawing.Point(3, 0);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(37, 13);
            this.labelMode.TabIndex = 4;
            this.labelMode.Text = "Mode:";
            // 
            // buttonFind
            // 
            this.buttonFind.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFind.Location = new System.Drawing.Point(478, 124);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(75, 23);
            this.buttonFind.TabIndex = 4;
            this.buttonFind.Text = "Find";
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // listViewSearchResult
            // 
            this.listViewSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSearchResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTable,
            this.columnHeaderColumn,
            this.columnHeaderRow,
            this.columnHeaderValue});
            this.listViewSearchResult.FullRowSelect = true;
            this.listViewSearchResult.HideSelection = false;
            this.listViewSearchResult.Location = new System.Drawing.Point(12, 179);
            this.listViewSearchResult.MultiSelect = false;
            this.listViewSearchResult.Name = "listViewSearchResult";
            this.listViewSearchResult.Size = new System.Drawing.Size(622, 225);
            this.listViewSearchResult.TabIndex = 6;
            this.listViewSearchResult.UseCompatibleStateImageBehavior = false;
            this.listViewSearchResult.View = System.Windows.Forms.View.Details;
            this.listViewSearchResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSearchResult_MouseDoubleClick);
            this.listViewSearchResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewSearchResult_MouseClick);
            // 
            // columnHeaderTable
            // 
            this.columnHeaderTable.Text = "Table";
            this.columnHeaderTable.Width = 150;
            // 
            // columnHeaderColumn
            // 
            this.columnHeaderColumn.Text = "Column";
            this.columnHeaderColumn.Width = 140;
            // 
            // columnHeaderRow
            // 
            this.columnHeaderRow.Text = "Row";
            this.columnHeaderRow.Width = 45;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "Value";
            this.columnHeaderValue.Width = 262;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(12, 163);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(40, 13);
            this.labelResult.TabIndex = 10;
            this.labelResult.Text = "Result:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(559, 124);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxSearchValue
            // 
            this.comboBoxSearchValue.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSearchValue.FormattingEnabled = true;
            this.comboBoxSearchValue.Location = new System.Drawing.Point(12, 28);
            this.comboBoxSearchValue.Name = "comboBoxSearchValue";
            this.comboBoxSearchValue.Size = new System.Drawing.Size(622, 21);
            this.comboBoxSearchValue.TabIndex = 0;
            this.comboBoxSearchValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxSearchValue_KeyDown);
            // 
            // AdvancedSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(646, 416);
            this.Controls.Add(this.comboBoxSearchValue);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.listViewSearchResult);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.checkBoxMatchWholeValue);
            this.Controls.Add(this.checkBoxMatchCase);
            this.Controls.Add(this.labelFindWhat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSearchForm";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Advanced Search";
            this.Load += new System.EventHandler(this.AdvancedSearchForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedSearchForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelFindWhat;
		private System.Windows.Forms.Label labelLookIn;
		private System.Windows.Forms.ComboBox comboBoxSearchMode;
		private System.Windows.Forms.CheckBox checkBoxMatchCase;
		private System.Windows.Forms.CheckBox checkBoxMatchWholeValue;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ComboBox comboBoxSearchTable;
		private System.Windows.Forms.Label labelMode;
		private System.Windows.Forms.Button buttonFind;
		private System.Windows.Forms.ListView listViewSearchResult;
		private System.Windows.Forms.Label labelResult;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ComboBox comboBoxSearchValue;
		private System.Windows.Forms.ColumnHeader columnHeaderTable;
		private System.Windows.Forms.ColumnHeader columnHeaderColumn;
		private System.Windows.Forms.ColumnHeader columnHeaderRow;
		private System.Windows.Forms.ColumnHeader columnHeaderValue;
	}
}