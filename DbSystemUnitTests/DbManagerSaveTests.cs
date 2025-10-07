using DbSystemLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbSystemUnitTests
{
    public class DbManagerSaveTests
    {
        private static void SetupSampleDb(Database db)
        {
            db.AddTable("Products");
            var t1 = db.GetTable("Products");
            t1.AddColumn("Id", DbTypeEnum.Integer);
            t1.AddColumn("Name", DbTypeEnum.String);
            t1.AddColumn("Price", DbTypeEnum.Real);
            t1.AddRow(new Row(new() { "1", "Bread", "23,5" }));
            t1.AddRow(new Row(new() { "2", "Milk", "31,2" }));

            db.AddTable("Sales");
            var t2 = db.GetTable("Sales");
            t2.AddColumn("ProductId", DbTypeEnum.Integer);
            t2.AddColumn("Qty", DbTypeEnum.Integer);
            t2.AddRow(new Row(new() { "1", "3" }));
            t2.AddRow(new Row(new() { "2", "1" }));
        }

        [Fact]
        public void SaveDB_ThenLoadDB_Restores_Exact_Schema_And_Data()
        {
            var original = DbManager.CreateDB("TestDb");
            SetupSampleDb(original);

            var jsonDb = DbManager.Serialize(original);

            var restored = DbManager.Deserialize(jsonDb);
            var originalTables = new[] { "Products", "Sales" };
            foreach (var tableName in originalTables)
            {
                var tOrig = original.GetTable(tableName);
                var tRest = restored.GetTable(tableName);

                Assert.Equal(tOrig.Columns.Count, tRest.Columns.Count);
                for (int i = 0; i < tOrig.Columns.Count; i++)
                {
                    Assert.Equal(tOrig.Columns[i].Name, tRest.Columns[i].Name);
                    Assert.Equal(tOrig.Columns[i].Type, tRest.Columns[i].Type);
                }

                Assert.Equal(tOrig.Rows.Count, tRest.Rows.Count);
                for (int r = 0; r < tOrig.Rows.Count; r++)
                {
                    var origVals = tOrig.Rows[r].ValuesList;
                    var restVals = tRest.Rows[r].ValuesList;
                    Assert.Equal(origVals.Count, restVals.Count);
                    Assert.True(origVals.SequenceEqual(restVals), $"Row {r} in '{tableName}' differs: [{string.Join(",", origVals)}] vs [{string.Join(",", restVals)}]");
                }
            }
        }
    }
}
