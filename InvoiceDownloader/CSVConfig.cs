using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDownloader
{
    public class CSVConfig
    {
        public static async Task<List<T>> CsvToList<T>(Stream file)
        {
            string csvContent;

            using (StreamReader streamReader = new(file!))
            {
                csvContent = await streamReader.ReadToEndAsync();
            }

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                HasHeaderRecord = true,
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null,
                MissingFieldFound = null,
            };

            List<T> csvList;

            using TextReader fileReader = new StringReader(csvContent);
            var csv = new CsvReader(fileReader, config);
            csv.Context.TypeConverterCache.AddConverter<string>(new NullStringConverter());
            csvList = csv.GetRecords<T>()
                .ToList();

            return csvList;
        }
    }
}
