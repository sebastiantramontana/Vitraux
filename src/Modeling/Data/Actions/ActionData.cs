namespace Vitraux.Modeling.Data.Actions;

internal record class ActionData(Delegate DataFunc) : DelegateDataBase<IActionTarget>(DataFunc);