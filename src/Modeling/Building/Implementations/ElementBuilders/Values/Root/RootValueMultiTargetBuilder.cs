using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;

internal class RootValueMultiTargetBuilder<TViewModel, TValue>(
    ValueData valueData,
    IModelMapper<TViewModel> modelMapper)
    : IRootValueMultiTargetBuilder<TViewModel, TValue>
{
    public IRootValueElementSelectorBuilder<TViewModel, TValue> ToElements
        => BuildElementTarget();

    public IRootValueCustomJsBuilder<TViewModel, TValue> ToJsFunction(string jsFunction)
        => BuildCustomJsTarget(jsFunction);

    private RootValueElementSelectorBuilder<TViewModel, TValue> BuildElementTarget()
    {
        var newTarget = new ElementTarget();
        valueData.AddTarget(newTarget);

        return new(newTarget, modelMapper, this);
    }

    private RootValueCustomJsBuilder<TViewModel, TValue> BuildCustomJsTarget(string jsFunction)
    {
        var newTarget = new CustomJsValueTarget(jsFunction);
        valueData.AddTarget(newTarget);

        return new(newTarget, modelMapper, this);
    }
}
