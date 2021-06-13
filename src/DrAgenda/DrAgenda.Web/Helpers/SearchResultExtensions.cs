using System.Linq;
using DrAgenda.Shared.Dto.Helpers;

namespace DrAgenda.Web.Helpers
{
    public static class SearchResultExtensions
    {
        public static object ToJsonObject(this SearchResultDto dto)
        {
            return new
            {
                totalCount = dto.TotalCount,
                items = dto.Items.Select(x => new
                {
                    id = x.Id,
                    text = x.Text
                }).ToList()
            };
        }
    }
}
