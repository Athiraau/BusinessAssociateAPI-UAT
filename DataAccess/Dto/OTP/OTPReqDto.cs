using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.OTP
{
    public class OTPReqDto
    {
        public string flag { get; set; } = string.Empty;
        //  public int empId { get; set; } = 0;
        public int accId { get; set; } = 0;
        public string mobileNo { get; set; } = string.Empty;
        
        //  public string BAname { get; set; } = string.Empty;

    }
}
