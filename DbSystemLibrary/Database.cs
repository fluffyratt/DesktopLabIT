using System;
using System.Collections.Generic;
using System.Linq;

namespace DbSystemLibrary
{
    public class Database
    {
        string _name;
        Dictionary<string, Table> _tablesDict;

        public Database(string dbname)
        {
            _name = dbname;
            _tablesDict = new Dictionary<string, Table>();
        }
        public string Name => _name;

        public IReadOnlyDictionary<string, Table> TablesDict => _tablesDict;

        public void AddTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentException("Table name must be specified.", nameof(tableName));

            if (_tablesDict.ContainsKey(tableName))
                throw new InvalidOperationException($"Table with name '{tableName}' already exist!");

            this._tablesDict.Add(tableName, new Table(tableName));
        }

        public Table GetTable(string tableName)
        {
            if (!_tablesDict.TryGetValue(tableName, out Table table))
            {
                throw new InvalidOperationException($"Table with name '{tableName}' doesn't exist");
            }

            return table;
        }

        public void DeleteTable(string tableName)
        {
            if(!this._tablesDict.Remove(tableName))
                throw new InvalidOperationException($"Table with name '{tableName}' doesn't exist");
        }

        public List<string> GetTableNameList()
        {
            return this._tablesDict.Keys.ToList();
        }

        public bool TableExists(string tableName)
        {
            return this._tablesDict.ContainsKey(tableName);
        }

        public Table UnionTables(string tableName1, string tableName2, string unionTableName, bool distinct = false)
        {
            var table1 = this.GetTable(tableName1);
            var table2 = this.GetTable(tableName2);
            
            if (table1.Columns.Count != table2.Columns.Count)
                throw new InvalidOperationException("Tables have different number of columns.");

            var result = new Table(unionTableName);
            for (int i = 0; i < table1.Columns.Count; i++)
            {
                var c1 = table1.Columns[i];
                var c2 = table2.Columns[i];

                if (!string.Equals(c1.Name, c2.Name, StringComparison.Ordinal))
                    throw new InvalidOperationException($"Column name mismatch at position {i}: '{c1.Name}' vs '{c2.Name}'.");

                if (c1.Type != c2.Type)
                    throw new InvalidOperationException($"Column type mismatch for '{c1.Name}': '{c1.Type}' vs '{c2.Type}'.");

                result.AddColumn(c1.Name, c1.Type);
            }

            HashSet<string> seen = new HashSet<string>();
            void AddRowIfNeeded(Row src)
            {
                var key = src.ToString();
                if (!distinct || seen.Add(key))
                {
                    var copy = new Row();
                    copy.ValuesList.AddRange(src.ValuesList);
                    result.AddUncheckedRow(copy);
                }
            }

            foreach (var r in table1.Rows) AddRowIfNeeded(r);
            foreach (var r in table2.Rows) AddRowIfNeeded(r);

            return result;
        }
    }
}
