using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public interface IParameterBinderViewModelRegistrar<TViewModel> : IViewModelRegistrar
{
    IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinder<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>() 
        where TParameterBinder : class, IActionParametersBinder<TViewModel>;

    IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinder<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinder<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService;

    IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinderAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinder>() 
        where TParameterBinder : class, IActionParametersBinderAsync<TViewModel>;

    IParameterBinderViewModelRegistrar<TViewModel> AddActionParameterBinderAsync<TParameterBinderService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TParameterBinderImplementation>()
        where TParameterBinderService : class, IActionParametersBinderAsync<TViewModel>
        where TParameterBinderImplementation : class, TParameterBinderService;
}
