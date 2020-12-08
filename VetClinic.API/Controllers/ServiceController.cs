using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.Service;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ServiceController(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetService()
        {
            //var service = CreateService();
            //var serviceDTO = _mapper.Map<ServiceDTO>(service);
            var services = CreateServices();
            var servicesDTO = _mapper.Map<List<ServiceDTO>>(services);
            return Ok(servicesDTO);
        }

        public static Service CreateService()
        {
            var service = new Service()
            {
                Id = 1,
                ServiceName = "SomeService",
            };

            var appointments = new List<Appointment>() { new Appointment { Id = 1, AppointmentDate = DateTime.Now, Service = service, ServiceId = service.Id } };
            service.Appointments = appointments;
            if (service.Appointments != null)
            {
                service.Appointments = null;
            }
            
            return service;
        }

        public static List<Service> CreateServices()
        {
            return new List<Service>()
            {
                new Service
                {
                    Id = 1,
                    ServiceName = "SomeService",
                    Appointments = new List<Appointment>() { new Appointment {Id = 1, AppointmentDate = DateTime.Now } }
                },
                new Service
                {
                    Id = 2,
                    ServiceName = "SomeService2",
                    Appointments = new List<Appointment>() { new Appointment {Id = 2, AppointmentDate = DateTime.Now.AddDays(4) } }
                },
                new Service
                {
                    Id = 3,
                    ServiceName = "SomeService3",
                    Appointments = new List<Appointment>() { new Appointment {Id = 3, AppointmentDate = DateTime.Now.AddDays(3) } }
                }
            };
        }
    }
}
