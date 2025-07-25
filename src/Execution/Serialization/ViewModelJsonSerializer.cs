using System.Buffers;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Vitraux.Execution.Tracking;
using Vitraux.Helpers;

namespace Vitraux.Execution.Serialization;

internal class ViewModelJsonSerializer(INotImplementedCaseGuard notImplementedCaseGuard) : IViewModelJsonSerializer
{
    public async Task<string> Serialize(EncodedTrackedViewModelAllData encodedTrackedViewModelAllData)
    {
        var buffer = new ArrayBufferWriter<byte>();
        await using var writer = new Utf8JsonWriter(buffer, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.Default,
            Indented = false,
            IndentCharacter = ' ',
            IndentSize = 4,
            SkipValidation = true,
            NewLine = Environment.NewLine,
            MaxDepth = 0
        });

        SerializeObjectToJson(encodedTrackedViewModelAllData, writer);

        await writer.FlushAsync();

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }

    private void SerializeObjectToJson(EncodedTrackedViewModelAllData encodedTrackedViewModelAllData, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WriteStartObject();
        SerializePropertiesToJson(encodedTrackedViewModelAllData, utf8JsonWriter);
        utf8JsonWriter.WriteEndObject();
    }

    private void SerializePropertiesToJson(EncodedTrackedViewModelAllData encodedTrackedViewModelAllData, Utf8JsonWriter utf8JsonWriter)
    {
        SerializeValuesToJson(encodedTrackedViewModelAllData.ValueProperties, utf8JsonWriter);
        SerializeCollectionsToJson(encodedTrackedViewModelAllData.CollectionProperties, utf8JsonWriter);
    }

    private void SerializeValuesToJson(IEnumerable<EncodedTrackedViewModelValueData> values, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var value in values)
        {
            SerializeValueToJson(value, utf8JsonWriter);
        }
    }

    private void SerializeValueToJson(EncodedTrackedViewModelValueData value, Utf8JsonWriter utf8JsonWriter)
    {
        switch (value)
        {
            case EncodedTrackedViewModelSimpleValueData simpleValue:
                SerializeSimpleValueToJson(simpleValue, utf8JsonWriter);
                break;
            case EncodedTrackedViewModelComplexObjectValueData complexValue:
                SerializeComplexObjectValueToJson(complexValue, utf8JsonWriter);
                break;
            default:
                notImplementedCaseGuard.ThrowException(value);
                break;
        }
    }

    private void SerializeComplexObjectValueToJson(EncodedTrackedViewModelComplexObjectValueData objectValue, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WritePropertyName(objectValue.ValuePropertyName);
        SerializeObjectToJson(objectValue.PropertyAllData, utf8JsonWriter);
    }

    private static void SerializeSimpleValueToJson(EncodedTrackedViewModelSimpleValueData value, Utf8JsonWriter utf8JsonWriter)
    {
        switch (value.PropertyValue)
        {
            case bool boolValue:
                utf8JsonWriter.WriteBoolean(value.ValuePropertyName, boolValue);
                break;
            case byte byteValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, byteValue);
                break;
            case ushort ushortValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, ushortValue);
                break;
            case uint uintValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, uintValue);
                break;
            case ulong ulongValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, ulongValue);
                break;
            case SByte sbyteValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, sbyteValue);
                break;
            case short shortValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, shortValue);
                break;
            case int intValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, intValue);
                break;
            case long longValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, longValue);
                break;
            case Int128 or UInt128 or BigInteger:
                utf8JsonWriter.WriteString(value.ValuePropertyName, value.PropertyValue.ToString());
                break;
            case float floatValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, floatValue);
                break;
            case double doubleValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, doubleValue);
                break;
            case decimal decimalValue:
                utf8JsonWriter.WriteNumber(value.ValuePropertyName, decimalValue);
                break;
            case DateTime dateTimeValue:
                utf8JsonWriter.WriteString(value.ValuePropertyName, dateTimeValue);
                break;
            case DateTimeOffset dateTimeOffsetValue:
                utf8JsonWriter.WriteString(value.ValuePropertyName, dateTimeOffsetValue);
                break;
            case Guid guidValue:
                utf8JsonWriter.WriteString(value.ValuePropertyName, guidValue);
                break;
            default:
                utf8JsonWriter.WriteString(value.ValuePropertyName, value.PropertyValue.ToString());
                break;
        }
    }

    private void SerializeCollectionsToJson(IEnumerable<EncodedTrackedViewModelCollectionData> collections, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var col in collections)
        {
            SerializeCollectionToJson(col, utf8JsonWriter);
        }
    }

    private void SerializeCollectionToJson(EncodedTrackedViewModelCollectionData collection, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WritePropertyName(collection.ValuePropertyName);
        utf8JsonWriter.WriteStartArray();

        foreach (var child in collection.DataChildren)
        {
            SerializeObjectToJson(child, utf8JsonWriter);
        }

        utf8JsonWriter.WriteEndArray();
    }
}
