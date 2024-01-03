using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class DtoWrapper
    {
        private BAFlagCheckDto _baFlagCheckDto;
        private BAPostReqDto _baPostReqDto;
        private BA_ResponseDto _baResponseDto;
        private BAPostDataReqDto _baPostDataReqDto;
        private ImgDto _imgDto;
        private BA_imageRes _imgRes;

        public BA_imageRes imgRes
        {
            get
            {
                if (_imgRes == null)
                {
                    _imgRes = new BA_imageRes();
                }
                return _imgRes;

            }
        }
        public BAFlagCheckDto baFlagCheckDto
        {
            get
            {
                if (_baFlagCheckDto == null)
                {
                    _baFlagCheckDto = new BAFlagCheckDto();
                }
                return _baFlagCheckDto;

            }
        }

        public BAPostDataReqDto baPostDataReqDto
        {
            get
            {
                if (_baPostDataReqDto == null)
                {
                    _baPostDataReqDto = new BAPostDataReqDto();
                }
                return _baPostDataReqDto;

            }
        }

        public BAPostReqDto baPostReqDto
        {
            get
            {
                if (_baPostReqDto==null)
                {
                    _baPostReqDto = new BAPostReqDto();
                }
                return _baPostReqDto;
                
            }
        }

        public BA_ResponseDto baResposeDto
        {
            get
            {
                if (_baResponseDto == null)
                {
                    _baResponseDto = new BA_ResponseDto();
                }
                return _baResponseDto;

            }
        }

        public ImgDto imgDto
        {
            get
            {
                if (_imgDto == null)
                {
                    _imgDto = new ImgDto();
                }
                return _imgDto;

            }
        }
    }
}
