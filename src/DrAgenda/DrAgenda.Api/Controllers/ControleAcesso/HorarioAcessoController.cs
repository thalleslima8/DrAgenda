using System.Linq;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.Repository.Helpers;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.ControleAcesso;
using DrAgenda.Shared.Enums;
using Codout.Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DrAgenda.Api.Controllers.ControleAcesso
{
    [Route("api/v1/horario-acesso")]
    public class HorarioAcessoController : RestApiControllerBase<HorarioAcesso, HorarioAcessoDto>
    {
        public HorarioAcessoController(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<HorarioAcessoController> logger,
            IHttpContextAccessor httpContextAccessor) 
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
        }

        protected override HorarioAcesso ToDomain(HorarioAcessoDto dto)
        {
            var domain = GetDomain(dto);
            domain.DiaSemana = (DiaSemana)dto.DiaSemana;
            domain.HoraFim = dto.HoraFim;
            domain.HoraInicio = dto.HoraInicio;
            domain.Usuario = UnitOfWork.Usuario.LoadOrDefault(dto.Usuario.Id);
            return domain;
        }

        protected override HorarioAcessoDto ToDto(HorarioAcesso domain)
            => new HorarioAcessoDto
            {
                Id = domain.Id,
                Usuario = new DtoAggregate { Id = domain.Usuario.Id, Descricao = domain.Usuario.Nome },
                DiaSemana = domain.DiaSemana,
                HoraFim = domain.HoraFim,
                HoraInicio = domain.HoraInicio
            };

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
            => UnitOfWork.HorarioAcesso
                    .All()
                    .Select(x => new
                    {
                        x.Id,
                        x.DiaSemana,
                        x.HoraFim,
                        x.HoraInicio,
                        Usuario = new DtoAggregate {Id = x.Usuario.Id, Descricao = x.Usuario.Nome}
                    })
                    .ToDataSourceResult(request);

        [HttpPost("to-data-source-usuario")]
        public object ToDataSourceUsuario([FromBody] ConsultarAcessoPeriodoDto dto)
            => UnitOfWork.HorarioAcesso
                .All()
                .Where(x => x.Usuario.Id == dto.UsuarioId)
                .Select(x => new
                {
                    x.Id,
                    x.DiaSemana,
                    x.HoraFim,
                    x.HoraInicio,
                    Usuario = new DtoAggregate { Id = x.Usuario.Id, Descricao = x.Usuario.Nome }
                })
                .ToDataSourceResult(dto.DataSourceRequest);
    }
}