using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal interface IViewModelChangesTracker<TViewModel>
{
    EncodedTrackedViewModelAllData Track(object? objToTrack, ViewModelJsNames vmNames);
}
