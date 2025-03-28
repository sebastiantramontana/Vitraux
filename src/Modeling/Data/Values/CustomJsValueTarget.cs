namespace Vitraux.Modeling.Data.Values;

internal record class CustomJsValueTarget(string FunctionName) : CustomJsTargetBase<ValueTarget>(FunctionName);
