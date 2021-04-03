using Codout.Framework.NH;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository.ControleAcesso
{
    public class UsuarioRepository : NHRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ISession session) 
            : base(session)
        {
        }
    }
}