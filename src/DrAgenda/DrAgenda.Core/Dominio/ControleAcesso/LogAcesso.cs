using System;
using DrAgenda.Core.Dominio.Base;

namespace DrAgenda.Core.Dominio.ControleAcesso
{
    public class LogAcesso : EntityAudit
    {
        public virtual DateTime? DataHoraAcesso { get; set; }
        public virtual string CaminhoUrl { get; set; }
        public virtual string HostPublicoAcesso { get; set; }
        public virtual string HostLocalAcesso { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string MethodRequest { get; set; }
        public virtual string BodyRequest { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}