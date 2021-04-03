using Codout.Framework.NH;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository.ControleAcesso
{
    public class LogAcessoRepository : NHRepository<LogAcesso>, ILogAcessoRepository
    {
        public LogAcessoRepository(ISession session) : base(session) { }

    }
}