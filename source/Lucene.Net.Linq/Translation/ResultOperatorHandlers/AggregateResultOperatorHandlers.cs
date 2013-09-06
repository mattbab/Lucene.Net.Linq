using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using System;

namespace Lucene.Net.Linq.Translation.ResultOperatorHandlers
{
    internal class TakeResultOperationHandler : ResultOperatorHandler<TakeResultOperator>
    {
        protected override void AcceptInternal(TakeResultOperator take, LuceneQueryModel model)
        {
            model.MaxResults = Math.Min(take.GetConstantCount(), model.MaxResults);
        }
    }

    internal class SkipResultOperatorHandler : ResultOperatorHandler<SkipResultOperator>
    {
        protected override void AcceptInternal(SkipResultOperator skip, LuceneQueryModel model)
        {
            var additionalSkip = skip.GetConstantCount();
            model.SkipResults += additionalSkip;

            if (model.MaxResults != int.MaxValue)
            {
                model.MaxResults -= additionalSkip;
            }
        }
    }

    internal class FirstResultOperatorHandler : ResultOperatorHandler<FirstResultOperator>
    {
        protected override void AcceptInternal(FirstResultOperator resultOperator, LuceneQueryModel model)
        {
            model.MaxResults = 1;
        }
    }

    internal class LastResultOperatorHandler : ResultOperatorHandler<LastResultOperator>
    {
        protected override void AcceptInternal(LastResultOperator resultOperator, LuceneQueryModel model)
        {
            model.Last = true;
        }
    }

    internal class MaxResultOperatorHandler : ResultOperatorHandler<MaxResultOperator>
    {
        protected override void AcceptInternal(MaxResultOperator resultOperator, LuceneQueryModel model)
        {
            model.ResetSorts();
            model.AddSort(model.SelectClause, OrderingDirection.Desc);
            model.MaxResults = 1;
        }
    }

    internal class MinResultOperatorHandler : ResultOperatorHandler<MinResultOperator>
    {
        protected override void AcceptInternal(MinResultOperator resultOperator, LuceneQueryModel model)
        {
            model.ResetSorts();
            model.AddSort(model.SelectClause, OrderingDirection.Asc);
            model.MaxResults = 1;
            model.Aggregate = true;
        }
    }
}