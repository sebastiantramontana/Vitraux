using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Vitraux.Test.Example;

namespace Vitraux.Test.Execution;

public class PetownerAutomationExecutionTest
{
    [Fact]
    public async Task IoCTest()
    {
        var container = new ServiceCollection();

        var jsInProcessRuntime = Mock.Of<IJSInProcessRuntime>();

        _ = container
                .AddSingleton<IJSRuntime>(jsInProcessRuntime)
                .AddVitraux()
                .AddModelConfiguration<PetOwner, PetOwnerConfiguration>();

        var serviceProvider = container.BuildServiceProvider();

        await serviceProvider.BuildVitraux();

        var viewUpdater = serviceProvider.GetRequiredService<IViewlUpdater<PetOwner>>();

        Assert.NotNull(viewUpdater);
    }
}
