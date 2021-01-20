using VetClinic.API.DTO.User;

namespace VetClinic.API.DTO.Doctor
{
    public class UpdateDoctorDto
    {       
        public string Education { get; set; }
        public string Biography { get; set; }
        public string Experience { get; set; }
        public string Photo { get; set; }
        public int PositionId { get; set; }
        
        public UpdateUserDto User { get; set; }       
    }
}
