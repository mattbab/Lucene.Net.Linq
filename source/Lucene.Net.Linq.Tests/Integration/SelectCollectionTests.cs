using Lucene.Net.Linq.Mapping;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Lucene.Net.Linq.Tests.Integration
{
    [TestFixture]
    public class SelectCollectionTests : IntegrationTestBase
    {
        private class TaggedDocument
        {
            public string Name { get; set; }

            [Field("tag")]
            public IEnumerable<string> Tags { get; set; }
        }

        [Test]
        public void Enumerable_Contains()
        {
            using (var session = provider.OpenSession<TaggedDocument>())
            {
                session.Add(new TaggedDocument { Name = "First", Tags = new[] { "a", "b" } });
                session.Add(new TaggedDocument { Name = "Second", Tags = new[] { "c", "d" } });
                session.Commit();
            }

            var documents = provider.AsQueryable<TaggedDocument>();

            var result = from doc in documents where doc.Tags.Contains("c") select doc;

            Assert.That(result.Single().Name, Is.EqualTo("Second"));
        }
    }
}