using Remotion.Linq;
using Remotion.Linq.Clauses;
using System.Linq.Expressions;

namespace Lucene.Net.Linq.Clauses
{
    internal class BoostClause : ExtensionClause<LambdaExpression>
    {
        public BoostClause(LambdaExpression expression)
            : base(expression)
        {
        }

        public LambdaExpression BoostFunction
        {
            get { return expression; }
        }

        protected override void Accept(ILuceneQueryModelVisitor visitor, QueryModel queryModel, int index)
        {
            visitor.VisitBoostClause(this, queryModel, index);
        }

        public override IBodyClause Clone(CloneContext cloneContext)
        {
            return new BoostClause(expression);
        }

        public override string ToString()
        {
            return "boost " + expression;
        }
    }
}