using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public interface IViewModelRegistrar
{
    IViewModelConfigurationRegistrar<TViewModel> AddViewModel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>() where TViewModel : class;
    IParameterBinderViewModelRegistrar<TViewModel> AddViewModelConfiguration<TViewModel, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModelConfiguration>()
        where TViewModel : class
        where TViewModelConfiguration : class, IViewModelConfiguration<TViewModel>;
}
