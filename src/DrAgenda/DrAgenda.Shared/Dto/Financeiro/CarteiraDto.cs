using System.Collections.Generic;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.Financeiro
{
    public class CarteiraDto : DtoBase
    {
        public decimal Saldo { get; set; }
        public List<DtoAggregate> Movimentos { get; set; }
        public DtoAggregate Profissional { get; set; }
    }
}
