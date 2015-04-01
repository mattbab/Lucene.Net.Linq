using HDS.Data.Index;
using Lucene.Net.Linq;
using Lucene.Net.Store;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Sandbox
{
    public static class Dumper
    {
        public static string Dump(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }

    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var directory = new RAMDirectory())
            {
                var ctx = directory.GetProviderContext(false);
                using (var session = ctx.OpenSession<Person>())
                {
                    session.Add(new[] {
                        new Person
                        {
                            FirstName = "Matt",
                            LastName = "Babcock"
                        },
                        new Person
                        {
                            FirstName = "Alicia",
                            LastName = "Babcock"
                        },
                        new Person
                        {
                            FirstName = "Ayden",
                            LastName = "Babcock"
                        },
                        new Person
                        {
                            FirstName = "Dakota",
                            LastName = "Babcock"
                        },
                        new Person
                        {
                            FirstName = "Ryenn",
                            LastName = "Babcock"
                        },
                        new Person
                        {
                            FirstName = "Robert",
                            LastName = "Greenhagen"
                        },
                    });

                    session.Commit();
                }

                Console.WriteLine(ctx.AsQueryable<Person>().Where(p => p.FirstName.SimilarTo("Robbert")).ToList().Dump());
                Console.ReadKey(true);
            }
        }
    }
}