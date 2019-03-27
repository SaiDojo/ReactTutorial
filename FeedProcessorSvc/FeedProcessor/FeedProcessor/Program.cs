using FeedProcessor.Models;
using FeedProcessor.Writer;
using FeedProcessor.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using FeedProcessor.Processor;

namespace FeedProcessor
{
    class Program
    {
        static void Main(string[] args)
        {

            //Instantiate Feedprocessor(FeedName, IFeedReader, IFeedWriter) 
            /*
             IFeedReader --> Json Stream, JsonFile, csv, Xml, Delimited, FixedLength
             IFeedWriter --> Json, csv, Xml, Delimited, FixedLength
             */

            /*
            List<Person> persons = new List<Person>();
            var p = new Person(1, "sai", "kommera");
            var p1 = new Person(2, "Arjun", "Kommera");
            //var p2 = new Person(3, "Resh", "Kota");
            //var p3 = new Person(4, "Shloka", "Kommera");
            persons.Add(p);
            persons.Add(p1);
            //persons.Add(p2);
            //persons.Add(p3);
            IFeedWriter<Person> writer = new CsvFileWriter<Person>("");
            writer.Write(persons);
            writer = new XmlFileWriter<Person>("");
            writer.Write(persons);
            writer = new DelimitedFileWriter<Person>("","|");
            writer.Write(persons);

            IDictionary<string, int> fieldwidths = new Dictionary<string, int>();
            fieldwidths.Add("Id", 5);
            fieldwidths.Add("Name", 10);
            fieldwidths.Add("Lastname", 10);

            writer = new FixedWidthFileWriter<Person>("", fieldwidths);
            writer.Write(persons);

            writer = new JsonFileWriter<Person>("");
            writer.Write(persons);

            //Reader ---> popualtes a list<T> from file

            Console.ReadLine();
            */
            //var people = new List<Person>
            //{
            //    new Person(1, "sai", "Test"),
            //    new Person(2, "Arjun", "Test"),
            //    new Person(3, "Resh", "Test"),
            //    new Person(4, "Shloka", "Test")
            //};

            //var cw = new CsvFileWriter<Person>("example1.csv");
            //cw.Write(people);

            //var cr = new CsvFileReader<Person>("example1.csv");
            //var csvPeople = cr.Read(false);
            //foreach (var person in csvPeople)
            //{
            //    Console.WriteLine("{0} - {1} - {2}",person.Id, person.Name, person.Lastname);
            //}

            //var cw = new DelimitedFileWriter<Person>("example2.csv", "|","header", "footer");
            //var cw1 = new XmlFileWriter<Person>("example2.xml","header","footer");
            //cw.Write(csvPeople);
            //cw1.Write(csvPeople);
            //Console.ReadLine();
            /*
            IFeedReader<Person> cr = new CsvFileReader<Person>(false, false);
            var csvPeople = cr.Read("example1.csv");

            IDictionary<string, int> fieldwidths = new Dictionary<string, int>();
            fieldwidths.Add("Id", 5);
            fieldwidths.Add("Name", 10);
            fieldwidths.Add("Lastname", 10);

            IFeedWriter<Person>  writer = new FixedWidthFileWriter<Person>("exFixedWidth.txt", fieldwidths, "", "");
            IFeedWriter<Person>  jwriter = new JsonFileWriter<Person>("exFixedWidth.json");
            writer.Write(csvPeople);
            jwriter.Write(csvPeople);
            */

            /*
            IFeedReader<Person> reader = new XmlFileReader<Person>("Person");
            var persons = reader.Read("example2.xml");
            */
            BlockingCollection<Person> bCollection = new BlockingCollection<Person>(boundedCapacity: 100);
            Task consumerThread = new Task<int>(() => {
                StartConsumer(bCollection);
                return 1;
                });
            Task producerThread = new Task<int>(() => {
                StartProducer(bCollection);
                return 1;
            });


            consumerThread.Start();
            producerThread.Start();
            Task.WaitAll(producerThread, consumerThread);



            //Console.WriteLine("Consumer started");
            //Task consumerThread = Task.Factory.StartNew(() =>
            //{
            //    while (!bCollection.IsCompleted)
            //    {
            //        Person person = bCollection.Take();
            //        //Console.WriteLine(item);
            //        Console.WriteLine("{0} - {1} - {2}", person.Id, person.Name, person.Lastname);

            //    }
            //});
            //Console.WriteLine("Producer started");
            //Task producerThread = Task.Factory.StartNew(() =>
            //{
            //    IFeedReader<Person> reader = new CsvFileReader<Person>(false, false);
            //    reader.Read("example1.csv", bCollection);

            //    bCollection.CompleteAdding();
            //});
            //Task.WaitAll(producerThread, consumerThread);

        }

        public static void StartWriter(Task consumerThread, BlockingCollection<Person> bCollection)
        {
            Console.WriteLine("Consumer started");
            consumerThread = Task.Factory.StartNew(() =>
            {
                while (!bCollection.IsCompleted)
                {
                    Person person = bCollection.Take();
                    //Console.WriteLine(item);
                    Console.WriteLine("{0} - {1} - {2}", person.Id, person.Name, person.Lastname);

                }
            });
        }

        public static void StartReader(Task producerThread, BlockingCollection<Person> bCollection)
        {
            Console.WriteLine("Producer started");
            producerThread = Task.Factory.StartNew(() =>
            {
                IFeedReader<Person> reader = new CsvFileReader<Person>(false, false);
                reader.Read("example1.csv", bCollection);

                bCollection.CompleteAdding();
            });
        }

        public static void StartConsumer(BlockingCollection<Person> bCollection)
        {
            Console.WriteLine("Consumer started");
            while (!bCollection.IsCompleted)
            {
                Person person = bCollection.Take();
                AbstractProcessor<Person> processor = new PersonProcessor();
                var persons = processor.Run(person);
                foreach (var p in persons)
                {
                    Console.WriteLine("{0} - {1} - {2}", p.Id, p.Name, p.Lastname);
                }
                //Console.WriteLine(item);

            }
        }
        public static void StartProducer(BlockingCollection<Person> bCollection)
        {
            Console.WriteLine("Producer started");
            IFeedReader<Person> reader = new CsvFileReader<Person>(false, false);
            reader.Read("MOCK_DATA.csv", bCollection);
            bCollection.CompleteAdding();
        }

    }
}
