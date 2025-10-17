using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Vitraux.Execution;
using Vitraux.Execution.Building;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux;

internal class ModelRegistrar(IServiceCollection container) : IModelRegistrar
{
    public IParameterBinderModelRegistrar<TViewModel> AddModelConfiguration<TViewModel, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TModelConfiguration>()
        where TModelConfiguration : class, IModelConfiguration<TViewModel>
    {
        _ = container
            .AddSingleton<IModelMapper<TViewModel>, ModelMapper<TViewModel>>()
            .AddSingleton<IModelConfiguration<TViewModel>, TModelConfiguration>()
            .AddSingleton<IViewModelJsNamesCacheGeneric<TViewModel>, ViewModelJsNamesCacheGeneric<TViewModel>>()
            .AddSingleton<IViewModelUpdateFunctionBuilder<TViewModel>, ViewModelUpdateFunctionBuilder<TViewModel>>()
            .AddSingleton<IViewModelActionsBuilder<TViewModel>, ViewModelActionsBuilder<TViewModel>>()
            .AddSingleton<IBuilder, ViewModelRuntimeBuilder<TViewModel>>()
            .AddSingleton<IViewModelChangeTrackingContext<TViewModel>, ViewModelChangeTrackingContext<TViewModel>>()
            .AddSingleton<IViewModelNoChangesTracker<TViewModel>, ViewModelNoChangesTracker<TViewModel>>()
            .AddSingleton<IViewModelShallowChangesTracker<TViewModel>, ViewModelShallowChangesTracker<TViewModel>>()
            .AddSingleton<IViewUpdater<TViewModel>, ViewUpdater<TViewModel>>();

        return new ParameterBinderModelRegistrar<TViewModel>(container);
    }
}
