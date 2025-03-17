namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IValueElementSelectorBuilder<TViewModel, TValue>
{
    IValueElementPlaceBuilder<TViewModel, TValue> ById(string id);
    IValueElementPlaceBuilder<TViewModel, TValue> ById(Func<TValue, string> idFunc);
    IValueElementPlaceBuilder<TViewModel, TValue> ById(Func<TViewModel, string> idFunc);
    IValueElementPlaceBuilder<TViewModel, TValue> ByQuery(string query);
    IValueElementPlaceBuilder<TViewModel, TValue> ByQuery(Func<TValue, string> queryFunc);
    IValueElementPlaceBuilder<TViewModel, TValue> ByQuery(Func<TViewModel, string> queryFunc);
}