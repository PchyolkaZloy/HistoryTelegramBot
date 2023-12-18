using System.Text;
using Contracts.QuizService;
using Microsoft.Extensions.Logging;
using Models;
using Models.ResultTypes;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Constants;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class QuizCheckAnswerHandler : AsyncChainLinkBase
{
    private readonly IQuizService _quizService;

    public QuizCheckAnswerHandler(IQuizService quizService)
    {
        _quizService = quizService;
    }

    public override async Task HandleAsync(UpdateHandlerContext context)
    {
        if (context.Update.Message?.Text is null) return;

        if (context.UpdateHandler.IsWaitAnswer)
        {
            context.Logger.LogInformation("Receive message text: {MessageText}", context.Update.Message.Text);

            var replyKeyboard = new ReplyKeyboardMarkup(
                new List<KeyboardButton[]>
                {
                    new[]
                    {
                        new KeyboardButton(MessageTextConstants.BackToMenuMessage),
                        new KeyboardButton(MessageTextConstants.NextQuestionMessage),
                    },
                }) { ResizeKeyboard = true, };

            AnswerResult result = _quizService.CheckAnswer(new UserAnswer(context.Update.Message.Text.Trim()));
            var builder = new StringBuilder();

            if (result is AnswerResult.Correct)
            {
                builder.Append(AnswerTextMessage.CorrectAnswerMessage);
            }
            else
            {
                builder.Append(AnswerTextMessage.IncorrectAnswerMessage);
                builder.Append(_quizService.GetFirstAnswerToCurrentQuestion());
            }

            context.UpdateHandler.IsWaitAnswer = false;
            await context.BotClient.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                builder.ToString(),
                replyMarkup: replyKeyboard,
                cancellationToken: context.Token).ConfigureAwait(false);
        }
        else
        {
            NextHandler?.HandleAsync(context);
        }
    }
}