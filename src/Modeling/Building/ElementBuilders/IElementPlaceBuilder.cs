using Vitraux.Modeling.Building.Finallizables;

namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IElementPlaceBuilder<TViewModel, TFinallizable>
    where TFinallizable : IFinallizable<TViewModel>
{
    TFinallizable ToAttribute(string attribute);
    TFinallizable ToContent { get; }
}
