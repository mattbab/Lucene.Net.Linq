using Lucene.Net.Linq.Clauses.Expressions;
using Remotion.Linq;
using Remotion.Linq.Parsing.ExpressionTreeVisitors.Transformation;
using System.Linq.Expressions;

namespace Lucene.Net.Linq.Transformation
{
    internal class AllowSpecialCharactersExpressionTransformer : IExpressionTransformer<MethodCallExpression>
    {
        public Expression Transform(MethodCallExpression expression)
        {
            if (expression.Method.Name != ReflectionUtility.GetMethod(() => LuceneMethods.AllowSpecialCharacters<object>(null)).Name)
            {
                return expression;
            }

            return new AllowSpecialCharactersExpression(expression.Arguments[0]);
        }

        public ExpressionType[] SupportedExpressionTypes
        {
            get { return new[] { ExpressionType.Call }; }
        }
    }
}