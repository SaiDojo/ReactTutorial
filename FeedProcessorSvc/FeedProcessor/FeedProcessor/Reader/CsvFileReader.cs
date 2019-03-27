using FeedProcessor.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Reader
{
    public class CsvFileReader<T> : IFeedReader<T> where T : AbstractModel, new()
    {
        public bool HasHeader { get; set; }
        public bool HasFooter { get; set; }

        public CsvFileReader(bool hasHeader, bool hasFooter)
        {
            this.HasHeader = hasHeader;
            this.HasFooter = hasFooter;
        }

        public IEnumerable<T> Read(string filePath) //bool hasHeaders
        {
            using (var sr = new StreamReader(filePath))
            {
                return ReadStream(sr);
            }
        }

        public IEnumerable<T> ReadStream(StreamReader streamReader)
        {
            List<T> objects = new List<T>();
            if (HasHeader)
                streamReader.ReadLine(); //read header
            string line;
            do
            {
                line = streamReader.ReadLine();
                if (!streamReader.EndOfStream)//EndOfStream will be true once the list line is read 
                {
                    var obj = new T();
                    var propertyValues = line.Split(',');
                    obj.AssignValuesToInstance(propertyValues);
                    objects.Add(obj);
                }
            } while (line != null);

            return objects;
        }

        public bool Read(string filepath, BlockingCollection<T> outPutObjects)
        {
            using (var sr = new StreamReader(filepath))
            {
                return ReadStream(sr, outPutObjects);
            }
        }

        public bool ReadStream(StreamReader streamReader, BlockingCollection<T> outPutObjects)
        {
            if (HasHeader)
                streamReader.ReadLine(); //read header
            string line;
            do
            {
                if (!streamReader.EndOfStream)
                    line = streamReader.ReadLine();
                else
                    line = null;


                //if (!streamReader.EndOfStream || (streamReader.EndOfStream && line != null))//EndOfStream will be true once the last line is read 
                if (line != null)//EndOfStream will be true once the last line is read 
                {
                    var obj = new T();
                    //Console.WriteLine(line);
                    var propertyValues = line.Split(',');
                    obj.AssignValuesToInstance(propertyValues);
                    outPutObjects.Add(obj);
                }
            } while (line != null);

            return true;
        }


    }
}
