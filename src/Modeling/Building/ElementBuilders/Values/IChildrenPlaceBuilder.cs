namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IChildrenPlaceBuilder<TViewModel, TValue>
{
    IValueFinallizable<TViewModel, TValue> ToContent { get; }
    IValueFinallizable<TViewModel, TValue> ToAttribute(string attribute);
}