using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.OTP
{
    public class OtpDtoWrapper
    {
        private OTPReqDto _otpreqDto;
        private OTPResDto _otpResDto;
        private VerifyOTPReqDto _verifyOTPReqDto;
        private VerifyOTPResDto _verifyOTPResDto;


        public OTPReqDto otpReq
        {
            get
            {
                if (_otpreqDto == null)
                {
                    _otpreqDto = new OTPReqDto();
                }
                return _otpreqDto;

            }
        }

        public OTPResDto otpRes
        {
            get
            {
                if (_otpResDto == null)
                {
                    _otpResDto = new OTPResDto();
                }
                return _otpResDto;

            }
        }

        public VerifyOTPReqDto verifyotpReq
        {
            get
            {
                if (_verifyOTPReqDto == null)
                {
                    _verifyOTPReqDto = new VerifyOTPReqDto();
                }
                return _verifyOTPReqDto;

            }
        }

        public VerifyOTPResDto verifyotpRes
        {
            get
            {
                if (_verifyOTPResDto == null)
                {
                    _verifyOTPResDto = new VerifyOTPResDto();
                }
                return _verifyOTPResDto;

            }
        }
    }
}
