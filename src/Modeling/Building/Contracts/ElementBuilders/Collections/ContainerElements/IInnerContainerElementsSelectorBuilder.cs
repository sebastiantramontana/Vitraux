namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;

public interface IInnerContainerElementsSelectorBuilder<TItem, TEndCollectionReturn>
{
    ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn> ByQuery(string query);
}

