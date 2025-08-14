using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using Vitraux;

namespace ClockWasm;

[SupportedOSPlatform("browser")]
public partial class Program
{
    private static readonly ClockViewModel _clock = new();
    private static IViewUpdater<ClockViewModel> _viewUpdater = default!;
    private static CancellationTokenSource _cancellationTokenSouce = new();

    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        _ = builder.Services
            .AddVitraux()
            .AddDefaultConfiguration()
            .AddModelConfiguration<ClockViewModel, ClockModelConfiguration>();

        await using var host = builder.Build();

        await host.Services.BuildVitraux();

        _viewUpdater = host.Services.GetRequiredService<IViewUpdater<ClockViewModel>>();

        await host.RunAsync();
    }

    [JSExport]
    public static async Task Reset()
    {
        try
        {
            _clock.Reset();
            await _viewUpdater.Update(_clock);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    [JSExport]
    public static async Task Start()
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1) / 10.0);

        try
        {
            while (await timer.WaitForNextTickAsync(_cancellationTokenSouce.Token))
            {
                _clock.AddTenthSecond();
                await _viewUpdater.Update(_clock);
            }
        }
        catch (OperationCanceledException)
        {
            if (!_cancellationTokenSouce.TryReset())
            {
                _cancellationTokenSouce.Dispose();
                _cancellationTokenSouce = new CancellationTokenSource();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            timer.Dispose();
        }
    }

    [JSExport]
    public static void Stop()
    {
        try
        {
            _cancellationTokenSouce.Cancel();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
