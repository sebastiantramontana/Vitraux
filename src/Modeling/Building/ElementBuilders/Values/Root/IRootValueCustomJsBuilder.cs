namespace Vitraux.Modeling.Building.ElementBuilders.Values.Root;
public interface IRootValueCustomJsBuilder<TViewModel, TValue> : IRootValueFinallizable<TViewModel, TValue>
{
    IRootValueFinallizable<TViewModel, TValue> FromModule(Uri moduleUri);
}
