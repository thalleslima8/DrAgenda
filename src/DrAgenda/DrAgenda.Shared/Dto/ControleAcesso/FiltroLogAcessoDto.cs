using System;
using DrAgenda.Shared.Dto.Base;
using Kendo.DynamicLinq;

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