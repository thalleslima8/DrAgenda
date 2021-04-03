using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM.ControleAcesso
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