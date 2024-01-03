using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.OTP
{
    public class VerifyOTPReqDto
    {
        public string flag { get; set; } = string.Empty;
      //  public int empId { get; set; } = 0;
        public string OTP { get; set; } = string.Empty;
    }
}
