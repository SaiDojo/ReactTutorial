using FeedProcessor.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FeedProcessor.Reader
{
    class XmlFileReader<T> : IFeedReader<T> where T: AbstractModel, new()
    {
        public string ElementName { get; set; }
        public XmlFileReader(string elementName)
        {
            this.ElementName = elementName;
        }

        public IEnumerable<T> Read(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return ReadStream(sr);
            }
        }

        public IEnumerable<T> ReadStream(StreamReader streamReader)
        {
            XmlReader xmlReader = XmlReader.Create(streamReader, null);
            return ReadUsingXmlReader(xmlReader, ElementName);
        }

        public IEnumerable<T> ReadUsingXmlReader(XmlReader reader, string elementName)
        {
            List<T> objects = new List<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            reader.MoveToContent(); // will not advance reader if already on a content node; if successful, ReadState is Interactive
            reader.Read();          // this is needed, even with MoveToContent and ReadState.Interactive
            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals(elementName))
                {
                    // this advances the reader...so it's either XNode.ReadFrom() or reader.Read(), but not both
                    var matchedElement = XNode.ReadFrom(reader) as XElement;
                    if (matchedElement != null)
                    {
                        StringReader stringReader = new StringReader(matchedElement.ToString());
                        objects.Add((T)xmlSerializer.Deserialize(stringReader));
                    }
                }
                else
                    reader.Read();
            }
            return objects;
        }

        //using blocking collection

        public bool Read(string filePath, BlockingCollection<T> outPutObjects)
        {
            using (var sr = new StreamReader(filePath))
            {
                return ReadStream(sr, outPutObjects);
            }
        }

        public bool ReadStream(StreamReader streamReader, BlockingCollection<T> outPutObjects)
        {
            XmlReader xmlReader = XmlReader.Create(streamReader, null);
            return ReadUsingXmlReader(xmlReader, ElementName, outPutObjects);
        }

        public bool ReadUsingXmlReader(XmlReader reader, string elementName, BlockingCollection<T> outPutObjects)
        {
            List<T> objects = new List<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            reader.MoveToContent(); // will not advance reader if already on a content node; if successful, ReadState is Interactive
            reader.Read();          // this is needed, even with MoveToContent and ReadState.Interactive
            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals(elementName))
                {
                    // this advances the reader...so it's either XNode.ReadFrom() or reader.Read(), but not both
                    var matchedElement = XNode.ReadFrom(reader) as XElement;
                    if (matchedElement != null)
                    {
                        StringReader stringReader = new StringReader(matchedElement.ToString());
                        objects.Add((T)xmlSerializer.Deserialize(stringReader));
                    }
                }
                else
                    reader.Read();
            }
            return true;
        }



    }
}