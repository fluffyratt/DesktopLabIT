using System.Collections.Generic;

namespace Lab1IT
{
    class myType
    {
        private List<string> EnableTypes = new List<string> { "Integer", "Real", "Char", "String", "Date", "DateInvl" };
        string curType;

        public myType(string type)
        {
            if (EnableTypes.Contains(type))
            {
                curType = type;
            }
            else
            {
                curType = EnableTypes[3];
            }
        }

        public bool Validation(string value)
        {
            switch (curType)
            {
                case "dg":
                    break;
            }

            return true;
        }
    }
}
