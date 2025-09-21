using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab1IT
{
    class dbManager
    {
        Database db;

        public bool CreateDB(string dbname)
        {
            if (dbname.Equals("")) return false;

            db = new Database(dbname);
            return true;
        }

        public bool AddTable(string tname)
        {
            if (tname.Equals("")) return false;
            if (db == null) return false;

            db.dbTablesDict.Add(tname, new Table(tname));
            return true;
        }

        public Table GetTable(string tableName)
        {
            if (db.dbTablesDict == null || db.dbTablesDict.Count == 0)
            {
                throw new InvalidOperationException("The list of tables is empty.");
            }

            if (!db.dbTablesDict.TryGetValue(tableName, out Table table))
            {
                throw new ArgumentException(nameof(tableName), "Index is out of range. Ensure the index is within the bounds of the table list.");
            }

            return table;
        }

        public bool AddColumn(string tableName, string cname, string ctype)
        {
            if (db == null || IsDBHasTable(tableName)) return false;
            if (db.dbTablesDict.Count <= 0) return false;

            db.dbTablesDict[tableName].tColumnsList.Add(new Column(cname, ctype));
            for (int i = 0; i < db.dbTablesDict[tableName].tRowsList.Count; ++i)
            {
                db.dbTablesDict[tableName].tRowsList[i].rValuesList.Add("");
            }
            return true;
        }

        public bool AddRow(string tableName)
        {
            if (db == null || IsDBHasTable(tableName)) return false;
            if (db.dbTablesDict.Count <= 0) return false;
            if (db.dbTablesDict[tableName].tColumnsList.Count <= 0) return false;

            db.dbTablesDict[tableName].tRowsList.Add(new Row());
            for (int i = 0; i < db.dbTablesDict[tableName].tColumnsList.Count; ++i)
            {
                db.dbTablesDict[tableName].tRowsList.Last().rValuesList.Add("");
            }
            return true;
        }

        public bool ChangeValue(string newValue, string tableName, int cind, int rind)
        {
            if (db.dbTablesDict[tableName].tColumnsList[cind].cType.Validation(newValue))
            {
                db.dbTablesDict[tableName].tRowsList[rind].rValuesList[cind] = newValue;
                return true;
            }
            MessageBox.Show("Wrong input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        public void DeleteRow(string tableName, int rind)
        {
            db.dbTablesDict[tableName].tRowsList.RemoveAt(rind);
        }

        public void DeleteColumn(string tableName, int cind)
        {
            db.dbTablesDict[tableName].tColumnsList.RemoveAt(cind);
            for (int i = 0; i < db.dbTablesDict[tableName].tRowsList.Count; ++i)
            {
                db.dbTablesDict[tableName].tRowsList[i].rValuesList.RemoveAt(cind);
            }
        }

        public void DeleteTable(string tableName)
        {
            db.dbTablesDict.Remove(tableName);
        }

        char sep = '$';
        char space = '#';
        public void SaveDB(string path)
        {
            StreamWriter sw = new StreamWriter(path);

            sw.WriteLine(db.dbName);
            var tablesList = db.dbTablesDict.Values.ToList();
            foreach (Table t in tablesList)
            {
                sw.WriteLine(sep);
                sw.WriteLine(t.tName);
                foreach (Column c in t.tColumnsList)
                {
                    sw.Write(c.cName + space);
                }
                sw.WriteLine();
                foreach (Column c in t.tColumnsList)
                {
                    sw.Write(c.typeName + space);
                }
                sw.WriteLine();
                foreach (Row r in t.tRowsList)
                {
                    foreach (string s in r.rValuesList)
                    {
                        sw.Write(s + space);
                    }
                    sw.WriteLine();
                }
            }

            sw.Close();
        }

        public void OpenDB(string path)
        {
            StreamReader sr = new StreamReader(path);
            string file = sr.ReadToEnd();
            string[] parts = file.Split(sep);

            db = new Database(parts[0]);

            for (int i = 1; i < parts.Length; ++i)
            {
                parts[i] = parts[i].Replace("\r\n", "\r");
                List<string> buf = parts[i].Split('\r').ToList();
                buf.RemoveAt(0);
                buf.RemoveAt(buf.Count - 1);
                string tableName = null;
                if (buf.Count > 0)
                {
                    tableName = buf[0];
                    db.dbTablesDict.Add(tableName, new Table(tableName));
                }
                if (buf.Count > 2)
                {
                    string[] cname = buf[1].Split(space);
                    string[] ctype = buf[2].Split(space);
                    int length = cname.Length - 1;
                    for (int j = 0; j < length; ++j)
                    {
                        db.dbTablesDict[tableName].tColumnsList.Add(new Column(cname[j], ctype[j]));
                    }

                    for (int j = 3; j < buf.Count; ++j)
                    {
                        db.dbTablesDict[tableName].tRowsList.Add(new Row());
                        List<string> values = buf[j].Split(space).ToList();
                        values.RemoveAt(values.Count - 1);
                        db.dbTablesDict[tableName].tRowsList.Last().rValuesList.AddRange(values);
                    }
                }
            }

            sr.Close();
        }

        public List<string> GetTableNameList()
        {
            return db.dbTablesDict.Keys.ToList();
        }

        public bool IsDBHasTable(string tableName)
        {
            return db.dbTablesDict.ContainsKey(tableName);
        }

        public Table UnionTables(string tableName1, string tableName2, bool distinct = false)
        {
            if (!db.dbTablesDict.TryGetValue(tableName1, out var table1) ||
                !db.dbTablesDict.TryGetValue(tableName2, out var table2))
            {
                throw new ArgumentException("One or both tables not found.");
            }

            if (table1.tColumnsList.Count != table2.tColumnsList.Count)
                throw new ArgumentException("Tables have different number of columns.");

            for (int i = 0; i < table1.tColumnsList.Count; i++)
            {
                var c1 = table1.tColumnsList[i];
                var c2 = table2.tColumnsList[i];

                if (!string.Equals(c1.cName, c2.cName, StringComparison.Ordinal))
                    throw new ArgumentException($"Column name mismatch at position {i}: '{c1.cName}' vs '{c2.cName}'.");

                if (!string.Equals(c1.typeName, c2.typeName, StringComparison.Ordinal))
                    throw new ArgumentException($"Column type mismatch for '{c1.cName}': '{c1.typeName}' vs '{c2.typeName}'.");
            }

            var distinctText = distinct ? "_distinct" : "";
            var result = new Table($"{table1.tName}_{table2.tName}_Union{distinctText}");
            result.tColumnsList.AddRange(table1.tColumnsList);

            HashSet<string> seen = new HashSet<string>();
            void AddRowIfNeeded(Row src)
            {
                if (src.rValuesList.Count != result.tColumnsList.Count)
                    throw new ArgumentException("Row has different number of values than columns.");

                var key = GetRowKey(src);
                if (!distinct || seen.Add(key))
                {
                    var copy = new Row();
                    copy.rValuesList.AddRange(src.rValuesList);
                    result.tRowsList.Add(copy);
                }
            }

            foreach (var r in table1.tRowsList) AddRowIfNeeded(r);
            foreach (var r in table2.tRowsList) AddRowIfNeeded(r);

            return result;
        }

        private string GetRowKey(Row r)
        {
            return string.Join("\u001F", r.rValuesList.Select(v => v ?? string.Empty));
        }
    }
}
