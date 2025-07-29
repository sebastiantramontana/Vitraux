using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root.Insertions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root;

internal class RootValueElementPlaceBuilder<TViewModel, TValue>(
    ElementValueTarget target,
    IModelMapper<TViewModel> modelMapper,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilder)
    : IRootValueElementPlaceBuilder<TViewModel, TValue>
{
    public IRootInsertFromBuilder<TViewModel, TValue> Insert
        => new RootInsertFromBuilder<TViewModel, TValue>(target, modelMapper, multiTargetBuilder);

    public IRootValueFinallizable<TViewModel, TValue> ToContent
        => SetElementPlace(ContentElementPlace.Instance);

    public IRootValueFinallizable<TViewModel, TValue> ToHtml
        => SetElementPlace(HtmlElementPlace.Instance);

    public IRootValueFinallizable<TViewModel, TValue> ToAttribute(string attribute)
        => SetElementPlace(new AttributeElementPlace(attribute));

    private RootValueFinallizable<TViewModel, TValue> SetElementPlace(ElementPlace place)
    {
        target.Place = place;
        return new(modelMapper, multiTargetBuilder);
    }
}
