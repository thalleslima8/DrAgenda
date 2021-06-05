using System.ComponentModel.DataAnnotations;
using DrAgenda.Shared.Dto.Base;

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

        [MaxLength(2, ErrorMessage = "Sigla UF")]
        public string Uf { get; set; }
    }
}
