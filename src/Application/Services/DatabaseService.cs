using Dapper;
using Microsoft.Data.SqlClient;
using SetZero.src.Domain.Entities;

namespace SetZero.src.Application.Services
{
    public class DatabaseService
    {
        public async Task UpdateMovements(MovimentData data, SqlConnection connection)
        {
            string selectMovementIdQuery = SelectMovementIdeQuery();
            string selectItemValueQuery = SelectMovementProductValueQuery();
            string updateMovementProductQuery = UpdateMovementProductQuery();
            string updateMovementQuery = UpdateMovementQuery();

            var queryParameters = new
            {
                filialCodeParam = data.Filial__Codigo,
                sequenceParam = data.Sequencia,
                itemLineParam = data.Linha
            };

            try
            {
                var movementIde = await connection.QuerySingleOrDefaultAsync<Guid>(selectMovementIdQuery, queryParameters);
                var itemLine = await connection.QuerySingleOrDefaultAsync<int?>(CheckMovementProductLine(), new { movementIde, itemLineParam = data.Linha });
                var itemValue = await connection.QuerySingleOrDefaultAsync<double>(selectItemValueQuery, new { movementIde, itemLineParam = data.Linha });

                if (movementIde == Guid.Empty)
                    throw new Exception("Movimento não encontrado.");

                if (itemLine ==  null)
                    throw new Exception("Linha não encontrada");

                if (itemValue == 0)
                    throw new Exception("O valor do item já foi zerado.");

                await connection.ExecuteAsync(updateMovementProductQuery, new { movementIde, itemLineParam = data.Linha });
                await connection.ExecuteAsync(updateMovementQuery, new { movementIde, itemValue });
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar: {ex.Message}");
            }
        }

        private static string SelectMovementIdeQuery() =>
            "SELECT Ide FROM Movimento WHERE Sequencia = @sequenceParam AND Filial__Codigo = @filialCodeParam;";

        private static string CheckMovementProductLine() =>
            "SELECT Linha FROM Movimento_Produto WHERE Movimento__Ide = @movementIde AND Linha = @itemLineParam;";

        private static string SelectMovementProductValueQuery() =>
            "SELECT Valor_Final FROM Movimento_Produto WHERE Movimento__Ide = @movementIde AND Linha = @itemLineParam;";

        private static string UpdateMovementProductQuery() =>
            "UPDATE Movimento_Produto SET Valor_Final = 0 WHERE Movimento__Ide = @movementIde AND Linha = @itemLineParam;";

        private static string UpdateMovementQuery() =>
            "UPDATE Movimento SET Total_Final = Total_Final - @itemValue WHERE Ide = @movementIde;";
    }
}
