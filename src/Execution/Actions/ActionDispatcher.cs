using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using Vitraux.Execution.ViewModelNames.Actions;

namespace Vitraux.Execution.Actions;

[SupportedOSPlatform("browser")]
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public partial class ActionDispatcher
{
    [JSExport]
    public static void DispatchAction(string vmKey, string actionKey)
        => InvokeDelegate(vmKey, actionKey);

    [JSExport]
    public static Task DispatchActionAsync(string vmKey, string actionKey)
        => (InvokeDelegate(vmKey, actionKey) as Task)!;

    [JSExport]
    public static void DispatchParametrizableAction(string vmKey, string actionKey, JSObject jsParameters)
        => InvokeActionParametersBinder(vmKey, actionKey, jsParameters);

    [JSExport]
    public static Task DispatchParametrizableActionAsync(string vmKey, string actionKey, JSObject jsParameters)
        => (InvokeActionParametersBinder(vmKey, actionKey, jsParameters) as Task)!;

    private static object? InvokeActionParametersBinder(string vmKey, string actionKey, JSObject jsParameters)
    {
        var actionInfo = ViewModelJsActionsRepository.GetViewModelActionInfo(vmKey, actionKey);
        var parameters = ConvertParametersToDictionary(jsParameters, actionInfo.ParanNames, actionInfo.PassInputValueParameter);
        var viewModel = ViewModelRepository.GetViewModel(vmKey);
        var binder = (actionInfo.Invokable as IActionParametersBinderDispatch)!;

        return binder.BindActionToDispatch(viewModel, parameters);
    }

    private static object? InvokeDelegate(string vmKey, string actionKey)
    {
        var actionInfo = ViewModelJsActionsRepository.GetViewModelActionInfo(vmKey, actionKey);
        var viewModel = ViewModelRepository.GetViewModel(vmKey);
        var @delegate = (actionInfo.Invokable as Delegate)!;

        return @delegate.DynamicInvoke(viewModel);
    }

    private static Dictionary<string, IEnumerable<string>> ConvertParametersToDictionary(JSObject jsParameters, IEnumerable<string> paramNames, bool isInputValueParameterPassed)
    {
        var parameters = paramNames.ToDictionary(pn => pn, pn => GetParameterValues(jsParameters, pn));

        if (isInputValueParameterPassed)
            parameters.Add("inputValue", GetParameterValues(jsParameters, "inputValue"));

        return parameters;
    }

    private static IEnumerable<string> GetParameterValues(JSObject jsParameters, string paramName)
    {
        var paramValuesArray = jsParameters.GetPropertyAsJSObject(paramName);

        if (paramValuesArray is null)
            return [];

        var length = paramValuesArray.GetPropertyAsInt32("length");
        var values = new List<string>(length);

        for (var i = 0; i < length; i++)
            values.Add(paramValuesArray.GetPropertyAsString(i.ToString()) ?? string.Empty);

        return [.. values];
    }
}