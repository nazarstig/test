using AutoMapper;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.API.DTO.ClientDTO;

namespace VetClinic.API.Mapping
{
    public class ClientProfile : Profile
    {
        public  ClientProfile()
        {
            CreateMap<CreateClientDto, Client>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
