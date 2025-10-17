namespace Vitraux;

public interface IParameterBinderModelRegistrar<TViewModel> : IModelRegistrar
{
    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinder>() 
        where TParameterBinder : class, IActionParametersBinder<TViewModel>;

    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinderService, TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinder<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService;

    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinder>() 
        where TParameterBinder : class, IActionParametersBinderAsync<TViewModel>;

    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinderService, TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinderAsync<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService;
}
