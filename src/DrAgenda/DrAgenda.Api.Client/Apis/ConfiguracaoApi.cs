using System.Collections.Generic;
using System.Threading.Tasks;
using Codout.Framework.Api.Client;
using DrAgenda.Api.Client.Extensions;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Api.Client.Apis
{
    public class ConfiguracaoApi : ApiClientBase
    {
        public ConfiguracaoApi(string baseUrl, string apiKey)
            : base("api/v1/configuracao", baseUrl, apiKey)
        {
        }

        public  async Task<ConfiguracaoDto> Current()
        {
            return await Client.Get<ConfiguracaoDto>($"{UriService}/current");
        }

        public async Task Update(ConfiguracaoDto model)
        {
            await Client.Post($"{UriService}/update", model);
        }

        public async Task<IList<string>> GetTiposIntegracaoExportacao()
        {
            return await Client.Get<IList<string>>($"{UriService}/get-tipo-integracao");
        }
    }
}
