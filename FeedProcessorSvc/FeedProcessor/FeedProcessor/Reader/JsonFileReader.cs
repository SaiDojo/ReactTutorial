using FeedProcessor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Reader
{
    public class JsonFileReader<T> : IFeedReader<T> where T : AbstractModel, new()
    {
        public bool FromFile { get; set; }

        public JsonFileReader(bool readFromFile)
        {
            this.FromFile = readFromFile;
        }
                
        public IEnumerable<T> Read(string fileOrUrlPath)
        {
            if (FromFile == true)
            {
                return ReadFromFile(fileOrUrlPath);
            }
            else
            {
                return ReadFromUrl(fileOrUrlPath);
            }
        }

        public IEnumerable<T> ReadFromFile(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return ReadStream(sr);
            }
        }

        public IEnumerable<T> ReadFromUrl(string url)
        {
            List<T> objects = new List<T>();
            using (WebClient client = new WebClient())
            using (Stream stream = client.OpenRead(url))
            using (StreamReader streamReader = new StreamReader(stream))
            {
                return ReadStream(streamReader);
            }
        }

        public IEnumerable<T> ReadStream(StreamReader streamReader)
        {
            List<T> objects = new List<T>();
                using (JsonTextReader reader = new JsonTextReader(streamReader))
                {
                    reader.SupportMultipleContent = true;

                    var serializer = new JsonSerializer();
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            T obj = serializer.Deserialize<T>(reader);
                            objects.Add(obj);
                        }
                    }
                }
            return objects;
        }


        //Using blocking collection


        public bool Read(string fileOrUrlPath, BlockingCollection<T> outPutObjects)
        {
            if (FromFile == true)
            {
                return ReadFromFile(fileOrUrlPath, outPutObjects);
            }
            else
            {
                return ReadFromUrl(fileOrUrlPath, outPutObjects);
            }
        }

        public bool ReadFromFile(string filePath, BlockingCollection<T> outPutObjects)
        {
            using (var sr = new StreamReader(filePath))
            {
                return ReadStream(sr, outPutObjects);
            }
        }

        public bool ReadFromUrl(string url, BlockingCollection<T> outPutObjects)
        {
            using (WebClient client = new WebClient())
            using (Stream stream = client.OpenRead(url))
            using (StreamReader streamReader = new StreamReader(stream))
            {
                return ReadStream(streamReader, outPutObjects);
            }
        }

        public bool ReadStream(StreamReader streamReader, BlockingCollection<T> outPutObjects)
        {
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;

                var serializer = new JsonSerializer();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        T obj = serializer.Deserialize<T>(reader);
                        outPutObjects.Add(obj);
                    }
                }
            }
            return true;
        }
    }
    
}
