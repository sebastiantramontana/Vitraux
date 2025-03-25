using Vitraux.Modeling.Building.ElementBuilders.Values;

namespace Vitraux.Modeling.Building.ElementBuilders.Collections.ContainerElements;

public interface IContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>
{
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ById(string id);
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ById(Func<TItem, string> idFunc);
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(string query);
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc);
}

