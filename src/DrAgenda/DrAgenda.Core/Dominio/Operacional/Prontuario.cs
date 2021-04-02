using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Person;

namespace DrAgenda.Core.Dominio.Operacional
{
    public class Prontuario : EntityAudit
    {
        public string HipoteseDiagnostico { get; set; }
        public string EvolucaoClinica { get; set; }
        public string HistoricoClinico { get; set; }
        public Paciente Paciente { get; set; }
        public Profissional Profissional { get; set; }

        public Prontuario()
        {
        }

        public Prontuario(Paciente paciente, Profissional profissional)
        {
            HipoteseDiagnostico = "";
            EvolucaoClinica = "";
            HistoricoClinico = "";
            Paciente = paciente;
            Profissional = profissional;
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
