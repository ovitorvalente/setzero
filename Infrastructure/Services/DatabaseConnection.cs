using System.Windows;
using Microsoft.Data.SqlClient;
using SetZero.Models.Entities;

namespace SetZero.Infrastructure.Services
{
    public class DatabaseConnection
    {
        public static void ConnectToDatabase(DatabaseConfig config)
        {
            var connectionString = $"Server={config.DataSource}; Database=ETrade; User Id={config.User}; Password={config.Password}; TrustServerCertificate=True; Connection Timeout={config.Timeout}";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexão bem-sucedida com o banco de dados!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (SqlException sqle)
                {
                    MessageBox.Show($"Erro ao conectar ao banco de dados: {sqle.Message}", "Erro SQL", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Erro inesperado: {e.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
