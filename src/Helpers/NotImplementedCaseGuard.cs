namespace Vitraux.Helpers;

internal class NotImplementedCaseGuard : INotImplementedCaseGuard
{
    public TRet ThrowException<TRet>(object obj)
        => throw new NotImplementedException($"Case for type {obj.GetType().Name} not implemented. See StackTrace.");

    public void ThrowException(object obj)
        => _ = ThrowException<object>(obj);
}