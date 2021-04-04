using System.Collections.Generic;
using System.Threading.Tasks;
using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Api.Client.Extensions;
using DrAgenda.Shared.Dto.ControleAcesso;

namespace DrAgenda.Api.Client.Apis.ControleAcesso
{
    public class PermissaoAcessoApi : DrAgendaApiClient<PermissaoAcessoDto>
    {
        public PermissaoAcessoApi(string baseUrl, string apiKey)
            : base("api/v1/permissao-acesso", baseUrl, apiKey) { }

        public async Task<PermissaoAcessoDto> ObterPorAcao(string acao)
        {
            return await Client.Get<PermissaoAcessoDto>($"{UriService}/obter-por-acao/{acao}");
        }

        public async Task<IEnumerable<PermissaoAcessoDto>> ObterFilhos(string paidId)
        {
            return await Client.Get<IEnumerable<PermissaoAcessoDto>>($"{UriService}/obter-filhos/{paidId}");
        }
    }
}
