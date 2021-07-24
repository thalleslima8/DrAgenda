using System.Linq;
using Codout.Kendo.DynamicLinq;
using Kendo.Mvc;

namespace DrAgenda.Web.Helpers
{
    public static class KendoExtensions
    {
        public static string GetLogic(this FilterOperator filterOperator)
        {
            switch (filterOperator)
            {
                case FilterOperator.IsLessThan:
                    return "lt";
                case FilterOperator.IsLessThanOrEqualTo:
                    return "lte";
                case FilterOperator.IsEqualTo:
                    return "eq";
                case FilterOperator.IsNotEqualTo:
                    return "neq";
                case FilterOperator.IsGreaterThanOrEqualTo:
                    return "gte";
                case FilterOperator.IsGreaterThan:
                    return "gt";
                case FilterOperator.StartsWith:
                    return "startswith";
                case FilterOperator.EndsWith:
                    return "endswith";
                case FilterOperator.Contains:
                    return "contains";
                case FilterOperator.IsContainedIn:
                    return "contains";
                case FilterOperator.DoesNotContain:
                    return "doesnotcontain";
                case FilterOperator.IsNull:
                    break;
                case FilterOperator.IsNotNull:
                case FilterOperator.IsEmpty:
                case FilterOperator.IsNotEmpty:
                default:
                    return null;
            }

            return null;
        }

        public static DataSourceRequest ToSourceRequestApi(this Kendo.Mvc.UI.DataSourceRequest request)
        {
            var sort = request.Sorts?.Select(x =>
                    new Sort
                    {
                        Dir = x.SortDirection == ListSortDirection.Ascending ? "Asc" : "Desc",
                        Field = x.Member
                    })
                .ToList();

            Filter filter = null;

            if (request.Filters != null)
                foreach (var filterDescriptor in request.Filters)
                {
                    if (filterDescriptor is CompositeFilterDescriptor)
                    {
                        var composite = (CompositeFilterDescriptor)filterDescriptor;
                        filter = new Filter
                        {
                            Logic = composite.LogicalOperator.ToString(),
                            Filters = composite.FilterDescriptors.Select(x => new Filter
                            {
                                Logic = "And",
                                Field = ((FilterDescriptor)x).Member,
                                Operator = ((FilterDescriptor)x).Operator.GetLogic(),
                                Value = ((FilterDescriptor)x).Value
                            })
                        };
                    }
                    else
                    {
                        filter = new Filter
                        {
                            Logic = "And",
                            Filters = request.Filters.Select(x => new Filter
                            {
                                Logic = "And",
                                Field = ((FilterDescriptor)x).Member,
                                Operator = ((FilterDescriptor)x).Operator.GetLogic(),
                                Value = ((FilterDescriptor)x).Value
                            })
                        };
                    }
                }

            return new DataSourceRequest
            {
                Filter = filter,
                Skip = ((request.Page <= 0 ? 1 : request.Page) - 1) * request.PageSize,
                Sort = sort,
                Take = request.PageSize
            };
        }
    }
}
