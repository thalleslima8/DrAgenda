using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Core.Dominio.Person
{
    public class Profissional : BasePerson
    {
        private readonly ISet<Consulta> _consultas = new HashSet<Consulta>();
        private readonly ISet<Paciente> _pacientes = new HashSet<Paciente>();

        public virtual IReadOnlyCollection<Consulta> Consultas => new ReadOnlyCollection<Consulta>(_consultas.ToList());
        public virtual IReadOnlyCollection<Paciente> Pacientes => new ReadOnlyCollection<Paciente>(_pacientes.ToList());
        
        public virtual Carteira Carteira { get; set; }

        [Required]
        public virtual Formacao Formacao { get; set; }

        public Profissional()
        {
        }

        public virtual IEnumerable<Paciente> GetPacientes()
        {
            return Pacientes;
        }

        public virtual List<Consulta> GetConsultas()
        {
            return Consultas.OrderBy(p => p.Horario).ToList();
        }


        #region Consultas

        public virtual void AdicionaConsulta(Consulta consulta, Paciente paciente)
        {
            if (_consultas.Contains(consulta)) return;

            consulta.Profissional = this;
            consulta.Paciente = paciente;
            _consultas.Add(consulta);
        }

        public virtual void RemoveConsulta(Consulta consulta)
        {
            if (!_consultas.Contains(consulta)) return;

            consulta.Profissional = null;
            consulta.Paciente = null;
            _consultas.Remove(consulta);
        }

        public virtual void LimparConsultas()
        {
            foreach (var item in _consultas.ToArray())
                RemoveConsulta(item);
        }

        #endregion

        #region Pacientes

        public virtual void AdicionaPaciente(Paciente paciente)
        {
            if (_pacientes.Contains(paciente)) return;

            paciente.AdicionaProfissional(this);
            _pacientes.Add(paciente);
        }

        public virtual void RemovePaciente(Paciente paciente)
        {
            if (!_pacientes.Contains(paciente)) return;

            paciente.RemoveProfissional(this); 
            _pacientes.Remove(paciente);
        }

        public virtual void LimparPacientes()
        {
            foreach (var item in _pacientes.ToArray())
                RemovePaciente(item);
        }

        #endregion
    }
}
