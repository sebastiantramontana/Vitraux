namespace Vitraux.Modeling.Data.Values;

internal record class ValueData(Delegate DataFunc) : DelegateDataBase<IValueTarget>(DataFunc);