using System;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Person;

namespace DrAgenda.Core.Dominio.Financeiro
{
    public class Movimento : EntityAudit
    {
        public virtual DateTime Data { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual Carteira Carteira { get; set; }
        public virtual Profissional Profissional { get; set; }
        public virtual Paciente Paciente { get; set; }

        public Movimento()
        {
        }

    }
}
