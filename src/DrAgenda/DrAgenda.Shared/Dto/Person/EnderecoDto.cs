using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Shared.Dto.Person
{
    public class EnderecoDto : DtoBase
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Municipio { get; set; }
        public UF Uf { get; set; }
    }
}
