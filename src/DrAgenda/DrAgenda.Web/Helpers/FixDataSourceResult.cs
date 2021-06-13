using System;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace DrAgenda.Web.Helpers
{
    public static class FixDataSourceResult
    {
        public static DataSourceResult ToGroupingDataSourceResult<TModel, TResult>(this IQueryable<TModel> enumerable, DataSourceRequest request, Func<TModel, TResult> selector)
        {
            var grouping = request.Groups;
            var aggregates = request.Aggregates;
            var hasGrouping = grouping != null && grouping.Count > 0;
            var hasAggregates = aggregates != null && aggregates.Count > 0;
            if (!hasGrouping && !hasAggregates)
            {
                return enumerable.ToDataSourceResult(request, selector);
            }

            //Clear the grouping options
            request.Groups = null;
            request.Aggregates = null;

            //Execute the query without grouping
            var result = enumerable.ToDataSourceResult(request, selector);

            //Restore grouping
            request.Groups = grouping;
            if (hasGrouping && hasAggregates)
                request.Aggregates = aggregates;

            //var aggregateResults =  result.Data.ToDataSourceResult(request);
            //result.AggregateResults = aggregateResults.AggregateResults;
            result.AggregateResults = enumerable.Aggregate(aggregates.SelectMany(x => x.Aggregates)).ToList();

            result.Data = result.Data.AsQueryable().GroupBy(grouping, true);

            result.Total = enumerable.Count();

            return result;
        }
    }
}