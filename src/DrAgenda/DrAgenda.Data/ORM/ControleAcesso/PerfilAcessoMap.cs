using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM.ControleAcesso
{
    public class PerfilAcessoMap : AuditClassMapBase<PerfilAcesso>
    {
        public PerfilAcessoMap() : base("TBPerfilAcessos")
        {
            Not.LazyLoad();

            Map(x => x.Nome).Not.Nullable().Length(100);

            HasManyToMany(x => x.PermissoesAcesso)
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("TBPerfilAcessosPermissoesAcesso")
                .ParentKeyColumn("PerfilAcesso_id")
                .ChildKeyColumn("PermissaoAcesso_id")
                .Not.LazyLoad();
        }
    }
}
