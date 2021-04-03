using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codout.Framework.NH;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository
{
    public class ProntuarioRepository : NHRepository<Prontuario>, IProntuarioRepository
    {
        public ProntuarioRepository(ISession session) : base(session)
        {
        }
    }
}
