namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IChildrenSelectorBuilder<TViewModel, TValue>
{
    IChildrenPlaceBuilder<TViewModel, TValue> ByQuery(string query);
    IChildrenPlaceBuilder<TViewModel, TValue> ByQuery(Func<TValue, string> queryFunc);
    IChildrenPlaceBuilder<TViewModel, TValue> ByQuery(Func<TViewModel, string> queryFunc);
}
