using Models;

namespace Abstractions.Repositories;

public interface IQuestionRepository
{
    Task<IList<Question?>> FindQuestionBatchById(int firstQuestionId, int offset, int batchSize = 10);
}