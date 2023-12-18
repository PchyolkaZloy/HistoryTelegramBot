using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class QuestionMenuHandler : AsyncChainLinkBase
{
    public override async Task HandleAsync(UpdateHandlerContext context)
    {
        if (context.Update.Message?.Text is null) return;

        if (context.Update.Message.Text.Equals(
                MessageTextConstants.CheckKnowledgeMenuMessage, StringComparison.Ordinal)
            || context.Update.Message.Text.Equals(MessageTextConstants.QuizCommandMessage, StringComparison.Ordinal))
        {
            context.Logger.LogInformation("Receive message text: {MessageText}", context.Update.Message.Text);

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.BackToMenuMessage),
                        new KeyboardButton(MessageTextConstants.QuizStartMessage),
                    },
                }) { ResizeKeyboard = true, };

            await context.BotClient.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                DescriptionConstants.QuestionMenuDescription,
                replyMarkup: replyKeyboard,
                cancellationToken: context.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(context);
        }
    }
}