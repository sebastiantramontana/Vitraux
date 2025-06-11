using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution;
using Vitraux.Execution.Building;
using Vitraux.Execution.Serialization;

namespace Vitraux;

internal class ModelRegistrar(IServiceCollection container) : IModelRegistrar
{
    public IModelRegistrar AddModelConfiguration<TViewModel, TModelConfiguration>()
        where TModelConfiguration : class, IModelConfiguration<TViewModel>
    {
        _ = container
            .AddSingleton<IModelMapper<TViewModel>, ModelMapper<TViewModel>>()
            .AddSingleton<IModelConfiguration<TViewModel>, TModelConfiguration>()
            .AddSingleton<IViewModelSerializationDataCache<TViewModel>, ViewModelSerializationDataCache<TViewModel>>()
            .AddSingleton<IViewModelUpdateFunctionBuilder, ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>>()
            .AddSingleton<IViewlUpdater<TViewModel>, ViewUpdater<TViewModel>>();

        return this;
    }
}
