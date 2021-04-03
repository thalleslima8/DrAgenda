using DrAgenda.Core.Dominio.Person;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM
{
    public class EnderecoMap : AuditClassMapBase<Endereco> 
    {
        public EnderecoMap() : base("TBEnderecos")
        {
            Map(x => x.Logradouro);
            Map(x => x.Complemento);
            Map(x => x.Numero);
            Map(x => x.Bairro);
            Map(x => x.Cep);
            Map(x => x.Municipio);
            Map(x => x.UF);
        }
    }
}
