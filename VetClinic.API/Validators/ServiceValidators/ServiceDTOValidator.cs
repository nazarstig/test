using FluentValidation;
using VetClinic.API.DTO;

namespace VetClinic.API.Validators
{
    public class ServiceDTOValidator : AbstractValidator<ServiceDTO>
    {
        public ServiceDTOValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().WithMessage("Ім'я сервісу не може бути порожнім");
            RuleFor(service => service.ServiceName).MaximumLength(50).WithMessage("Ім'я сервісу не може бути більшим 50 символів");
        }
    }
}
