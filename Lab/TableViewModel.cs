using DbSystemLibrary;
using System;
using System.Data;
using System.Linq;

namespace Lab1IT
{
    internal class TableViewModel
    {
        private readonly Table _table;
        public DataTable Grid { get; private set; }
        public string Name => _table.Name;
        public bool AllowEdit { get; set; } = true;

        public TableViewModel(Table table)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            RebuildGridFromModel();
        }

        public void RebuildGridFromModel()
        {
            var dt = new DataTable();
            foreach (var c in _table.Columns) dt.Columns.Add(c.Name, typeof(string));
            foreach (var r in _table.Rows)
                dt.Rows.Add(r.ValuesList.Select(v => (object)(v ?? string.Empty)).ToArray());
            Grid = dt;
        }

        public void AddRow()
        {
            _table.AddRow();
            Grid.Rows.Add(Enumerable.Repeat("", _table.Columns.Count).ToArray());
        }

        public void DeleteRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= Grid.Rows.Count) return;
            _table.DeleteRow(rowIndex);
            Grid.Rows.RemoveAt(rowIndex);
        }

        public void AddColumn(string name, DbTypeEnum type)
        {
            _table.AddColumn(name, type);
            Grid.Columns.Add(name, typeof(string));
            foreach (DataRow dr in Grid.Rows) dr[name] = "";
        }

        public void DeleteColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= Grid.Columns.Count) return;
            _table.DeleteColumn(columnIndex);
            Grid.Columns.RemoveAt(columnIndex);
        }

        public void SetCell(int rowIndex, int colIndex, string newValue)
        {
            _table.ChangeValue(newValue ?? "", colIndex, rowIndex);
            Grid.Rows[rowIndex][colIndex] = newValue ?? "";
        }
    }
}
