using Contracts.FactService;
using Contracts.QuizService;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class MainMessageTextHandler : AsyncChainLinkBase
{
    public MainMessageTextHandler(IFactService factService, IQuizService quizService)
    {
        var startHandler = new MessageTextStartHandler();
        AddNext(startHandler);
        var questionMenuHandler = new QuestionMenuHandler();
        startHandler.AddNext(questionMenuHandler);
        var factTestPrintHandler = new FactTextPrintHandler(factService);
        questionMenuHandler.AddNext(factTestPrintHandler);
        var quizPrintHandler = new QuizPrintHandler(quizService);
        factTestPrintHandler.AddNext(quizPrintHandler);
        var quizCheckAnswerHandler = new QuizCheckAnswerHandler(quizService);
        quizPrintHandler.AddNext(quizCheckAnswerHandler);
    }

    public override async Task HandleAsync(UpdateHandlerContext context)
    {
        if (NextHandler is not null)
        {
            await NextHandler.HandleAsync(context);
        }
    }
}