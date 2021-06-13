using DrAgenda.Shared.Dto.ControleAcesso;

namespace DrAgenda.Web.Models.Home
{
    public class AutenticacaoViewModel
    {
        public int Codigo { get; set; }

        public UsuarioDto Usuario  { get; set; }

        public bool KeepConnected { get; set; }

        public string  ReturnUrl { get; set; }
    }
}
