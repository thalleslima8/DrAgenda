using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Operacional;

namespace DrAgenda.Api.Client.Apis.Operacional
{
    public class ProntuarioApi : DrAgendaApiClient<ProntuarioDto>
    { public ProntuarioApi(string baseUrl, string apiKey) 
            : base("api/v1/prontuario", baseUrl, apiKey)
        {
        }
    }
}
