using TelegramBot.Handlers.ChainLinks.Models;

namespace TelegramBot.Handlers.ChainLinks.Abstractions;

public interface IAsyncChainLink
{
    void AddNext(IAsyncChainLink link);

    Task HandleAsync(Request request);
}