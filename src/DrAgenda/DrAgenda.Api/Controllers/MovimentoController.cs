using System;
using System.Linq;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.Financeiro;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DrAgenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class MovimentoController : RestApiControllerBase<Movimento, MovimentoDto>
    {
        public MovimentoController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Movimento ToDomain(MovimentoDto dto)
        {
            if(dto.Valor == 0)
                throw new DomainException("Valor inválido");

            if(dto.Data > DateTime.Now)
                throw new DomainException("Horário inválido");

            if(dto.Profissional == null || !dto.Profissional.Id.HasValue)
                throw new DomainException("Profissional inválido.");
            
            var profissional = UnitOfWork.Profissional.Find(x => x.Id == dto.Profissional.Id).FirstOrDefault();

            if(profissional == null)
                throw new DomainException("Profissional não encontrado.");

            if(dto.Paciente == null || !dto.Paciente.Id.HasValue)
                throw new DomainException("Paciente inválido.");

            var paciente = UnitOfWork.Paciente.Find(x => x.Id == dto.Paciente.Id).FirstOrDefault();

            if(paciente == null)
                throw new DomainException("Paciente não encontrado.");

            if(dto.Consulta == null || !dto.Consulta.Id.HasValue)
                throw new DomainException("Consulta inválida.");
            
            var consulta = UnitOfWork.Consulta.Find(x => x.Id == dto.Consulta.Id).FirstOrDefault();

            if(consulta == null)
                throw new DomainException("Consulta não encontrada.");


            var domain = GetDomain(dto);
            domain.Profissional = profissional;
            domain.Paciente = paciente;
            domain.Carteira = profissional.Carteira;
            domain.Valor = dto.Valor;
            domain.Data = dto.Data;
            domain.Consulta = consulta;
            
            return domain;

        }

        protected override MovimentoDto ToDto(Movimento domain)
        {
            var dto = new MovimentoDto()
            {
              Id  = domain.Id,
              Carteira = new DtoAggregate(domain.Carteira.Id, ""),
              Profissional = new DtoAggregate(domain.Profissional.Id, domain.Profissional.Nome),
              Paciente = new DtoAggregate(domain.Paciente.Id, domain.Paciente.Nome),
              Consulta = new DtoAggregate(domain.Consulta.Id, $"Profissional: {domain.Consulta.Profissional.Nome}, Paciente: {domain.Consulta.Paciente.Nome}"),
              Valor = domain.Valor,
              Data = domain.Data
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Movimento.All()
                .Select(x => new
                {
                    x.Id,
                    Profissional = new DtoAggregate(x.Profissional.Id, x.Profissional.Nome),
                    Paciente = new DtoAggregate(x.Paciente.Id, x.Paciente.Nome),
                    Consulta = new DtoAggregate(x.Consulta.Id, $"Profissional: {x.Consulta.Profissional.Nome}, Paciente: {x.Consulta.Paciente.Nome}"),
                    Valor = x.Valor,
                    Data = x.Data
                })
                .ToDataSourceResult(request);
        }
    }
}
