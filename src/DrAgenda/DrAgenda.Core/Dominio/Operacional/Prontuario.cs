using System;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Person;

namespace DrAgenda.Core.Dominio.Operacional
{
    public class Prontuario : EntityAudit
    {
        protected virtual DateTime DataGravacao { get; set; }
        public virtual string HipoteseDiagnostico { get; set; }
        public virtual string EvolucaoClinica { get; set; }
        public virtual string HistoricoClinico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Profissional Profissional { get; set; }

        public Prontuario()
        {
            DataGravacao = DateTime.Now;
        }
        
        public DateTime GetData()
        {
            return DataGravacao;
        }

        public void AdicionaEvolucaoClinica(string mensagem)
        {
            DateTime data = DateTime.Now;

            EvolucaoClinica += $"\n{data}\n{mensagem}";
        }

        public override string ToString()
        {
            return $"Paciente: {Paciente.Nome}, Profissional: {Profissional.Nome}" +
                   $" Histórico Clinico: {HistoricoClinico}, \nHipotese Diagnostico: " +
                   $"{HipoteseDiagnostico} \nEvolução Clínica: {EvolucaoClinica}";
        }
    }
}
