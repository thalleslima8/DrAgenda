using System.ComponentModel.DataAnnotations;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class AlterarSenhaDto : DtoBase
    {
        [MaxLength(20, ErrorMessage = "Comprimento máximo de {0} deve ser {1} caracteres")]
        [Required(ErrorMessage = "{0} não pode ser vazio")]
        public string SenhaAtual { get; set; }

        [MaxLength(20, ErrorMessage = "Comprimento máximo de {0} deve ser {1} caracteres")]
        [Required(ErrorMessage = "{0} não pode ser vazio")]
        public string NovaSenha { get; set; }
    }
}