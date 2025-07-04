namespace Vitraux.Execution.Serialization;

internal interface ISerializablePropertyValueExtractor
{
    string GetValue(Delegate @delegate, object? value);
    IEnumerable<object> GetCollection(Delegate @delegate, object? value);
}
