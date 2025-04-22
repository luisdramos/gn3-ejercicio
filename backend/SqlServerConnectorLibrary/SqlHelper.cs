using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace SqlServerConnector {
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<DataTable> ExecuteQueryAsync(string query, SqlParameter[]? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            var dataTable = new DataTable();

            try
            {
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la consulta: " + ex.Message, ex);
            }

            return dataTable;
        }

        [Obsolete]
        public async Task<int> ExecuteNonQueryAsync(string commandText, SqlParameter[]? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(commandText, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            try
            {
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar el comando: " + ex.Message, ex);
            }
        }

        [Obsolete]
        public async Task<object?> ExecuteScalarAsync(string query, SqlParameter[]? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            try
            {
                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la consulta escalar: " + ex.Message, ex);
            }
        }
    }
}

