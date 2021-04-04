using System.Linq;
using System.Linq.Dynamic.Core;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Shared.Dto.Base;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;

namespace DrAgenda.Api.Controllers.Base
{
    public class PacienteController : RestApiControllerBase<Paciente, PacienteDto>
    {
        public PacienteController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Paciente ToDomain(PacienteDto dto)
        {
            var domain = GetDomain(dto);
            domain.Nome = dto.Nome;
            domain.CPF = dto.CPF;
            domain.Email = dto.Email;
            domain.Profissao = dto.Profissao;
            domain.Telefone = dto.Telefone;
            domain.Endereco = DynamicQueryableExtensions.FirstOrDefault(UnitOfWork.Endereco.Find(x => x.Id == dto.Endereco.Id));
            domain.Status = dto.StatusPaciente;
            
            return domain;

        }

        protected override PacienteDto ToDto(Paciente domain)
        {
            var dto = new PacienteDto()
            {
              Id  = domain.Id,
              Endereco = new DtoAggregate(domain.Endereco.Id, domain.Endereco.ToString()),
              CPF = domain.CPF,
              Email = domain.Email,
              Nome = domain.Nome,
              Profissao = domain.Profissao,
              StatusPaciente = domain.Status,
              Telefone = domain.Telefone,
              Consultas = domain.Consultas.Select(x => new DtoAggregate
              {
                  Id = x.Id,
                  Descricao = $"Paciente: {x.Paciente.Nome}, Profissional: {x.Profissional.Nome}, Horário: {x.Horario}"
              }).ToList(),
              Profissionais = domain.Profissionais.Select(x => new DtoAggregate
              {
                  Id = x.Id,
                  Descricao = $"{x.Nome} - {x.Formacao}"
              }).ToList(),
              Prontuarios = domain.Prontuarios.Select(x => new DtoAggregate
              {
                  Id = x.Id,
                  Descricao = x.ToString()
              }).ToList()
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
