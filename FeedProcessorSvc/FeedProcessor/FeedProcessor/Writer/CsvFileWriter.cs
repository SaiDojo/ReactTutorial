using FeedProcessor.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Writer
{
    public class CsvFileWriter<T> : IFeedWriter<T>
    {
        public string DestinationFilePath { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
        public int rows;

        public CsvFileWriter(string destinationFilePath)
        {
            this.DestinationFilePath = destinationFilePath;
        }
        public CsvFileWriter(string destinationFilePath, string headerText, string footerText)
        {
            this.DestinationFilePath = destinationFilePath;
            this.HeaderText = headerText;
            this.FooterText = footerText;
        }

        public void Write(IEnumerable<T> records)
        {
            //Console.WriteLine(CreateCsvData(records));
            using (var sw = new StreamWriter(DestinationFilePath))
            {
                Console.WriteLine("Creating csv file..");

                foreach (var record in records)
                {
                    if (HeaderText != null)
                        sw.WriteLine(HeaderText);
                    string data = CreateCsvData(record);
                    sw.WriteLine(data);
                    if (FooterText != null)
                        sw.WriteLine(FooterText);
                    rows++;
                }
            }
        }

        public void Write(BlockingCollection<T> records)
        {
            //Console.WriteLine(CreateCsvData(records));
            using (var sw = new StreamWriter(DestinationFilePath))
            {
                Console.WriteLine("Creating csv file..");

                foreach (var record in records)
                {
                    if (HeaderText != null)
                        sw.WriteLine(HeaderText);
                    string data = CreateCsvData(record);
                    sw.WriteLine(data);
                    if (FooterText != null)
                        sw.WriteLine(FooterText);
                    rows++;
                }
            }
        }


        public string CreateCsvData(T record)
        {
            StringBuilder output = new StringBuilder("");

            var properties = record.GetType().GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                output.Append(PreProcess(properties[i].GetValue(record).ToString()));
                if (i != properties.Length - 1)
                {
                    output.Append(",");
                }
            }
            return output.ToString();
        }

        /*
        public string CreateCsvData()
        {
            Console.WriteLine("Creating csv file..");
            string output = "";

            foreach (var record in records)
            {
                var properties = record.GetType().GetProperties();

                for (var i = 0; i < properties.Length; i++)
                {
                    output += PreProcess(properties[i].GetValue(record).ToString());
                    if (i != properties.Length - 1)
                    {
                        output += ",";
                    }
                }
            }
            Console.WriteLine("Writing csv file..");

            return output;
        }
        */

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