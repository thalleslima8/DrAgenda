using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Shared.Dto.ControleAcesso;

namespace DrAgenda.Api.Client.Apis.ControleAcesso
{
    public class PerfilAcessoApi : DrAgendaApiClient<PerfilAcessoDto>
    {
        public PerfilAcessoApi(string baseUrl, string apiKey)
            : base("api/v1/perfilacesso", baseUrl, apiKey) { }
    }
}
