using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public interface IModelRegistrar
{
    IParameterBinderModelRegistrar<TViewModel> AddModelConfiguration<TViewModel, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TModelConfiguration>() where TModelConfiguration : class, IModelConfiguration<TViewModel>;
}
