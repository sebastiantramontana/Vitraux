using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Vitraux.Execution.Building;
using Vitraux.Test.Example;

namespace Vitraux.Test.Execution;

public class AutomationExecutionTest
{
    [Fact]
    public void IoCTest()
    {
        var container = new ServiceCollection();
        var jsInProcessRuntime = Mock.Of<IJSInProcessRuntime>();

        _ = container
            .AddSingleton<IJSRuntime>(jsInProcessRuntime)
            .AddVitraux()
            .AddDefaultConfiguration()
            .AddViewModelConfiguration<PetOwner, PetOwnerConfiguration>()
                .AddActionParameterBinder<IPetOwnerActionParameterBinder1, PetOwnerActionParameterBinder1>()
                .AddActionParameterBinderAsync<IPetOwnerActionParameterBinder2, PetOwnerActionParameterBinder2>()
                .AddActionParameterBinderAsync<PetOwnerActionParameterBinder3>()
                .AddActionParameterBinder<PetOwnerActionParameterBinder4>()
            .AddViewModelConfiguration<Vaccine, VaccineConfiguration>()
            .AddViewModelConfiguration<CustomerViewModel, CustomerViewModelConfiguration>();

        var serviceProvider = container.BuildServiceProvider();

        var builder = serviceProvider.GetRequiredService<IVitrauxBuilder>();
        var viewUpdaterPetOwner = serviceProvider.GetRequiredService<IViewUpdater<PetOwner>>();
        var viewUpdaterCustomerViewModel = serviceProvider.GetRequiredService<IViewUpdater<CustomerViewModel>>();

        Assert.NotNull(builder);
        Assert.NotNull(viewUpdaterPetOwner);
        Assert.NotNull(viewUpdaterCustomerViewModel);
    }
}
