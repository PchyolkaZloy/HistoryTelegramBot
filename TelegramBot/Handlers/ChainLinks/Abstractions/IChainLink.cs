using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.Abstractions;

public interface IChainLink
{
    void AddNext(IChainLink link);

    Task Handle(Request request);
}