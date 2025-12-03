using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public class ViewModelConfigurationRegistrar<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>(IViewModelRegistrar modelRegistrar) : IViewModelConfigurationRegistrar<TViewModel> where TViewModel : class
{
    public IParameterBinderViewModelRegistrar<TViewModel> AddConfiguration<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TModelConfiguration>()
        where TModelConfiguration : class, IViewModelConfiguration<TViewModel>
        => modelRegistrar.AddViewModelConfiguration<TViewModel, TModelConfiguration>();
}