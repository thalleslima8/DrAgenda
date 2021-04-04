using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM
{
    public class ConsultaMap : AuditClassMapBase<Consulta>
    {
        public ConsultaMap() : base("TBConsultas")
        {
            Map(x => x.Horario);
            Map(x => x.Valor);
            Map(x => x.Status);
            
            References(x => x.Profissional);
            References(x => x.Paciente);

        }
    }
}
