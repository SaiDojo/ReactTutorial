using FeedProcessor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FeedProcessor.Writer
{
    class XmlFileWriter<T> : IFeedWriter<T>
    {
        public string DestinationFilePath { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
        public int rows;

        public XmlFileWriter(string destinationFilePath, string header, string footer)
        {
            this.DestinationFilePath = destinationFilePath;
            this.HeaderText = header;
            this.FooterText = footer;
        }

        public void Write(IEnumerable<T> records)
        {
            Console.WriteLine("Creating xml file..");

            using (var sw = new StreamWriter(DestinationFilePath))
            {
                foreach (var record in records)
                {
                    //sw.Write(CreateXmlData(record, sw));
                    if (HeaderText != null)
                        sw.WriteLine("<Header>" + HeaderText + "</Header>");
                    sw.WriteLine(CreateXmlData(record, sw));
                    if (FooterText != null)
                        sw.WriteLine("<Footer>" + FooterText + "</Footer>");
                    rows++;
                }
            }
        }

        public string CreateXmlData(T record, StreamWriter sw)
        {
            string output = "";

            StringWriter stringWriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                xs.Serialize(writer, record);
                output = stringWriter.ToString();
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
