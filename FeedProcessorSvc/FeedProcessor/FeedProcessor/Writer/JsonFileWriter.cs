using FeedProcessor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FeedProcessor.Writer
{
    public class JsonFileWriter<T> : IFeedWriter<T>
    {
        public string DestinationFilePath { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
        public int rows;

        public JsonFileWriter(string destinationFilePath)
        {
            this.DestinationFilePath = destinationFilePath;
        }

        public void Write(IEnumerable<T> records)
        {
            //Console.WriteLine(CreateJsonData(record));
            using (var sw = new StreamWriter(DestinationFilePath))
            {
                foreach (var record in records)
                {
                    sw.WriteLine(CreateJsonData(record));
                    rows++;
                }
            }
        }

        public string CreateJsonData(T record)
        {
            string output = "";
            output = JsonConvert.SerializeObject(record);
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