using FluentValidation;
using VetClinic.API.DTO.ProcedureDTO;

namespace VetClinic.API.Validators.Procedure
{
    public class CreateProcedureDtoValidator : AbstractValidator<CreateProcedureDTO>
    {
        public CreateProcedureDtoValidator()
        {
            RuleFor(proc => proc.ProcedureName).NotEmpty().WithMessage("Ім'я процедури не може бути порожнім")
                .MaximumLength(100).WithMessage("Нікнейм не може бути довшим ніж 100 символів");

            RuleFor(proc => proc.Description).NotEmpty().WithMessage("Опис процедури не може бути порожнім");

            RuleFor(proc => proc.Price).GreaterThan(0M).WithMessage("Ціна не може бути від'ємною");

        }
    }
}
