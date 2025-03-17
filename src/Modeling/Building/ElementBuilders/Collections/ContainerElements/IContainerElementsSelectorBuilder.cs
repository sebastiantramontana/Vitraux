using Vitraux.Modeling.Building.ElementBuilders.Values;

namespace Vitraux.Modeling.Building.ElementBuilders.Collections.ContainerElements;

public interface IContainerElementsSelectorBuilder<TItem, TViewModelBack>
{
    ICollectionPopulateFromBuilder<TItem, TViewModelBack> ById(string id);
    ICollectionPopulateFromBuilder<TItem, TViewModelBack> ById(Func<TItem, string> idFunc);
    ICollectionPopulateFromBuilder<TItem, TViewModelBack> ByQuery(string query);
    ICollectionPopulateFromBuilder<TItem, TViewModelBack> ByQuery(Func<TItem, string> queryFunc);
}

