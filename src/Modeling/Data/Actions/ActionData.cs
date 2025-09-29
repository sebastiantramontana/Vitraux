namespace Vitraux.Modeling.Data.Actions;

internal record class ActionData(Delegate DataFunc, bool IsAsync) : DelegateDataBase<ActionTarget>(DataFunc);