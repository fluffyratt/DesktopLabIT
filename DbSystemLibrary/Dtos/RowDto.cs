using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbSystemLibrary.Dtos
{
    internal class RowDto
    {
        [JsonPropertyName("values")]
        public List<string> Values{ get; }

        public RowDto(List<string> values)
        {
            Values = values;
        }
    }
}
