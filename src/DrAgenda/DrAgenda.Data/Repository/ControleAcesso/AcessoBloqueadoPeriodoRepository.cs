using Codout.Framework.NH;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository.ControleAcesso
{
    public class AcessoBloqueadoPeriodoRepository : NHRepository<AcessoBloqueadoPeriodo>, IAcessoBloqueadoPeriodoRepository
    {
        public AcessoBloqueadoPeriodoRepository(ISession session) 
            : base(session)
        {
        }
    }
}