using System;
using DrAgenda.Shared.Dto.Person;

namespace DrAgenda.Shared.Dto.Base
{
    public class BasePersonDto : DtoBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}
