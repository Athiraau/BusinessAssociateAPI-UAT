using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct.Messages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Repository
{
    public class DocumentUpload : IDocumentUpload
    {
            private DapperContext _context;
            private DtoWrapper _dto;
            private readonly ILoggerService _logger;


        public DocumentUpload(DapperContext context, DtoWrapper dto,ILoggerService logger)
            {
                _context = context;
                _dto = dto;
            _logger = logger;
            }
        public async Task PostImageUpload(string flag, Docu_convertedBytesDto DocuUpload)
        {
            var sql = " ";

            using var connection = _context.CreateConnection();
            connection.Open();
            OracleParameter[] prm = new OracleParameter[1];
            OracleCommand cmd = (OracleCommand)connection.CreateCommand();




            if (flag == "FileUpload1")  //UPDATE id proof photo in neft_customer tabel
            {
                sql = "UPDATE neft_customer s SET s.id_proof=:ph WHERE s.BENEFICIARY_ACCOUNT='" + DocuUpload.BENEFICIARY_ACCOUNT + "'  AND s.moduleid = 115";
                prm[0] = cmd.Parameters.Add("ph", OracleDbType.Blob, DocuUpload.ID_PROOF, ParameterDirection.Input);

            }
            else if (flag == "FileUpload2") //UPDATE pan_attachment in tbl_add_businessagent
            {
                sql = "UPDATE tbl_add_businessagent s SET s.pan_attachment=:ph WHERE S.phone='" + DocuUpload.phone + "' ";
                prm[0] = cmd.Parameters.Add("ph", OracleDbType.Blob, DocuUpload.pan_attachment, ParameterDirection.Input);

            }
            else if (flag == "FileUpload3") //UPDATE Customer Photo in tbl_add_businessagent
            {
                sql = "UPDATE tbl_add_businessagent s SET s.cus_photo=:ph WHERE S.phone='" + DocuUpload.phone + "' ";
                prm[0] = cmd.Parameters.Add("ph", OracleDbType.Blob, DocuUpload.cus_photo, ParameterDirection.Input);

            }
            else if (flag == "FileUpload4")  //UPDATE ID Proof in tbl_add_businessagent
            {
                sql = "UPDATE tbl_add_businessagent s SET s.id_proof=:ph WHERE S.phone='" + DocuUpload.phone + "' ";
                prm[0] = cmd.Parameters.Add("ph", OracleDbType.Blob, DocuUpload.ID_PROOF, ParameterDirection.Input);

            }
            else if (flag == "FileUpload5")  //UPDATE Qualification Proof in TBL_PAID_BA_MASTER
            {
                sql = "UPDATE TBL_PAID_BA_MASTER s SET s.QUALI_DOC=:ph WHERE S.PBA_CODE='" + DocuUpload.PBA_CODE + "' ";
                prm[0] = cmd.Parameters.Add("ph", OracleDbType.Blob, DocuUpload.ID_PROOF, ParameterDirection.Input);

            }
            else
            {
                //DocuUpload=null;
                _logger.LogError($" invalid Flag .. Details of filter data could not be returned in db.");
                return;
            }

            cmd.CommandText = sql;
             cmd.ExecuteNonQuery();
            return;


            //var result = await connection.QuerySingleOrDefaultAsync(sql,prm);
            //return result;

           

        }


     
    
    }
}
