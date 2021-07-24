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
    [Route("api/v1/log-acesso")]
    public class LogAcessoController : RestApiControllerBase<LogAcesso, LogAcessoDto>
    {
        public LogAcessoController(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<LogAcessoController> logger,
            IHttpContextAccessor httpContextAccessor) 
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
        }

        protected override LogAcesso ToDomain(LogAcessoDto dto)
        {
            var domain = GetDomain(dto);
            domain.DataHoraAcesso = dto.DataHoraAcesso;
            domain.HostPublicoAcesso = dto.HostPublicoAcesso;
            domain.CaminhoUrl = dto.CaminhoUrl;
            domain.HostLocalAcesso = dto.HostLocalAcesso;
            domain.UserAgent = dto.UserAgent;
            domain.MethodRequest = dto.MethodRequest;
            domain.BodyRequest = dto.BodyRequest;
            domain.Usuario = UnitOfWork.Usuario.LoadOrDefault(dto.Usuario.Id);
            return domain;
        }

        protected override LogAcessoDto ToDto(LogAcesso domain)
            => new LogAcessoDto
            {
                Id = domain.Id,
                Usuario = new DtoAggregate { Id = domain.Usuario.Id, Descricao = domain.Usuario.Nome },
                DataHoraAcesso = domain.DataHoraAcesso,
                HostPublicoAcesso = domain.HostPublicoAcesso,
                CaminhoUrl = domain.CaminhoUrl,
                UserAgent = domain.UserAgent,
                MethodRequest = domain.MethodRequest,
                HostLocalAcesso = domain.HostLocalAcesso,
                BodyRequest = domain.BodyRequest
            };

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

        [HttpPost("data-source-table-filtro-log-acesso")]
        public virtual object DataSourceTableFiltroLogAcesso([FromBody] FiltroLogAcessoDto dto)
        {
            var query = UnitOfWork.LogAcesso.All();

            if (dto.DataHoraInicial.HasValue)
                query = query.Where(c => c.DataHoraAcesso >= dto.DataHoraInicial);

            if (dto.DataHoraFinal.HasValue)
                query = query.Where(c => c.DataHoraAcesso <= dto.DataHoraFinal);

            if (dto.UsuarioId.HasValue)
                query = query.Where(c => c.Usuario.Id == dto.UsuarioId);

            return query.Select(c => new
            {
                c.Id,
                c.DataHoraAcesso,
                c.CaminhoUrl,
                c.MethodRequest,
                c.HostLocalAcesso,
                c.HostPublicoAcesso,
                c.UserAgent,
                c.BodyRequest,
                Usuario = new
                {
                    Id = c.Usuario.Id,
                    Descricao = c.Usuario.Nome
                }
            }).ToDataSourceResult(dto.SourceRequest);
        }
    }
}