using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.OTP
{
    public class VerifyOTPResDto
    {
        public int status { get; set; } = 0;
        public string message { get; set; } = string.Empty;
    }
}
