using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Shared;

namespace DrAgenda.Core.Dominio.Person
{
    public class Paciente : BasePerson
    {
        private readonly ISet<Consulta> _consultas = new HashSet<Consulta>();
        public virtual IReadOnlyCollection<Consulta> Consultas => new ReadOnlyCollection<Consulta>(_consultas.ToList());

        public Endereco Endereco { get; set; }

        public Profissional Profissional { get; set; }

        public StatusPaciente Status { get; set; }
        public Prontuario Prontuario { get; set; }

        [Required(ErrorMessage = "Profissão obrigatório!")]
        [StringLength(30, ErrorMessage = "Máximo de 30 caracteres")]
        public string Profissao { get; set; }

        public Paciente()
        {
            Status = StatusPaciente.Ativo;
            Prontuario = new Prontuario();
        }

        public void AdicionaConsulta(Consulta consulta)
        {
            if (_consultas.Contains(consulta)) return;

            consulta.Paciente = this;
            _consultas.Add(consulta);
        }

        public void RemoveConsulta(Consulta consulta)
        {
            if (!_consultas.Contains(consulta)) return;

            consulta.Paciente = null;
            _consultas.Remove(consulta);
        }

        public virtual void LimparConsultas()
        {
            foreach (var item in _consultas.ToArray())
                RemoveConsulta(item);
        }
        
    }
}
