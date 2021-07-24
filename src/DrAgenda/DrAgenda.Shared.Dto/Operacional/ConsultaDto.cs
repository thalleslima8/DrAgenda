using System;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Shared.Dto.Operacional 
{
    public class ConsultaDto : DtoBase
    {
        public DateTime Horario { get; set; }
        public DtoAggregate Paciente { get; set; }
        public DtoAggregate Profissional { get; set; }
        public decimal Valor { get; set; }
        public StatusConsulta Status { get; set; }
    }
}
