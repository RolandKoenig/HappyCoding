using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HappyCoding.JsonInSqlServer.Util
{
    internal static class DBUtil
    {
        public static async Task DropDBIfExistsAsync(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var initialCatalog = connectionStringBuilder.InitialCatalog;
            connectionStringBuilder.Remove("Initial Catalog");

            await using var connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            await connection.OpenAsync();

            if (await connection.DBExistsAsync(initialCatalog))
            {
                await connection.DropDBAsync(initialCatalog);
            }
        }

        public static async Task EnsureNewDBAsync(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var initialCatalog = connectionStringBuilder.InitialCatalog;
            connectionStringBuilder.Remove("Initial Catalog");

            await using var connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            await connection.OpenAsync();

            if (await connection.DBExistsAsync(initialCatalog))
            {
                await connection.DropDBAsync(initialCatalog);
            }

            await connection.CreateDBAsync(initialCatalog);
        }

        public static async Task ExecuteNonQuery(string connectionString, string sql)
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }

        private static async Task<bool> DBExistsAsync(this SqlConnection connection, string initialCatalog)
        {
            await using var command = new SqlCommand($"SELECT * FROM sys.databases WHERE NAME = '{initialCatalog}'",
                connection);
            await using var reader = await command.ExecuteReaderAsync();
            return reader.HasRows;
        }

        private static async Task CreateDBAsync(this SqlConnection connection, string initialCatalog)
        {
            await using var command = new SqlCommand($"CREATE DATABASE {initialCatalog}", connection);
            await command.ExecuteNonQueryAsync();
        }

        private static async Task DropDBAsync(this SqlConnection connection, string initialCatalog)
        {
            await using var command = new SqlCommand(
                "USE master;" +
                $"ALTER DATABASE {initialCatalog} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;" +
                $"DROP DATABASE {initialCatalog}",
                connection);
            await command.ExecuteNonQueryAsync();
        }
    }
}