using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codout.Framework.Api.Client;
using Codout.Framework.Api.Dto;
using Codout.Framework.Api.Dto.Default;
using DrAgenda.Api.Client.Extensions;
using DrAgenda.Shared.Dto.Helpers;
using Kendo.DynamicLinq;

namespace DrAgenda.Api.Client.Apis.Base
{
    public class DrAgendaApiClient<T> : ApiClient<T, Guid?>, IApiClientBase<T> where T : DtoBase<Guid?>
    {
        public DrAgendaApiClient(string uriService, string baseUrl)
            : base(uriService, baseUrl)
        {
        }

        public DrAgendaApiClient(string uriService, string baseUrl, string apiKey)
            : base(uriService, baseUrl, apiKey)
        {
        }

        public async Task<object> DataSource(int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates)
        {
            return await Client.DataSource($"{UriService}/datasource", take, skip, sort, filter, aggregates);
        }

        public async Task<object> DataSource(DataSourceRequest dataSourceRequest)
        {
            return await Client.DataSource($"{UriService}/datasource", dataSourceRequest);
        }

        public async Task<SearchResultDto> Search(string term, int pageSize, int pageNum = 1, params object[] parameters)
        {
            return await Client.Post<SearchResultDto, SearchDto> ($"{UriService}/search", new SearchDto
            {
                SearchTerm = term,
                PageSize = pageSize,
                PageNum = pageNum,
                Parameters = parameters
            });
        }

        public new async Task<IEnumerable<T>> Get()
        {
            return await Client.Get<IEnumerable<T>>(UriService);
        }

        public new async Task<IPagedResult<T>> Get(int page, int size)
        {
            return await Client.Get<IPagedResult<T>>($"{UriService}?page={(page < 0 ? 0 : page)}&size={(size < 1 ? 1 : size)}");
        }

        public new async Task<T> Get(Guid? id)
        {
            return await Client.Get<T>($"{UriService}/{id}");
        }

        public new async Task<T> Post(T model)
        {
            return await Client.Post<T, T>(UriService, model);
        }

        public new async Task<T> Put(T model)
        {
            return await Client.Put<T, T>($"{UriService}/{model.Id}", model);
        }

        public new async Task Delete(Guid? id)
        {
            await Client.Delete($"{UriService}/{id}");
        }
    }
}
