using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root.Insertions;

internal class RootInsertFromBuilder<TViewModel, TValue>(
    ElementValueTarget target,
    IModelMapper<TViewModel> modelMapper,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilder)
    : IRootInsertFromBuilder<TViewModel, TValue>
{
    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(string id)
        => SetInsertSelector(new InsertElementTemplateSelectorId(id));

    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Uri uri)
        => SetInsertSelector(new InsertElementUriSelectorUri(uri));

    private RootInsertToChildrenBuilder<TViewModel, TValue> SetInsertSelector(InsertElementSelectorBase insertElementSelector)
    {
        target.Insertion = insertElementSelector;
        return new(target, modelMapper, multiTargetBuilder);
    }
}
