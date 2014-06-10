using Lucene.Net.Linq.Clauses.Expressions;
using Lucene.Net.Linq.Search;
using Lucene.Net.Search;
using Remotion.Linq.Parsing;
using System.Linq;
using System.Linq.Expressions;

namespace Lucene.Net.Linq.Transformation.TreeVisitors
{
    /// <summary>
    /// Replaces supported method calls like [LuceneQueryFieldExpression].StartsWith("foo") with a
    /// LuceneQueryPredicateExpression like [LuceneQueryPredicateExpression](+Field:foo*)
    /// </summary>
    internal class MethodCallToLuceneQueryPredicateExpressionTreeVisitor : ExpressionTreeVisitor
    {
        protected override Expression VisitMethodCallExpression(MethodCallExpression expression)
        {
            Expression[] args;
            var queryField = expression.Object as LuceneQueryFieldExpression;

            if (queryField != null)
                args = expression.Arguments.ToArray();
            else if (queryField == null && expression.Arguments.Count < 2)
                return base.VisitMethodCallExpression(expression);
            else
            {
                queryField = expression.Arguments[0] as LuceneQueryFieldExpression;

                if (queryField != null)
                    args = expression.Arguments.Skip(1).ToArray();
                else
                    return base.VisitMethodCallExpression(expression);
            }

            if (expression.Method.Name == "StartsWith")
            {
                return new LuceneQueryPredicateExpression(queryField, args[0], Occur.MUST, QueryType.Prefix);
            }
            if (expression.Method.Name == "EndsWith")
            {
                return new LuceneQueryPredicateExpression(queryField, args[0], Occur.MUST, QueryType.Suffix);
            }
            if (expression.Method.Name == "Contains")
            {
                return new LuceneQueryPredicateExpression(queryField, args[0], Occur.MUST, QueryType.Wildcard);
            }
            if (expression.Method.Name == "SimilarTo")
            {
                return new LuceneQueryPredicateExpression(queryField, args[0], Occur.MUST, QueryType.Fuzzy);
            }

            return base.VisitMethodCallExpression(expression);
        }
    }
}