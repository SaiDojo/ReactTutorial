using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Processor
{
    public abstract class AbstractProcessor<T>
    {
        protected abstract IEnumerable<T> PreProcesss(T record);
        protected abstract IEnumerable<T> PreProcesssBatch(IEnumerable<T> records);
        protected abstract IEnumerable<T> ProcessBatch(IEnumerable<T> records);
        protected abstract IEnumerable<T> PostProcessBatch(IEnumerable<T> records);

        public IEnumerable<T> Run(T record)
        {
            IEnumerable<T> preRecords = PreProcesss(record);
            var proRecords = ProcessBatch(preRecords);
            Console.WriteLine("Single Object Process Finished ");

            return PostProcessBatch(proRecords);
        }

        //Batch processing

        public IEnumerable<T> Run(IEnumerable<T> records)
        {
            Console.WriteLine("Batch Process Started ");

            //List<T> outRecords = new List<T>();
            //foreach (var record in records)
            //{
            //    outRecords.AddRange(Run(record));
            //}

            IEnumerable<T> preRecords = PreProcesssBatch(records);
            var proRecords = ProcessBatch(preRecords);
            Console.WriteLine("Batch Process Finished ");
            return PostProcessBatch(proRecords);
        }


    }
}
