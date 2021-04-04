using System;
using System.Linq.Expressions;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.Helpers
{
    public static class DtoExtensions
    {
        public static DtoAggregate ToDtoAggregate<T>(this T obj, Expression<Func<T, Guid?>> id, Expression<Func<T, string>> description)
        {
            return new DtoAggregate
            {
                Id = id.Compile()(obj),
                Descricao = description.Compile()(obj)
            };
        }
    }
}
