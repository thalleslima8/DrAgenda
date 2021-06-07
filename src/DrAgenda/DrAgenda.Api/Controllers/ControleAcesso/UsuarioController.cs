using System;
using System.Linq;
using System.Threading.Tasks;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.Repository.Helpers;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.ControleAcesso;
using DrAgenda.Shared.Dto.Model;
using DrAgenda.Shared.Enums;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;

namespace DrAgenda.Api.Controllers.ControleAcesso
{
    [Route("api/v1/[controller]")]
    public class UsuarioController : RestApiControllerBase<Usuario, UsuarioDto>
    {
        public UsuarioController(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<UsuarioController> logger,
            IHttpContextAccessor httpContextAccessor) 
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(UsuarioDto))]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {

            try
            {
                var usuario = await UnitOfWork.Usuario.GetAsync(x => x.NomeUsuario.Trim().ToLower() == model.UserName.Trim().ToLower());

                Logger.LogInformation("Authenticando usuario {0}", usuario);

                if (usuario == null)
                    return Unauthorized();

                if (usuario.Autenticar(model.Password))
                {
                    if (!usuario.Admin)
                    {
                        var dataAtual = DateTime.Now;
                        var diaSemana = (DiaSemana)dataAtual.DayOfWeek;

                        if (usuario.AcessosBloqueadoPeriodos.Any(c => dataAtual >= c.DataInicio && dataAtual <= c.DataFim))
                            return Unauthorized();

                        if (usuario.HorariosAcesso.Any() &&
                            !usuario.HorariosAcesso.Any(c =>
                                c.DiaSemana == diaSemana
                                && dataAtual.TimeOfDay >= c.HoraInicio
                                && dataAtual.TimeOfDay <= c.HoraFim))
                        {
                            return BadRequest("Horário de acesso não permitido!");
                        }
                    }

                    return Ok(ToDto(usuario));
                }

                return Unauthorized("Login ou senha inválidos");
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, exception.Message);
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public override async Task<IActionResult> Post([FromBody] UsuarioDto dto)
        {
            if (UnitOfWork.Usuario.Find(x => x.NomeUsuario.Trim().ToLower() == dto.NomeUsuario.Trim().ToLower()).Any())
                throw new DomainException("Nome de usário já cadastrado");

            if (UnitOfWork.Usuario.Find(x => x.Email.Trim().ToLower() == dto.Email.Trim().ToLower()).Any())
                throw new DomainException("E-mail já cadastrado");

            return await base.Post(dto);
        }

        [NonAction]
        protected override Usuario ToDomain(UsuarioDto dto)
        {
            var domain = GetDomain(dto);
            domain.Admin = dto.Admin;
            domain.Nome = dto.Nome;
            domain.NomeUsuario = dto.NomeUsuario;
            domain.Email = dto.Email;
            domain.Inativo = dto.Inativo;
            domain.Telefone = dto.Telefone;

            if (!dto.Id.HasValue || dto.AlterarSenha)
                domain.DefinirSenha(dto.Senha ?? string.Empty);

            domain.LimparPerfisAcesso();
            dto.PerfisAcesso.ToList().ForEach(x =>
                {
                    domain.AdicionarPerfilAcesso(UnitOfWork.PerfilAcesso.LoadOrDefault(x.Id));
                });

            return domain;
        }

        [NonAction]
        protected override UsuarioDto ToDto(Usuario domain)
            => new()
            {
                Id = domain.Id,
                Admin = domain.Admin,
                Nome = domain.Nome,
                NomeUsuario = domain.NomeUsuario,
                Email = domain.Email,
                Inativo = domain.Inativo,
                Telefone = domain.Telefone,
                PerfisAcesso = domain.PerfilAcessos.Select(x => new PerfilAcessoDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    PermissoesAcesso = x.PermissoesAcesso.Select(p => new PerfilAcessoDto.PermissaoAcessoItemDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Acao = p.Acao
                    }).ToList()
                }).ToList(),
            };

        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Usuario
                    .All()
                    .Select(x => new UsuarioDto
                    {
                        Id = x.Id,
                        Admin = x.Admin,
                        Nome = x.Nome,
                        NomeUsuario = x.NomeUsuario,
                        Email = x.Email,
                        Inativo = x.Inativo,
                        Telefone = x.Telefone,
                        CodigoAgente = x.CodigoAgente
                    }).ToListAsync();

                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [NonAction]
        protected override DataSourceResult ToDataSource(DataSourceRequest request)
            => UnitOfWork.Usuario
                .All()
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.NomeUsuario,
                    x.Email,
                    x.Admin,
                    x.Inativo,
                    x.Telefone,
                    x.CodigoAgente
                })
                .ToDataSourceResult(request);

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                    return null;

                var domain = UnitOfWork.Usuario.All()
                    .FetchMany(x => x.PerfilAcessos)
                    .ThenFetchMany(x => x.PermissoesAcesso)
                    .FirstOrDefault(x => x.Id == id);

                if (domain == null)
                    return NotFound();

                return Ok(ToDto(domain));
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpPost("alterar-senha")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public virtual async Task<IActionResult> AlterarSenha(AlterarSenhaDto dto)
        {
            try
            {
                var domain = UnitOfWork.Usuario.Get(dto.Id);

                if (domain == null)
                    return BadRequest($"Usuário id:{dto.Id} não existe");

                if (!domain.Autenticar(dto.SenhaAtual))
                    return BadRequest("Senha atual inválida");

                domain.DefinirSenha(dto.NovaSenha);

                UnitOfWork.SaveChanges();

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}