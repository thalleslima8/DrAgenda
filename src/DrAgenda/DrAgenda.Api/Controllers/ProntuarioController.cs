using System.Linq;
using DrAgenda.Api.Controllers.Base;
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
    public class ProntuarioController : RestApiControllerBase<Prontuario, ProntuarioDto>
    {
        public ProntuarioController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Prontuario ToDomain(ProntuarioDto dto)
        {
            var domain = GetDomain(dto);
            domain.EvolucaoClinica = dto.EvolucaoClinica;
            domain.HipoteseDiagnostico = dto.HipoteseDiagnostico;
            domain.HistoricoClinico = dto.HistoricoClinico;
            domain.Profissional = UnitOfWork.Profissional.Find(x => x.Id == dto.Profissional.Id).FirstOrDefault();
            domain.Paciente = UnitOfWork.Paciente.Find(x => x.Id == dto.Paciente.Id).FirstOrDefault();
            
            return domain;

        }

        protected override ProntuarioDto ToDto(Prontuario domain)
        {
            var dto = new ProntuarioDto()
            {
              Id  = domain.Id,
              Profissional = new DtoAggregate(domain.Profissional.Id, domain.Profissional.Nome),
              Paciente = new DtoAggregate(domain.Paciente.Id, domain.Paciente.Nome),
              EvolucaoClinica = domain.EvolucaoClinica,
              HipoteseDiagnostico = domain.HipoteseDiagnostico,
              HistoricoClinico = domain.HistoricoClinico,
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Prontuario.All()
                .Select(x => new
                {
                    x.Id,
                    x.EvolucaoClinica,
                    x.HipoteseDiagnostico,
                    x.HistoricoClinico,
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
