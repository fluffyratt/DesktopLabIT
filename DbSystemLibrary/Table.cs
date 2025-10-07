using System;
using System.Collections.Generic;

namespace DbSystemLibrary
{
    public class Table
    {
        string _name;
        List<Column> _columns;
        List<Row> _rows;

        public Table(string tname)
        {
            _name = tname;
            _columns = new List<Column>();
            _rows = new List<Row>();
        }
        public string Name => _name;
        public IReadOnlyList<Column> Columns => _columns;
        public IReadOnlyList<Row> Rows => _rows;

        public void AddColumn(string columnName, DbTypeEnum columnType)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentException("Column name name must be specified.", nameof(columnName));

            var isTableHasColumn = _columns.Exists(col => col.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
            if (isTableHasColumn)
                throw new InvalidOperationException($"Table already has column with name '{columnName}'");

            _columns.Add(new Column(columnName, columnType));
            for (int i = 0; i < _rows.Count; ++i)
            {
                _rows[i].ValuesList.Add("");
            }
        }

        public void AddRow(Row row = null)
        {
            if (_columns.Count < 1)
                throw new InvalidOperationException("Table doesn't have any column");

            if (row != null)
            {
                if(row.ValuesList.Count != _columns.Count)
                    throw new ArgumentException("Row has different number of values than table has columns.", nameof(row));
            
                for (int i = 0; i < _columns.Count; ++i)
                {
                    if (!_columns[i].IsValid(row.ValuesList[i]))
                        throw new ArgumentException($"Wrong input data type for column '{_columns[i].Name}'!", nameof(row));
                }
            }
            else
            {
                row = GetEmptyRow();
            }
                
            _rows.Add(row);
        }

        public void ChangeValue(string newValue, int cind, int rind)
        {
            if (cind < 0 || cind >= _columns.Count)
                throw new ArgumentOutOfRangeException(nameof(cind), "Column index is out of range");

            if (rind < 0 || rind >= _rows.Count)
                throw new ArgumentOutOfRangeException(nameof(rind), "Row index is out of range");

            if (!_columns[cind].IsValid(newValue))
                throw new ArgumentException("Wrong input data type!", nameof(newValue));

            _rows[rind].ValuesList[cind] = newValue;
        }

        public void DeleteRow(int rind)
        {
            if (rind < 0 || rind >= _rows.Count)
                throw new ArgumentOutOfRangeException(nameof(rind), "Row index is out of range");

            _rows.RemoveAt(rind);
        }

        public void DeleteColumn(int cind)
        {
            if (cind < 0 || cind >= _columns.Count)
                throw new ArgumentOutOfRangeException(nameof(cind), "Column index is out of range");

            _columns.RemoveAt(cind);
            for (int i = 0; i < _rows.Count; ++i)
            {
                _rows[i].ValuesList.RemoveAt(cind);
            }
        }

        internal void AddUncheckedRow(Row row)
        {
            if (row is null)
                throw new ArgumentNullException(nameof(row));

            if (row.ValuesList.Count != _columns.Count)
                throw new ArgumentException("Row has different number of values than table has columns.", nameof(row));

            _rows.Add(row);
        }

        private Row GetEmptyRow()
        {
            var newRow = new Row();
            for (int i = 0; i < _columns.Count; ++i)
            {
                newRow.ValuesList.Add("");
            }
            return newRow;
        }
    }
}
