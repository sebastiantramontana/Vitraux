namespace Vitraux.Execution.Serialization;

internal interface ISerializablePropertyValueExtractor
{
    SerializableValueInfo GetValueInfo(Delegate @delegate, object? obj);
    string GetStringValue(object? value);
    IEnumerable<object> GetCollection(Delegate @delegate, object? obj);
}
