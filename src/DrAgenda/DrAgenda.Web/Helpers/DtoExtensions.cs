using DrAgenda.Shared.Dto.Base;
using DrAgenda.Web.Models.Base;

namespace DrAgenda.Web.Helpers
{
    public static class DtoExtensions
    {
        public static DtoAggregate ToDtoAggregate(this ViewModelAggregate viewModelAggregate)
        {
            return new DtoAggregate
            {
                Id = viewModelAggregate.Id,
                Descricao = viewModelAggregate.Descricao
            };
        }

        public static ViewModelAggregate ToViewModelAggregate(this DtoAggregate dtoAggregate)
        {
            return new ViewModelAggregate
            {
                Id = dtoAggregate.Id,
                Descricao = dtoAggregate.Descricao
            };
        }
    }
}