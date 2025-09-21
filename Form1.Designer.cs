namespace Lab1IT
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
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
            this.butOpen = new System.Windows.Forms.Button();
            this.butCreate = new System.Windows.Forms.Button();
            this.butAddColumn = new System.Windows.Forms.Button();
            this.butAddRow = new System.Windows.Forms.Button();
            this.tbCreateDBName = new System.Windows.Forms.TextBox();
            this.butAddTable = new System.Windows.Forms.Button();
            this.tbAddTableName = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.cbTypes = new System.Windows.Forms.ComboBox();
            this.tbAddColumnName = new System.Windows.Forms.TextBox();
            this.ofdChooseFilePath = new System.Windows.Forms.OpenFileDialog();
            this.butDeleteColumn = new System.Windows.Forms.Button();
            this.butDeleteRow = new System.Windows.Forms.Button();
            this.butDeleteTable = new System.Windows.Forms.Button();
            this.sfdSaveDB = new System.Windows.Forms.SaveFileDialog();
            this.butSaveDB = new System.Windows.Forms.Button();
            this.ofdOpenDB = new System.Windows.Forms.OpenFileDialog();
            this.tbTable1 = new System.Windows.Forms.TextBox();
            this.tbTable2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.unionTables = new System.Windows.Forms.Button();
            this.distinctUnionTables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Location = new System.Drawing.Point(16, 192);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1035, 30);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // butOpen
            // 
            this.butOpen.Location = new System.Drawing.Point(17, 16);
            this.butOpen.Margin = new System.Windows.Forms.Padding(4);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(100, 28);
            this.butOpen.TabIndex = 1;
            this.butOpen.Text = "Open DB";
            this.butOpen.UseVisualStyleBackColor = true;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // butCreate
            // 
            this.butCreate.Location = new System.Drawing.Point(125, 15);
            this.butCreate.Margin = new System.Windows.Forms.Padding(4);
            this.butCreate.Name = "butCreate";
            this.butCreate.Size = new System.Drawing.Size(100, 28);
            this.butCreate.TabIndex = 2;
            this.butCreate.Text = "New DB";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // butAddColumn
            // 
            this.butAddColumn.Location = new System.Drawing.Point(512, 15);
            this.butAddColumn.Margin = new System.Windows.Forms.Padding(4);
            this.butAddColumn.Name = "butAddColumn";
            this.butAddColumn.Size = new System.Drawing.Size(100, 28);
            this.butAddColumn.TabIndex = 3;
            this.butAddColumn.Text = "Add Column";
            this.butAddColumn.UseVisualStyleBackColor = true;
            this.butAddColumn.Click += new System.EventHandler(this.butAddColumn_Click);
            // 
            // butAddRow
            // 
            this.butAddRow.Location = new System.Drawing.Point(951, 15);
            this.butAddRow.Margin = new System.Windows.Forms.Padding(4);
            this.butAddRow.Name = "butAddRow";
            this.butAddRow.Size = new System.Drawing.Size(100, 28);
            this.butAddRow.TabIndex = 4;
            this.butAddRow.Text = "Add Row";
            this.butAddRow.UseVisualStyleBackColor = true;
            this.butAddRow.Click += new System.EventHandler(this.butAddRow_Click);
            // 
            // tbCreateDBName
            // 
            this.tbCreateDBName.Location = new System.Drawing.Point(125, 50);
            this.tbCreateDBName.Margin = new System.Windows.Forms.Padding(4);
            this.tbCreateDBName.Name = "tbCreateDBName";
            this.tbCreateDBName.Size = new System.Drawing.Size(132, 22);
            this.tbCreateDBName.TabIndex = 5;
            // 
            // butAddTable
            // 
            this.butAddTable.Location = new System.Drawing.Point(309, 15);
            this.butAddTable.Margin = new System.Windows.Forms.Padding(4);
            this.butAddTable.Name = "butAddTable";
            this.butAddTable.Size = new System.Drawing.Size(100, 28);
            this.butAddTable.TabIndex = 6;
            this.butAddTable.Text = "Add Table";
            this.butAddTable.UseVisualStyleBackColor = true;
            this.butAddTable.Click += new System.EventHandler(this.butAddTable_Click);
            // 
            // tbAddTableName
            // 
            this.tbAddTableName.Location = new System.Drawing.Point(309, 50);
            this.tbAddTableName.Margin = new System.Windows.Forms.Padding(4);
            this.tbAddTableName.Name = "tbAddTableName";
            this.tbAddTableName.Size = new System.Drawing.Size(132, 22);
            this.tbAddTableName.TabIndex = 7;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(17, 229);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.Size = new System.Drawing.Size(1033, 389);
            this.dataGridView.TabIndex = 8;
            this.dataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_CellBeginEdit);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            // 
            // cbTypes
            // 
            this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypes.FormattingEnabled = true;
            this.cbTypes.Items.AddRange(new object[] {
            "Integer",
            "Real",
            "Char",
            "String",
            "Time",
            "TimeInvl"});
            this.cbTypes.Location = new System.Drawing.Point(512, 82);
            this.cbTypes.Margin = new System.Windows.Forms.Padding(4);
            this.cbTypes.Name = "cbTypes";
            this.cbTypes.Size = new System.Drawing.Size(160, 24);
            this.cbTypes.TabIndex = 10;
            // 
            // tbAddColumnName
            // 
            this.tbAddColumnName.Location = new System.Drawing.Point(512, 50);
            this.tbAddColumnName.Margin = new System.Windows.Forms.Padding(4);
            this.tbAddColumnName.Name = "tbAddColumnName";
            this.tbAddColumnName.Size = new System.Drawing.Size(160, 22);
            this.tbAddColumnName.TabIndex = 11;
            // 
            // butDeleteColumn
            // 
            this.butDeleteColumn.Location = new System.Drawing.Point(512, 116);
            this.butDeleteColumn.Margin = new System.Windows.Forms.Padding(4);
            this.butDeleteColumn.Name = "butDeleteColumn";
            this.butDeleteColumn.Size = new System.Drawing.Size(127, 28);
            this.butDeleteColumn.TabIndex = 14;
            this.butDeleteColumn.Text = "Delete Column";
            this.butDeleteColumn.UseVisualStyleBackColor = true;
            this.butDeleteColumn.Click += new System.EventHandler(this.butDeleteColumn_Click);
            // 
            // butDeleteRow
            // 
            this.butDeleteRow.Location = new System.Drawing.Point(951, 62);
            this.butDeleteRow.Margin = new System.Windows.Forms.Padding(4);
            this.butDeleteRow.Name = "butDeleteRow";
            this.butDeleteRow.Size = new System.Drawing.Size(100, 28);
            this.butDeleteRow.TabIndex = 15;
            this.butDeleteRow.Text = "Delete Row";
            this.butDeleteRow.UseVisualStyleBackColor = true;
            this.butDeleteRow.Click += new System.EventHandler(this.butDeleteRow_Click);
            // 
            // butDeleteTable
            // 
            this.butDeleteTable.Location = new System.Drawing.Point(309, 82);
            this.butDeleteTable.Margin = new System.Windows.Forms.Padding(4);
            this.butDeleteTable.Name = "butDeleteTable";
            this.butDeleteTable.Size = new System.Drawing.Size(129, 28);
            this.butDeleteTable.TabIndex = 16;
            this.butDeleteTable.Text = "Delete Table";
            this.butDeleteTable.UseVisualStyleBackColor = true;
            this.butDeleteTable.Click += new System.EventHandler(this.butDeleteTable_Click);
            // 
            // butSaveDB
            // 
            this.butSaveDB.Location = new System.Drawing.Point(16, 50);
            this.butSaveDB.Margin = new System.Windows.Forms.Padding(4);
            this.butSaveDB.Name = "butSaveDB";
            this.butSaveDB.Size = new System.Drawing.Size(100, 28);
            this.butSaveDB.TabIndex = 17;
            this.butSaveDB.Text = "Save DB";
            this.butSaveDB.UseVisualStyleBackColor = true;
            this.butSaveDB.Click += new System.EventHandler(this.butSaveDB_Click);
            // 
            // tbTable1
            // 
            this.tbTable1.Location = new System.Drawing.Point(915, 98);
            this.tbTable1.Margin = new System.Windows.Forms.Padding(4);
            this.tbTable1.Name = "tbTable1";
            this.tbTable1.Size = new System.Drawing.Size(132, 22);
            this.tbTable1.TabIndex = 19;
            // 
            // tbTable2
            // 
            this.tbTable2.Location = new System.Drawing.Point(915, 129);
            this.tbTable2.Margin = new System.Windows.Forms.Padding(4);
            this.tbTable2.Name = "tbTable2";
            this.tbTable2.Size = new System.Drawing.Size(132, 22);
            this.tbTable2.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(849, 102);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Table 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(849, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "Table 2";
            // 
            // unionTables
            // 
            this.unionTables.Location = new System.Drawing.Point(693, 95);
            this.unionTables.Margin = new System.Windows.Forms.Padding(4);
            this.unionTables.Name = "unionTables";
            this.unionTables.Size = new System.Drawing.Size(148, 28);
            this.unionTables.TabIndex = 24;
            this.unionTables.Text = "Union Tables";
            this.unionTables.UseVisualStyleBackColor = true;
            this.unionTables.Click += new System.EventHandler(this.unionTables_Click);
            // 
            // distinctUnionTables
            // 
            this.distinctUnionTables.Location = new System.Drawing.Point(693, 126);
            this.distinctUnionTables.Margin = new System.Windows.Forms.Padding(4);
            this.distinctUnionTables.Name = "distinctUnionTables";
            this.distinctUnionTables.Size = new System.Drawing.Size(148, 28);
            this.distinctUnionTables.TabIndex = 25;
            this.distinctUnionTables.Text = "Distinct Union Tables";
            this.distinctUnionTables.UseVisualStyleBackColor = true;
            this.distinctUnionTables.Click += new System.EventHandler(this.distinctUnionTables_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 633);
            this.Controls.Add(this.distinctUnionTables);
            this.Controls.Add(this.unionTables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbTable2);
            this.Controls.Add(this.tbTable1);
            this.Controls.Add(this.butSaveDB);
            this.Controls.Add(this.butDeleteTable);
            this.Controls.Add(this.butDeleteRow);
            this.Controls.Add(this.butDeleteColumn);
            this.Controls.Add(this.tbAddColumnName);
            this.Controls.Add(this.cbTypes);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.tbAddTableName);
            this.Controls.Add(this.butAddTable);
            this.Controls.Add(this.tbCreateDBName);
            this.Controls.Add(this.butAddRow);
            this.Controls.Add(this.butAddColumn);
            this.Controls.Add(this.butCreate);
            this.Controls.Add(this.butOpen);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Lab1 IT";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button butOpen;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butAddColumn;
        private System.Windows.Forms.Button butAddRow;
        private System.Windows.Forms.TextBox tbCreateDBName;
        private System.Windows.Forms.Button butAddTable;
        private System.Windows.Forms.TextBox tbAddTableName;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox cbTypes;
        private System.Windows.Forms.TextBox tbAddColumnName;
        private System.Windows.Forms.OpenFileDialog ofdChooseFilePath;
        private System.Windows.Forms.Button butDeleteColumn;
        private System.Windows.Forms.Button butDeleteRow;
        private System.Windows.Forms.Button butDeleteTable;
        private System.Windows.Forms.SaveFileDialog sfdSaveDB;
        private System.Windows.Forms.Button butSaveDB;
        private System.Windows.Forms.OpenFileDialog ofdOpenDB;
        private System.Windows.Forms.TextBox tbTable1;
        private System.Windows.Forms.TextBox tbTable2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button unionTables;
        private System.Windows.Forms.Button distinctUnionTables;
    }
}

