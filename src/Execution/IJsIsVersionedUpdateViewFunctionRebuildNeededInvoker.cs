namespace Vitraux.Execution;

internal interface IJsIsVersionedUpdateViewFunctionRebuildNeededInvoker
{
    ValueTask<bool> IsRebuildNeeded(string vmKey, string version);
}