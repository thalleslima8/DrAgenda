using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Person;

namespace DrAgenda.Api.Client.Apis.Person
{
    public class ProfissionalApi : DrAgendaApiClient<ProfissionalDto>
    { public ProfissionalApi(string baseUrl, string apiKey) 
            : base("api/v1/profissional", baseUrl, apiKey)
        {
        }
    }
}
