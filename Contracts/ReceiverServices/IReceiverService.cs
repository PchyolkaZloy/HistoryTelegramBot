namespace Contracts.ReceiverServices;

public interface IReceiverService
{
    Task ReceiveAsync(CancellationToken stoppingToken);
}