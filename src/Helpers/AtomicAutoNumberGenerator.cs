namespace Vitraux.Helpers;

internal class AtomicAutoNumberGenerator : IAtomicAutoNumberGenerator
{
    private int _counter = -1;

    public int Next()
        => Interlocked.Increment(ref _counter);
}
