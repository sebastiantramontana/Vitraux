using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Vitraux.Execution;
using Vitraux.Execution.Building;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux;

internal class ModelRegistrar(IServiceCollection container) : IModelRegistrar
{
    public IModelRegistrar AddModelConfiguration<TViewModel, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TModelConfiguration>()
        where TModelConfiguration : class, IModelConfiguration<TViewModel>
    {
        _ = container
            .AddSingleton<IModelMapper<TViewModel>, ModelMapper<TViewModel>>()
            .AddSingleton<IModelConfiguration<TViewModel>, TModelConfiguration>()
            .AddSingleton<IViewModelJsNamesCache<TViewModel>, ViewModelJsNamesCache<TViewModel>>()
            .AddSingleton<IViewModelUpdateFunctionBuilder, ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>>()
            .AddSingleton<IViewModelChangeTrackingContext<TViewModel>, ViewModelChangeTrackingContext<TViewModel>>()
            .AddSingleton<IViewModelNoChangesTracker<TViewModel>, ViewModelNoChangesTracker<TViewModel>>()
            .AddSingleton<IViewModelShallowChangesTracker<TViewModel>, ViewModelShallowChangesTracker<TViewModel>>()
            .AddSingleton<IViewlUpdater<TViewModel>, ViewUpdater<TViewModel>>();

        return this;
    }
}
