using System.Linq;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Shared.Dto.ControleAcesso;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DrAgenda.Api.Controllers.ControleAcesso
{
    [Route("api/v1/[controller]")]
    public class PerfilAcessoController : RestApiControllerBase<PerfilAcesso, PerfilAcessoDto>
    {
        public PerfilAcessoController(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<PerfilAcessoController> logger,
            IHttpContextAccessor httpContextAccessor) 
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
        }

        protected override PerfilAcesso ToDomain(PerfilAcessoDto dto)
        {
            var domain = GetDomain(dto);
            domain.Nome = dto.Nome;

            domain.RemoverPermissoesAcesso();

            if (dto.PermissoesAcesso != null)
                foreach (var permissaoAcesso in dto.PermissoesAcesso)
                    AddAccessPermissionRecursive(domain, UnitOfWork.PermissaoAcesso.Get(permissaoAcesso.Id));

            return domain;
        }

        protected override PerfilAcessoDto ToDto(PerfilAcesso domain)
            => new PerfilAcessoDto
            {
                Id = domain.Id,
                Nome = domain.Nome,
                PermissoesAcesso = domain.PermissoesAcesso.Select(x => new PerfilAcessoDto.PermissaoAcessoItemDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Acao = x.Acao
                }).ToList()
            };

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
            => UnitOfWork.PerfilAcesso
                .All()
                .Select(x => new
                {
                    x.Id,
                    x.Nome
                })
                .ToDataSourceResult(request);

        #region Metodos Privados

        private void AddAccessPermissionRecursive(PerfilAcesso domain, PermissaoAcesso permissaoAcesso)
        {
            domain.AdicionarPermissaoAcesso(permissaoAcesso);
            if (permissaoAcesso.PermissaoAcessoPai != null)
                AddAccessPermissionRecursive(domain, permissaoAcesso.PermissaoAcessoPai);
        }

        #endregion
    }
}
