using System.Linq;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.Repository.Helpers;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.ControleAcesso;
using Codout.Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DrAgenda.Api.Controllers.ControleAcesso
{
    [Route("api/v1/acesso-bloqueado-periodo")]
    public class AcessoBloqueadoPeriodoController : RestApiControllerBase<AcessoBloqueadoPeriodo, AcessoBloqueadoPeriodoDto>
    {
        public AcessoBloqueadoPeriodoController(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<AcessoBloqueadoPeriodoController> logger,
            IHttpContextAccessor httpContextAccessor)
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
            
        }

        [NonAction]
        protected override AcessoBloqueadoPeriodo ToDomain(AcessoBloqueadoPeriodoDto dto)
        {
            var domain = GetDomain(dto);
            domain.DataFim = dto.DataFim;
            domain.DataInicio = dto.DataInicio;
            domain.Motivo = dto.Motivo;
            domain.Usuario = UnitOfWork.Usuario.LoadOrDefault(dto.Usuario.Id);
            return domain;
        }

        [NonAction]
        protected override AcessoBloqueadoPeriodoDto ToDto(AcessoBloqueadoPeriodo domain)
            => new()
            {
                Id = domain.Id,
                Usuario = new DtoAggregate { Id = domain.Usuario.Id, Descricao = domain.Usuario.Nome },
                Motivo = domain.Motivo,
                DataFim = domain.DataFim,
                DataInicio = domain.DataInicio
            };

        [NonAction]
        protected override DataSourceResult ToDataSource(DataSourceRequest request)
            => UnitOfWork.AcessoBloqueadoPeriodo
                .All()
                .Select(x => new
                {
                    x.Id,
                    x.DataFim,
                    x.DataInicio,
                    x.Motivo,
                    Usuario = new DtoAggregate { Id = x.Usuario.Id, Descricao = x.Usuario.Nome }
                })
                .ToDataSourceResult(request);

        [HttpPost("to-data-source-usuario")]
        public object ToDataSourceUsuario([FromBody] ConsultarAcessoPeriodoDto dto)
            => UnitOfWork.AcessoBloqueadoPeriodo
                .All()
                .Where(x => x.Usuario.Id == dto.UsuarioId)
                .Select(x => new
                {
                    x.Id,
                    x.DataInicio,
                    x.DataFim,
                    x.Motivo,
                    Usuario = new DtoAggregate { Id = x.Usuario.Id, Descricao = x.Usuario.Nome }
                })
                .ToDataSourceResult(dto.DataSourceRequest);
    }
}