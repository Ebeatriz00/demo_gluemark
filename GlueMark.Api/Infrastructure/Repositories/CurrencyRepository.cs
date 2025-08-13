using Core.Entities;
using Core.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Infrastructure.Persistence.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public CurrencyRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ExistsAsync(string description, string codeSunat, long businessId, long? excludeId = null)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                string query = "SELECT COUNT(*) FROM CURRENCY WHERE CURRENCY_NAME = @DESC AND CURRENCY_CODE_SUNAT = @CODE AND BUSINESS_ID = @BID";

                if (excludeId.HasValue)
                    query += " AND ID <> @ID";

                using var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DESC", description);
                cmd.Parameters.AddWithValue("@CODE", codeSunat);
                cmd.Parameters.AddWithValue("@BID", businessId);
                if (excludeId.HasValue) cmd.Parameters.AddWithValue("@ID", excludeId.Value);

                await connection.OpenAsync();
                var count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al validar existencia de la moneda.", ex.Message);
            }
        }
        public async Task<string> GetLastCurrencyCodeAsync(long businessId)
        {
            try
            {
                using var cn = _connectionFactory.CreateConnection();
                await cn.OpenAsync();

                string query = "SELECT MAX(CURRENCY_CODE) FROM CURRENCY WHERE BUSINESS_ID = @BID";
                using var cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@BID", businessId);

                object result = await cmd.ExecuteScalarAsync();
                return result == DBNull.Value || result == null ? null : result.ToString();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al obtener el último código de moneda.", ex.Message);
            }
        }

        public async Task AddAsync(Currency entity)
        {
            try
            {
                using var connection = _connectionFactory.CreateConnection();
                using var cmd = new SqlCommand("SP_WS_REGISTER_CURRENCY", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BUSINESS_ID", entity.BusinessId);
                cmd.Parameters.AddWithValue("@CURRENCY_CODE", entity.Code);
                cmd.Parameters.AddWithValue("@CURRENCY_CODE_SUNAT", entity.CodeSunat);
                cmd.Parameters.AddWithValue("@CURRENCY_NAME", entity.Description);
                cmd.Parameters.AddWithValue("@CURRENCY_SYMBOL", entity.symbol);
                cmd.Parameters.AddWithValue("@CREATE_USER", entity.UsersBy);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al registrar la moneda en base de datos.", ex.Message);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error inesperado al guardar la moneda.", ex.Message);
            }
        }
        public async Task<List<Currency>> GetAllAsync(int businessId)
        {
            try
            {
                var list = new List<Currency>();
                using var cn = _connectionFactory.CreateConnection();
                await cn.OpenAsync();

                using var cmd = new SqlCommand("SP_WS_LIST_CURRENCY", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BUSINESS_ID", businessId);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new Currency
                    {
                        BusinessId = reader.GetInt64(0),
                        CurrencyId = reader.GetInt64(1),
                        Code = reader.GetString(2),
                        CodeSunat = reader.GetString(3),
                        Description = reader.GetString(4),
                        symbol = reader.GetString(5),
                        Status = reader.GetString(6)
                    });
                }
                return list;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al obtener lista de monedas.", ex.Message);
            }
        }
        public async Task<Currency> GetByIdAsync(long CurrencyId)
        {
            try
            {
                using var cn = _connectionFactory.CreateConnection();
                await cn.OpenAsync();

                using var cmd = new SqlCommand("SP_WS_CURRENCY_ID", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CURRENCY_ID", CurrencyId);

                using var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows && await reader.ReadAsync())
                {
                    return new Currency
                    {
                        CurrencyId = (int)reader.GetInt64(0),
                        BusinessId = (int)reader.GetInt64(1),
                        Description = reader.GetString(2),
                        CodeSunat = reader.GetString(3),
                        symbol = reader.GetString(4)
                    };
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al obtener la moneda por ID.", ex.Message);
            }
        }
        public async Task<bool> UpdateAsync(Currency cur)
        {
            try
            {
                using var cn = _connectionFactory.CreateConnection();
                await cn.OpenAsync();

                using var cmd = new SqlCommand("SP_WS_UPDATE_CURRENCY", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CURRENCY_ID", cur.CurrencyId);
                cmd.Parameters.AddWithValue("@BUSINESS_ID", cur.BusinessId);
                cmd.Parameters.AddWithValue("@CURRENCY_CODE_SUNAT", cur.CodeSunat);
                cmd.Parameters.AddWithValue("@CURRENCY_NAME", cur.Description);
                cmd.Parameters.AddWithValue("@CURRENCY_SYMBOL", cur.symbol);
                cmd.Parameters.AddWithValue("@UPDATE_USER", cur.UsersBy);

                using var reader = await cmd.ExecuteReaderAsync();
                return reader.HasRows && await reader.ReadAsync();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al actualizar la moneda.", ex.Message);
            }
        }
        public async Task<bool> PatchStatusAsync(long currencyId, string status, int UsersBy, long businessId)
        {
            try
            {
                using var cn = _connectionFactory.CreateConnection();
                await cn.OpenAsync();

                using var cmd = new SqlCommand("SP_WS_UPDATE_CURRENCY_ACTIVE", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BUSINESS_ID", businessId);
                cmd.Parameters.AddWithValue("@CURRENCY_ID", currencyId);
                cmd.Parameters.AddWithValue("@UPDATE_USER", UsersBy);
                cmd.Parameters.AddWithValue("@STATUS", status);

                using var reader = await cmd.ExecuteReaderAsync();
                return reader.HasRows && await reader.ReadAsync();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error al cambiar el estado de la moneda.", ex.Message);
            }
        }


    }
}
