using System.Collections.Generic;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Shared.Dto.Person
{
    public class PacienteDto : PersonBaseDto
    {
        public IList<DtoAggregate> Consultas { get; set; }
        public IList<DtoAggregate> Profissionais { get; set; }
        public IList<DtoAggregate> Prontuarios { get; set; }
        public StatusPaciente StatusPaciente { get; set; }
        public  string Profissao { get; set; }
    }
}
