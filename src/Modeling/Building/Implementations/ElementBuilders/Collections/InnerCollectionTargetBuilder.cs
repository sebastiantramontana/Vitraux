using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionTargetBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper> : ICollectionTargetBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>
{
    private readonly CollectionData _collectionData;
    private readonly IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper> _endCollectionReturn;

    public InnerCollectionTargetBuilder(CollectionData collectionData, ICollectionModelMapper<TItemBack, TEndCollectionReturnModelMapper> modelMapper, TEndCollectionReturnModelMapper endCollectionReturnModelMapper)
    {
        _collectionData = collectionData;
        _endCollectionReturn = new InnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>(endCollectionReturnModelMapper, modelMapper, this);
    }

    public ITableSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> ToTables
        => new TableSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>(_collectionData, _endCollectionReturn);

    public IContainerElementsSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> ToContainerElements
        => new ContainerElementsSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>(_collectionData, _endCollectionReturn);

    public ICollectionCustomJsBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        _collectionData.AddTarget(target);

        return new CollectionCustomJsBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>(target, _collectionData, _endCollectionReturn);
    }
}
