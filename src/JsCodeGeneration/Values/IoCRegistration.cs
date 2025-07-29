using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.Values;

internal static class IoCRegistration
{
    internal static IServiceCollection AddValues(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IElementPlaceAttributeJsGenerator, ElementPlaceAttributeJsGenerator>()
            .AddSingleton<IElementPlaceContentJsGenerator, ElementPlaceContentJsGenerator>()
            .AddSingleton<IElementPlaceHtmlJsGenerator, ElementPlaceHtmlJsGenerator>()
            .AddSingleton<ITargetElementsDirectUpdateValueJsGenerator, TargetElementsDirectUpdateValueJsGenerator>()
            .AddSingleton<ITargetElementsUpdateValueInsertJsGenerator, TargetElementsUpdateValueInsertJsGenerator>()
            .AddSingleton<ITargetElementsValueJsGenerator, TargetElementsValueJsGenerator>()
            .AddSingleton<IValueNamesGenerator, ValueNamesGenerator>()
            .AddSingleton<IValuesJsCodeGenerationBuilder, ValuesJsCodeGenerationBuilder>();
}