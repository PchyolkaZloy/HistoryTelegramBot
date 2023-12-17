using Abstractions.Repositories;
using Models;
using Npgsql;

namespace DataAccess.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly string _connectionString;

    public QuestionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IList<Question?>> FindQuestionBatchById(int firstQuestionId, int offset, int batchSize = 10)
    {
        const string sqlRequest = @"
            SELECT question_id, question_text, answers
            FROM your_table_name 
            WHERE id = @firstQuestionId 
            ORDER BY question_id 
            LIMIT @batchSize OFFSET @offset";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync().ConfigureAwait(false);

        await using var command = new NpgsqlCommand();
        command.Connection = connection;
        command.CommandText = sqlRequest;
        command.Parameters.AddWithValue("firstQuestionId", firstQuestionId);
        command.Parameters.AddWithValue("batchSize", batchSize);
        command.Parameters.AddWithValue("offset", offset);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        var questions = new List<Question?>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            int questionId = reader.GetInt32(0);
            string questionText = reader.GetString(1);
            string answers = reader.GetString(2);

            questions.Add(
                new Question(
                    Id: questionId,
                    Text: questionText,
                    Answers: answers));
        }

        return questions;
    }
}