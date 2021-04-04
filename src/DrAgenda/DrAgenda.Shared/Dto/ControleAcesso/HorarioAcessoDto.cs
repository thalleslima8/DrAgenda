using System;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Shared.Dto.ControleAcesso
{
    public class HorarioAcessoDto : DtoBase
    {
        public DtoAggregate Usuario { get; set; }

        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }
}