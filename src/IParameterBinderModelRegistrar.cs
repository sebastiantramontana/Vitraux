using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public interface IParameterBinderModelRegistrar<TViewModel> : IModelRegistrar
{
    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>() 
        where TParameterBinder : class, IActionParametersBinder<TViewModel>;

    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinder<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService;

    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>() 
        where TParameterBinder : class, IActionParametersBinderAsync<TViewModel>;

    IParameterBinderModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinderAsync<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService;
}
