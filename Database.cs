using System.Collections.Generic;

namespace Lab1IT
{
    class Database
    {
        public string dbName;
        public Dictionary<string, Table> dbTablesDict;

        public Database(string dbname)
        {
            dbName = dbname;
            dbTablesDict = new Dictionary<string, Table>();
        }
    }
}
