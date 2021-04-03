using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            References(x => x.Profissional);

            HasMany(x => x.Movimentos)
                .Access.CamelCaseField(Prefix.Underscore)
                .AsSet()
                .Cascade.AllDeleteOrphan();
        }
    }
}
