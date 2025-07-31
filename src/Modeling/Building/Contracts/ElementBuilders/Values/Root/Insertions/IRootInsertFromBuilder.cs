namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
public interface IRootInsertFromBuilder<TViewModel, TValue>
{
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(string id);
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Uri uri);
}
