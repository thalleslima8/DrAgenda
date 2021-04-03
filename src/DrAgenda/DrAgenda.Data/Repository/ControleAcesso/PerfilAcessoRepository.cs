using Codout.Framework.NH;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository.ControleAcesso
{
    public class PerfilAcessoRepository : NHRepository<PerfilAcesso>, IPerfilAcessoRepository
    {
        public PerfilAcessoRepository(ISession session) 
            : base(session)
        {
        }
    }
}