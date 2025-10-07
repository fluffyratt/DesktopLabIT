using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DbSystemLibrary.Dtos
{
    internal class ColumnDto
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("type")]
        public DbTypeEnum Type { get; }

        public ColumnDto(string name, DbTypeEnum type)
        {
            Name = name;
            Type = type;
        }
    }
}
