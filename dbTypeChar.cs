namespace Lab1IT
{
    class dbTypeChar : dbType
    {
        public override bool Validation(string value)
        {
            char buf;
            if (char.TryParse(value, out buf)) return true;
            return false;
        }
    }
}
