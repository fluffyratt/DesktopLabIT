using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbSystemLibrary.Dtos
{
    internal class TableDto
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("columns")]
        public List<ColumnDto> Columns { get; }

        [JsonPropertyName("rows")]
        public List<RowDto> Rows { get; }

        public TableDto(string name, List<ColumnDto> columns, List<RowDto> rows)
        {
            Name = name;
            Columns = columns;
            Rows = rows;
        }
    }
}
