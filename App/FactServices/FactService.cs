using Abstractions.Repositories;
using Contracts.FactService;
using Models;

namespace App.FactServices;

public class FactService : IFactService
{
    private readonly IFactRepository _factRepository;
    private Stack<Fact?> _facts;

    public FactService(IFactRepository factRepository)
    {
        _factRepository = factRepository;
        _facts = new Stack<Fact?>();
    }

    public async Task<Fact> GetFact()
    {
        const int defaultLimit = 5;
        if (_facts.Count == 0)
        {
            _facts = await _factRepository.FindLimitFactsAsync(defaultLimit);
        }

        Fact? newFact = _facts.Pop();

        return newFact ?? new Fact(-1, "no facts in data base");
    }
}