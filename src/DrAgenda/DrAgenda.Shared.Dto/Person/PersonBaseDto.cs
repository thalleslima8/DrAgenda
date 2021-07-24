using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.Person
{
    public class PersonBaseDto : DtoBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}
