using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class DocumentTypeResponse
    {
        public long BusinessId { get; set; }
        public long DocumentTypeId { get; set; }
        public string CodeSunat { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
