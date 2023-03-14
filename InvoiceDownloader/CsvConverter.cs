using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace InvoiceDownloader
{
    public class NullStringConverter : StringConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            else
            {
                return base.ConvertFromString(text, row, memberMapData)!;
            }
        }
    }

    public class CustomBooleanConverter : BooleanConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (bool.TryParse(text, out var result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

    }
    public class CustomDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (DateTime.TryParse(text, out var result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
