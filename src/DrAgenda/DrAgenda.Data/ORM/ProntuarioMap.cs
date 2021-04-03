using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM
{
    public class ProntuarioMap : AuditClassMapBase<Prontuario>
    {
        public ProntuarioMap() : base("TBProntuarios")
        {
            Map(x => x.EvolucaoClinica);
            Map(x => x.HipoteseDiagnostico);
            Map(x => x.HistoricoClinico);

            References(x => x.Paciente);
            References(x => x.Profissional);
        }
    }
}
