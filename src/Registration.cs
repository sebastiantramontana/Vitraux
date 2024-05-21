using Microsoft.Extensions.DependencyInjection;
using Vitraux.Modeling;

namespace Vitraux
{
    public static class Registration
    {
        public static void AddHtmlUpdater<TViewModel, TModelConfiguration>(this ServiceCollection serviceCollection)
            where TModelConfiguration : class, IModelConfiguration<TViewModel>
        {
            serviceCollection.AddSingleton<IHtmlUpdater<TViewModel>, HtmlUpdater<TViewModel>>();
            serviceCollection.AddSingleton<IModelConfiguration<TViewModel>, TModelConfiguration>();
        }
    }
}
