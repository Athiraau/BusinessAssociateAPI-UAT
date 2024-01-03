using Business.Contracts;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto.OTP;
using Business.Helpers;
using System.Drawing;
using System.Xml.Linq;

namespace Business.Services.BA
{
    class Global
    {
        public static string OTP_value;

    }
    public class BAService : IBAService
    {
        private readonly DtoWrapper _dto;
        private readonly IRepositoryWrapper _repo;
        private IServiceHelper _helper;
        private readonly IConfiguration _config;
        private readonly OtpDtoWrapper _otpDto;
        private readonly OTPHelperClass _otpHelperClass;
        private readonly PAN_Validation pAN_Validation;

        public BAService(IRepositoryWrapper repo, DtoWrapper dto, IConfiguration config, IServiceHelper helper, OtpDtoWrapper otpDto, OTPHelperClass otpHelperClass)

        {
            _repo = repo;
            _dto = dto;
            _config = config;
            _helper = helper;
            _otpDto = otpDto;
            _otpHelperClass = otpHelperClass;
        }

        public async Task<dynamic> GetBA_DataService(string flag, string indata)
        {
            var BA_data = await _repo.Associate.GetBA_Data(flag, indata);
            // return BA_data;

            _dto.baResposeDto.BA_ResposeData = BA_data;
            return _dto.baResposeDto;

        }
        public async Task<dynamic> PostBA_DataService(BAPostDataReqDto baPostDataReqDto)
        {
            var BA_data = await _repo.Associate.PostBA_Data(baPostDataReqDto);
            // return BA_data;

            _dto.baResposeDto.BA_ResposeData = BA_data;
            return _dto.baResposeDto;

        }

        public async Task<dynamic> Get_Document(string flag, string indata)
        {
            byte[] blob = await _repo.Associate.Get_Document(flag, indata);

            if (blob != null)
            {
                int decompressSize = Convert.ToInt32(_config["Image:DecompressionSize"]);

                blob = _helper.ImgVHelper.IncreaseImageSize(blob, decompressSize);
                _dto.imgRes.response = Convert.ToBase64String(blob);

                return _dto.imgRes;
            }
            else
            {
                // _logger.LogError("Invalid/wrong request data  sent from client.");
                return 0;
            }

        }

        public async Task<OTPResDto> SendOTP(OTPReqDto otpReq)
        {
            if (otpReq.flag == "send_ba_otp")
            {

                //Generate OTP
                var otpLength = Convert.ToInt32(_config["OTPLength"]);
                var OTP = _otpHelperClass.GenerateOTP(otpLength);

                 Global.OTP_value = OTP;

            //Create SendSMS request
            string mobileNo = otpReq.mobileNo;
            string SMSContent = "", smsResult = "";
            //  string BAname = otpReq.BAname;

            SMSContent = "Dear Customer,Your OTP for Manappuram eService portal is " + OTP + ". (Usable only once and valid for 5 mins.) DO NOT SHARE WITH ANY ONE. Manappuram";
            

            smsResult = _otpHelperClass.SolutionInfiniSend(otpReq.accId, mobileNo, SMSContent);

            if (smsResult != null)
            {
                //   _dto.EmpOTPResponse.status = (int)SaveOTPStatus.Success;
                _otpDto.otpRes.OTPLife = 1;
                _otpDto.otpRes.message = "SMS sent successfully";

            }
            else
            {
                _otpDto.otpRes.status = 0;
                _otpDto.otpRes.message = "Save OTP to corresponding employee failed";
            }



            }
            else
            {
                _otpDto.verifyotpRes.status = 0;
                _otpDto.verifyotpRes.message = "Invalid Flag..!!";
            }

            return _otpDto.otpRes;
        }


        public async Task<VerifyOTPResDto> VerifyOTP(VerifyOTPReqDto verifyotpReq)
        {
            //comparing with the global otp variable

            if (verifyotpReq.flag == "verify_ba_otp")
            {

                if (verifyotpReq.OTP == Global.OTP_value)
                {
                    _otpDto.verifyotpRes.status = 1;
                    _otpDto.verifyotpRes.message = "OTP verified successfully..!!";

                }
                else
                {
                    _otpDto.verifyotpRes.status = 0;
                    _otpDto.verifyotpRes.message = "OTP verification failed..!!";
                }
            }
            else
            {
                _otpDto.verifyotpRes.status = 0;
                _otpDto.verifyotpRes.message = "Invalid Flag..!!";
            }

            return _otpDto.verifyotpRes;


        }


        public async Task<dynamic> PAN_Validation(BAPostDataReqDto baPostDataReqDto)
        {
            var pan_data = await _repo.pan_Validation.validate_PAN(baPostDataReqDto);
            // return BA_data;

            if (pan_data == 1)
            {
                _dto.baResposeDto.BA_ResposeData = pan_data;
                _dto.baResposeDto.Result = "PAN number is Valid..!!";

            }
            else if (pan_data==2)
            {
                _dto.baResposeDto.BA_ResposeData = pan_data;
                _dto.baResposeDto.Result = "flag is invalid..!!";

            }

            else
            {
                _dto.baResposeDto.BA_ResposeData = pan_data;
                _dto.baResposeDto.Result = "PAN number is Invalid..!!";

            }

            return _dto.baResposeDto;

        }

    }
}
