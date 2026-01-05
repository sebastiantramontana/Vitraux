using Vitraux.Execution.Tracking.Encoded;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Tracking;

internal interface IViewModelChangesTracker<TViewModel>
{
    EncodedTrackedViewModelJsAllData Track(object? viewModelToTrack, ViewModelJsNames vmNames);
}