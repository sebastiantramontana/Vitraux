namespace Vitraux.Execution.Serialization;

internal record class SerializableValueInfo(object? Value, Type ValueType, bool IsSimpleType);
