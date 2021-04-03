using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codout.Framework.NH;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository
{
    public class MovimentoRepository : NHRepository<Movimento>, IMovimentoRepository
    {
        public MovimentoRepository(ISession session) : base(session)
        {
        }
    }
}
