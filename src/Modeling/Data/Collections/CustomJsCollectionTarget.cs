namespace Vitraux.Modeling.Data.Collections;

internal record class CustomJsCollectionTarget(string FunctionName) : CustomJsTargetBase(FunctionName), ICollectionTarget;
