using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DrAgenda.Core.Dominio.Base;
using DrAgenda.Core.Dominio.Person;

namespace DrAgenda.Core.Dominio.Financeiro
{
    public class Carteira : EntityAudit
    {
        public decimal Saldo { get; set; }

        private readonly ISet<Movimento> _movimentos = new HashSet<Movimento>();
        public virtual IReadOnlyCollection<Movimento> Movimentos => new ReadOnlyCollection<Movimento>(_movimentos.ToList());

        public Profissional Profissional { get; set; }
        public Guid ProfissionalId { get; set; }

        public Carteira()
        {
            Saldo = 0;
        }

        public void AdicionaMovimento(Movimento movimento, Paciente paciente)
        {
            if (!_movimentos.Contains(movimento))
            {
                movimento.Carteira = this;
                movimento.Profissional = this.Profissional;
                movimento.Paciente = paciente;
                _movimentos.Add(movimento);
            }
        }

        public void RemoveMovimento(Movimento movimento)
        {
            if (_movimentos.Contains(movimento))
            {
                movimento.Carteira = null;
                movimento.Profissional = null;
                movimento.Paciente = null;
                _movimentos.Remove(movimento);
            }
        }

        public virtual void LimparMovimentos()
        {
            foreach (var item in _movimentos.ToArray())
                RemoveMovimento(item);
        }

        public decimal GetSaldo()
        {
            return Movimentos.Sum(p => p.Valor);
        }

        public decimal GetSaldoNaData(DateTime initial, DateTime final)
        {
            return Movimentos.Where(p => p.Data >= initial && p.Data <= final).Sum(p => p.Valor);
        }
    }
}
