using System;

namespace Lab1IT
{
    class dbTypeTimeInvl : dbType
    {
        public override bool Validation(string value)
        {
            TimeSpan a;
            TimeSpan b;
            string[] buf = value.Split(' ');

            if (buf.Length != 2) return false;
            if (!TimeSpan.TryParse(buf[0], out a) || !TimeSpan.TryParse(buf[1], out b) || a >= b) return false;

            return true;
        }
    }
}
