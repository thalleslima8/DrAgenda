using AutoMapper;
using DrAgenda.Shared.Dto.Person;
using DrAgenda.Web.Models.Paciente;

namespace DrAgenda.Web.Helpers
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<PacienteDto, PacienteViewModel>().ReverseMap();
        }
    }
}
