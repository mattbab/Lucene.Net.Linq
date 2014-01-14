using Lucene.Net.Search;
using System;

namespace Lucene.Net.Linq
{
    public class SearcherLoadEventArgs : EventArgs
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