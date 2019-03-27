using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Writer
{
    public class DelimitedFileWriter<T> : IFeedWriter<T>
    {
        public string DestinationFilePath { get; set; }
        public string Delimeter { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }

        public DelimitedFileWriter(string destinationFilePath, string delimeter)
        {
            this.DestinationFilePath = destinationFilePath;
            this.Delimeter = delimeter;
        }
        public DelimitedFileWriter(string destinationFilePath, string delimeter, string headerText, string footerText)
        {
            this.DestinationFilePath = destinationFilePath;
            this.Delimeter = delimeter;
            this.HeaderText = headerText;
            this.FooterText = footerText;
        }
        public void Write(IEnumerable<T> records)
        {
            //Console.WriteLine(CreateDelimitedData(records));
            Console.WriteLine("Creating delimited file..");
            using (var sw = new StreamWriter(DestinationFilePath))
            {
                if (HeaderText != null)
                    sw.WriteLine(HeaderText);
                foreach (var record in records)
                {
                    sw.WriteLine(CreateDelimitedData(record));
                }
                //string data = CreateDelimitedData(records);
                if (FooterText != null)
                    sw.WriteLine(FooterText);
            }
        }

        public string CreateDelimitedData(T record)
        {
            StringBuilder output = new StringBuilder("");
            var properties = record.GetType().GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                output.Append(PreProcess(properties[i].GetValue(record).ToString()));
                if (i != properties.Length - 1)
                {
                    output.Append(Delimeter);
                }
            }
            return output.ToString();
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
