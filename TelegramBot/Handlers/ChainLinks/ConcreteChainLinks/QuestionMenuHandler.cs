using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class QuestionMenuHandler : AsyncChainLinkBase
{
    public override async Task HandleAsync(Request request)
    {
        if (request.Update.Message?.Text is null) return;

        if (request.Update.Message.Text.Equals(
                MessageTextConstants.CheckKnowledgeMenuMessage, StringComparison.Ordinal))
        {
            request.Logger.LogInformation("Receive message text: {MessageText}", request.Update.Message.Text);

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.BackToMenuMessage),
                        new KeyboardButton(MessageTextConstants.QuizStartMessage),
                    },
                }) { ResizeKeyboard = true, };

            await request.BotClient.SendTextMessageAsync(
                request.Update.Message.Chat.Id,
                DescriptionConstants.QuestionMenuDescription,
                replyMarkup: replyKeyboard,
                cancellationToken: request.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(request);
        }
    }
}