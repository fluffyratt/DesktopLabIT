namespace Lab1IT
{
    class Column
    {
        public string cName;
        public dbType cType;
        public string typeName;

        public Column(string cname, string ctype)
        {
            cName = cname;
            typeName = ctype;

            switch (ctype)
            {
                case "Integer":
                    cType = new dbTypeInteger();
                    break;
                case "Real":
                    cType = new dbTypeReal();
                    break;
                case "Char":
                    cType = new dbTypeChar();
                    break;
                case "String":
                    cType = new dbTypeString();
                    break;
                case "Time":
                    cType = new dbTypeTime();
                    break;
                case "TimeInvl":
                    cType = new dbTypeTimeInvl();
                    break;
                default:
                    cType = new dbTypeString();
                    break;
            }
        }
    }
}
