using DataAccess.Dto.Request;
using FluentValidation;

namespace BusinessAssociateAPI.Validators
{
    public class DocumentValidator:AbstractValidator<DocumentUploadDto>
    {
        public DocumentValidator()
        {
            //RuleFor(d => d.secCode).NotNull().GreaterThan(0).WithMessage("Security Code must be greater than 0");
            RuleFor(d => d.Document).NotNull().NotEmpty().WithMessage("ID Proof required.!!");
           
        }

       
    }
}
