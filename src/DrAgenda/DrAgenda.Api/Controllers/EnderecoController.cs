using System;
using System.Linq;
using Codout.Framework.Common.Helpers;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Shared.Enums;
using Codout.Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DrAgenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class EnderecoController : RestApiControllerBase<Endereco, EnderecoDto>
    {
        public EnderecoController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Endereco ToDomain(EnderecoDto dto)
        {
            var domain = GetDomain(dto);
            domain.Logradouro = dto.Logradouro;
            domain.Numero = dto.Numero;
            domain.Complemento = dto.Complemento;
            domain.Bairro = dto.Bairro;
            domain.Cep = dto.Cep;
            domain.Municipio = dto.Municipio;
            
            domain.UF = Enum.Parse<UF>(dto.Uf);
            
            return domain;

        }

        protected override EnderecoDto ToDto(Endereco domain)
        {
            var dto = new EnderecoDto()
            {
              Id  = domain.Id,
              Logradouro = domain.Logradouro,
              Numero = domain.Numero,
              Complemento = domain.Complemento,
              Bairro = domain.Bairro,
              Cep = domain.Cep,
              Municipio = domain.Municipio,
              Uf = domain.UF.GetDescription()
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Endereco.All()
                .Select(x => new
                {
                    x.Id,
                    x.Logradouro,
                    x.Numero,
                    x.Complemento,
                    x.Bairro,
                    x.Cep,
                    x.Municipio,
                    x.UF
                })
                .ToDataSourceResult(request);
        }
    }
}
