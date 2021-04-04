using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Financeiro;

namespace DrAgenda.Api.Client.Apis.Financeiro
{
    public class MovimentoApi : DrAgendaApiClient<MovimentoDto>
    { 
        public MovimentoApi(string baseUrl, string apiKey) 
            : base("api/v1/movimento", baseUrl, apiKey)
        {
        }
    }
}
