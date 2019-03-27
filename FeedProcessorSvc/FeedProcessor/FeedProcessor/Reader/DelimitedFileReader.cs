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
    public class DelimitedFileReader<T> : IFeedReader<T> where T : AbstractModel, new()
    {
        public bool HasHeader { get; set; }
        public bool HasFooter { get; set; }
        public char Delimiter { get; set; }

        public DelimitedFileReader(bool hasHeader, bool hasFooter, char delimiter)
        {
            this.HasHeader = hasHeader;
            this.HasFooter = hasFooter;
            this.Delimiter = delimiter;
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
                    var propertyValues = line.Split(Delimiter);
                    obj.AssignValuesToInstance(propertyValues);
                    objects.Add(obj);
                }
            } while (line != null);

            return objects;
        }


        public bool Read(string filePath, BlockingCollection<T> outPutObjects) //bool hasHeaders
        {
            using (var sr = new StreamReader(filePath))
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
                line = streamReader.ReadLine();
                if (!streamReader.EndOfStream)//EndOfStream will be true once the list line is read 
                {
                    var obj = new T();
                    var propertyValues = line.Split(Delimiter);
                    obj.AssignValuesToInstance(propertyValues);
                    outPutObjects.Add(obj);
                }
            } while (line != null);

            return true;
        }
    }
}
