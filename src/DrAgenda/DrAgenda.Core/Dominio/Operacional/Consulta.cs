using System;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Core.Dominio.Operacional
{
    public class Consulta : EntityAudit
    {
        public virtual DateTime Horario { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Profissional Profissional { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual StatusConsulta Status { get; set; }

        public Consulta()
        {
            Status = StatusConsulta.Agendada;
        }

        public Consulta(DateTime horario, Paciente paciente, Profissional profissional, decimal valor)
        {
            Horario = horario;
            Paciente = paciente;
            Profissional = profissional;
            Valor = valor;
            Status = StatusConsulta.Agendada;
        }

        public virtual void RecebeConsulta()
        {
            var pagamento = new Movimento()
            {
                Carteira = this.Profissional.Carteira,
                Valor = this.Valor,
                Data = this.Horario
            };
            Profissional.Carteira.AdicionaMovimento(pagamento, this.Paciente);
        }
    }
}
