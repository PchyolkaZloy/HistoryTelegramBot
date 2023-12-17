using Models;

namespace Abstractions.Repositories;

public interface IQuestionRepository
{
    Task<Stack<Question?>> FindLimitQuestionsAsync(int limit);
}