using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

internal class ParameterBinderModelRegistrar<TViewModel>(IServiceCollection container) : ModelRegistrar(container), IParameterBinderModelRegistrar<TViewModel>
{
    private readonly IServiceCollection _container = container;

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>()
        where TParameterBinder : class, IActionParametersBinder<TViewModel>
    {
        _ = _container.AddSingleton<TParameterBinder>();
        return this;
    }

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>()
        where TParameterBinder : class, IActionParametersBinderAsync<TViewModel>
    {
        _ = _container.AddSingleton<TParameterBinder>();
        return this;
    }

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinder<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService
    {
        _ = _container.AddSingleton<TParameterBinderService, TParameterBinderImplementation>();
        return this;
    }

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinderAsync<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService
    {
        _ = _container.AddSingleton<TParameterBinderService, TParameterBinderImplementation>();
        return this;
    }
}
