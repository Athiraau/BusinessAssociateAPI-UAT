using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class Docu_convertedBytesDto
    {
        public byte[] ID_PROOF { get; set; }
        public byte[] pan_attachment { get; set; }
        public byte[] cus_photo { get; set; }

        public string BENEFICIARY_ACCOUNT { get; set; }
        public string phone { get; set; }
        public string PBA_CODE { get; set; }




    }
}
