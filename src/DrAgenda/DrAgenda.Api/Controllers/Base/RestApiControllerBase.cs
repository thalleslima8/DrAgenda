using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Codout.Framework.Api;
using Codout.Framework.DAL.Entity;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using DrAgenda.Data.Repository.Helpers;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.Helpers;
using DrAgenda.Shared.Dto.Model;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;

namespace DrAgenda.Api.Controllers.Base
{
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthorizeFilter))]
    public abstract class RestApiControllerBase<TDomain, TDto> : ApiControllerBase, IApi<TDto, Guid?>
        where TDomain : class, IEntity<Guid?>, new()
        where TDto : DtoBase, new()
    {
        protected RestApiControllerBase(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) 
            : base(hostingEnvironment, unitOfWork)
        {
        }

        protected RestApiControllerBase(IWebHostEnvironment hostingEnvironment, 
            IDrAgendaUnitOfWork unitOfWork,
            ILogger<ApiControllerBase> logger,
            IHttpContextAccessor httpContextAccessor) 
            : base(hostingEnvironment, unitOfWork, logger, httpContextAccessor)
        {
        }

        [NonAction]
        protected abstract TDomain ToDomain(TDto dto);

        [NonAction]
        protected abstract TDto ToDto(TDomain domain);
        
        [NonAction]
        protected abstract DataSourceResult ToDataSource(DataSourceRequest request);

        [NonAction]
        protected TDomain GetDomain(TDto dto) => UnitOfWork.Repository<TDomain>().GetOrCreateInstance(dto.Id);

        [NonAction]
        protected virtual Expression<Func<TDomain, SearchResultItemDto>> SelectorSearchItemDto()
        {
            return x => new SearchResultItemDto { Id = x.Id.ToString() };
        }

        [NonAction]
        protected virtual IQueryable<TDomain> SelectorSearchQuery(object[] parameters)
        {
            return UnitOfWork.Repository<TDomain>().All();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                var result = await UnitOfWork.Repository<TDomain>()
                    .All()
                    .Select(x => ToDto(x))
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpGet("page={page}&size={size}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public virtual async Task<IActionResult> Get(int page, int size)
        {
            try
            {
                var data = UnitOfWork.Repository<TDomain>().All();

                var result = new PagedResult<TDto>(data.Select(x => ToDto(x)), page, size);

                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpPost("datasource")]
        public virtual object DataSource([FromBody]DataSourceRequest dataSourceRequest)
        {
            return ToDataSource(dataSourceRequest);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public virtual async Task<IActionResult> Get(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                    return NotFound();

                var domain = await UnitOfWork.Repository<TDomain>().GetAsync(id);

                if (domain == null)
                    return NotFound();

                return Ok(ToDto(domain));
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [NonAction]
        public virtual void AfterSave(TDomain domain){ }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public virtual async Task<IActionResult> Post([FromBody]TDto value)
        {
            try
            {
                if (value == null)
                    return BadRequest($"O objeto[{nameof(TDto)}] não deve ser nulo.");

                TDomain domain;

                try
                {
                    domain = ToDomain(value);
                }
                catch (DomainException e)
                {
                    return BadRequest(e.Message);
                }

                await UnitOfWork.Repository<TDomain>().SaveAsync(domain);

                UnitOfWork.SaveChanges();

                AfterSave(domain);

                return Ok(ToDto(domain));
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public virtual async Task<IActionResult> Put(Guid? id, [FromBody]TDto dto)
        {
            try
            {
                if (!id.HasValue)
                    return NotFound();

                var domain = await UnitOfWork.Repository<TDomain>().GetAsync(id);

                if (domain == null)
                    return NotFound();

                dto.Id = id;

                try
                {
                    domain = ToDomain(dto);
                }
                catch (DomainException e)
                {
                    return BadRequest(e.Message);
                }

                UnitOfWork.SaveChanges();

                return Ok(ToDto(domain));
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public virtual async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                    return NotFound();

                UnitOfWork.Repository<TDomain>().Delete(await UnitOfWork.Repository<TDomain>().GetAsync(id));

                UnitOfWork.SaveChanges();
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpPost("sync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroDto))]
        public virtual async Task Sync([FromBody]IEnumerable<TDto> value)
        {
            var ids = await UnitOfWork.Repository<TDomain>().All().Select(x => new { x.Id }).ToListAsync();

            foreach (var item in value)
            {
                var exists = ids.FirstOrDefault(x => x.Id == item.Id);

                if (exists != null)
                    await Put(exists.Id, item);
                else
                    await Post(item);
            }
        }

        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public SearchResultDto Search(SearchDto dto)
        {
            var query = SelectorSearchQuery(dto.Parameters).Select(SelectorSearchItemDto());
                
            if (!string.IsNullOrWhiteSpace(dto.SearchTerm))
                query = query.Where(x => x.Text.Contains(dto.SearchTerm.ToUpper()));

            var items = query
                .OrderBy(p => p.Text)
                .Skip((dto.PageNum - 1) * dto.PageSize)
                .Take(dto.PageSize);

            return new SearchResultDto
            {
                TotalCount = items.Count(),
                Items = items.ToList()
            };
        }

    }
}