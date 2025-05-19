namespace Vitraux.Helpers;

internal interface INotImplementedCaseGuard
{
    T ThrowException<T>(object obj);
}
