using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Shared;

namespace DrAgenda.Core.Dominio.Person
{
    public class Profissional : BasePerson
    {
        private readonly ISet<Consulta> _consultas = new HashSet<Consulta>();
        private readonly ISet<Paciente> _pacientes = new HashSet<Paciente>();

        public virtual IReadOnlyCollection<Consulta> Consultas => new ReadOnlyCollection<Consulta>(_consultas.ToList());
        public virtual IReadOnlyCollection<Paciente> Pacientes => new ReadOnlyCollection<Paciente>(_pacientes.ToList());
        
        public Carteira Carteira { get; set; }
        [Required]
        public Formacao Formacao { get; set; }

        public Profissional()
        {
            Carteira = new Carteira();
        }

        public IEnumerable<Paciente> GetPacientes()
        {
            return Pacientes;
        }

        public List<Consulta> GetConsultas()
        {
            return Consultas.OrderBy(p => p.Horario).ToList();
        }
    }
}
