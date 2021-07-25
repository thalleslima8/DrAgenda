using System.ComponentModel.DataAnnotations;
using DrAgenda.Web.Models.Base;

namespace DrAgenda.Web.Models.Endereco
{
    public class EnderecoViewModel : ModelBase
    {
        [Required]
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
