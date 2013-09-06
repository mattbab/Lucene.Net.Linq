﻿using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using System;

namespace Lucene.Net.Linq.Abstractions
{
    /// <summary>
    /// Abstraction of IndexWriter to faciliate unit testing.
    /// </summary>
    /// <see cref="Lucene.Net.Index.IndexWriter"/>
    public interface IIndexWriter : IDisposable
    {
        /// <see cref="IndexWriter.AddDocument(Lucene.Net.Documents.Document)"/>
        void AddDocument(Document doc);

        /// <see cref="IndexWriter.DeleteDocuments(Lucene.Net.Search.Query[])"/>
        void DeleteDocuments(Query[] queries);

        /// <see cref="IndexWriter.DeleteAll"/>
        void DeleteAll();

        /// <see cref="IndexWriter.Commit()"/>
        void Commit();

        /// <see cref="IndexWriter.Rollback"/>
        void Rollback();

        /// <see cref="IndexWriter.Optimize()"/>
        void Optimize();

        /// <see cref="IndexWriter.GetReader()"/>
        IndexReader GetReader();

        /// <summary>
        /// Gets a value indicating whether this instance has been closed either
        /// by <see cref="Dispose"/> or <see cref="Rollback"/> being called.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        bool IsClosed { get; }
    }

    /// <summary>
    /// Wraps an IndexWriter with an implementation of <c cref="IIndexWriter"/>.
    /// </summary>
    public class IndexWriterAdapter : IIndexWriter
    {
        private readonly IndexWriter target;
        private bool closed;

        /// <param name="target">The IndexWriter instance to delegate method calls to.</param>
        public IndexWriterAdapter(IndexWriter target)
        {
            this.target = target;
        }

        public void DeleteAll()
        {
            target.DeleteAll();
        }

        public void DeleteDocuments(Query[] queries)
        {
            target.DeleteDocuments(queries);
        }

        public void Commit()
        {
            target.Commit();
        }

        public void AddDocument(Document doc)
        {
            target.AddDocument(doc);
        }

        public void Dispose()
        {
            closed = true;
            target.Dispose();
        }

        public void Optimize()
        {
            target.Optimize();
        }

        public void Rollback()
        {
            closed = true;
            target.Rollback();
        }

        public IndexReader GetReader()
        {
            return target.GetReader();
        }

        public bool IsClosed { get { return closed; } }
    }
}