using FluentValidation;
using VetClinic.API.DTO.Appointments;

namespace VetClinic.API.Validators.Appointments
{
    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(a => a.AnimalId)
                .NotEmpty()
                .WithMessage("Should not be empty");

            RuleFor(a => a.ServiceId)
                .NotEmpty()
                .WithMessage("Should not be empty");

            RuleFor(a => a.AppointmentDate)
                .NotEmpty()
                .WithMessage("Should not be empty");

            RuleFor(a => a.Complaints)
                .NotEmpty().WithMessage("Should not be empty")
                .MaximumLength(100).WithMessage("Should contain less than 100 characters");
        }
    }
}