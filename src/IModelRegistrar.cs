namespace Vitraux;

public interface IModelRegistrar
{
    IModelRegistrar AddModelConfiguration<TViewModel, TModelConfiguration>() where TModelConfiguration : class, IModelConfiguration<TViewModel>;
}
