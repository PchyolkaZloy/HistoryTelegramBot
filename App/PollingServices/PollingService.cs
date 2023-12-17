using App.ReceiverServices;
using Contracts.PollingServices;
using Microsoft.Extensions.Logging;

namespace App.PollingServices;

public class PollingService : PollingServiceBase<ReceiverService>
{
    public PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
        : base(serviceProvider, logger)
    {
    }
}