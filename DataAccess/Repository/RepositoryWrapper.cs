using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RepositoryWrapper :IRepositoryWrapper
    {
        private DapperContext _repoContext;
        private DtoWrapper _dto;
        private readonly ILoggerService _logger;
        // private IEmployeeRepository _employee;
        // private IHelperRepository _helper;

        private IBusinessAssociate _ba;
        private IDocumentUpload _documentUpload;
        private IPAN_Validation _pan_validation;

        public RepositoryWrapper(DapperContext repoContext, DtoWrapper dto,ILoggerService logger)
        {
            _repoContext = repoContext;
            _dto = dto;
            _logger = logger;
        }

        public IBusinessAssociate Associate
        {
            get
            {
                if (_ba == null)
                {
                    _ba = new BusinessAssociate(_repoContext, _dto);
                }
                return _ba;
            }
        }
     
        public IDocumentUpload documentUpload
        {
            get
            {
                if (_documentUpload == null)
                {
                    _documentUpload = new DocumentUpload(_repoContext, _dto,_logger);
                }
                return _documentUpload;
            }
        }

        public IPAN_Validation pan_Validation
        {
            get
            {
                if (_pan_validation == null)
                {
                    _pan_validation = new PAN_Validation(_repoContext, _dto);
                }
                return _pan_validation;
            }
        }


        public RepositoryWrapper(DapperContext dapperContext)
        {
            _repoContext = dapperContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }

}
