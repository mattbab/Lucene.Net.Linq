using Lucene.Net.Search;
using System;

namespace Lucene.Net.Linq
{
    public interface ISearcherHandle : IDisposable
    {
        IndexSearcher Searcher { get; }
    }
}