using Lucene.Net.Search;
using System;

namespace Lucene.Net.Linq
{
    internal class SearcherLoadEventArgs : EventArgs
    {
        private readonly IndexSearcher indexSearcher;

        public SearcherLoadEventArgs(IndexSearcher indexSearcher)
        {
            this.indexSearcher = indexSearcher;
        }

        public IndexSearcher IndexSearcher
        {
            get { return indexSearcher; }
        }
    }
}