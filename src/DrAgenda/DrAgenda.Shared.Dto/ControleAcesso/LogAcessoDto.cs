using System;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class LogAcessoDto : DtoBase
    {
        public virtual DateTime? DataHoraAcesso { get; set; }
        public virtual string CaminhoUrl { get; set; }
        public virtual string HostPublicoAcesso { get; set; }
        public virtual string HostLocalAcesso { get; set; }
        public virtual string UserAgent { get; set; }
        public virtual string MethodRequest { get; set; }
        public virtual string BodyRequest { get; set; }

        public DtoAggregate Usuario { get; set; }
    }
}