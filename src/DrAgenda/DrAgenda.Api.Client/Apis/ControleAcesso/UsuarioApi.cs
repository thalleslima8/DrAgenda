using System.Threading.Tasks;
using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Api.Client.Extensions;
using DrAgenda.Shared.Dto.ControleAcesso;

namespace DrAgenda.Api.Client.Apis.ControleAcesso
{
    public class UsuarioApi : DrAgendaApiClient<UsuarioDto>
    {
        public UsuarioApi(string baseUrl, string apiKey)
            : base("api/v1/usuario", baseUrl, apiKey) { }

        public async Task<UsuarioDto> Login(LoginDto model)
        {
            return await Client.Post<UsuarioDto, LoginDto>($"{UriService}/login", model);
        }

        public async Task AlterarSenha(AlterarSenhaDto model)
        {
            await Client.Post($"{UriService}/alterar-senha", model);
        }
    }
}