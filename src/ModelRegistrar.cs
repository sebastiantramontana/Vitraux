using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Vitraux.Execution;
using Vitraux.Execution.Building;
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
            .AddSingleton<IViewlUpdater<TViewModel>, ViewUpdater<TViewModel>>();

        return this;
    }
}
