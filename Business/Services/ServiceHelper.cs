using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
   public class ServiceHelper : IServiceHelper
    {
        private ValidationHelper _vHelper;
        private DocValidationHelper _DVHelper;
        
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<BAFlagCheckDto> _ba_flag_Validator;
        private readonly IValidator<DocumentUploadDto> _DocuValidator;
       
        public ServiceHelper(ErrorResponse error, DtoWrapper dto, IValidator<BAFlagCheckDto> ba_flag_Validator, IValidator<DocumentUploadDto> DocuValidator)
        {
            _error = error;
            _dto = dto;
            _ba_flag_Validator = ba_flag_Validator;
            _DocuValidator= DocuValidator;
           
           
        }
        public ValidationHelper VHelper
        {
            get
            {
                if (_vHelper == null)
                {
                    _vHelper = new ValidationHelper(_error, _dto, _ba_flag_Validator);
                }
                return _vHelper;
            }
        }
        public DocValidationHelper ImgVHelper
        {
            get
            {
                if (_DVHelper == null)
                {
                    _DVHelper = new DocValidationHelper(_error, _dto, _DocuValidator);
                }
                return _DVHelper;
            }
        }
     



    }
}
