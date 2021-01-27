using AutoMapper;
using VetClinic.DAL.Entities;
using VetClinic.API.DTO.ProcedureDTO;

namespace VetClinic.API.Mapping
{
    public class ProcedureProfile : Profile
    {
        public ProcedureProfile()
        {
            CreateMap<CreateProcedureDto, Procedure>().
                 ForMember(d => d.ProcedureName,
                 opt => opt.MapFrom(src => src.ProcedureName)).
                 ForMember(d => d.Description,
                 opt => opt.MapFrom(src => src.Description)).
                 ForMember(d => d.Price,
                 opt => opt.MapFrom(src => src.Price));
            // ForMember(d => d.IsSelectable,
            //opt => opt.MapFrom(src => src.IsSelectable));

            CreateMap<Procedure, ReadProcedureDto>().
                  ForMember(d => d.Id,
                 opt => opt.MapFrom(src => src.Id)).
                 ForMember(d => d.ProcedureName,
                 opt => opt.MapFrom(src => src.ProcedureName)).
                 ForMember(d => d.Description,
                 opt => opt.MapFrom(src => src.Description)).
                 ForMember(d => d.Price,
                 opt => opt.MapFrom(src => src.Price));
            // ForMember(d => d.IsSelectable,
            //opt => opt.MapFrom(src => src.IsSelectable));

            CreateMap<UpdateProcedureDto, Procedure>().
                 ForMember(d => d.ProcedureName,
                 opt => opt.MapFrom(src => src.ProcedureName)).
                 ForMember(d => d.Description,
                 opt => opt.MapFrom(src => src.Description)).
                 ForMember(d => d.Price,
                 opt => opt.MapFrom(src => src.Price));
                 // ForMember(d => d.IsSelectable,
                 //opt => opt.MapFrom(src => src.IsSelectable));
        }
    }
}
