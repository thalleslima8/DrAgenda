using System;
using System.Linq;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Operacional;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.Operacional;
using Codout.Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DrAgenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ConsultaController : RestApiControllerBase<Consulta, ConsultaDto>
    {
        public ConsultaController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Consulta ToDomain(ConsultaDto dto)
        {
            if(dto.Valor == 0)
                throw new DomainException("Valor inválido");

            if(dto.Horario < DateTime.Now)
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

            var domain = GetDomain(dto);
            domain.Horario = dto.Horario;
            domain.Status = dto.Status;
            domain.Valor = dto.Valor;
            domain.Profissional = profissional;
            domain.Paciente = paciente;
            
            return domain;
            
        }

        protected override ConsultaDto ToDto(Consulta domain)
        {
            var dto = new ConsultaDto()
            {
              Id  = domain.Id,
              Profissional = new DtoAggregate(domain.Profissional.Id, domain.Profissional.Nome),
              Paciente = new DtoAggregate(domain.Paciente.Id, domain.Paciente.Nome),
              Status = domain.Status,
              Valor = domain.Valor,
              Horario = domain.Horario,
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Consulta.All()
                .Select(x => new
                {
                    x.Id,
                    x.Horario,
                    x.Status,
                    x.Valor,
                    Paciente = new
                    {
                        x.Paciente.Id,
                        x.Paciente.Nome
                    },
                    Profissional = new
                    {
                        x.Profissional.Id,
                        x.Profissional.Nome
                    }
                })
                .ToDataSourceResult(request);
        }
    }
}
