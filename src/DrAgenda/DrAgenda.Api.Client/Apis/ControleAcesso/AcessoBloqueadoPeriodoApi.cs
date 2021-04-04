using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Api.Client.Extensions;
using DrAgenda.Shared.Dto.ControleAcesso;
using Kendo.DynamicLinq;

namespace DrAgenda.Api.Client.Apis.ControleAcesso
{
    public class AcessoBloqueadoPeriodoApi : DrAgendaApiClient<AcessoBloqueadoPeriodoDto>
    {
        public AcessoBloqueadoPeriodoApi(string baseUrl, string apiKey)
            : base("api/v1/acesso-bloqueado-periodo", baseUrl, apiKey) { }

        public async Task<object> DataSourceUsuario(int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates, Guid? usuarioId)
        {
            return await Client.Post<object, ConsultarAcessoPeriodoDto>($"{UriService}/to-data-source-usuario", new ConsultarAcessoPeriodoDto
            {
                UsuarioId = usuarioId,
                DataSourceRequest = new DataSourceRequest
                {
                    Aggregate = aggregates,
                    Filter = filter,
                    Skip = skip,
                    Sort = sort,
                    Take = take
                }
            });
        }
    }
}
