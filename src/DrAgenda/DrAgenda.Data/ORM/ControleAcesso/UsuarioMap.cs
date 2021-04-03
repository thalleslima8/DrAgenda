using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM.ControleAcesso
{
    public class UsuarioMap : AuditClassMapBase<Usuario>
    {
        public UsuarioMap() : base("TBUsuarios")
        {
            Map(x => x.Nome)
                .Not.Nullable().Length(100);

            Map(x => x.Email)
                .Not.Nullable().Length(150);

            Map(x => x.NomeUsuario)
                .Not.Nullable().Length(100);

            Map(x => x.Senha)
                .Not.Nullable()
                .Access.CamelCaseField(Prefix.Underscore)
                .Length(1000);

            Map(x => x.Token)
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.DataExpiracaoToken)
                .Access.CamelCaseField(Prefix.Underscore);

            Map(x => x.Inativo);

            Map(x => x.Admin);

            Map(x => x.AutenticacaoDoisFatores);

            Map(x => x.Telefone);

            Map(x => x.CodigoAgente);

            HasManyToMany(x => x.PerfilAcessos)
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("TBUsuariosPerfilAcessos")
                .ChildKeyColumn("PerfilAcesso_id")
                .ParentKeyColumn("Usuario_id")
                .AsSet();

            HasMany(x => x.HorariosAcesso)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse().Cascade.All()
                .AsSet();

            HasMany(x => x.AcessosBloqueadoPeriodos)
                .Access.CamelCaseField(Prefix.Underscore)
                .Inverse().Cascade.All()
                .AsSet(); 
        }
    }
}