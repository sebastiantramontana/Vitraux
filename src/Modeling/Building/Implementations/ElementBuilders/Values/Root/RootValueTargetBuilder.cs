using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;

internal class RootValueTargetBuilder<TViewModel, TValue>(
    ValueData valueData,
    IModelMapper<TViewModel> modelMapper)
    : RootValueMultiTargetBuilder<TViewModel, TValue>(valueData, modelMapper), IRootValueTargetBuilder<TViewModel, TValue>
{
    private readonly ValueData _valueData = valueData;
    private readonly IModelMapper<TViewModel> _modelMapper = modelMapper;

    public IModelMapper<TViewModel> ToOwnMapping
        => BuildOwnMapping();

    private IModelMapper<TViewModel> BuildOwnMapping()
    {
        var ownMappingTarget = new OwnMappingTarget(typeof(TValue));
        _valueData.AddTarget(ownMappingTarget);

        return _modelMapper;
    }
}