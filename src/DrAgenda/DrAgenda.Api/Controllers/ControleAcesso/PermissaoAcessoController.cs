using System;
using System.Linq;
using System.Threading.Tasks;
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
using NHibernate.Linq;

namespace DrAgenda.Api.Controllers.ControleAcesso
{
    [Route("api/v1/permissao-acesso")]
    public class PermissaoAcessoController : RestApiControllerBase<PermissaoAcesso, PermissaoAcessoDto>
    {
        public PermissaoAcessoController(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<PermissaoAcessoController> logger,
            IHttpContextAccessor httpContextAccessor) 
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
        }

        protected override PermissaoAcesso ToDomain(PermissaoAcessoDto dto)
        {
            var domain = GetDomain(dto);
            domain.Nome = dto.Nome;
            domain.Acao = dto.Acao;
            domain.PermissaoAcessoPai = UnitOfWork.PermissaoAcesso.LoadOrDefault(dto.PermissoesAcessoPai?.Id);
            return domain;
        }

        [HttpGet("obter-por-acao/{acao}")]
        public async Task<IActionResult> ObterPorAcao(string acao)
        {
            try
            {
                var domain = await UnitOfWork.PermissaoAcesso.Find(x => x.Acao == acao).FirstOrDefaultAsync();

                return Ok(domain == null ? null : ToDto(domain));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("obter-filhos/{paiId:guid?}")]
        public async Task<IActionResult> ObterFilhos(Guid? paiId)
        {
            try
            {
                var items = await UnitOfWork.PermissaoAcesso
                .Find(x => x.PermissaoAcessoPai.Id == paiId)
                .Select(x => ToDto(x))
                .ToListAsync();

                return Ok(items);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        protected override PermissaoAcessoDto ToDto(PermissaoAcesso domain)
            => new PermissaoAcessoDto
            {
                Id = domain.Id,
                Nome = domain.Nome,
                Acao = domain.Acao,
                PermissoesAcessoPai = new DtoAggregate(domain.PermissaoAcessoPai?.Id, domain.PermissaoAcessoPai?.Nome),
                PermissoesAcesso = domain.PermissoesAcesso.Select(x => new DtoAggregate { Id = x.Id, Descricao = x.Nome }).ToList()
            };

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
            => UnitOfWork.PermissaoAcesso
                .All()
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Acao
                })
                .ToDataSourceResult(request);
    }
}
