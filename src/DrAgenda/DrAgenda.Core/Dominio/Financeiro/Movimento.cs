using System;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Person;

namespace DrAgenda.Core.Dominio.Financeiro
{
    public class Movimento : EntityAudit
    {
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public Carteira Carteira { get; set; }
        public Profissional Profissional { get; set; }
        public Paciente Paciente { get; set; }

        public Movimento()
        {
        }

    }
}
