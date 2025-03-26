using Microsoft.Data.SqlClient;
using SetZero.Domain.Interfaces;
using SetZero.src.Domain.Entities;

namespace SetZero.Infrastructure.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SqlConnection ConnectToDatabase(DatabaseConfig config)
        {
            var connectionString = $"Server={config.DataSource}; Database=ETrade; User Id={config.User}; Password={config.Password}; TrustServerCertificate=True; Connection Timeout={config.Timeout}";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
