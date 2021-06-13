using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Web.Models.Home
{
    public class LoginViewModel
    {
        [Display(Name = "Nome de Usuário")]
        [MaxLength(200, ErrorMessage = "Informe no máximo {0} caracteres")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        public string Username { get; set; }

        [Display(Name = "Senha")]
        [MaxLength(12, ErrorMessage = "Informe no máximo {0} caracteres")]
        [Required(ErrorMessage = "{0} deve ser informado")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Mantenha-me Conectado")]
        public bool KeepConnected { get; set; }

        public string ReturnUrl { get; set; }
    }
}
