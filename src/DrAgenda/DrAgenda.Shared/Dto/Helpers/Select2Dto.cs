using System;

namespace DrAgenda.Shared.Dto.Helpers
{
    public class Select2SearchDto
    {
        public int PageSize { get; set; } 
        public int PageNum { get; set; }
        public string SearchTerm { get; set; }
    }

    public class Select2DtoParameter
    {
        public int PageSize { get; set; }
        public int PageNum { get; set; }
        public string SearchTerm { get; set; }
        public Guid? ParameterSearch { get; set; }
    }

    public class Select2ItemDto
    {
        public Guid? Id { get; set; }
        public string Text { get; set; }
    }
    
}
