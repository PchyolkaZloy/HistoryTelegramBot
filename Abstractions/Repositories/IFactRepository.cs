using Models;

namespace Abstractions.Repositories;

public interface IFactRepository
{
    Task<IList<Fact?>> FindLimitFactsAsync(int limit);
}