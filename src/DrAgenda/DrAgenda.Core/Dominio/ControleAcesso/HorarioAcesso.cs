using System;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Core.Dominio.ControleAcesso
{
    public class HorarioAcesso : EntityAudit
    {
        public virtual Usuario Usuario { get; set; }
        public virtual DiaSemana DiaSemana { get; set; }
        public virtual TimeSpan HoraInicio { get; set; }
        public virtual TimeSpan HoraFim { get; set; }
    }
}