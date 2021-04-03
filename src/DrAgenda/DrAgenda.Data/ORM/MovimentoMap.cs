using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM
{
    public class MovimentoMap : AuditClassMapBase<Movimento>
    {
        public MovimentoMap() : base("TBMovimentos")
        {
            Map(x => x.Data);
            Map(x => x.Valor);

            References(x => x.Profissional);
            References(x => x.Carteira);
            References(x => x.Paciente);
        }
    }
}
