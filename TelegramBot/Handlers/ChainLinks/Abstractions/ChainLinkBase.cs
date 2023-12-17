using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.Abstractions;

public abstract class ChainLinkBase : IChainLink
{
    protected IChainLink? Next { get; private set; }

    public void AddNext(IChainLink link)
    {
        if (Next is null)
        {
            Next = link;
        }
        else
        {
            Next.AddNext(link);
        }
    }

    public abstract Task Handle(Request request);
}