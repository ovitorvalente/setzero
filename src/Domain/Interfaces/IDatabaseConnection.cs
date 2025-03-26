using Microsoft.Data.SqlClient;
using SetZero.src.Domain.Entities;

namespace SetZero.Domain.Interfaces
{
    public interface IDatabaseConnection
    {
        SqlConnection ConnectToDatabase(DatabaseConfig config);
    }
}
