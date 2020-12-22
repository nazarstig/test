using FluentValidation;
using VetClinic.API.DTO.Role;

namespace VetClinic.API.Validators.Role
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(role => role.Name).NotEmpty().WithMessage("Role name cannot be empty");
            RuleFor(role => role.Name).MaximumLength(32).WithMessage("Role name cannot be longer than 32 characters");
        }
    }
}
