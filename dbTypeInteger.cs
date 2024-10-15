namespace Lab1IT
{
    class dbTypeInteger : dbType
    {
        public override bool Validation(string value)
        {
            int buf;
            if (int.TryParse(value, out buf)) return true;
            return false;
        }
    }
}
