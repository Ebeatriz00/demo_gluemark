using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public class GlobalResponse
    {
        public int Status { get; set; } = 1; // 1 = OK, 0 = Error
        public string Message { get; set; } = "Operación exitosa";
    }
}
