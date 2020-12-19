using FluentValidation;
using VetClinic.DAL.Entities;

namespace VetClinic.API.Validators
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(service => service.ServiceName).NotEmpty().MaximumLength(50);
        }
    }
}
