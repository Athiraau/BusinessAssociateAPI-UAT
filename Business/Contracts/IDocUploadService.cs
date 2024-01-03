using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IDocUploadService
    {
        public Task<dynamic> Post_UploadService(DocumentUploadDto docuUploadDto);


    }
}
