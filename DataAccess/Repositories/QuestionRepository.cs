using Abstractions.Repositories;
using Models;
using Newtonsoft.Json;
using Npgsql;

namespace DataAccess.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly string _connectionString;

    public QuestionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Stack<Question?>> FindLimitQuestionsAsync(int limit)
    {
        const string sqlRequest = """
                                  SELECT QuestionId, text, answers
                                  FROM questions
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
        var questions = new Stack<Question?>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            int questionId = reader.GetInt32(0);
            string questionText = reader.GetString(1);
            string answers = reader.GetString(2);

            var jsonObject = JsonConvert.DeserializeAnonymousType(answers, new { answers = new List<string>() });

            questions.Push(new Question(
                questionId,
                questionText,
                jsonObject?.answers ?? new List<string>()));
        }

        return questions;
    }
}