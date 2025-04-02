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
        var target = new OwnMappingTarget();
        _valueData.AddTarget(target);

        return _modelMapper;
    }
}