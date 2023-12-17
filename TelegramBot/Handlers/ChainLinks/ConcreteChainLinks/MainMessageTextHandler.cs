using Contracts.FactService;
using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class MainMessageTextHandler : AsyncChainLinkBase
{
    public MainMessageTextHandler(IFactService factService)
    {
        var startHandler = new MessageTextStartHandler();
        AddNext(startHandler);
        var questionMenuHandler = new QuestionMenuHandler();
        startHandler.AddNext(questionMenuHandler);
        var factTestPrintHandler = new FactTextPrintHandler(factService);
        questionMenuHandler.AddNext(factTestPrintHandler);
    }

    public override async Task HandleAsync(Request request)
    {
        if (NextHandler is not null)
        {
            await NextHandler.HandleAsync(request);
        }
    }
}