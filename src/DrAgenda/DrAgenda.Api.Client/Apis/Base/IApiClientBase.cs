using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codout.Framework.Api.Client;
using Codout.Framework.Api.Dto;
using Codout.Kendo.DynamicLinq;

namespace DrAgenda.Api.Client.Apis.Base
{
    public interface IApiClientBase<T> : IApiClient<T, Guid?> where T : IDto<Guid?>
    {
        Task<object> DataSource(int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates);
    }
}