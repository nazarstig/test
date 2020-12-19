using FluentValidation;
using VetClinic.API.DTO.Service;

namespace VetClinic.API.Validators.ServiceValidators
{
    public class ServiceUpdateDTOValidator : AbstractValidator<ServiceUpdateDTO>
    {
        public ServiceUpdateDTOValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().MaximumLength(50);
        }
    }
}
