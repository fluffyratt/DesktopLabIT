using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbSystemLibrary.Dtos
{
    internal class DatabaseDto
    {
        [JsonPropertyName("format")]
        public string Format { get; }

        [JsonPropertyName("version")]
        public int Version { get; }

        [JsonPropertyName("dbName")]
        public string DbName { get; }

        [JsonPropertyName("tables")]
        public List<TableDto> Tables { get; }

        public DatabaseDto(string format, int version, string dbName, List<TableDto> tables)
        {
            Format = format;
            Version = version;
            DbName = dbName;
            Tables = tables;
        }
    }
}
