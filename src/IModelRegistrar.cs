using System.Diagnostics.CodeAnalysis;

namespace Vitraux;

public interface IModelRegistrar
{
    IParameterBinderModelRegistrar<TViewModel> AddModelConfiguration<TViewModel, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TModelConfiguration>()
        where TViewModel : notnull
        where TModelConfiguration : class, IModelConfiguration<TViewModel>;
}
