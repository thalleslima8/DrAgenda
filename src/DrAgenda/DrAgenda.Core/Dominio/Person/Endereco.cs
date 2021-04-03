using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Shared;

namespace DrAgenda.Core.Dominio.Person
{
    public class Endereco : EntityAudit
    {
        [DataMember]
        [Required(ErrorMessage = "Logradouro obrigatório!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Logradouro inválido")]
        public virtual string Logradouro { get; set; } = "";

        [DataMember]
        [Required(ErrorMessage = "Obrigatório!")]
        public virtual int Numero { get; set; } = 0;

        [DataMember]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(50, ErrorMessage = "Máximo 50 Caracteres!")]
        public virtual string Complemento { get; set; } = "";

        [DataMember]
        [Required(ErrorMessage = "Bairro obrigatório!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Bairro inválido")]
        public virtual string Bairro { get; set; } = "";

        [DataMember]
        [Required(ErrorMessage = "Municipio obrigatório!")]
        public virtual string Municipio { get; set; } = "";

        [DataMember]
        [Required(ErrorMessage = "UF obrigatório!")]
        public virtual UF UF { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Campo obrigatório!")]
        [StringLength(10, ErrorMessage = "Máximo 10 Caracteres!")]
        public virtual string Cep { get; set; } = "";


        public Endereco() { }
        public Endereco(string log, int n, string comp, string bairro, string mun, string est, string cep)
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
