using DbSystemLibrary.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DbSystemLibrary
{
    public static class DbManager
    {
        public const string DbFormat = "json";

        public static Database CreateDB(string dbname)
        {
            if (string.IsNullOrEmpty(dbname)) return null;

            var db = new Database(dbname);
            return db;
        }

        public static string Serialize(Database db)
        {
            var dto = new DatabaseDto(
                format: "TinyDB",
                version: 1,
                dbName: db.Name,
                tables: db.TablesDict.Values.Select(t =>
                    new TableDto(
                        t.Name,
                        t.Columns.Select(c => new ColumnDto(c.Name, c.Type)).ToList(),
                        t.Rows.Select(r => new RowDto(r.ValuesList)).ToList()
                    )
                ).ToList()
            );

            var opts = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            return JsonSerializer.Serialize(dto, opts);
        }

        public static Database Deserialize(string json)
        {
            var opts = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
            var dto = JsonSerializer.Deserialize<DatabaseDto>(json, opts);
            if (dto == null)
                throw new InvalidDataException("Invalid JSON DB file");

            if (dto.Format != "TinyDB")
                throw new NotSupportedException("Unknown DB format");
            if (dto.Version != 1)
                throw new NotSupportedException($"Unsupported version {dto.Version}");

            var db = new Database(dto.DbName);

            foreach (var t in dto.Tables)
            {
                db.AddTable(t.Name);
                var table = db.GetTable(t.Name);

                foreach (var c in t.Columns)
                    table.AddColumn(c.Name, c.Type);

                foreach (var r in t.Rows)
                    table.AddRow(new Row(r.Values));
            }

            return db;
        }
    }
}
