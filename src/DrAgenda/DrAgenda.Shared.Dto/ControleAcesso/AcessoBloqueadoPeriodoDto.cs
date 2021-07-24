using System;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class AcessoBloqueadoPeriodoDto : DtoBase
    {
        public DtoAggregate Usuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Motivo { get; set; }
    }
}