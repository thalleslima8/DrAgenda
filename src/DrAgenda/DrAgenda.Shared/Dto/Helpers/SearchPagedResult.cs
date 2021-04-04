using System.Collections.Generic;

namespace DrAgenda.Shared.Dto.Helpers
{
    public class SearchResultDto2
    {
        public int TotalCount { get; set; }

        public List<SearchResult> Items { get; set; }
    }

    public class SearchResult
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
