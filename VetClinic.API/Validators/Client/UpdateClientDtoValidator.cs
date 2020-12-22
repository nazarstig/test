using FluentValidation;
using FluentValidation.Validators;
using VetClinic.API.DTO.ClientDto;

namespace VetClinic.API.Validators.Client
{
    public class UpdateClientDtoValidator : AbstractValidator<UpdateClientDto>
    {
        public UpdateClientDtoValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Username cannot be empty")
                .MaximumLength(50).WithMessage("Username cannot be more than 50 symbols");

            RuleFor(user => user.FirstName).NotEmpty().WithMessage("FirstName cannot be empty")
                .MaximumLength(30).WithMessage("FirstName cannot be more than 30 symbols");

            RuleFor(user => user.LastName).NotEmpty().WithMessage("SecondName cannot be empty")
                .MaximumLength(30).WithMessage("SecondName cannot be more than 30 symbols");

            RuleFor(user => user.Email).NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email format is incorrect")
                .MaximumLength(50).WithMessage("Email cannot be longer than 50 symbols");

            RuleFor(user => user.PhoneNumber).NotEmpty().WithMessage("Phone cannot be empty")
                .MaximumLength(12).WithMessage("Phone number cannot be longer than 12 numbers")
                .Matches("^[0-9]").WithMessage("Phone number can contain numbers");

            RuleFor(user => user.Password).NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(6).WithMessage("Password is too small")
                .MaximumLength(300).WithMessage("Password is too long");
        }
    }
}
