using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.Abstractions;

public abstract class AsyncChainLinkBase : IAsyncChainLink
{
    protected IAsyncChainLink? NextHandler { get; private set; }

    public void AddNext(IAsyncChainLink link)
    {
        if (NextHandler is null)
        {
            NextHandler = link;
        }
        else
        {
            NextHandler.AddNext(link);
        }
    }

    public abstract Task HandleAsync(UpdateHandlerContext context);
}