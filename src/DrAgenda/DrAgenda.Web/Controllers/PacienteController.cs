using AutoMapper;
using DrAgenda.Api.Client;
using DrAgenda.Api.Client.Apis.Person;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Web.Controllers.Base;
using DrAgenda.Web.Helpers;
using DrAgenda.Web.Models.Paciente;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;

namespace DrAgenda.Web.Controllers
{
    public class PacienteController : CrudController<PacienteDto, PacienteViewModel, PacienteApi>
    {
        private readonly IMapper _mapper;

        public PacienteController(IWebHostEnvironment webHostEnvironment,
            DrAgendaService apiClient,
            IMapper mapper,
            AppUserState appUserState,
            IToastNotification toastNotification)
            : base(webHostEnvironment, apiClient, appUserState, toastNotification)
        {
            _mapper = mapper;
        }


        public override string Title => "Pacientes";
        public override PacienteViewModel ToModel(PacienteDto dto)
        {
            var model = _mapper.Map<PacienteDto, PacienteViewModel>(dto);
            return model;
        }

        public override PacienteDto ToDto(PacienteViewModel model)
        {
            var dto = _mapper.Map<PacienteViewModel, PacienteDto>(model);
            return dto;
        }
    }
}
