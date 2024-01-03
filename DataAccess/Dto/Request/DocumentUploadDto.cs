using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto.Request
{
    public class DocumentUploadDto
    {
      

        [Required]
        public string flag { get; set; }
        public string Document { get; set; }
        public string indata { get; set; }

     

    }
}
