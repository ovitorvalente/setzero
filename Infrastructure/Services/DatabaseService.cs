using Dapper;
using SetZero.Infrastructure.Services;
using SetZero.Models.Entities;

public class DatabaseService
{
    public static async Task UpdateMoviments(MovimentData data, DatabaseConfig config)
    {
        var connection = DatabaseConnection.ConnectToDatabase(config);

        string selectMovimentoIdeQuery = GetSelectMovimentoIdeQuery();
        string selectValorDoItemQuery = GetSelectValorDoItemQuery();
        string updateMovimentoProdutoQuery = GetUpdateMovimentoProdutoQuery();
        string updateMovimentoQuery = GetUpdateMovimentoQuery();

        var parameters = new
        {
            filialParam = data.Filial__Codigo,
            sequenciaParam = data.Sequencia,
            itemParam = data.Linha
        };

        try
        {
            var movimentoIde = await connection.QuerySingleOrDefaultAsync<Guid>(selectMovimentoIdeQuery, parameters);
            if (movimentoIde == Guid.Empty)
                throw new Exception("Movimento não encontrado.");

            var valorDoItem = await connection.QuerySingleOrDefaultAsync<double>(selectValorDoItemQuery, new { movimentoIde, itemParam = data.Linha });
            if (valorDoItem == 0)
                throw new Exception("Valor do item não encontrado.");

            await connection.ExecuteAsync(updateMovimentoProdutoQuery, new { movimentoIde, itemParam = data.Linha });
            await connection.ExecuteAsync(updateMovimentoQuery, new { movimentoIde, valorDoItem });
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao atualizar: {ex.Message}");
        }

    }

    private static string GetSelectMovimentoIdeQuery()
    {
        return @"
            SELECT Ide
            FROM Movimento
            WHERE Sequencia = @sequenciaParam
                AND Filial__Codigo = @filialParam;
        ";
    }

    private static string GetSelectValorDoItemQuery()
    {
        return @"
            SELECT Valor_Final
            FROM Movimento_Produto
            WHERE Movimento__Ide = @movimentoIde
                AND Linha = @itemParam;
        ";
    }
    private static string GetUpdateMovimentoProdutoQuery()
    {
        return @"
            UPDATE Movimento_Produto
            SET Valor_Final = 0
            WHERE Movimento__Ide = @movimentoIde
                AND Linha = @itemParam;
        ";
    }
    private static string GetUpdateMovimentoQuery()
    {
        return @"
            UPDATE Movimento
            SET Total_Final = Total_Final - @valorDoItem
            WHERE Ide = @movimentoIde;
        ";
    }
}
