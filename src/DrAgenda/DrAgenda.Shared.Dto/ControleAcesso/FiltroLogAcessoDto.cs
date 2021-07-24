using System;
using Codout.Kendo.DynamicLinq;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class FiltroLogAcessoDto : DtoBase
    {
        public DateTime? DataHoraInicial { get; set; }
        public DateTime? DataHoraFinal { get; set; }
        public Guid? UsuarioId { get; set; }
        public DataSourceRequest SourceRequest { get; set; }
    }
}