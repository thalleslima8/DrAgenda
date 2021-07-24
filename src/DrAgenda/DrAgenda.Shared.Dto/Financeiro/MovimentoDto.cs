using System;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.Financeiro
{
    public class MovimentoDto : DtoBase
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public DtoAggregate Carteira { get; set; }
        public DtoAggregate Profissional { get; set; }
        public DtoAggregate Paciente { get; set; }
        public DtoAggregate Consulta { get; set; }
    }
}
