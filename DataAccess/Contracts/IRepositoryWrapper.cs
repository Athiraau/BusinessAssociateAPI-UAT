using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IRepositoryWrapper
    {
        IBusinessAssociate Associate { get; }
        IDocumentUpload documentUpload { get; }
        IPAN_Validation pan_Validation { get; } 
        void Save();
    }
}
