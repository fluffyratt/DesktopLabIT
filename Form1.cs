using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Lab1IT
{
    public partial class Form1 : Form
    {
        dbManager dbm = new dbManager();
        string cellOldValue = "";
        string cellNewValue = "";
        Dictionary<string, Table> temporaryTables = new Dictionary<string, Table>();

        public Form1()
        {
            InitializeComponent();
            cbTypes.SelectedIndex = 0;
        }
        private string SelectedTabText
        {
            get
            {
                return tabControl.SelectedTab?.Text ?? string.Empty;
            }
        }

        private void butCreate_Click(object sender, EventArgs e)
        {
            dbm.CreateDB(tbCreateDBName.Text);
            tabControl.TabPages.Clear();
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            temporaryTables.Clear();
        }

        private void butAddTable_Click(object sender, EventArgs e)
        {
            if (dbm.AddTable(tbAddTableName.Text))
            {
                tabControl.TabPages.Add(tbAddTableName.Text);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowSelectedTable();
        }

        void VisualTable(Table t, bool allowEdit)
        {
            try
            {
                dataGridView.Rows.Clear();
                dataGridView.Columns.Clear();

                foreach (Column c in t.tColumnsList)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                    {
                        Name = c.cName,
                        HeaderText = c.cName,
                        ReadOnly = !allowEdit
                    };
                    dataGridView.Columns.Add(column);
                }

                foreach (Row r in t.tRowsList)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    foreach (string s in r.rValuesList)
                    {
                        DataGridViewCell cell = new DataGridViewTextBoxCell();
                        cell.Value = s;
                        row.Cells.Add(cell);
                        cell.ReadOnly = !allowEdit;
                    }
                    try
                    {
                        dataGridView.Rows.Add(row);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }

                dataGridView.ReadOnly = !allowEdit;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToDeleteRows = false;
                dataGridView.EditMode = allowEdit
                    ? DataGridViewEditMode.EditOnKeystrokeOrF2
                    : DataGridViewEditMode.EditProgrammatically;
            }
            catch { }
        }

        private void butAddColumn_Click(object sender, EventArgs e)
        {
            var columnName = tbAddColumnName.Text.Trim();
            if (String.IsNullOrEmpty(columnName)) return;

            if (dbm.AddColumn(SelectedTabText, columnName, cbTypes.Text))
            {
                this.ShowSelectedTable();
            }
        }

        private void butAddRow_Click(object sender, EventArgs e)
        {
            if (dbm.AddRow(SelectedTabText))
            {
                this.ShowSelectedTable();
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cellOldValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cellNewValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (!dbm.ChangeValue(cellNewValue, SelectedTabText, e.ColumnIndex, e.RowIndex))
            {
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellOldValue;
            }

            this.ShowSelectedTable();
        }

        private void butDeleteRow_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count == 0) return;
            try
            {
                dbm.DeleteRow(SelectedTabText, dataGridView.CurrentCell.RowIndex);
            }
            catch { }

            this.ShowSelectedTable();
        }

        private void butDeleteColumn_Click(object sender, EventArgs e)
        {
            if (dataGridView.Columns.Count == 0 || dataGridView.CurrentCell == null || dbm.IsDBHasTable(SelectedTabText)) return;
            try
            {
                dbm.DeleteColumn(SelectedTabText, dataGridView.CurrentCell.ColumnIndex);
            }
            catch { }

            this.ShowSelectedTable();
        }

        private void butDeleteTable_Click(object sender, EventArgs e)
        {
            if (tabControl.TabCount == 0) return;
            try
            {
                if (dbm.IsDBHasTable(SelectedTabText))  // Перевірка меж індексу
                {
                    VisualTable(dbm.GetTable(SelectedTabText), true);
                    dbm.DeleteTable(SelectedTabText);
                } 
                else if (temporaryTables.ContainsKey(SelectedTabText))
                {
                    VisualTable(temporaryTables[SelectedTabText], false);
                    temporaryTables.Remove(SelectedTabText);
                }
                tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);
            }
            catch { }
            if (tabControl.TabCount == 0) return;

            this.ShowSelectedTable();
        }

        private void butSaveDB_Click(object sender, EventArgs e)
        {
            Stream myStream;

            sfdSaveDB.Filter = "tdb files (*.tdb)|*.tdb";
            sfdSaveDB.FilterIndex = 1;
            sfdSaveDB.RestoreDirectory = true;
            sfdSaveDB.FileName = tbCreateDBName.Text;

            if (sfdSaveDB.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = sfdSaveDB.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    myStream.Close();

                    dbm.SaveDB(sfdSaveDB.FileName);
                }
            }
        }
        private void butOpen_Click(object sender, EventArgs e)
        {
            ofdOpenDB.Filter = "tdb files (*.tdb)|*.tdb";
            ofdOpenDB.FilterIndex = 1;
            ofdOpenDB.RestoreDirectory = true;

            if (ofdChooseFilePath.ShowDialog() == DialogResult.OK)
            {
                temporaryTables.Clear();
                dbm.OpenDB(ofdChooseFilePath.FileName);
            }
            else
            {
                return;
            }

            tabControl.TabPages.Clear();
            List<string> buf = dbm.GetTableNameList();
            foreach (string s in buf)
                tabControl.TabPages.Add(s);

            this.ShowSelectedTable();
        }

        private void unionTables_Click(object sender, EventArgs e)
        {
            this.UnionTables(false);
        }

        private void distinctUnionTables_Click(object sender, EventArgs e)
        {
            this.UnionTables(true);
        }

        private void UnionTables(bool distinct)
        {
            try
            {
                string tableName1 = tbTable1.Text;
                string tableName2 = tbTable2.Text;

                if (string.IsNullOrEmpty(tableName1) || string.IsNullOrEmpty(tableName2))
                {
                    MessageBox.Show("Please enter both table names.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var unionTable = dbm.UnionTables(tableName1, tableName2, distinct);
                unionTable.tName = this.GetTemporaryTableName(unionTable.tName);

                TabPage newTab = new TabPage(unionTable.tName);
                tabControl.TabPages.Add(newTab);
                temporaryTables.Add(unionTable.tName, unionTable);
                tabControl.SelectTab(tabControl.TabPages.Count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetTemporaryTableName(string tableName)
        {
            int index = 1;
            string newTableName = tableName;
            while (temporaryTables.ContainsKey(newTableName))
            {
                newTableName = tableName + index;
            }

            return newTableName;
        }

        private void ShowSelectedTable()
        {
            if (tabControl.SelectedIndex == -1) return;

            if (dbm.IsDBHasTable(SelectedTabText))
                VisualTable(dbm.GetTable(SelectedTabText), true);
            else
                VisualTable(temporaryTables[SelectedTabText], false);
        }
    }
}
