namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.ContainerElements;

public interface IRootContainerElementsSelectorBuilder<TItem, TViewModel>
{
    ICollectionPopulateFromBuilder<TItem, IModelMapper<TViewModel>> ById(string id);
    ICollectionPopulateFromBuilder<TItem, IModelMapper<TViewModel>> ByQuery(string query);
}