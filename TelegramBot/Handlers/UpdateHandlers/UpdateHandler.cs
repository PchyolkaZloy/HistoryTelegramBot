using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.UpdateHandlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;

    public UpdateHandler(ILogger<UpdateHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Task handler = update switch
        {
            { Message: not null } => new MainMessageTextHandler().HandleAsync(
                new Request(botClient, update, _logger, cancellationToken)),
            _ => UnknownUpdateHandlerAsync(update),
        };

        await handler.ConfigureAwait(false);
    }

    public async Task HandlePollingErrorAsync(
        ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        string errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString(),
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", errorMessage);

        // Cooldown in case of network connection error
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken).ConfigureAwait(false);
    }

    private Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}