using Codout.Framework.NH;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository.ControleAcesso
{
    public class PermissaoAcessoRepository : NHRepository<PermissaoAcesso>, IPermissaoAcessoRepository
    {
        public PermissaoAcessoRepository(ISession session) 
            : base(session)
        {
        }
    }
}