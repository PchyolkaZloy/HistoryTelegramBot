using Contracts.QuizService;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class QuizPrintHandler : AsyncChainLinkBase
{
    private readonly IQuizService _quizService;

    public QuizPrintHandler(IQuizService quizService)
    {
        _quizService = quizService;
    }

    public override async Task HandleAsync(UpdateHandlerContext context)
    {
        if (context.Update.Message?.Text is null) return;

        if (context.Update.Message.Text.Equals(MessageTextConstants.NextQuestionMessage, StringComparison.Ordinal)
            || context.Update.Message.Text.Equals(MessageTextConstants.QuizStartMessage, StringComparison.Ordinal))
        {
            context.Logger.LogInformation("Receive message text: {MessageText}", context.Update.Message.Text);
            context.UpdateHandler.IsWaitAnswer = true;

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.BackToMenuMessage),
                        new KeyboardButton(MessageTextConstants.NextQuestionMessage),
                    },
                }) { ResizeKeyboard = true, };

            await context.BotClient.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                (await _quizService.GetQuestion()).Text,
                replyMarkup: replyKeyboard,
                cancellationToken: context.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(context);
        }
    }
}