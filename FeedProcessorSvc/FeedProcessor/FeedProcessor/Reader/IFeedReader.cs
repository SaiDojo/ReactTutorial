using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Reader
{
    public interface IFeedReader<T>
    {
        IEnumerable<T> Read(string path);
        IEnumerable<T> ReadStream(StreamReader streamReader);
        bool Read(string path, BlockingCollection<T> outPutObjects);
        bool ReadStream(StreamReader streamReader, BlockingCollection<T> outPutObjects);
    }
}
