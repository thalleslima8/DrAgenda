using System;
using System.Linq;
using Codout.Framework.Common.Helpers;
using DrAgenda.Api.Controllers.Base;
using DrAgenda.Api.Helpers;
using DrAgenda.Core;
using DrAgenda.Core.Dominio.Person;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Shared.Enums;
using Kendo.DynamicLinq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DrAgenda.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PacienteController : RestApiControllerBase<Paciente, PacienteDto>
    {
        public PacienteController(IWebHostEnvironment hostingEnvironment, IDrAgendaUnitOfWork unitOfWork) : base(hostingEnvironment, unitOfWork)
        {
        }

        protected override Paciente ToDomain(PacienteDto dto)
        {
            if(string.IsNullOrEmpty(dto.CPF))
                throw new DomainException("CPF não informado!");

            if(UnitOfWork.Paciente.Find(x => x.CPF == dto.CPF).Any())
                throw new DomainException("CPF já cadastrado.");

            var domain = GetDomain(dto);
            domain.Nome = dto.Nome;
            domain.CPF = dto.CPF;
            domain.Email = dto.Email;
            domain.Profissao = dto.Profissao;
            domain.Telefone = dto.Telefone;
            domain.Status = dto.StatusPaciente;
            
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
            
            return domain;

        }

        protected override PacienteDto ToDto(Paciente domain)
        {
            var dto = new PacienteDto()
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
              Profissao = domain.Profissao,
              StatusPaciente = domain.Status,
              Telefone = domain.Telefone,
              Consultas = domain.Consultas.Select(x => new DtoAggregate
              {
                  Id = x.Id,
                  Descricao = $"Paciente: {x.Paciente.Nome}, Profissional: {x.Profissional.Nome}, Horário: {x.Horario}"
              }).ToList(),
              Profissionais = domain.Profissionais.Select(x => new DtoAggregate
              {
                  Id = x.Id,
                  Descricao = $"{x.Nome} - {x.Formacao}"
              }).ToList(),
              Prontuarios = domain.Prontuarios.Select(x => new DtoAggregate
              {
                  Id = x.Id,
                  Descricao = x.ToString()
              }).ToList()
            };
            return dto;
           
        }

        protected override DataSourceResult ToDataSource(DataSourceRequest request)
        {
            return UnitOfWork.Paciente.All()
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.CPF,
                    x.Status,
                    x.Profissao,
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
