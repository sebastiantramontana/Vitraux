using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vitraux;

namespace FromTemplate;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        _ = builder.Services
            .AddVitraux()
            .AddDefaultConfiguration()
            .AddViewModel<FromTemplateViewModel>()
                .AddConfiguration<FromTemplateModelConfiguration>();

        await using var host = builder.Build();
        await host.Services.BuildVitraux();
        await host.RunAsync();
    }
}
