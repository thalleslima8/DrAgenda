using System.ComponentModel.DataAnnotations;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Core.Dominio.Person
{
    public class Endereco : EntityAudit
    {
        [Required(ErrorMessage = "Logradouro obrigatório!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Logradouro inválido")]
        public virtual string Logradouro { get; set; } = "";
        
        [Required(ErrorMessage = "Obrigatório!")]
        public virtual int Numero { get; set; } = 0;
        
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(50, ErrorMessage = "Máximo 50 Caracteres!")]
        public virtual string Complemento { get; set; } = "";
        
        [Required(ErrorMessage = "Bairro obrigatório!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Bairro inválido")]
        public virtual string Bairro { get; set; } = "";
        
        [Required(ErrorMessage = "Municipio obrigatório!")]
        public virtual string Municipio { get; set; } = "";
        
        [Required(ErrorMessage = "UF obrigatório!")]
        public virtual UF UF { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(10, ErrorMessage = "Máximo 10 Caracteres!")]
        public virtual string Cep { get; set; } = "";


        public Endereco() { }
        public Endereco(string log, int n, string comp, string bairro, string mun, UF est, string cep)
        {
            Logradouro = log;
            Numero = n;
            Complemento = comp;
            Bairro = bairro;
            Municipio = mun;
            UF = est;
            Cep = cep;
        }
    }
}
