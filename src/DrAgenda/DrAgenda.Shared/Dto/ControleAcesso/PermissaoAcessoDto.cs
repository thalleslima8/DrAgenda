using System.Collections.Generic;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class PermissaoAcessoDto : DtoBase
    {
        public string Nome { get; set; }

        public string Acao { get; set; }

        public DtoAggregate PermissoesAcessoPai { get; set; }

        public List<DtoAggregate> PermissoesAcesso { get; set; }
    }
}
