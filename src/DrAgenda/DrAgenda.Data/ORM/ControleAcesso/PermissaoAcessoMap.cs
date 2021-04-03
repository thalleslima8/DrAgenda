using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM.ControleAcesso
{
    public class PermissaoAcessoMap : ClassMapBase<PermissaoAcesso>
    {
        public PermissaoAcessoMap() : base("TBPermissoesAcesso")
        {
            Not.LazyLoad();

            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Acao).Not.Nullable().Length(255);

            References(x => x.PermissaoAcessoPai);

            HasMany(x => x.PermissoesAcesso)
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
