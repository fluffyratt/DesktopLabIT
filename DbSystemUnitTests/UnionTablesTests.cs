using DbSystemLibrary;
using System;
using System.Reflection;

namespace DbSystemUnitTests
{
    public class UnionTablesTests
    {
        private static void AddSchema(Table t, params (string name, DbTypeEnum type)[] cols)
        {
            foreach (var (name, type) in cols)
                t.AddColumn(name, type);
        }

        private static void AddRow(Table t, params string[] values)
        {
            t.AddRow(new Row(values.ToList()));
        }

        private static (Database db, Table t1, Table t2) MakeTwoTables(
            (string name, DbTypeEnum type)[] cols,
            (string[] rows1, string[] rows2)[] dataPairs,
            string name1 = "T1", string name2 = "T2")
        {
            var db = new Database("TestDB");
            db.AddTable(name1);
            db.AddTable(name2);

            var t1 = db.GetTable(name1);
            var t2 = db.GetTable(name2);

            AddSchema(t1, cols);
            AddSchema(t2, cols);

            foreach (var (r1, r2) in dataPairs)
            {
                if (r1 != null) AddRow(t1, r1);
                if (r2 != null) AddRow(t2, r2);
            }

            return (db, t1, t2);
        }

        [Fact]
        public void Union_SameSchema_NoDistinct_MergesAllRows_PreservesOrder_AndName()
        {
            var cols = new[] { ("Id", DbTypeEnum.Integer), ("Name", DbTypeEnum.String) };
            var pairs = new[]
            {
                (new[] {"1","Ann"},  (string[])null),
                (new[] {"2","Bob"},  new[] {"3","Carl"}),
                ((string[])null,     new[] {"4","Dana"}),
            };

            var (db, t1, t2) = MakeTwoTables(cols, pairs);
            var result = db.UnionTables("T1", "T2", "Union", false);

            Assert.Equal("Union", result.Name);
            Assert.Equal(2, result.Columns.Count);
            Assert.Equal("Id", result.Columns[0].Name);
            Assert.Equal(DbTypeEnum.Integer, result.Columns[0].Type);
            Assert.Equal("Name", result.Columns[1].Name);
            Assert.Equal(DbTypeEnum.String, result.Columns[1].Type);

            Assert.Equal(4, result.Rows.Count);
            Assert.Equal(new[] { "1", "Ann" }, result.Rows[0].ValuesList);
            Assert.Equal(new[] { "2", "Bob" }, result.Rows[1].ValuesList);
            Assert.Equal(new[] { "3", "Carl" }, result.Rows[2].ValuesList);
            Assert.Equal(new[] { "4", "Dana" }, result.Rows[3].ValuesList);
        }

        [Fact]
        public void Union_SameSchema_Distinct_RemovesDuplicates()
        {
            var cols = new[] { ("A", DbTypeEnum.String), ("B", DbTypeEnum.String) };
            var pairs = new[]
            {
                (new[] {"x","1"}, new[] {"x","1"}),
                (new[] {"y","2"}, new[] {"z","3"})
            };

            var (db, _, _) = MakeTwoTables(cols, pairs);
            var result = db.UnionTables("T1", "T2", "U", true);

            Assert.Equal(3, result.Rows.Count);
            Assert.Contains(result.Rows, r => r.ValuesList.SequenceEqual(new[] { "x", "1" }));
            Assert.Contains(result.Rows, r => r.ValuesList.SequenceEqual(new[] { "y", "2" }));
            Assert.Contains(result.Rows, r => r.ValuesList.SequenceEqual(new[] { "z", "3" }));
        }

        [Fact]
        public void Union_Throws_When_ColumnCountDiffers()
        {
            var db = new Database("DB");
            db.AddTable("A");
            db.AddTable("B");

            var A = db.GetTable("A");
            var B = db.GetTable("B");

            A.AddColumn("Id", DbTypeEnum.Integer);

            B.AddColumn("Id", DbTypeEnum.Integer);
            B.AddColumn("Name", DbTypeEnum.String);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                db.UnionTables("A", "B", "U"));

            Assert.Contains("different number of columns", ex.Message);
        }

        [Fact]
        public void Union_Throws_When_ColumnNameMismatch()
        {
            var db = new Database("DB");
            db.AddTable("A");
            db.AddTable("B");

            var A = db.GetTable("A");
            var B = db.GetTable("B");

            A.AddColumn("Id", DbTypeEnum.Integer);
            B.AddColumn("ID", DbTypeEnum.Integer);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                db.UnionTables("A", "B", "U"));

            Assert.Contains("Column name mismatch", ex.Message);
        }

        [Fact]
        public void Union_Throws_When_ColumnTypeMismatch()
        {
            var db = new Database("DB");
            db.AddTable("A");
            db.AddTable("B");

            var A = db.GetTable("A");
            var B = db.GetTable("B");

            A.AddColumn("Id", DbTypeEnum.Integer);
            B.AddColumn("Id", DbTypeEnum.String);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                db.UnionTables("A", "B", "U"));

            Assert.Contains("Column type mismatch", ex.Message);
        }
    }
}
