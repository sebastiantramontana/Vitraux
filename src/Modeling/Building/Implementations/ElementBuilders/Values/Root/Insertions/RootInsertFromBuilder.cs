using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Values.Root.Insertions;

internal class RootInsertFromBuilder<TViewModel, TValue>(
    ElementTarget target,
    IModelMapper<TViewModel> modelMapper,
    IRootValueMultiTargetBuilder<TViewModel, TValue> multiTargetBuilder)
    : IRootInsertFromBuilder<TViewModel, TValue>
{
    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(string id)
        => SetInsertSelector(new InsertElementTemplateSelectorId(id));

    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(Func<TViewModel, string> idFunc)
        => SetInsertSelector(new InsertElementTemplateSelectorDelegate(idFunc));

    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(Func<TValue, string> idFunc)
        => SetInsertSelector(new InsertElementTemplateSelectorDelegate(idFunc));

    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Uri uri)
        => SetInsertSelector(new InsertElementUriSelectorUri(uri));

    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Func<TViewModel, Uri> uriFunc)
        => SetInsertSelector(new InsertElementUriSelectorDelegate(uriFunc));

    public IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Func<TValue, Uri> uriFunc)
        => SetInsertSelector(new InsertElementUriSelectorDelegate(uriFunc));

    private RootInsertToChildrenBuilder<TViewModel, TValue> SetInsertSelector(InsertElementSelectorBase insertElementSelector)
    {
        target.Insertion = insertElementSelector;
        return new(target, modelMapper, multiTargetBuilder);
    }
}
