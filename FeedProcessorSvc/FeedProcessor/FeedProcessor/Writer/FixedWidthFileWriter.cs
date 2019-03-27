using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Writer
{
    public class FixedWidthFileWriter<T> : IFeedWriter<T>
    {
        public string DestinationFilePath { get; set; }
        public IDictionary<string,int> FieldWidths { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
        public int rows;

        public FixedWidthFileWriter(string destinationFilePath, IDictionary<string, int> fieldWidths, string header, string footer)
        {
            this.DestinationFilePath = destinationFilePath;
            this.FieldWidths = fieldWidths;
            this.HeaderText = header;
            this.FooterText = footer;
        }

        public void Write(IEnumerable<T> records)
        {
            //Console.WriteLine(CreateFixedWidthData(records));
            using (var sw = new StreamWriter(DestinationFilePath))
            {
                foreach (var record in records)
                {
                    //sw.Write(CreateXmlData(record, sw));
                    if (String.IsNullOrEmpty(HeaderText) != false)
                        sw.WriteLine(HeaderText);
                    sw.WriteLine(CreateFixedWidthData(record));
                    if (String.IsNullOrEmpty(HeaderText) != false)
                            sw.WriteLine(FooterText);

                    rows++;
                }
            }

        }

        public string CreateFixedWidthData(T record)
        {
            string output = "";
            var properties = record.GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                if (FieldWidths.Keys.Contains(properties[i].Name))
                    output += PreProcess(properties[i].GetValue(record).ToString()).PadRight(FieldWidths[properties[i].Name], ' ');
                else
                    output += PreProcess(properties[i].GetValue(record).ToString());
            }
            return output;
        }
       
        private string PreProcess(string input)
        {
            input = input.Replace('ı', 'i')
                .Replace('ç', 'c')
                .Replace('ö', 'o')
                .Replace('ş', 's')
                .Replace('ü', 'u')
                .Replace('ğ', 'g')
                .Replace('İ', 'I')
                .Replace('Ç', 'C')
                .Replace('Ö', 'O')
                .Replace('Ş', 'S')
                .Replace('Ü', 'U')
                .Replace('Ğ', 'G')
                .Replace("\"", "\"\"")
                .Trim();
            if (input.Contains(","))
            {
                input = "\"" + input + "\"";
            }
            return input;
        }
    }
}
