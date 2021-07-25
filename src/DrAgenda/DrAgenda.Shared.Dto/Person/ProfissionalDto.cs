using System.Collections.Generic;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Shared.Dto.Person
{
    public class ProfissionalDto : BasePersonDto
    {
        public IList<DtoAggregate> Consultas { get; set; }
        public IList<DtoAggregate> Pacientes { get; set; }
        public DtoAggregate Carteira { get; set; }
        public Formacao Formacao { get; set; }
    }
}
