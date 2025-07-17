using System.Buffers;
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
            case EncodedTrackedViewModelStringValueData stringValue:
                SerializeStringValueToJson(stringValue, utf8JsonWriter);
                break;
            case EncodedTrackedViewModelObjectValueData objectValue:
                SerializeObjectValueToJson(objectValue, utf8JsonWriter);
                break;
            default:
                notImplementedCaseGuard.ThrowException(value);
                break;
        }
    }

    private void SerializeObjectValueToJson(EncodedTrackedViewModelObjectValueData objectValue, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WritePropertyName(objectValue.ValuePropertyName);
        SerializeObjectToJson(objectValue.PropertyAllData, utf8JsonWriter);
    }

    private static void SerializeStringValueToJson(EncodedTrackedViewModelStringValueData stringValue, Utf8JsonWriter utf8JsonWriter)
        => utf8JsonWriter.WriteString(stringValue.ValuePropertyName, stringValue.PropertyValue);

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
