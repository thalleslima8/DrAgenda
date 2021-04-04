using System.Collections.Generic;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class PerfilAcessoDto : DtoBase
    {
        public string Nome { get; set; }

        public IList<PermissaoAcessoItemDto> PermissoesAcesso { get; set; } = new List<PermissaoAcessoItemDto>();

        public class PermissaoAcessoItemDto : DtoBase
        {
            public string Nome { get; set; }

            public string Acao { get; set; }
        }
    }
}
