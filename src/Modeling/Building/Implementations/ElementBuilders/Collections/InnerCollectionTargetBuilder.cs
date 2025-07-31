using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections;

internal class InnerCollectionTargetBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper> : IInnerCollectionTargetBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>
{
    private readonly CollectionData _collectionData;
    private readonly ICollectionModelMapper<TItemBack, TEndCollectionReturnModelMapper> _modelMapper;
    private readonly IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper> _endCollectionReturn;
    private readonly TEndCollectionReturnModelMapper _endCollectionReturnModelMapper;
    private readonly IServiceProvider _serviceProvider;

    public InnerCollectionTargetBuilder(
        CollectionData collectionData,
        ICollectionModelMapper<TItemBack, TEndCollectionReturnModelMapper> modelMapper,
        TEndCollectionReturnModelMapper endCollectionReturnModelMapper,
        IServiceProvider serviceProvider)
    {
        _collectionData = collectionData;
        _modelMapper = modelMapper;
        _endCollectionReturnModelMapper = endCollectionReturnModelMapper;
        _serviceProvider = serviceProvider;
        _endCollectionReturn = new InnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>(_endCollectionReturnModelMapper, _modelMapper, this);
    }

    public IInnerTableSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> ToTables
        => new InnerTableSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>(_collectionData, _endCollectionReturn, _serviceProvider);

    public IInnerContainerElementsSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> ToContainerElements
        => new InnerContainerElementsSelectorBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>>(_collectionData, _endCollectionReturn, _serviceProvider);

    public IInnerCollectionCustomJsBuilder<TItem, IInnerCollectionFinallizable<TItemBack, TItem, TEndCollectionReturnModelMapper>> ToJsFunction(string jsFunction)
    {
        var target = new CustomJsCollectionTarget(jsFunction);
        _collectionData.AddTarget(target);

        return new InnerCollectionCustomJsBuilder<TItem, TItemBack, TEndCollectionReturnModelMapper>(target, _collectionData, _modelMapper, _endCollectionReturnModelMapper, _serviceProvider);
    }
}
