using App.Extensions;
using App.PollingServices;
using App.ReceiverServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBot.UpdateHandlers;

namespace HistoryTelegramBot;

public static class Program
{
    public static async Task Main()
    {
        IHost host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<BotConfiguration>(context.Configuration.GetSection(BotConfiguration.Configuration));

                services.AddHttpClient("telegram_bot_client")
                    .AddTypedClient<ITelegramBotClient>((httpClient, serviceProvider) =>
                    {
                        BotConfiguration botConfig = serviceProvider.GetConfiguration<BotConfiguration>();
                        var options = new TelegramBotClientOptions(botConfig.BotToken);
                        return new TelegramBotClient(options, httpClient);
                    });

                services.AddScoped<IUpdateHandler, UpdateHandler>();
                services.AddScoped<ReceiverService>();
                services.AddHostedService<PollingService>();
            })
            .Build();

        await host.RunAsync().ConfigureAwait(false);
    }
}