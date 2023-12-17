using Models;

namespace Abstractions.Repositories;

public interface IFactRepository
{
    Task<Stack<Fact?>> FindLimitFactsAsync(int limit);
}