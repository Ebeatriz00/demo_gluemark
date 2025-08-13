using SharedKernel;
using SharedKernel.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    public class DatabaseException : BaseException
    {
        public DatabaseException(string userMessage, string? technicalDetails = null)
            : base("DATABASE_ERROR", 500, new[]
            {
                new GlobalErrorDetail(ErrorCodes.DatabaseExecutionError, userMessage)
            }, technicalDetails)
        {
        }
    }

}
