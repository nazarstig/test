using FluentValidation;
using VetClinic.API.DTO.Doctor;

namespace VetClinic.API.Validators.Doctor
{
    public class CreateDoctorDtoValidator : AbstractValidator<CreateDoctorDto>
    {
        public CreateDoctorDtoValidator()
        {
            RuleFor(doctor => doctor.PositionId).NotEqual(0).WithMessage("Procedure name cannot be 0");
        }
    }
}
