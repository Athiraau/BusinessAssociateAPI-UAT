using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;

using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Emit;
using DataAccess.Dto.Response;
using DataAccess.Dto.Request;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repository
{
    public class BusinessAssociate:IBusinessAssociate
    {
        private DapperContext _context;
        private DtoWrapper _dto;

        public BusinessAssociate(DapperContext context, DtoWrapper dto)
        {
            _context = context;
            _dto = dto;
        }

        public async Task<dynamic> GetBA_Data(string flag, string indata)
        {

            OracleRefCursor result = null;
            
            var procedureName = "proc_BA_get_data"; 
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_indata", indata, OracleMappingType.NVarchar2, ParameterDirection.Input);

            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);



            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
           
            return response;
           
        }
        public async Task<dynamic> PostBA_Data(BAPostDataReqDto baPostDataReqDto)
        {

            OracleRefCursor result = null;

            var procedureName = "proc_BA_post_data";
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", baPostDataReqDto.flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_indata", baPostDataReqDto.indata, OracleMappingType.NVarchar2, ParameterDirection.Input);

            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);
            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);

            return response;

        }


        public async Task<dynamic>Get_Document(string flag,string indata)
        {

           
          var sql1 = " ";

           using var connection = _context.CreateConnection();
           connection.Open();

           if (flag == "FileUpload1")  // proof photo in neft_customer table
           {
                   sql1 = "select s.id_proof from neft_customer s where beneficiary_account='" + indata + "' ";
               // sql1 = "select t.m_sec_photo from dms.m_sec_punch_photo t  where t.sec_code='000100165'  and to_date(t.curr_date)=to_date(sysdate)";
            }

           else if (flag == "FileUpload2")  // PAN card in tbl_add_businessagent table
           {
                sql1 = "select s.pan_attachment from tbl_add_businessagent s where s.phone='" + indata + "' ";
           }

           else if (flag == "FileUpload3")  // customer photo from tbl_add_businessagent
           {
                sql1 = "select s.cus_photo from tbl_add_businessagent s where s.phone='" + indata + "' ";
           }

           else if (flag == "FileUpload4")  // ID proof in tbl_add_businessagent table
           {
                sql1 = "select s.id_proof from tbl_add_businessagent s where s.phone='" + indata + "' ";
           }

            else if (flag == "FileUpload5")  // Qualification Document in TBL_PAID_BA_MASTER table
            {
                sql1 = "select s.QUALI_DOC from TBL_PAID_BA_MASTER s where S.PBA_CODE='" + indata + "' ";
            }

            var result = await connection.QuerySingleOrDefaultAsync<byte[]>(sql1);

          //  var result = await connection.QuerySingleOrDefaultAsync<ImgDto>(sql1);
           // var result = await connection.QuerySingleOrDefaultAsync(sql1);

           return result;

           

    }
       


    }

}

