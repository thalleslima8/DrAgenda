using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Person;

namespace DrAgenda.Api.Client.Apis.Person
{
    public class EnderecoApi : DrAgendaApiClient<EnderecoDto>
    { public EnderecoApi(string baseUrl, string apiKey) 
            : base("api/v1/endereco", baseUrl, apiKey)
        {
        }
    }
}
