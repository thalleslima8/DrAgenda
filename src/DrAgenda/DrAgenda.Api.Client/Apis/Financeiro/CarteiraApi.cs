using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Financeiro;
using DrAgenda.Shared.Dto.Person;

namespace DrAgenda.Api.Client.Apis.Person
{
    public class CarteiraApi : DrAgendaApiClient<CarteiraDto>
    { public CarteiraApi(string baseUrl, string apiKey) 
            : base("api/v1/endereco", baseUrl, apiKey)
        {
        }
    }
}
