using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.Person;

namespace DrAgenda.Api.Client.Apis.Person
{
    public class PacienteApi : DrAgendaApiClient<PacienteDto>
    { public PacienteApi(string baseUrl, string apiKey) 
            : base("api/v1/paciente", baseUrl, apiKey)
        {
        }
    }
}
