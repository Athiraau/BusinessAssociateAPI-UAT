using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Response
{
    public class BA_ResponseDto
    {
        public dynamic BA_ResposeData { get; set; } = string.Empty;

        public string Result { get; set; } = string.Empty;
    }
}
