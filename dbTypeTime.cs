using System;

namespace Lab1IT
{
    class dbTypeTime : dbType
    {
        public override bool Validation(string value)
        {
            TimeSpan buf;
            if (TimeSpan.TryParse(value, out buf)) return true;
            return false;
        }
    }
}
