using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IValueFinallizable<TViewModel, TValue> : IModelMapper<TViewModel>, IValueTargetBuilder<TViewModel, TValue>
{
}