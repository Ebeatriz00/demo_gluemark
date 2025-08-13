using Core.Entities;
using Core.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public  class BusinessRepository : IBusinessRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public BusinessRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ExistsAsync(string ruc_business, string code_license, long businessId, long? excludeId = null)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();

                var baseQuery = new StringBuilder("SELECT COUNT(*) FROM BUSINESS WHERE BUSINESS_RUC = @BUSINESS_RUC AND CODE_LICENSE = @CODE_LICENSE AND BUSINESS_ID = @BID");

                if (excludeId.HasValue)
                    baseQuery.Append(" AND DOCUMENT_TYPE_ID <> @ID");

                using var cmd = new SqlCommand(baseQuery.ToString(), connection);
                cmd.Parameters.AddWithValue("@BUSINESS_RUC", ruc_business);
                cmd.Parameters.AddWithValue("@CODE_LICENSE", code_license);
                cmd.Parameters.AddWithValue("@BID", businessId);
                if (excludeId.HasValue)
                    cmd.Parameters.AddWithValue("@ID", excludeId.Value);

                await connection.OpenAsync();
                var count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al validar existencia de la empresa.", ex.Message);
            }

        }

        public async Task AddAsync(Business entity)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                using var cmd = new SqlCommand("SP_WS_REGISTER_DOCUMENT_TYPE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CODE_LICENSE", SqlDbType.VarChar).Value = entity.CodeLicense;
                cmd.Parameters.AddWithValue("@BUSINESS_RUC", SqlDbType.VarChar).Value = entity.BusinessRuc;
                cmd.Parameters.AddWithValue("@BUSINESS_START_DATE", SqlDbType.VarChar).Value = entity.BusinessStartDate;
                cmd.Parameters.AddWithValue("@BUSINESS_EXERCISE", SqlDbType.VarChar).Value = entity.BusinessExercise;
                cmd.Parameters.AddWithValue("@BUSINESS_NAME", SqlDbType.VarChar).Value = entity.BusinessName;
                cmd.Parameters.AddWithValue("@BUSINESS_ADDRESS", SqlDbType.VarChar).Value = entity.BusinessAddress;
                cmd.Parameters.AddWithValue("@BUSINESS_COUNTRY", SqlDbType.VarChar).Value = entity.BusinessCountry;
                cmd.Parameters.AddWithValue("@DEPARTMENT", SqlDbType.VarChar).Value = entity.Department;
                cmd.Parameters.AddWithValue("@PROVINCE", SqlDbType.VarChar).Value = entity.Province;
                cmd.Parameters.AddWithValue("@DISTRICT", SqlDbType.VarChar).Value = entity.District;
                cmd.Parameters.AddWithValue("@BUSINESS_PHONE", SqlDbType.VarChar).Value = entity.BusinessPhone;
                cmd.Parameters.AddWithValue("@BUSINESS_EMAIL", SqlDbType.VarChar).Value = entity.BusinessEmail;
                cmd.Parameters.AddWithValue("@BUSINESS_LEGAL_REPRE", SqlDbType.VarChar).Value = entity.BusinessLegalRepre;
                cmd.Parameters.AddWithValue("@LEGAL_DOCUMENT_TYPE", SqlDbType.BigInt).Value = entity.LegalDocumentType;
                cmd.Parameters.AddWithValue("@LEGAL_DOCUMENT", SqlDbType.VarChar).Value = entity.LegalDocument;
                cmd.Parameters.AddWithValue("@LEGAL_FIRM", SqlDbType.VarChar).Value = entity.LegalFirm;
                cmd.Parameters.AddWithValue("@CURRENCY_MAIN", SqlDbType.BigInt).Value = entity.CurrencyMain;
                cmd.Parameters.AddWithValue("@CURRENCY_SECONDARY", SqlDbType.BigInt).Value = entity.CurrencySecondary;
                cmd.Parameters.AddWithValue("@BUSINESS_LOGO", SqlDbType.VarChar).Value = entity.BusinessLogo;
                cmd.Parameters.AddWithValue("@RETENTION_AGENT", SqlDbType.Char).Value = entity.RetentionAgent;
                cmd.Parameters.AddWithValue("@PERCEPTION_AGENT", SqlDbType.Char).Value = entity.PerceptionAgent;
                cmd.Parameters.AddWithValue("@PRICOS", SqlDbType.Char).Value = entity.Pricos;
                cmd.Parameters.AddWithValue("@CREATE_USER", SqlDbType.Int).Value = entity.UsersBy;

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al registrar la empresa en la base de datos.", ex.Message);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error inesperado al guardar la empresa.", ex.Message);
            }
        }


    }
}
