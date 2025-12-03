using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public interface IViewModelConfigurationRegistrar<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel> where TViewModel : class
{
    public IParameterBinderViewModelRegistrar<TViewModel> AddConfiguration<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TModelConfiguration>()
        where TModelConfiguration : class, IViewModelConfiguration<TViewModel>;
}
