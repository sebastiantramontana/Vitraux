using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vitraux;

namespace LogActions;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        _ = builder.Services
            .AddVitraux()
            .AddDefaultConfiguration()
            .AddViewModel<LogViewModel>()
                .AddConfiguration<LogViewModelConfiguration>()
                .AddActionParameterBinder<SyncBinder>()
                .AddActionParameterBinderAsync<AsyncBinder>();

        await using var host = builder.Build();

        try
        {
            await host.Services.BuildVitraux();
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e}");
        }

        await host.RunAsync();
    }
}
