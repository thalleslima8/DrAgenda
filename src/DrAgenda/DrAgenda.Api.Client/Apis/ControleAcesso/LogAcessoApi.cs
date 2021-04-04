using System.Threading.Tasks;
using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Api.Client.Extensions;
using DrAgenda.Shared.Dto.ControleAcesso;

namespace DrAgenda.Api.Client.Apis.ControleAcesso
{
    public class LogAcessoApi : DrAgendaApiClient<LogAcessoDto>
    {
        public LogAcessoApi(string baseUrl, string apiKey)
            : base("api/v1/log-acesso", baseUrl, apiKey) { }

        public async Task<object> DatasourceTableFiltroLogAcesso(FiltroLogAcessoDto dto)
        {
            return await Client.Post<object, FiltroLogAcessoDto>($"{UriService}/data-source-table-filtro-log-acesso", dto);
        }
    }
}
