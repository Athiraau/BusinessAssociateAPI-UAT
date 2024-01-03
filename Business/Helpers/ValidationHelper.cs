using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class ValidationHelper
    {
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<BAFlagCheckDto> _baFlagDtoValidator;


        public ValidationHelper(ErrorResponse error, DtoWrapper dto, IValidator<BAFlagCheckDto> baFlagDtoValidator)
        {
            _error = error;
            _dto = dto;
            _baFlagDtoValidator = baFlagDtoValidator;
   
        }

        public async Task<ErrorResponse> Validate_BA_Data(string flag)
        {
            ErrorResponse errorRes = null;

            _dto.baFlagCheckDto.p_flag = flag;
            var validationResult = await _baFlagDtoValidator.ValidateAsync(_dto.baFlagCheckDto);

            errorRes = ReturnErrorRes(validationResult);

            return errorRes;
        }

        




        public ErrorResponse ReturnErrorRes(FluentValidation.Results.ValidationResult Res)

        {
            List<string> errors = new List<string>();
            foreach (var row in Res.Errors.ToArray())
            {
                errors.Add(row.ErrorMessage.ToString());
            }
            _error.errorMessage = errors;
            return _error;
        }


    }

}
