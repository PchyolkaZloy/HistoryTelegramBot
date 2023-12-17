using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Contracts.ReceiverServices;

public abstract class ReceiverServiceBase<TUpdateHandler> : IReceiverService
    where TUpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUpdateHandler _updateHandler;
    private readonly ILogger<ReceiverServiceBase<TUpdateHandler>> _logger;

    protected ReceiverServiceBase(
        ITelegramBotClient botClient,
        IUpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
        _logger = logger;
    }

    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = Array.Empty<UpdateType>(),
            ThrowPendingUpdates = true,
        };

        User botInfo = await _botClient.GetMeAsync(stoppingToken).ConfigureAwait(false);
        _logger.LogInformation("Start receiving updates for {BotName}", botInfo.Username ?? "My Awesome Bot");

        await _botClient.ReceiveAsync(_updateHandler, receiverOptions, stoppingToken).ConfigureAwait(false);
    }
}