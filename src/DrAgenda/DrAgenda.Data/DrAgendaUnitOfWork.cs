﻿using Codout.Framework.DAL;
using Codout.Framework.NH;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Core.IRepository;
using DrAgenda.Data.Repository;

namespace DrAgenda.Data
{
    public class DrAgendaUnitOfWork : NHUnitOfWork, IDrAgendaUnitOfWork
    {
        private readonly RepositoryFactory _factory;

        public DrAgendaUnitOfWork(ITenant tenant) : base(tenant: tenant)
        {
            _factory = new RepositoryFactory(Session);
        }

        static DrAgendaUnitOfWork()
        {
            RepositoryFactory.RegisterRepository<IPacienteRepository, PacienteRepository, Paciente>();
            RepositoryFactory.RegisterRepository<IProfissionalRepository, ProfissionalRepository, Profissional>();
            RepositoryFactory.RegisterRepository<IEnderecoRepository, EnderecoRepository, Endereco>();
            RepositoryFactory.RegisterRepository<ICarteiraRepository, CarteiraRepository, Carteira>();
            RepositoryFactory.RegisterRepository<IMovimentoRepository, MovimentoRepository, Movimento>();
            RepositoryFactory.RegisterRepository<IConsultaRepository, ConsultaRepository, Consulta>();
            RepositoryFactory.RegisterRepository<IProntuarioRepository, ProntuarioRepository, Prontuario>();
        }

        public IPacienteRepository Paciente => _factory.Get<IPacienteRepository>();
        public IProfissionalRepository Profissional => _factory.Get<IProfissionalRepository>();
        public IEnderecoRepository Endereco => _factory.Get<IEnderecoRepository>();
        public IMovimentoRepository Movimento => _factory.Get<IMovimentoRepository>();
        public ICarteiraRepository Carteria => _factory.Get<ICarteiraRepository>();
        public IConsultaRepository Consulta => _factory.Get<IConsultaRepository>();
        public IProntuarioRepository Prontuario => _factory.Get<IProntuarioRepository>();

        protected override void Dispose(bool disposing)
        {
            _factory?.Dispose();
            base.Dispose(disposing);
        }
    }
}
