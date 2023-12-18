using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Handlers.UpdateHandlers;

namespace TelegramBot.Handlers.ChainLinks.Models;

public record UpdateHandlerContext(
    ITelegramBotClient BotClient,
    Update Update,
    ILogger Logger,
    UpdateHandler UpdateHandler,
    CancellationToken Token);