namespace Vitraux.Modeling.Data.Actions;

internal record class ActionData(Delegate DataFunc) : DelegateDataBase<ActionTarget>(DataFunc);