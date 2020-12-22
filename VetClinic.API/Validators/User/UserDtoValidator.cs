using FluentValidation;
using VetClinic.API.DTO.User;

namespace VetClinic.API.Validators.User
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Нікнейм не може бути порожнім");
            RuleFor(user => user.UserName).MaximumLength(50).WithMessage("Нікнейм не може бути довшим 50 символів");
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Ім'я не може бути порожнім");
            RuleFor(user => user.FirstName).MaximumLength(30).WithMessage("Ім'я не може бути довше 30 символів");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Прізвище не може бути порожнім");
            RuleFor(user => user.LastName).MaximumLength(30).WithMessage("Прізвище не може бути довше 30 символів");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email не може бути порожнім");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Email невірного формату");
            RuleFor(user => user.Email).MaximumLength(50).WithMessage("Email не може бути довше 50 символів");
            RuleFor(user => user.PhoneNumber).NotEmpty().WithMessage("Телефон не може бути порожнім");
            RuleFor(user => user.PhoneNumber).MaximumLength(12).WithMessage("Телефон не може бути довше 12 символів");
        }
    }
}
