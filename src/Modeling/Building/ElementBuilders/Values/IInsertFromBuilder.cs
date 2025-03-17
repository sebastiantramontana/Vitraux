namespace Vitraux.Modeling.Building.ElementBuilders.Values;
public interface IInsertFromBuilder<TViewModel, TValue>
{
    IInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(string id);
    IInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(Func<TViewModel, string> idFunc);
    IInsertToChildrenBuilder<TViewModel, TValue> FromTemplate(Func<TValue, string> idFunc);
    IInsertToChildrenBuilder<TViewModel, TValue> FromUri(Uri uri);
    IInsertToChildrenBuilder<TViewModel, TValue> FromUri(Func<TViewModel, Uri> uriFunc);
    IInsertToChildrenBuilder<TViewModel, TValue> FromUri(Func<TValue, Uri> uriFunc);
}
