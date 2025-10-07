using DbSystemLibrary;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab1IT
{
    public partial class Form1 : Form
    {
        private DatabaseViewModel _dbvm;
        private string _cellOldValue = "";

        public Form1()
        {
            InitializeComponent();
            cbTypes.SelectedIndex = 0;
        }

        private string SelectedTabName => tabControl.SelectedTab?.Text ?? string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateButtonsState();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Point clientLoc = this.PointToClient(Cursor.Position);
            Control child = this.GetChildAtPoint(clientLoc);
            if (child != null && child.Tag != null)
            {
                var tagType = child.Tag.GetType();
                var toolTipText = tagType.GetProperty("ToolTipText")?.GetValue(child.Tag) as string;
                if (!string.IsNullOrEmpty(toolTipText))
                {
                    toolTip.SetToolTip(this, toolTipText);
                    toolTip.Show(toolTipText, this, 10000);
                }
            }
            else
                toolTip.SetToolTip(this, null);
        }

        private void RefreshTabs()
        {
            tabControl.TabPages.Clear();
            if (_dbvm == null) return;

            foreach (var name in _dbvm.GetTabNames())
                AddTab(name);

            if (tabControl.TabPages.Count > 0)
                tabControl.SelectedIndex = 0;
        }

        private void AddTab(string name)
        {
            tabControl.TabPages.Add(new TabPage(name) { Name = name });
        }

        private void ShowSelectedTable()
        {
            if (_dbvm == null || tabControl.SelectedIndex == -1) return;

            var name = SelectedTabName;
            var table = _dbvm.GetTable(name);
            var allowEdit = !_dbvm.IsTemporary(name);

            VisualTable(table, allowEdit);
        }

        private void VisualTable(Table t, bool allowEdit)
        {
            if (t == null) return;

            dataGridView.SuspendLayout();
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            foreach (var c in t.Columns)
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = c.Name,
                    HeaderText = c.Name,
                    ReadOnly = !allowEdit
                });
            }

            foreach (var r in t.Rows)
            {
                var row = new DataGridViewRow();
                foreach (var s in r.ValuesList) row.Cells.Add(new DataGridViewTextBoxCell { Value = s });
                dataGridView.Rows.Add(row);
            }

            dataGridView.ReadOnly = !allowEdit;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.EditMode = allowEdit
                ? DataGridViewEditMode.EditOnKeystrokeOrF2
                : DataGridViewEditMode.EditProgrammatically;

            dataGridView.ResumeLayout();
        }

        private void butCreate_Click(object sender, EventArgs e)
        {
            var dbName = tbCreateDBName.Text.Trim();
            if (string.IsNullOrEmpty(dbName))
            {
                MessageBox.Show("Please enter a valid database name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var db = DbManager.CreateDB(dbName);
            _dbvm = new DatabaseViewModel(db);

            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            RefreshTabs();
            UpdateButtonsState();
        }

        private void butAddTable_Click(object sender, EventArgs e)
        {
            var name = tbAddTableName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a valid table name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dbvm.AddTableToDatabase(name);
            AddTab(name);
            UpdateButtonsState();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowSelectedTable();
            UpdateButtonsState();
        }

        private void butAddColumn_Click(object sender, EventArgs e)
        {
            var columnName = tbAddColumnName.Text.Trim();
            if (string.IsNullOrEmpty(columnName)) return;
            if (!Enum.TryParse<DbTypeEnum>(cbTypes.Text, true, out var dbType)) return;
            try
            {
                var table = _dbvm.GetTable(SelectedTabName);
                table.AddColumn(columnName, dbType);
                ShowSelectedTable();
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                var table = _dbvm.GetTable(SelectedTabName);
                table.AddRow();
                ShowSelectedTable();
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var v = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            _cellOldValue = v?.ToString() ?? "";
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var newVal = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
            try
            {
                var table = _dbvm.GetTable(SelectedTabName);
                table.ChangeValue(newVal, e.ColumnIndex, e.RowIndex);
            }
            catch (Exception ex)
            {
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = _cellOldValue;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butDeleteRow_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell == null) return;

            try
            {
                var table = _dbvm.GetTable(SelectedTabName);
                table.DeleteRow(dataGridView.CurrentCell.RowIndex);
                ShowSelectedTable();
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butDeleteColumn_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell == null) return;

            try
            {
                var table = _dbvm.GetTable(SelectedTabName);
                table.DeleteColumn(dataGridView.CurrentCell.ColumnIndex);
                ShowSelectedTable();
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butDeleteTable_Click(object sender, EventArgs e)
        {
            try
            {
                var name = SelectedTabName;
                var wasTemp = _dbvm.IsTemporary(name);
                _dbvm.DeleteTable(name);
                tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);

                if (tabControl.TabCount > 0) ShowSelectedTable();
                else
                {
                    dataGridView.Rows.Clear();
                    dataGridView.Columns.Clear();
                }
                UpdateButtonsState();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butSaveDB_Click(object sender, EventArgs e)
        {
            sfdSaveDB.Filter = GetDbFormatFilter();
            sfdSaveDB.FilterIndex = 1;
            sfdSaveDB.RestoreDirectory = true;
            sfdSaveDB.FileName = _dbvm.Name;

            if (sfdSaveDB.ShowDialog() != DialogResult.OK) return;
            try
            {
                var dbJson = DbManager.Serialize(_dbvm.Model);
                File.WriteAllText(sfdSaveDB.FileName, dbJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            ofdOpenDB.Filter = GetDbFormatFilter();
            ofdOpenDB.FilterIndex = 1;
            ofdOpenDB.RestoreDirectory = true;

            if (ofdChooseFilePath.ShowDialog() != DialogResult.OK) return;

            try
            {
                var json = File.ReadAllText(ofdChooseFilePath.FileName);
                var db = DbManager.Deserialize(json);
                _dbvm = new DatabaseViewModel(db);
                RefreshTabs();
                UpdateButtonsState();
                ShowSelectedTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDbFormatFilter()
        {
            var format = DbManager.DbFormat;
            var filter = $"{format} files (*.{format})|*.{format}";
            return filter;
        }

        private void unionTables_Click(object sender, EventArgs e) => UnionTables(false);
        private void distinctUnionTables_Click(object sender, EventArgs e) => UnionTables(true);

        private void UnionTables(bool distinct)
        {
            try
            {
                var t1 = tbTable1.Text?.Trim();
                var t2 = tbTable2.Text?.Trim();
                if (string.IsNullOrEmpty(t1) || string.IsNullOrEmpty(t2))
                {
                    MessageBox.Show("Please enter both table names.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tempName = _dbvm.UnionTables(t1, t2, distinct);
                AddTab(tempName);
                tabControl.SelectTab(tabControl.TabPages.Count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButtonsState()
        {
            var hasDb = _dbvm != null;
            string noDbIssue = "Firstly you need to create database.";

            var hasTable = hasDb && tabControl.SelectedIndex != -1;
            string hasTableIssue = "Please select a table.";

            var isTemp = hasTable && _dbvm.IsTemporary(SelectedTabName);
            var tempTableIssue = "You can't edit temporary tables.";
            var canAddColumn = hasTable && !isTemp;
            var canAddColumnIssue = isTemp ? tempTableIssue : hasTableIssue;
            var canAddRow = canAddColumn && _dbvm.IsHasColumns(SelectedTabName);
            var canAddRowIssue = "You need to add at least one column before adding rows.";

            var hasCell = hasTable && dataGridView.CurrentCell != null;
            var canDeleteRowColumn = hasCell && !isTemp;
            string canDeleteRowColumnIssue = isTemp ? tempTableIssue : hasTableIssue;

            var canUnion = hasDb && _dbvm.DatabaseTableCount > 1;
            string canUnionIssue = "You need at least two tables in database to perform union.";

            void SetState(Button btn, bool enabled, string disabledReason)
            {
                btn.Enabled = enabled;
                btn.Tag = enabled ? null : new { ToolTipText = disabledReason };
            }

            SetState(butAddTable, hasDb, noDbIssue);
            SetState(butSaveDB, hasDb, noDbIssue);

            SetState(butDeleteTable, hasTable, hasTableIssue);

            SetState(butAddColumn, canAddColumn, canAddColumnIssue);
            SetState(butDeleteColumn, canDeleteRowColumn, canDeleteRowColumnIssue);
            SetState(butAddRow, canAddRow, canAddRowIssue);
            SetState(butDeleteRow, canDeleteRowColumn, canDeleteRowColumnIssue);


            SetState(unionTables, canUnion, canUnionIssue);
            SetState(distinctUnionTables, canUnion, canUnionIssue);
        }
    }
}
