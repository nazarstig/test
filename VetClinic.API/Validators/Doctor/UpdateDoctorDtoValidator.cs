using FluentValidation;
using VetClinic.API.DTO.Doctor;
using VetClinic.API.Validators.User;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.API.Validators.Doctor
{
    public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorDtoValidator(IPositionService positionService, IUserService userService)
        {            
            RuleFor(doctor => doctor.Biography).MaximumLength(200).WithMessage("Biography cannot be longer than 200 characters");
            RuleFor(doctor => doctor.Education).MaximumLength(100).WithMessage("Biography cannot be longer than 100 characters");
            RuleFor(doctor => doctor.PositionId).MustAsync((id, exist) => positionService.IsAnyPositionAsync(id)).WithMessage("Position with selected Id does not exist");
            RuleFor(doctor => doctor).Must(newDoctor =>
            {
                var oldUser = userService.GetUser(newDoctor.UserId).Result;
                if (oldUser == null)
                {
                    return false;
                }
                return !userService.UserNameExistsAsync(newDoctor.User.UserName).Result || oldUser.UserName == newDoctor.User.UserName;
            })
                .WithMessage("Username already exists");

            RuleFor(doctor => doctor.User).SetValidator(new UserDtoValidator());
        }
    }
}
