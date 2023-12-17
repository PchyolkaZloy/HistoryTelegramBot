using Microsoft.Extensions.Logging;
using Telegram.Bot;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class MessageTextStartHandler : ChainLinkBase
{
    public override async Task Handle(Request request)
    {
        if (request.Update.Message?.Text is null) return;

        if (request.Update.Message.Text.Equals("/start", StringComparison.Ordinal))
        {
            request.Logger.LogInformation("Receive message text: {MessageText}", request.Update.Message.Text);

            const string startText = "Hello this is history bot";
            await request.BotClient.SendTextMessageAsync(
                request.Update.Message.Chat.Id,
                startText,
                cancellationToken: request.Token).ConfigureAwait(false);
        }
        else
        {
            Next?.Handle(request);
        }
    }
}