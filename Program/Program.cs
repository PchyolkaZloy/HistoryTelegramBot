using App.Extensions;
using DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using TelegramBot.Extensions;

namespace HistoryTelegramBot;

public static class Program
{
    public static async Task Main()
    {
        IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.Configure<BotConfiguration>(
                    context.Configuration.GetSection(BotConfiguration.Configuration));
                services.Configure<PostgresConfiguration>(
                    context.Configuration.GetSection(PostgresConfiguration.Configuration));

                services.AddHttpClient("telegram_bot_client")
                    .AddTypedClient<ITelegramBotClient>((httpClient, serviceProvider) =>
                    {
                        BotConfiguration botConfig = serviceProvider.GetConfiguration<BotConfiguration>();
                        var options = new TelegramBotClientOptions(botConfig.BotToken);
                        return new TelegramBotClient(options, httpClient);
                    });

                ServiceProvider serviceProvider = services.BuildServiceProvider();

                services
                    .AddApplication()
                    .AddInfrastructureDataAccess(
                        serviceProvider.GetConfiguration<PostgresConfiguration>().ConnectionString)
                    .AddPresentationTelegramBot();
            })
            .Build();

        await host.RunAsync().ConfigureAwait(false);
    }
}