using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codout.Framework.DAL;
using DrAgenda.Core;

namespace DrAgenda.Data
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory<IDrAgendaUnitOfWork>
    {
        private readonly ITenant _tenant;

        public UnitOfWorkFactory(ITenant tenant)
        {
            _tenant = tenant;
        }

        public IDrAgendaUnitOfWork Create()
        {
            return new DrAgendaUnitOfWork(_tenant);
        }
    }
}
