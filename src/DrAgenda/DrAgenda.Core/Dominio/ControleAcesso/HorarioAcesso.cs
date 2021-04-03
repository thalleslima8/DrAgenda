using System;
using Codtran.Core.Domain.Base;
using Codtran.Core.Domain.ControleAcesso;
using Codtran.Core.Domain.ControleAcesso.Enumeradores;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Shared;

namespace DrAgenda.Core.Domain.ControleAcesso
{
    public class HorarioAcesso : EntityAudit
    {
        public virtual Usuario Usuario { get; set; }

        public virtual DiaSemana DiaSemana { get; set; }
        public virtual TimeSpan HoraInicio { get; set; }
        public virtual TimeSpan HoraFim { get; set; }
    }
}