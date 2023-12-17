using Abstractions.Repositories;
using Models;
using Npgsql;

namespace DataAccess.Repositories;

public class FactRepository : IFactRepository
{
    private readonly string _connectionString;

    public FactRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Stack<Fact?>> FindLimitFactsAsync(int limit)
    {
        const string sqlRequest = """
                                  SELECT factid, facttext
                                  FROM facts
                                  ORDER BY RANDOM()
                                  LIMIT @limit;
                                  """;

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync().ConfigureAwait(false);

        await using var command = new NpgsqlCommand();
        command.Connection = connection;
        command.CommandText = sqlRequest;
        command.Parameters.AddWithValue("limit", limit);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        var facts = new Stack<Fact?>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            int factId = reader.GetInt32(0);
            string factText = reader.GetString(1);

            facts.Push(new Fact(factId, factText));
        }

        return facts;
    }
}