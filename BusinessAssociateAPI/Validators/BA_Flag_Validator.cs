using DataAccess.Dto.Request;
using FluentValidation;

namespace BusinessAssociateAPI.Validators
{
    public class BA_Flag_Validator: AbstractValidator<BAFlagCheckDto>
    {
        public BA_Flag_Validator() 
        {
            RuleFor(d => d.p_flag).NotNull().NotEmpty().WithMessage("..Flag is required");
        }
    }
}
