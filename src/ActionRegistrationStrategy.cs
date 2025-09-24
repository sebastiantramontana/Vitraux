namespace Vitraux;

public enum ActionRegistrationStrategy
{
    OnlyOnceAtStart = 0,
    OnlyOnceOnFirstViewModelRendering,
    AlwaysOnViewModelRendering
}

