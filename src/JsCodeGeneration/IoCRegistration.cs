using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.BuiltInCalling;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration;

internal static class IoCRegistration
{
    internal static IServiceCollection AddJsCodeGeneration(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddThisNamespace()
            .AddBuiltInCalling()
            .AddCollections()
            .AddFormating()
            .AddInitialization()
            .AddJsObjectNames()
            .AddQueryElements()
            .AddUpdateViews()
            .AddValues();

    private static IServiceCollection AddThisNamespace(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsGenerator, JsGenerator>()
            .AddSingleton<IPromiseJsGenerator, PromiseJsGenerator>()
            .AddSingleton<IPropertyCheckerJsCodeGeneration, PropertyCheckerJsCodeGeneration>()
            .AddSingleton<IRootJsGenerator, RootJsGenerator>();

    private static IServiceCollection AddFormating(this IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<ICodeFormatter, CodeFormatter>();

    private static IServiceCollection AddInitialization(this IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<IInitializeJsGeneratorContext, InitializeJsGeneratorContext>();

    private static IServiceCollection AddUpdateViews(this IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<IUpdateViewJsGenerator, UpdateViewJsGenerator>();
}
