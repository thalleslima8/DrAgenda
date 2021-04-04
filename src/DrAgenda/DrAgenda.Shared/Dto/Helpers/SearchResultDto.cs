using System.Collections.Generic;

namespace DrAgenda.Shared.Dto.Helpers
{
    public class SearchResultDto
    {
        public int TotalCount { get; set; }

        public IList<SearchResultItemDto> Items { get; set; } = new List<SearchResultItemDto>();
    }

    public class SearchResultItemDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
