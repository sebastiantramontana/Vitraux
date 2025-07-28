using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelJsNamesMapper : IViewModelJsNamesMapper
{
    public ViewModelJsNames MapFromFull(FullObjectNames fullObjectNames)
    {
        var values = MapValues(fullObjectNames.ValueNames);
        var collections = MapCollections(fullObjectNames.CollectionNames);

        return new ViewModelJsNames(values, collections);
    }

    private static IEnumerable<ViewModelJsValueName> MapValues(IEnumerable<FullValueObjectName> valueNames)
        => valueNames.Select(value => new ViewModelJsValueName(value.Name, value.AssociatedData.DataFunc));

    private IEnumerable<ViewModelJsCollectionName> MapCollections(IEnumerable<FullCollectionObjectName> collectionNames)
        => collectionNames.Select(colItem =>
        {
            var childrenJsNames = GetChildrenJsNames(colItem.AssociatedNames);
            return new ViewModelJsCollectionName(colItem.Name, colItem.AssociatedData.DataFunc, childrenJsNames);
        });

    private IEnumerable<ViewModelJsNames> GetChildrenJsNames(IEnumerable<JsCollectionNames> associatedNames)
        => associatedNames
            .OfType<JsCollectionElementObjectPairNames>()
            .Select(e => MapFromFull(e.Children));
}