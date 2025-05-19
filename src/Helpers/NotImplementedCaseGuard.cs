namespace Vitraux.Helpers;

internal class NotImplementedCaseGuard : INotImplementedCaseGuard
{
    public T ThrowException<T>(object obj)
        => throw new NotImplementedException($"Case for type {obj.GetType().Name} not implemented. See StackTrace.");
}