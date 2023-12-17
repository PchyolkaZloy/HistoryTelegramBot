using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Polling;
using TelegramBot.Handlers.UpdateHandlers;

namespace TelegramBot.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationTelegramBot(this IServiceCollection collection)
    {
        collection.AddScoped<IUpdateHandler, UpdateHandler>();

        return collection;
    }
}