using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vitraux;

namespace Chronometer;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        _ = builder.Services
            .AddSingleton<IChronometerUpdater, ChronometerUpdater>()
            .AddSingleton<IChronometerService, ChronometerService>()
            .AddVitraux()
            .AddDefaultConfiguration()
            .AddViewModel<ChronometerViewModel>()
                .AddConfiguration<ChronometerModelConfiguration>();

        await using var host = builder.Build();
        await host.Services.BuildVitraux();
        await host.RunAsync();
    }
}
