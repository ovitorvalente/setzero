using Microsoft.Data.SqlClient;
using SetZero.src.Domain.Entities;

namespace SetZero.src.Domain.Interfaces
{
    public interface IDatabaseService
    {
        Task UpdateMoviments(MovimentData data, SqlConnection connection);
    }
}
