using FluentValidation;
using VetClinic.API.DTO.Service;

namespace VetClinic.API.Validators.ServiceValidators
{
    public class ServiceCreateDTOValidator : AbstractValidator<ServiceCreateDTO>
    {
        public ServiceCreateDTOValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().MaximumLength(50);
        }
    }
}
