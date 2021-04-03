using Codtran.Core.Domain.ControleAcesso;
using Codtran.Data.ORM.Base;
using DrAgenda.Core.Domain.ControleAcesso;
using DrAgenda.Data.ORM.Base;

namespace Codtran.Data.ORM
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