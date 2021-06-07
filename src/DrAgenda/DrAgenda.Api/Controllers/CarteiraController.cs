using System.Linq;
using DrAgenda.Api.Controllers.Base;
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
    public class CarteiraController : RestApiControllerBase<Carteira, CarteiraDto>
    {
        public CarteiraController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Carteira ToDomain(CarteiraDto dto)
        {
            var domain = GetDomain(dto);
            domain.Saldo = dto.Saldo;
            
            domain.LimparMovimentos();

            return domain;

        }

        protected override CarteiraDto ToDto(Carteira domain)
        {
            var dto = new CarteiraDto()
            {
              Id  = domain.Id,
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Carteria.All()
                .Select(x => new
                {
                    x.Id,
                    x.Saldo
                })
                .ToDataSourceResult(request);
        }
    }
}
