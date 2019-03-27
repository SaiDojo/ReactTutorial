using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Writer
{
    public interface IFeedWriter<T>
    {
        void Write(IEnumerable<T> records);
    }
}
