using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codout.Framework.NH;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Core.IRepository;
using NHibernate;

namespace DrAgenda.Data.Repository
{
    public class EnderecoRepository : NHRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ISession session) : base(session)
        {
        }
    }
}
