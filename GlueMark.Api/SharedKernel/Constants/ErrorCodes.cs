using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Constants
{
    public static class ErrorCodes
    {
        // Validación (1000-1999)
        public const string ValidationEmpty = "1001";
        public const string ValidationLength = "1002";
        public const string ValidationFormat = "1003";
        public const string ValidationDuplicate = "1004";
        public const string ValidationRange = "1005";
        public const string ValidationIllegalChar = "1006";
        public const string ValidationCharacterNegative = "1007";
        public const string ValidationCharacterInvalid = "1008";

        // Autenticación (2000-2999)
        public const string InvalidCredential = "2001";
        public const string TokenExpired = "2002";
        public const string Unauthorized = "2003";
        public const string Forbidden = "2004";
        public const string MissingToken = "2005";

        // Negocio (3000-3999)
        public const string BusinessRuleViolation = "3001";
        public const string DuplicateEntry = "3002";
        public const string InactiveEntity = "3003";
        public const string EntityNotFound = "3004";
        public const string LimitExceeded = "3005";
        public const string Conflict = "3006";

        // Base de Datos (4000-4999)
        public const string ConnectionFailed = "4001";
        public const string DatabaseExecutionError = "4002";
        public const string Timeout = "4003";
        public const string ConstraintViolation = "4004";
        public const string TransactionFailed = "4005";

        // API externa (5000-5999)
        public const string ExternalApiFailed = "5001";
        public const string ExternalTimeout = "5002";
        public const string ExternalInvalidData = "5003";
        public const string ExternalUnauthorized = "5003";

        // Errores de archivos (6000-6999)
        public const string FileNotFound = "6001";
        public const string FileUploadFailed = "6002";
        public const string FileFormantInvalid = "6003";
        public const string FileSizeExceeded = "6004";

        // ... etc.
    }
}
