using System;
using System.Globalization;

namespace DbSystemLibrary
{
    public class Column
    {
        string _name;
        DbTypeEnum _type;

        public Column(string name, DbTypeEnum type)
        {
            _name = name;
            _type = type;
        }

        public string Name => _name;

        public DbTypeEnum Type => _type;

        public bool IsValid(string value)
        {
            switch (_type)
            {
                case DbTypeEnum.Char:
                    return char.TryParse(value, out _);
                case DbTypeEnum.Integer:
                    return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out _);
                case DbTypeEnum.Real:
                    return double.TryParse(value, out _);
                case DbTypeEnum.String:
                    return true;
                case DbTypeEnum.Time:
                    return TimeSpan.TryParse(value, out _);
                case DbTypeEnum.TimeInvl:
                {
                    var parts = value.Split(new[] { ' ', '\t', '-', '–', '—' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2) return false;

                    if (!TimeSpan.TryParse(parts[0], CultureInfo.InvariantCulture, out var a)) return false;
                    if (!TimeSpan.TryParse(parts[1], CultureInfo.InvariantCulture, out var b)) return false;

                    return a < b;
                }
                default:
                    return false;
            }
        }
    }
}
