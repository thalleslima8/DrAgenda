using System;
using System.ComponentModel.DataAnnotations;
using DrAgenda.Core.Dominio.Base;

namespace DrAgenda.Core.Dominio.Person
{
    public class BasePerson : EntityAudit
    {
        [Required(ErrorMessage = "CPF obrigatório!")]
        [StringLength(11, MinimumLength = 11)]
        public virtual string CPF { get; set; }

        [Required(ErrorMessage = "Nome obrigatório!")]
        [StringLength(60, MinimumLength = 3)]
        public virtual string Nome { get; set; }

        [Required(ErrorMessage = "Telefone obrigatório!")]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "O número precisa ter no minimo 8 e no máximo 10 digitos")]
        public virtual string Telefone { get; set; }
        
        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        public virtual DateTime DataNascimento { get; set; }
        public virtual Endereco Endereco { get; set; }

    }
}
