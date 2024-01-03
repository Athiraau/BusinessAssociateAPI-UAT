using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IBusinessAssociate
    {
        public Task<dynamic> GetBA_Data(string flag, string indata);

        public Task<dynamic> Get_Document(string flag,string indata);
        public Task<dynamic> PostBA_Data(BAPostDataReqDto baPostDataReqDto);
       // public Task<dynamic> PostBA_GenerateOtp(int Length);

    }
}
