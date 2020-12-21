using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;
using VetClinic.API.DTO.ProcedureDTO;

namespace VetClinic.API.Mapping
{
    public class ProcedureProfile : Profile
    {
        public ProcedureProfile()
        {
            CreateMap<CreateProcedureDTO, Procedure>().
                 ForMember(dto => dto.ProcedureName,
                 opt => opt.MapFrom(src => src.ProcedureName)).
                 ForMember(dto => dto.Description,
                 opt => opt.MapFrom(src => src.Description)).
                 ForMember(dto => dto.Price,
                 opt => opt.MapFrom(src => src.Price)).
                  ForMember(dto => dto.IsSelectable,
                 opt => opt.MapFrom(src => src.IsSelectable));
            
            //CreateMap<DeleteProcedureDTO, Procedure>().
            //     ForMember(dto => dto.Id,
            //     opt => opt.MapFrom(src => src.Id));

            //CreateMap<DeleteProcedureDTO, int>().
            //     ForMember(obj => obj,
            //     opt => opt.MapFrom(src => src.Id));

            CreateMap<ReadProcedureDTO, Procedure>().
                 ForMember(dto => dto.Id,
                 opt => opt.MapFrom(src => src.Id));

            CreateMap<UpdateProcedureDTO, Procedure>().
                 ForMember(dto => dto.Id,
                 opt => opt.MapFrom(src => src.Id)).
                 ForMember(dto => dto.ProcedureName,
                 opt => opt.MapFrom(src => src.ProcedureName)).
                 ForMember(dto => dto.Description,
                 opt => opt.MapFrom(src => src.Description)).
                 ForMember(dto => dto.Price,
                 opt => opt.MapFrom(src => src.Price)).
                  ForMember(dto => dto.IsSelectable,
                 opt => opt.MapFrom(src => src.IsSelectable));
        }
    }
}
