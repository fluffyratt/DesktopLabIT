using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1IT
{
    class dbTypeDateInvl : dbType
    {
        public override bool Validation(string value)
        {
            DateTime a;
            DateTime b;
            string[] buf = value.Split(' ');

            if (buf.Length != 2) return false;
            if (!DateTime.TryParse(buf[0], out a) || !DateTime.TryParse(buf[1], out b) || a >= b) return false;
            return true;
        }
    }
}
