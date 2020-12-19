using FluentValidation;
using VetClinic.API.DTO.Role;

namespace VetClinic.API.Validators.Role
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(role => role.Name).NotEmpty().WithMessage("Ім'я ролі не повинно бути порожнім");
            RuleFor(role => role.Name).MaximumLength(32).WithMessage("Ім'я ролі не повинно бути довше 32 символів");
        }
    }
}
