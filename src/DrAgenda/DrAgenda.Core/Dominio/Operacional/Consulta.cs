using System;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Shared;

namespace DrAgenda.Core.Dominio.Operacional
{
    public class Consulta : EntityAudit
    {
        public DateTime Horario { get; set; }
        public Paciente Paciente { get; set; }
        public Profissional Profissional { get; set; }
        public decimal Taxa { get; set; }
        public StatusConsulta Status { get; set; }

        public Consulta()
        {
            Status = StatusConsulta.Agendada;
        }

        public Consulta(DateTime horario, Paciente paciente, Profissional profissional, decimal taxa)
        {
            Horario = horario;
            Paciente = paciente;
            Profissional = profissional;
            Taxa = taxa;
            Status = StatusConsulta.Agendada;
        }

        public void RecebeConsulta()
        {
            var pagamento = new Movimento(this.Horario, this.Taxa, this.Profissional.Carteira);
            Profissional.Carteira.Movimentos.Add(pagamento);
        }
    }
}
