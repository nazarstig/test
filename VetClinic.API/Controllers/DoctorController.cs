using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(IDoctorService doctorService, IMapper mapper)
        {
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<DoctorDto>>> Index()
        {
            (var doctors, var users) = await _doctorService.GetDoctorAsync();
            var doctorsDto = _mapper.Map<ICollection<DoctorDto>>(doctors);
            return Ok(doctorsDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDto>> Show(int id)
        {
            (Doctor doctor, User user) = await _doctorService.GetDoctorAsync(id);
            DoctorDto doctorDto = _mapper.Map<DoctorDto>(doctor);
            return Ok(doctorDto);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> Create(Doctor doctor)
        {
            (var createdDoctor, var createdUser) =await _doctorService.AddDoctor(doctor, doctor.User);
            DoctorDto doctorDto = _mapper.Map<DoctorDto>(createdDoctor);
            return doctorDto;
        }
    }
}
