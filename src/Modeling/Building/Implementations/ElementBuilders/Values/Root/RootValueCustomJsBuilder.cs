using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;

internal class RootValueCustomJsBuilder<TViewModel, TValue>(
    CustomJsValueTarget target,
    IModelMapper<TViewModel> modelMapperWrapped,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilderWrapped)
    : RootValueFinallizable<TViewModel, TValue>(modelMapperWrapped, multiTargetBuilderWrapped), IRootValueCustomJsBuilder<TViewModel, TValue>
{
    public IRootValueFinallizable<TViewModel, TValue> FromModule(Uri moduleUri)
    {
        target.ModuleFrom = moduleUri;
        return this;
    }
}
