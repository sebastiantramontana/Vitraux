namespace Vitraux.Modeling.Building.ElementBuilders;

public interface IToChildrenElementSelector<TNext>
{
    IPopulatingChildrenElementSelector<TNext> ToChildren { get; }
}