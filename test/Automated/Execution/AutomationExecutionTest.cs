using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Vitraux.Test.Example;

namespace Vitraux.Test.Execution;

public class AutomationExecutionTest
{
    [Fact]
    public async Task IoCTest()
    {
        var container = new ServiceCollection();

        var jsInProcessRuntime = Mock.Of<IJSInProcessRuntime>();

        _ = container
                .AddSingleton<IJSRuntime>(jsInProcessRuntime)
                .AddVitraux()
                .AddModelConfiguration<PetOwner, PetOwnerConfiguration>()
                .AddModelConfiguration<Vaccine, VaccineConfiguration>()
                .AddModelConfiguration<CustomerViewModel, CustomerViewModelConfiguration>();

        var serviceProvider = container.BuildServiceProvider();

        await serviceProvider.BuildVitraux();

        var viewUpdaterPetOwner = serviceProvider.GetRequiredService<IViewlUpdater<PetOwner>>();
        var viewUpdaterCustomerViewModel = serviceProvider.GetRequiredService<IViewlUpdater<CustomerViewModel>>();

        Assert.NotNull(viewUpdaterPetOwner);
        Assert.NotNull(viewUpdaterCustomerViewModel);
    }
}
