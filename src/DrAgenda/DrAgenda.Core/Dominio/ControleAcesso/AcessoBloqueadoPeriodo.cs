using System;
using Codtran.Core.Domain.ControleAcesso;
using DrAgenda.Core.Dominio.Base;

namespace DrAgenda.Core.Domain.ControleAcesso
{
    public class AcessoBloqueadoPeriodo : EntityAudit
    {
        public virtual Usuario Usuario { get; set; }

        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataFim { get; set; }
        public virtual string Motivo { get; set; }
    }
}