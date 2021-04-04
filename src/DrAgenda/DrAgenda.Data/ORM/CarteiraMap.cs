using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM
{
    public class CarteiraMap : AuditClassMapBase<Carteira>
    {
        public CarteiraMap() : base("TBCarteiras")
        {
            Map(x => x.Saldo);

            HasOne(x => x.Profissional).Cascade.All().PropertyRef("Carteira_id");

            HasMany(x => x.Movimentos)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Cascade.AllDeleteOrphan();
        }
    }
}
