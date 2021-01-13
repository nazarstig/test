using System.Collections.Generic;
using VetClinic.API.DTO.Doctor.SecondaryDto;
using VetClinic.API.DTO.User;

namespace VetClinic.API.DTO.Doctor
{
    public class ReadDoctorDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public string Education { get; set; }
        public string Biography { get; set; }
        public string Experience { get; set; }
        public string Photo { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public IEnumerable<AppointmentDto> Appointments { get; set; }
    }
}
