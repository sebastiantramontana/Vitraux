namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;
public interface IRootInsertFromBuilder<TViewModel, TValue>
{
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(string id);
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(Func<TViewModel, string> idFunc);
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(Func<TValue, string> idFunc);
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Uri uri);
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Func<TViewModel, Uri> uriFunc);
    IRootInsertToChildrenBuilder<TViewModel, TValue> FromUri(Func<TValue, Uri> uriFunc);
}
