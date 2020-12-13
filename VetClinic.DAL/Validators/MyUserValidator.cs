using FluentValidation;
using VetClinic.DAL.Entities;

namespace VetClinic.DAL.Validators
{
    public class MyUserValidator : AbstractValidator<User>
    {
        public MyUserValidator()
        {
            RuleFor(user => user.UserName).NotNull().MaximumLength(50);
            RuleFor(user => user.Email).NotNull().EmailAddress().MaximumLength(50);
            RuleFor(user => user.NormalizedEmail).EmailAddress().MaximumLength(50);
            RuleFor(user => user.PhoneNumber).NotNull().MaximumLength(12);
            RuleFor(user => user.PasswordHash).NotNull().MaximumLength(300);
            RuleFor(user => user.MyRoles).NotNull();
        }
    }
}
