using Business.Contracts;
using Business.Helpers;
using Business.Services.BA;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.OTP;
using DataAccess.Dto.Request;
using DataAccess.Entities;
using DataAccess.Repository;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
   public class ServiceWrapper  : IServiceWrapper
    {   
        private IBAService _ibaservice;
        private IDocUploadService _idocuploadservice;
        private readonly IConfiguration _config;
        private readonly DtoWrapper _dto;
        private IJwtUtils _jwtUtils;
        private IServiceHelper _serviceHelper;
        private readonly ILoggerService _logger;
        private readonly ErrorResponse _error;
        private readonly OtpDtoWrapper _otpDto;
        private readonly OTPHelperClass _otpHelperClass;
        // private IBusinessAssociate _ba;
        //private IDocumentUpload _documentUpload;

        private readonly IValidator<BAFlagCheckDto> _baFlagCheckDtoValidator;
        private readonly IValidator<DocumentUploadDto> _DocuUploadValidator;
        //private readonly IValidator<BAPostReqDto> _baPostReqDtoValidator;

        private readonly IRepositoryWrapper _repository;
       //rivate readonly DocValidationHelper _helper;

        public ServiceWrapper(IRepositoryWrapper repository,ILoggerService logger,IConfiguration config,DtoWrapper dto,
            IJwtUtils jwtUtils,IServiceHelper serviceHelper,ErrorResponse error, IValidator<BAFlagCheckDto> baFlagCheckDtoValidator, IValidator<DocumentUploadDto> DocuUploadValidator
            , OtpDtoWrapper otpDto,OTPHelperClass otpHelperClass)
        {
            _logger = logger;
         //   _ibaservice = ibaservice;
          //  _idocuploadservice = docuploadservice;
            _config = config;
            _dto = dto;
            _jwtUtils = jwtUtils;
            _serviceHelper = serviceHelper;
            _repository = repository;
            _baFlagCheckDtoValidator = baFlagCheckDtoValidator;
            _DocuUploadValidator = DocuUploadValidator;
            _otpDto = otpDto;
        _otpHelperClass=otpHelperClass;





    }
        public IJwtUtils JwtUtils
        {
            get
            {
                if (_jwtUtils == null)
                {
                    // _jwtUtils = new JwtUtils(_config, _logger);
                    _jwtUtils = new JwtUtils(_config, _logger);

                }
                return _jwtUtils;
            }
        }
        public IBAService BAService
        {
            get
            {
                if (_ibaservice == null)
                {
                    _ibaservice = new BAService(_repository, _dto,_config, _serviceHelper, _otpDto, _otpHelperClass);
                }
                return _ibaservice;
            }
        }
        public IDocUploadService DocUploadService
        {
            get
            {
                if (_idocuploadservice == null)
                {
                    _idocuploadservice = new DocUploadService(_repository, _dto, _config, _serviceHelper);
                }
                return _idocuploadservice;
            }
        }
        public IServiceHelper ServiceHelper
        {
            get
            {
                if (_serviceHelper == null)
                {
                    _serviceHelper = new ServiceHelper(_error, _dto, _baFlagCheckDtoValidator, _DocuUploadValidator);
                }
                return _serviceHelper;
            }
        }



    }
}
