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
        private readonly ISet<Movimento> _movimentos = new HashSet<Movimento>();

        public virtual decimal Saldo { get; set; }
        public virtual IReadOnlyCollection<Movimento> Movimentos => new ReadOnlyCollection<Movimento>(_movimentos.ToList());

        public virtual Profissional Profissional { get; set; }

        public Carteira()
        {
            Saldo = 0;
        }

        public virtual void AdicionaMovimento(Movimento movimento, Paciente paciente)
        {
            if (!_movimentos.Contains(movimento))
            {
                movimento.Carteira = this;
                movimento.Profissional = this.Profissional;
                movimento.Paciente = paciente;
                _movimentos.Add(movimento);
            }
        }

        public virtual void RemoveMovimento(Movimento movimento)
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

        public virtual decimal GetSaldo()
        {
            return Movimentos.Sum(p => p.Valor);
        }

        public virtual IEnumerable<Movimento> GetMovimentos()
        {
            return _movimentos.ToList();
        }

        public virtual decimal GetSaldoNaData(DateTime initial, DateTime final)
        {
            return Movimentos.Where(p => p.Data >= initial && p.Data <= final).Sum(p => p.Valor);
        }
    }
}
