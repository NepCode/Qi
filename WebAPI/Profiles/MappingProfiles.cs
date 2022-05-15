using AutoMapper;
using BusinessLogic.Data;
using WebAPI.DTO.PersonaFisica;

namespace WebAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmpleadoCreateDTO, Empleado>();
            CreateMap<EmpleadoUpdateDTO, Empleado>();
        }
    
    }
}
