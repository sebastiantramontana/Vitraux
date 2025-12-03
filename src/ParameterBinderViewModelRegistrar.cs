using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

internal class ParameterBinderViewModelRegistrar<TViewModel>(IServiceCollection container) : ViewModelRegistrar(container), IParameterBinderViewModelRegistrar<TViewModel>
{
    private readonly IServiceCollection _container = container;

    public IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>()
        where TParameterBinder : class, IActionParametersBinder<TViewModel>
    {
        _ = _container.AddSingleton<TParameterBinder>();
        return this;
    }

    public IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinderAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>()
        where TParameterBinder : class, IActionParametersBinderAsync<TViewModel>
    {
        _ = _container.AddSingleton<TParameterBinder>();
        return this;
    }

    public IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinder<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService
    {
        _ = _container.AddSingleton<TParameterBinderService, TParameterBinderImplementation>();
        return this;
    }

    public IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinderAsync<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService
    {
        _ = _container.AddSingleton<TParameterBinderService, TParameterBinderImplementation>();
        return this;
    }
}
