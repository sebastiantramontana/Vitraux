namespace Vitraux.Modeling.Data.Collections;

internal record class CustomJsCollectionTarget(string FunctionName) : CustomJsTarget<CollectionTarget>(FunctionName);
