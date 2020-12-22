using FluentValidation;
using VetClinic.API.DTO;

namespace VetClinic.API.Validators
{
    public class ServiceDTOValidator : AbstractValidator<ServiceDTO>
    {
        public ServiceDTOValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().WithMessage("The service name can not be empty")
                .MaximumLength(50).WithMessage("The service name can not be longer than 50 characters");
        }
    }
}
