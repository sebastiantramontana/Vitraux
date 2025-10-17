using Microsoft.Extensions.DependencyInjection;

namespace Vitraux;

internal class ParameterBinderModelRegistrar<TViewModel>(IServiceCollection container) : ModelRegistrar(container), IParameterBinderModelRegistrar<TViewModel>
{
    private readonly IServiceCollection _container = container;

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinder>()
        where TParameterBinder : class, IActionParametersBinder<TViewModel>
    {
        _ = _container.AddSingleton<IActionParametersBinder<TViewModel>, TParameterBinder>();
        return this;
    }

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinder>()
        where TParameterBinder : class, IActionParametersBinderAsync<TViewModel>
    {
        _ = _container.AddSingleton<IActionParametersBinderAsync<TViewModel>, TParameterBinder>();
        return this;
    }

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinderService, TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinder<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService
    {
        _ = _container.AddSingleton<TParameterBinderService, TParameterBinderImplementation>();
        return this;
    }

    public IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinderService, TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinderAsync<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService
    {
        _ = _container.AddSingleton<TParameterBinderService, TParameterBinderImplementation>();
        return this;
    }
}
