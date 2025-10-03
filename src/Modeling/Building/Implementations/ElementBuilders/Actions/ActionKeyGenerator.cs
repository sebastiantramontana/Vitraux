using Vitraux.Helpers;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

namespace Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

internal class ActionKeyGenerator(IAtomicAutoNumberGenerator atomicAutoNumberGenerator) : IActionKeyGenerator
{
    public string Generate()
        => $"ak{atomicAutoNumberGenerator.Next()}";
}
