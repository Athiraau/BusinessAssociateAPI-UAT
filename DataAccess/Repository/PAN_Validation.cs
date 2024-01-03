using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using DataAccess.Context;
using DataAccess.Dto;
using Dapper.Oracle;
using Dapper;
using DataAccess.Dto.Request;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using DataAccess.Contracts;

namespace DataAccess.Repository
{
    public class PAN_Validation : IPAN_Validation
    {
        private DapperContext _context;
        private DtoWrapper _dto;


        public PAN_Validation(DapperContext context, DtoWrapper dto)
        {
            _context = context;
            _dto = dto;
        }

        public async Task<dynamic> validate_PAN(BAPostDataReqDto baPostDataReqDto)
        {

            char ch;
            
            int response = 0;

            if (baPostDataReqDto.flag == "pancard_validation")
            {

                string panID = baPostDataReqDto.indata;

                panID = panID.ToUpper();
                int l = panID.Length;

                if (l != 10)
                {
                    response = 0;
                }
                else
                {
                    string strRegex = @"[A-Z]{5}[0-9]{4}[A-Z]{1}$";
                    Regex re = new Regex(strRegex);
                    if (re.IsMatch(panID))
                        response = 1;
                    else
                        response = 0;
                }
            }
            else
                response = 2;

           return response;

        }

    }
}
