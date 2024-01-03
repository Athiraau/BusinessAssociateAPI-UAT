using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Contracts;
using Business.Contracts;
using Business.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using DataAccess.Context;
using Dapper;
using DataAccess.Dto.Request;
using FluentValidation;
using Business.Helpers;
using DataAccess.Entities;
using DataAccess.Dto;
using System.Xml.Linq;
using Microsoft.AspNetCore.JsonPatch.Internal;
using XAct.Messages;
using DataAccess.Dto.OTP;


namespace BusinessAssociateAPI.Controllers

{// [Authorize]
    [AllowAnonymous]
    [Route("api/BA")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class BusiAssocController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly ServiceHelper _serviceHelper;
        private readonly DapperContext _context;
        private readonly IValidator<DocumentUploadDto> _docuValidator;
         // private readonly DocValidationHelper _helper;

        public BusiAssocController(IValidator<DocumentUploadDto> docuValidator,DapperContext context, ILoggerService logger, IServiceWrapper service, ServiceHelper serviceHelper)
        {
            _logger = logger;
            _service = service;
            _serviceHelper = serviceHelper;
            _context = context;
            _docuValidator = docuValidator;
            
            
        }

        [HttpGet("GetBA_Data/{flag}/{indata}", Name = "GetBA_Data")]
        public async Task<IActionResult> GetBA_Data([FromRoute] string flag, string indata)
        {
            var errorRes = _serviceHelper.VHelper.Validate_BA_Data(flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }
            var punchdata = await _service.BAService.GetBA_DataService(flag, indata);
            if (punchdata == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                _logger.LogInfo($"Returned details of data required to load filter for flag: {flag}");

                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }

        [HttpPost("PostBA_Data", Name = "PostBA_Data")]
        public async Task<IActionResult> PostBA_Data([FromBody] BAPostDataReqDto baPostDataReqDto)
        {
            var errorRes = _serviceHelper.VHelper.Validate_BA_Data(baPostDataReqDto.flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }
            var punchdata = await _service.BAService.PostBA_DataService(baPostDataReqDto);
            if (punchdata == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                _logger.LogInfo($"Returned details of data required to load filter for flag: {baPostDataReqDto.flag}");

                return Ok(JsonConvert.SerializeObject(punchdata));

            }

        }

        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromBody] DocumentUploadDto docuUpload)
        {
            // ID PROOF VALIDATION  START-----------------------------------------
            var errorRes = _serviceHelper.ImgVHelper.ValidateImage(docuUpload);

            
            if (docuUpload is null)
            {
                _logger.LogError(" No attachment found..!!");
                return BadRequest("Image object is null");
            }
            else if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid image details sent from client.");
                return BadRequest(errorRes);
            }
            // ID PROOF CODE VALIDATION  END------------

            var docudata = await _service.DocUploadService.Post_UploadService(docuUpload);

            if (docudata == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                _logger.LogInfo($"Returned response data after saving early going req: {docudata}");

                  return Ok(JsonConvert.SerializeObject(docudata));
               
               // var message = "Document Successfully Uploaded..!";

                //return Ok(JsonConvert.SerializeObject(message));
             
               

            }
        }

        [HttpGet("GetDocument/{flag}/{indata}", Name = "GetDocument")]
        public async Task<IActionResult> GetDocument([FromRoute] string flag, string indata)
        {
            var errorRes = _serviceHelper.VHelper.Validate_BA_Data(flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }
            // ID PROOF CODE VALIDATION  END------------




            var docudata = await _service.BAService.Get_Document(flag,indata);
            if (docudata == null)
            {
                _logger.LogError($" Document could not be returned in db.");

                return NotFound();

            }
         
            else
            {
                _logger.LogInfo($"Returned response data (ID Proof ) of Customer data of  {indata}");

                return Ok(JsonConvert.SerializeObject(docudata));
            }

        }

        [HttpPost("SendBA_OTP", Name = "SendBA_OTP")]
        public async Task<IActionResult> SendBA_OTP([FromBody] OTPReqDto otpReq)
        {
            var errorRes = _serviceHelper.VHelper.Validate_BA_Data(otpReq.flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data/flag  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }
            else
            {
                var otpdata = await _service.BAService.SendOTP(otpReq);
            
                 if (otpdata == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
          
                _logger.LogInfo($"Returned details of data required to load filter for flag");

                return Ok(JsonConvert.SerializeObject(otpdata));

            }

        }

        [HttpPost("VerifyBA_OTP", Name = "VerifyBA_OTP")]
        public async Task<IActionResult> VerifyBA_OTP([FromBody] VerifyOTPReqDto verifyotpReq)
        {
            var errorRes = _serviceHelper.VHelper.Validate_BA_Data(verifyotpReq.flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data/flag  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }
            else
            {
                var otpdata = await _service.BAService.VerifyOTP(verifyotpReq);

                if (otpdata == null)
                {
                    _logger.LogError($"Details of filter data could not be returned in db.");

                    return NotFound();

                }

                _logger.LogInfo($"Returned details of data required to load filter for flag");

                return Ok(JsonConvert.SerializeObject(otpdata));

            }

        }

        [HttpPost("PAN_Validation", Name = "PAN_Validation")]
        public async Task<IActionResult> PAN_Validation([FromBody] BAPostDataReqDto baPostDataReqDto)
        {
            var errorRes = _serviceHelper.VHelper.Validate_BA_Data(baPostDataReqDto.flag);
            if (errorRes.Result.errorMessage.Count > 0)
            {
                _logger.LogError("Invalid/wrong request data  sent from client.");
                return BadRequest(errorRes.Result.errorMessage);
            }

             var res = await _service.BAService.PAN_Validation(baPostDataReqDto);
            
            if (res == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();

            }
            else
            {
                _logger.LogInfo($"Returned details of data required to load filter for flag: {baPostDataReqDto.flag}");

                return Ok(JsonConvert.SerializeObject(res));

            }

        }
    }
}
