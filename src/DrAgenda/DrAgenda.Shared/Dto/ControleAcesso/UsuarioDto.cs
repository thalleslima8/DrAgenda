using System.Collections.Generic;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class UsuarioDto : DtoBase
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public bool Inativo { get; set; }
        public bool Admin { get; set; }
        public bool AlterarSenha { get; set; }

        public bool AutenticacaoDoisFatores { get; set; }
        public string Telefone { get; set; }
        public string CodigoAgente { get; set;}

        public IList<PerfilAcessoDto> PerfisAcesso { get; set; } = new List<PerfilAcessoDto>();
        
    }
}