using Codtran.Core.Domain.ControleAcesso;
using Codtran.Data.ORM.Base;
using DrAgenda.Core.Domain.ControleAcesso;
using DrAgenda.Data.ORM.Base;

namespace Codtran.Data.ORM
{
    public class HorarioAcessoMap : AuditClassMapBase<HorarioAcesso>
    {
        public HorarioAcessoMap() : base("TBHorariosAcessos")
        {
            References(x => x.Usuario);

            Map(x => x.DiaSemana).Not.Nullable().Length(150);

            Map(x => x.HoraInicio).Not.Nullable().CustomType("TimeAsTimeSpan");

            Map(x => x.HoraFim).Not.Nullable().CustomType("TimeAsTimeSpan");
        }
    }
}