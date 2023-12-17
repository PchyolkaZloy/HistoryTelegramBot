using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.ChainLinks.Models;

public record Request(ITelegramBotClient BotClient, Update Update, ILogger Logger, CancellationToken Token);