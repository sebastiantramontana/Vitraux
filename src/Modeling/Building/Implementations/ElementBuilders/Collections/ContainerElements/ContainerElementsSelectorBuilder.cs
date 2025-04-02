using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Collections.ContainerElements;

internal class ContainerElementsSelectorBuilder<TItem, TEndCollectionReturn> : IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>
{
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ById(string id) => throw new NotImplementedException();
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ById(Func<TItem, string> idFunc) => throw new NotImplementedException();
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(string query) => throw new NotImplementedException();
    public ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc) => throw new NotImplementedException();
}

