using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Core.Dominio.Person
{
    public class Paciente : BasePerson
    {
        private readonly ISet<Consulta> _consultas = new HashSet<Consulta>();
        private readonly ISet<Prontuario> _prontuarios = new HashSet<Prontuario>();
        private readonly ISet<Profissional> _profissionais = new HashSet<Profissional>();
        public virtual IReadOnlyCollection<Consulta> Consultas => new ReadOnlyCollection<Consulta>(_consultas.ToList());
        public virtual IReadOnlyCollection<Prontuario> Prontuarios => new ReadOnlyCollection<Prontuario>(_prontuarios.ToList());
        public virtual IReadOnlyCollection<Profissional> Profissionais => new ReadOnlyCollection<Profissional>(_profissionais.ToList());
        public virtual StatusPaciente Status { get; set; }

        [Required(ErrorMessage = "Profissão obrigatório!")]
        [StringLength(30, ErrorMessage = "Máximo de 30 caracteres")]
        public virtual string Profissao { get; set; }

        public Paciente()
        {
            Status = StatusPaciente.Ativo;
        }

        #region Consultas

        public virtual void AdicionaConsulta(Consulta consulta)
        {
            if (_consultas.Contains(consulta)) return;

            consulta.Paciente = this;
            _consultas.Add(consulta);
        }

        public virtual void RemoveConsulta(Consulta consulta)
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

        #endregion

        #region Prontuarios

        public virtual void AdicionaProntuario(Prontuario prontuario)
        {
            if (_prontuarios.Contains(prontuario)) return;

            prontuario.Paciente = this;
            _prontuarios.Add(prontuario);
        }

        public virtual void RemoveProntuario(Prontuario prontuario)
        {
            if (!_prontuarios.Contains(prontuario)) return;

            prontuario.Paciente = null;
            _prontuarios.Remove(prontuario);
        }

        public virtual void LimparProntuarios()
        {
            foreach (var item in _prontuarios.ToArray())
                RemoveProntuario(item);
        }

        #endregion

        #region Profissionais

        public virtual void AdicionaProfissional(Profissional profissional)
        {
            if (_profissionais.Contains(profissional)) return;

            profissional.AdicionaPaciente(this);
            _profissionais.Add(profissional);
        }

        public virtual void RemoveProfissional(Profissional profissional)
        {
            if (!_profissionais.Contains(profissional)) return;

            profissional.RemovePaciente(this);
            _profissionais.Remove(profissional);
        }

        public virtual void LimparProfissionais()
        {
            foreach (var item in _profissionais.ToArray())
                RemoveProfissional(item);
        }

        #endregion
    }
}
