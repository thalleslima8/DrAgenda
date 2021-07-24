using System;
using System.Linq;
using Codout.Framework.Common.Helpers;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Financeiro;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Shared.Enums;
using Codout.Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DrAgenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProfissionalController : RestApiControllerBase<Profissional, ProfissionalDto>
    {
        public ProfissionalController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Profissional ToDomain(ProfissionalDto dto)
        {
            if(string.IsNullOrEmpty(dto.CPF))
                throw new DomainException("CPF não informado!");

            if(dto.Endereco == null)
                throw new DomainException("Endereco inválido");

            if(UnitOfWork.Paciente.Find(x => x.CPF == dto.CPF).Any())
                throw new DomainException("CPF já cadastrado.");

            var domain = GetDomain(dto);
            domain.Nome = dto.Nome;
            domain.CPF = dto.CPF;
            domain.Email = dto.Email;
            domain.Formacao = dto.Formacao;
            domain.Telefone = dto.Telefone;
            
            if(dto.Endereco.Id.HasValue)
                domain.Endereco = UnitOfWork.Endereco.Find(x => x.Id == dto.Endereco.Id).FirstOrDefault();
            else
                domain.Endereco = new Endereco
                {
                    Logradouro = dto.Endereco.Logradouro,
                    Numero = dto.Endereco.Numero,
                    Complemento = dto.Endereco.Complemento,
                    Bairro = dto.Endereco.Bairro,
                    Cep = dto.Endereco.Cep,
                    Municipio = dto.Endereco.Municipio,
                    UF = Enum.Parse<UF>(dto.Endereco.Uf)
                };

            if(domain.Carteira == null)
            {
                var carteira = new Carteira();
                UnitOfWork.Carteria.Save(carteira);
                UnitOfWork.SaveChanges();

                domain.Carteira = carteira;
            }

            return domain;

        }

        protected override ProfissionalDto ToDto(Profissional domain)
        {
            var dto = new ProfissionalDto()
            {
              Id  = domain.Id,
              Endereco = new EnderecoDto
              {
                  Id  = domain.Id,
                  Logradouro = domain.Endereco.Logradouro,
                  Numero = domain.Endereco.Numero,
                  Complemento = domain.Endereco.Complemento,
                  Bairro = domain.Endereco.Bairro,
                  Cep = domain.Endereco.Cep,
                  Municipio = domain.Endereco.Municipio,
                  Uf = domain.Endereco.UF.GetDescription()
              },
              CPF = domain.CPF,
              Email = domain.Email,
              Nome = domain.Nome,
              Formacao = domain.Formacao,
              Telefone = domain.Telefone,
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Profissional.All()
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.CPF,
                    x.Formacao,
                    x.Email,
                    x.Telefone,
                    Endereco = new
                    {
                        x.Endereco.Id,
                        x.Endereco.Logradouro
                    }
                })
                .ToDataSourceResult(request);
        }
    }
}
