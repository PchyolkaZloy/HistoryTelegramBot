using Abstractions.Repositories;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection, string connectionString)
    {
        collection.AddScoped<IFactRepository, FactRepository>(_ => new FactRepository(connectionString));
        collection.AddScoped<IQuestionRepository, QuestionRepository>(_ => new QuestionRepository(connectionString));

        return collection;
    }
}