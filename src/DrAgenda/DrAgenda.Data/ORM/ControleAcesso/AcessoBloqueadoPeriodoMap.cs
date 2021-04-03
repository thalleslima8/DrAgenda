using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM.ControleAcesso
{
    public class AcessoBloqueadoPeriodoMap : AuditClassMapBase<AcessoBloqueadoPeriodo>
    {
        public AcessoBloqueadoPeriodoMap() : base("TBAcessosBloqueadosPeriodo")
        {
            Map(x => x.DataFim).Not.Nullable();

            Map(x => x.DataInicio).Not.Nullable();

            Map(x => x.Motivo)
                .Not.Nullable()
                .Length(200);

            References(x => x.Usuario);
        }
    }
}