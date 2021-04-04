using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
