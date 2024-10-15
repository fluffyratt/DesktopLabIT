using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1IT
{
    class dbTypeDate : dbType
    {
        public override bool Validation(string value)
        {
            DateTime buf;
            if (DateTime.TryParse(value, out buf)) return true;
            return false;
        }
    }
}
