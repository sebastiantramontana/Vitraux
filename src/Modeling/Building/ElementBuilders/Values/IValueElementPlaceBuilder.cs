namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IValueElementPlaceBuilder<TViewModel, TValue>
{
    IInsertFromBuilder<TViewModel, TValue> Insert { get; }
    IValueFinallizable<TViewModel, TValue> ToContent { get; }
    IValueFinallizable<TViewModel, TValue> ToAttribute(string attribute);
}
