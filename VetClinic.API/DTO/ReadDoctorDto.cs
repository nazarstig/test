namespace VetClinic.API.DTO
{
    public class ReadDoctorDto
    {       
        public string Education { get; set; }
        public string Biography { get; set; }
        public string Experience { get; set; }
        public string Photo { get; set; }
        public int PositionId { get; set; }


        public string UserId { get; set; }
        public ReadUserDto User { get; set; }       
    }
}
