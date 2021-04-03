using Codout.Framework.NH;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository
{
    public class CarteiraRepository : NHRepository<Carteira>, ICarteiraRepository
    {
        public CarteiraRepository(ISession session) : base(session)
        {
        }
    }
}
