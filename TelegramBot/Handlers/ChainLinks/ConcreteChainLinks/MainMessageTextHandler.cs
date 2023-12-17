using TelegramBot.Handlers.ChainLinks.Abstractions;
using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.ConcreteChainLinks;

public class MainMessageTextHandler : AsyncChainLinkBase
{
    public MainMessageTextHandler()
    {
        var startHandler = new MessageTextStartHandler();
        AddNext(startHandler);
        startHandler.AddNext(new QuestionMenuHandler());
    }

    public override async Task HandleAsync(Request request)
    {
        if (NextHandler is not null)
        {
            await NextHandler.HandleAsync(request);
        }
    }
}