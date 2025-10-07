using System.Collections.Generic;
using System.Linq;

namespace DbSystemLibrary
{
    public class Row
    {
        private List<string> _valuesList;

        public Row(List<string> valuesList = null)
        {
            _valuesList = valuesList ?? new List<string>();
        }

        public List<string> ValuesList => _valuesList;


        public override string ToString()
        {
            return string.Join("\u001F", this.ValuesList.Select(v => v ?? string.Empty));
        }
    }
}
