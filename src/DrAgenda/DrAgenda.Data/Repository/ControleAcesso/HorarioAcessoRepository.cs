using Codout.Framework.NH;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository.ControleAcesso
{
    public class HorarioAcessoRepository : NHRepository<HorarioAcesso>, IHorarioAcessoRepository
    {
        public HorarioAcessoRepository(ISession session) 
            : base(session)
        {
        }
    }
}