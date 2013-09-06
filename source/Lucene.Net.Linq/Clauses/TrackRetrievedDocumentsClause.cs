using Remotion.Linq;
using Remotion.Linq.Clauses;
using System.Linq.Expressions;

namespace Lucene.Net.Linq.Clauses
{
    internal class TrackRetrievedDocumentsClause : ExtensionClause<ConstantExpression>
    {
        public TrackRetrievedDocumentsClause(ConstantExpression expression)
            : base(expression)
        {
        }

        public ConstantExpression Tracker
        {
            get { return expression; }
        }

        protected override void Accept(ILuceneQueryModelVisitor visitor, QueryModel queryModel, int index)
        {
            visitor.VisitTrackRetrievedDocumentsClause(this, queryModel, index);
        }

        public override IBodyClause Clone(CloneContext cloneContext)
        {
            return new TrackRetrievedDocumentsClause(expression);
        }
    }
}