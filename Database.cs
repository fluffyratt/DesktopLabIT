using System.Collections.Generic;

namespace Lab1IT
{
    class Database
    {
        public string dbName;
        public List<Table> dbTablesList;

        public Database(string dbname)
        {
            dbName = dbname;
            dbTablesList = new List<Table>();
        }
    }
}
