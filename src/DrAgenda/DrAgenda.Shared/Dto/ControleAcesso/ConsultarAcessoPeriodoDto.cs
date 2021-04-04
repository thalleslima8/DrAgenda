using System;
using DrAgenda.Shared.Dto.Base;
using Kendo.DynamicLinq;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class ConsultarAcessoPeriodoDto : DtoBase
    {
        public Guid? UsuarioId { get; set; }

        public DataSourceRequest DataSourceRequest { get; set; }
    }
}