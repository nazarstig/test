using FluentValidation;
using VetClinic.API.DTO.Appointments;

namespace VetClinic.API.Validators.Appointments
{
    public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentDtoValidator()
        {
            RuleFor(a => a.AnimalId)
                .NotEmpty().WithMessage("Should not be empty");

            RuleFor(a => a.ServiceId)
                .NotEmpty().WithMessage("Should not be empty");

            RuleFor(a => a.StatusId)
                .NotEmpty().WithMessage("Should not be empty");

            RuleFor(a => a.DoctorId)
                .NotEmpty().WithMessage("Should not be empty");

            RuleFor(a => a.ProceduresIds)
                .NotNull().WithMessage("Should not be null");

            RuleFor(a => a.AppointmentDate)
                .NotEmpty().WithMessage("Should not be empty");

            RuleFor(a => a.Complaints)
                .NotEmpty().WithMessage("Should not be empty")
                .MaximumLength(100).WithMessage("Should contain less than 100 characters");

            RuleFor(a => a.TreatmentDescription)
                .MaximumLength(500).WithMessage("Should contain less than 100 characters");
        }
    }
}