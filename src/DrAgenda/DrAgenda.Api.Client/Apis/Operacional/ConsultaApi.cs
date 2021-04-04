using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Operacional;

namespace DrAgenda.Api.Client.Apis.Operacional
{
    public class ConsultaApi : DrAgendaApiClient<ConsultaDto>
    { public ConsultaApi(string baseUrl, string apiKey) 
            : base("api/v1/consulta", baseUrl, apiKey)
        {
        }
    }
}
