using Models;

namespace Abstractions.Repositories;

public interface IQuestionRepository
{
    Task<IList<Question?>> FindLimitQuestionsAsync(int limit);
}