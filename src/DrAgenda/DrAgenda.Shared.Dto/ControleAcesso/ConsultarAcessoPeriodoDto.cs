using System;
using Codout.Kendo.DynamicLinq;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class ConsultarAcessoPeriodoDto : DtoBase
    {
        public Guid? UsuarioId { get; set; }

        public DataSourceRequest DataSourceRequest { get; set; }
    }
}