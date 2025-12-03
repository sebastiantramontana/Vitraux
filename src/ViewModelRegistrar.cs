using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Vitraux.Execution;
using Vitraux.Execution.Building;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux;

internal class ViewModelRegistrar(IServiceCollection container) : IViewModelRegistrar
{
    public IParameterBinderViewModelRegistrar<TViewModel> AddViewModelConfiguration<TViewModel, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModelConfiguration>()
        where TViewModel : class
        where TViewModelConfiguration : class, IViewModelConfiguration<TViewModel>
    {
        if (container.Any(c => c.ServiceType == typeof(IViewModelConfiguration<TViewModel>)))
            throw new InvalidOperationException($"Vitraux: Configuration for viewmodel {typeof(TViewModel).FullName} was already added to Vitraux. Probably AddViewModelConfiguration or AddConfigration were called twice");

        _ = container
            .AddSingleton<IModelMapper<TViewModel>, ModelMapper<TViewModel>>()
            .AddSingleton<IViewModelConfiguration<TViewModel>, TViewModelConfiguration>()
            .AddSingleton<IViewModelJsNamesRepositoryGeneric<TViewModel>, ViewModelJsNamesRepositoryGeneric<TViewModel>>()
            .AddSingleton<IViewModelUpdateFunctionBuilder<TViewModel>, ViewModelUpdateFunctionBuilder<TViewModel>>()
            .AddSingleton<IViewModelActionsBuilder<TViewModel>, ViewModelActionsBuilder<TViewModel>>()
            .AddSingleton<IBuilder, ViewModelRuntimeBuilder<TViewModel>>()
            .AddSingleton<IViewModelChangeTrackingContext<TViewModel>, ViewModelChangeTrackingContext<TViewModel>>()
            .AddSingleton<IViewModelNoChangesTracker<TViewModel>, ViewModelNoChangesTracker<TViewModel>>()
            .AddSingleton<IViewModelShallowChangesTracker<TViewModel>, ViewModelShallowChangesTracker<TViewModel>>()
            .AddSingleton<IViewUpdater<TViewModel>, ViewUpdater<TViewModel>>();

        return new ParameterBinderViewModelRegistrar<TViewModel>(container);
    }

    public IViewModelConfigurationRegistrar<TViewModel> AddViewModel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>()
        where TViewModel : class
    {
        _ = container.AddSingleton<TViewModel>();
        return new ViewModelConfigurationRegistrar<TViewModel>(this);
    }
}
