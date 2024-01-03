using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IPAN_Validation
    {
        public Task<dynamic> validate_PAN(BAPostDataReqDto baPostDataReqDto);

    }
}
