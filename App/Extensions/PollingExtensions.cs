using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App.Extensions;

public static class PollingExtensions
{
    public static T GetConfiguration<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        IOptions<T> options = serviceProvider.GetService<IOptions<T>>() ?? throw new ArgumentNullException(nameof(T));

        return options.Value;
    }
}