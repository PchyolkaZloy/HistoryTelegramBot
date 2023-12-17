using Models;

namespace Contracts.FactService;

public interface IFactService
{
    Task<Fact> GetFact();
}