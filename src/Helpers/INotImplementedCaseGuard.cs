namespace Vitraux.Helpers;

internal interface INotImplementedCaseGuard
{
    TRet ThrowException<TRet>(object obj);
    void ThrowException(object obj);
}
