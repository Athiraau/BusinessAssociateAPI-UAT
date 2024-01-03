using DataAccess.Dto.OTP;
using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
  public interface IBAService
    {
        public Task<dynamic> GetBA_DataService(string flag, string indata);
        public Task<dynamic> Get_Document(string flag ,string indata);
        public Task<dynamic> PostBA_DataService(BAPostDataReqDto baPostDataReqDto);
        public Task<OTPResDto> SendOTP(OTPReqDto otpReq);
        public Task<VerifyOTPResDto> VerifyOTP(VerifyOTPReqDto verifyotpReq);
        public Task<dynamic> PAN_Validation(BAPostDataReqDto baPostDataReqDto);

    }
}
