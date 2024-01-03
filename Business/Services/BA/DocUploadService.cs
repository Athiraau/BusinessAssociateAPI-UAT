using Business.Contracts;
using Business.Helpers;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.BA
{
    public class DocUploadService : IDocUploadService
    {
        private IRepositoryWrapper _repo;
        private readonly DtoWrapper _dto;
        private readonly IConfiguration _config;
       // private readonly DocValidationHelper _helper;
        private IServiceHelper _helper;

        public DocUploadService(IRepositoryWrapper repo, DtoWrapper dto, IConfiguration config, IServiceHelper helper)
        {
            _repo=repo;
            _dto = dto;
            _config=config;
            _helper=helper;
            
        }

        public async Task<dynamic>Post_UploadService(DocumentUploadDto docuUploadDto)
        {

            int compressSize = Convert.ToInt32(_config["Image:CompressionSize"]);

            Docu_convertedBytesDto docu_up = new Docu_convertedBytesDto();
            byte[] imageBytes = Convert.FromBase64String(docuUploadDto.Document);

            imageBytes = _helper.ImgVHelper.ReduceImageSize(imageBytes, compressSize);
            
            //educeImageSize(imageBytes, compressSize);

            docu_up.ID_PROOF = imageBytes;
            docu_up.pan_attachment = imageBytes;
            docu_up.cus_photo = imageBytes;

            docu_up.BENEFICIARY_ACCOUNT = docuUploadDto.indata;
            docu_up.phone = docuUploadDto.indata;
            docu_up.PBA_CODE = docuUploadDto.indata;

            var flag = docuUploadDto.flag;


            var ID_Data = _repo.documentUpload.PostImageUpload(flag, docu_up);

           // return ID_Data;
          _dto.baResposeDto.BA_ResposeData = ID_Data;
            return _dto.baResposeDto.BA_ResposeData;
        }
    }
}
