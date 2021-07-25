using System;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Web.Models.Endereco;

namespace DrAgenda.Web.Models.Base
{
    public class BasePersonViewModel : ModelBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public EnderecoViewModel Endereco { get; set; }
    }
}
