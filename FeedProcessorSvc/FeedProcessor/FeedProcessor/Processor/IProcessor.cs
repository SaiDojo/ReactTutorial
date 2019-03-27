using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Processor
{
    interface IProcessor<T>
    {
        void Run(IEnumerable<T> records);
    }
}
