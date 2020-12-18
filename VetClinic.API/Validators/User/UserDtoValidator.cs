using FluentValidation;
using VetClinic.API.DTO.User;

namespace VetClinic.API.Validators.User
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().MaximumLength(50);
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(30);
            RuleFor(user => user.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(user => user.PhoneNumber).NotEmpty().MaximumLength(12);
        }
    }
}
