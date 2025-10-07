using DbSystemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1IT
{
    internal class DatabaseViewModel
    {
        private readonly Database _db;
        private readonly Dictionary<string, Table> _temporary = new Dictionary<string, Table>(StringComparer.Ordinal);

        public Database Model => _db;
        public string Name => _db.Name;

        public DatabaseViewModel(Database db)
        {
            _db = db;
        }

        public List<string> GetTabNames()
        {
            var names = new List<string>();
            names.AddRange(_db.TablesDict.Keys.OrderBy(n => n, StringComparer.Ordinal));
            names.AddRange(_temporary.Keys.OrderBy(n => n, StringComparer.Ordinal));
            return names;
        }

        public int DatabaseTableCount => _db.TablesDict.Count;

        public bool IsTemporary(string tableName) => _temporary.ContainsKey(tableName);
        public bool IsHasColumns(string tableName) => _db.GetTable(tableName).Columns.Count > 0;
        public bool Exists(string tableName) => _db.TableExists(tableName) || _temporary.ContainsKey(tableName);

        public Table GetTable(string tableName)
        {
            if (_db.TableExists(tableName)) return _db.GetTable(tableName);
            if (_temporary.TryGetValue(tableName, out var t)) return t;
            throw new InvalidOperationException($"Table '{tableName}' not found.");
        }

        public void AddTableToDatabase(string name) => _db.AddTable(name);
        public void DeleteTable(string name)
        {
            if (_db.TableExists(name)) { 
                _db.DeleteTable(name); 
                return; 
            }
            _temporary.Remove(name);
        }

        public string UnionTables(string t1, string t2, bool distinct)
        {
            var baseName = $"{t1}_union_{t2}" + (distinct ? "_distinct" : "");
            var tempName = EnsureUniqueName(baseName);
            var union = _db.UnionTables(t1, t2, tempName, distinct);
            _temporary[tempName] = union;
            return tempName;
        }

        private string EnsureUniqueName(string baseName)
        {
            if (string.IsNullOrWhiteSpace(baseName)) baseName = "temp";
            var name = baseName;
            var index = 1;
            while (_db.TableExists(name) || _temporary.ContainsKey(name))
            {
                name = baseName + index; index++;
            }
            return name;
        }
    }
}
