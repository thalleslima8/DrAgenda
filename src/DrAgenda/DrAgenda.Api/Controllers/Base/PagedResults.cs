using System.Collections.Generic;
using System.Linq;
using Codout.Framework.Api.Dto;

namespace DrAgenda.Api.Controllers.Base
{
    public static class PagedExtensions
    {
        public static IQueryable<TDto> GetPage<TDto>(this IQueryable<TDto> source, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 1 : pageSize;
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }

    public class PagedResult<TDto> : IPagedResult<TDto>
    {
        public PagedResult(IQueryable<TDto> source, int pageIndex, int pageSize) :
            this(source.GetPage(pageIndex, pageSize), pageIndex, pageSize, source.Count())
        {
        }

        public PagedResult(IQueryable<TDto> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            pageSize = pageSize <= 0 ? 1 : pageSize;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;

            Results = totalCount > 0 ? source.ToList() : Enumerable.Empty<TDto>();
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public IEnumerable<TDto> Results { get; }

        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
