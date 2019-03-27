using FeedProcessor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedProcessor.Processor
{
    public class PersonProcessor:AbstractProcessor<Person>
    {
        public PersonProcessor()
        { }

        protected override IEnumerable<Person> PostProcessBatch(IEnumerable<Person> records)
        {
            Console.WriteLine("Post Process of batch completed");
            return records;
        }

        protected override IEnumerable<Person> PreProcesss(Person record)
        {
            List<Person> persons = new List<Person>();
            persons.Add(record);
            Console.WriteLine("Pre Process for single record completed");

            return persons;
        }

        protected override IEnumerable<Person> PreProcesssBatch(IEnumerable<Person> records)
        {
            Console.WriteLine("Pre Process Batch completed");

            return records;
        }

        protected override IEnumerable<Person> ProcessBatch(IEnumerable<Person> records)
        {
            Console.WriteLine("Process batch completed");

            return records;
        }
    }
}
