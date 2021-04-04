namespace DrAgenda.Shared.Dto.Helpers
{
    public class SearchDto
    {
        public int PageSize { get; set; } 
        public int PageNum { get; set; }
        public string SearchTerm { get; set; }
        public object[] Parameters { get; set; }
    }
}
