using App.FactServices;
using App.PollingServices;
using App.QuizServices;
using App.ReceiverServices;
using Contracts.FactService;
using Contracts.QuizService;
using Microsoft.Extensions.DependencyInjection;

namespace App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<ReceiverService>();
        collection.AddScoped<IFactService, FactService>();
        collection.AddScoped<IQuizService, QuizService>();
        collection.AddHostedService<PollingService>();

        return collection;
    }
}