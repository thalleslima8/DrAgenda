using System;
using DrAgenda.Shared.Dto.Base;

namespace DrAgenda.Shared.Dto.Operacional
{
    public class ProntuarioDto : DtoBase
    {
        private DateTime _dataGravacao { get; set; }
        public string HipoteseDiagnostico { get; set; }
        public string EvolucaoClinica { get; set; }
        public string HistoricoClinico { get; set; }
        public DtoAggregate Paciente { get; set; }
        public DtoAggregate Profissional { get; set; }
    }
}
