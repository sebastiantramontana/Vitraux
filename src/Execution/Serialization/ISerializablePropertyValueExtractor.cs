namespace Vitraux.Execution.Serialization;

internal interface ISerializablePropertyValueExtractor
{
    SerializableValueInfo GetValueInfo(Delegate @delegate, object? obj);
    object GetSafeValue(object? value);
    IEnumerable<object> GetCollection(Delegate @delegate, object? obj);
}
