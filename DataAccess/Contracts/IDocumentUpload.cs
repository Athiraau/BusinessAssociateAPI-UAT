using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IDocumentUpload
    {
        public Task PostImageUpload(string flag,Docu_convertedBytesDto DocuUpload);
    }
}
