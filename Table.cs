using System.Collections.Generic;

namespace Lab1IT
{
    class Table
    {
        public string tName;
        public List<Column> tColumnsList;
        public List<Row> tRowsList;

        public Table(string tname)
        {
            tName = tname;
            tColumnsList = new List<Column>();
            tRowsList = new List<Row>();
        }
    }
}
