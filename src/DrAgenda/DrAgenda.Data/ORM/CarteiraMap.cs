using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Data.ORM.Base;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM
{
    public class CarteiraMap : ClassMap<Carteira>
    {
        public CarteiraMap()
        {
            Table("TBCarteiras");

            Id(x => x.Id).GeneratedBy.Foreign("Profissional");
            Map(x => x.Saldo);

            HasOne(x => x.Profissional).Cascade.None();

            HasMany(x => x.Movimentos)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Cascade.AllDeleteOrphan();
        }
    }
}
