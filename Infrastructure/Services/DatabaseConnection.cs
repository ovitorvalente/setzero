using Microsoft.Data.SqlClient;
using SetZero.Models.Entities;

namespace SetZero.Infrastructure.Services
{
    public class DatabaseConnection
    {
        public static SqlConnection ConnectToDatabase(DatabaseConfig config)
        {
            var connectionString = $"Server={config.DataSource}; Database=ETrade; User Id={config.User}; Password={config.Password}; TrustServerCertificate=True; Connection Timeout={config.Timeout}";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
