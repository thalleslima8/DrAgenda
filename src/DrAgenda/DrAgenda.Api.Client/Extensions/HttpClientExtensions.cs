using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Codout.Framework.Api.Client.Helpers;
using Codout.Kendo.DynamicLinq;
using DrAgenda.Core.Helpers;
using DrAgenda.Shared.Dto;
using DrAgenda.Shared.Dto.Model;

namespace DrAgenda.Api.Client.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<object> DataSource(this HttpClient client, string uriService, int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates)
        {
            return await DataSource(client, uriService, new DataSourceRequest
            {
                Aggregate = aggregates,
                Filter = filter,
                Skip = skip,
                Sort = sort,
                Take = take
            });
        }

        public static async Task<object> DataSource(this HttpClient client, string uriService, DataSourceRequest dataSourceRequest)
        {
            HttpResponseMessage response = null;

            try
            {
                AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
                response = await client.PostAsJsonAsync($"{uriService}", dataSourceRequest);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsAsync<object>();
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                    throw new Exception("O tempo limite da requisição esgotou", ex);

                throw new Exception("A requisição foi cancelada", ex);
            }
            catch (AggregateException ex)
            {
                var message = ex.InnerExceptions.Aggregate(string.Empty, (current, agInnerException) => current + $"{agInnerException.Message}\n");
                throw new Exception(message, ex);
            }
            catch (Exception ex)
            {
                if (response != null)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(message))
                        throw new Exception(message, ex);
                }

                throw;
            }
        }

        public static async Task<TDto> Post<TDto, TModel>(this HttpClient client, string uriService, TModel model)
        {
            return await CallClient<TDto>(async () => await client.PostAsJsonAsync(uriService, model));
        }

        public static async Task Post<TModel>(this HttpClient client, string uriService, TModel model)
        {
            await CallClient(async () => await client.PostAsJsonAsync(uriService, model));
        }

        public static async Task Post(this HttpClient client, string uriService)
        {
            await CallClient(async () => await client.PostAsync(uriService, null));
        }

        public static async Task<TDto> Put<TDto, TModel>(this HttpClient client, string uriService, TModel model)
        {
            return await CallClient<TDto>(async () => await client.PutAsJsonAsync(uriService, model));
        }

        public static async Task<TDto> Get<TDto>(this HttpClient client, string uriService)
        {
            return await CallClient<TDto>(async () => await client.GetAsync(uriService));
        }

        public static async Task Delete(this HttpClient client, string uriService)
        {
            await CallClient(async () => await client.DeleteAsync(uriService));
        }

        private static async Task<TDto> CallClient<TDto>(Func<Task<HttpResponseMessage>> client)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await client();
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<TDto>();
            }
            catch (Exception ex)
            {
                throw response != null ? await response.GetException(ex) : ex;
            }
        }

        private static async Task CallClient(Func<Task<HttpResponseMessage>> client)
        {
            HttpResponseMessage response = null;

            try
            {
                response = await client();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw response != null ? await response.GetException(ex) : ex;
            }
        }

        private static async Task<Exception> GetException(this HttpResponseMessage httpResponseMessage, Exception originalException)
        {
            if (httpResponseMessage != null)
            {
                switch (httpResponseMessage.StatusCode)
                {
                    //Erro tratado
                    case HttpStatusCode.BadRequest:
                        try
                        {
                            var errorDto = await httpResponseMessage.Content.ReadAsAsync<ErroDto>();
                            throw new DrAgendaApiException(errorDto.Descricao, errorDto);
                        }
                        catch
                        {
                            throw new DrAgendaApiException(await httpResponseMessage.Content.ReadAsStringAsync());
                        }
                    //Erro não tratado
                    case HttpStatusCode.InternalServerError:
                        var errorDetails = await httpResponseMessage.Content.ReadAsAsync<ExceptionDetails>();
                        if (errorDetails != null)
                            return new Exception(errorDetails.Message, originalException);
                        break;
                }
            }

            return originalException;
        }
    }
}
