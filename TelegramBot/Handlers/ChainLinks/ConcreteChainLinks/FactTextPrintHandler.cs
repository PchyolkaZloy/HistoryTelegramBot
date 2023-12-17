using Contracts.FactService;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class FactTextPrintHandler : AsyncChainLinkBase
{
    private readonly IFactService _factService;

    public FactTextPrintHandler(IFactService factService)
    {
        _factService = factService;
    }

    public override async Task HandleAsync(Request request)
    {
        if (request.Update.Message?.Text is null) return;

        if (request.Update.Message.Text.Equals(MessageTextConstants.FactMenuMessage, StringComparison.Ordinal)
            || request.Update.Message.Text.Equals(MessageTextConstants.NextFactMessage, StringComparison.Ordinal))
        {
            request.Logger.LogInformation("Receive message text: {MessageText}", request.Update.Message.Text);

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.BackToMenuMessage),
                        new KeyboardButton(MessageTextConstants.NextFactMessage),
                    },
                }) { ResizeKeyboard = true, };

            await request.BotClient.SendTextMessageAsync(
                request.Update.Message.Chat.Id,
                (await _factService.GetFact()).Text,
                replyMarkup: replyKeyboard,
                cancellationToken: request.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(request);
        }
    }
}