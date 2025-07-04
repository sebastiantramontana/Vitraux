using System.Buffers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Vitraux.Execution.Tracking;

namespace Vitraux.Execution.Serialization;

internal class ViewModelJsonSerializer : IViewModelJsonSerializer
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

    private static void SerializeObjectToJson(EncodedTrackedViewModelAllData encodedTrackedViewModelAllData, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WriteStartObject();
        SerializePropertiesToJson(encodedTrackedViewModelAllData, utf8JsonWriter);
        utf8JsonWriter.WriteEndObject();
    }

    private static void SerializePropertiesToJson(EncodedTrackedViewModelAllData encodedTrackedViewModelAllData, Utf8JsonWriter utf8JsonWriter)
    {
        SerializeValuesToJson(encodedTrackedViewModelAllData.ValueProperties, utf8JsonWriter);
        SerializeCollectionsToJson(encodedTrackedViewModelAllData.CollectionProperties, utf8JsonWriter);
    }

    private static void SerializeValuesToJson(IEnumerable<EncodedTrackedViewModelValueData> values, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var value in values)
        {
            SerializeObjectValueToJson(value.ValuePropertyName, value.PropertyValue, utf8JsonWriter);
        }
    }

    private static void SerializeObjectValueToJson(JsonEncodedText propertyName, string? value, Utf8JsonWriter utf8JsonWriter)
    {
        if (value is null)
        {
            utf8JsonWriter.WriteNull(propertyName);
        }
        else
        {
            utf8JsonWriter.WriteString(propertyName, value.ToString());
        }
    }

    private static void SerializeCollectionsToJson(IEnumerable<EncodedTrackedViewModelCollectionData> collections, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var col in collections)
        {
            SerializeCollectionToJson(col, utf8JsonWriter);
        }
    }

    private static void SerializeCollectionToJson(EncodedTrackedViewModelCollectionData collection, Utf8JsonWriter utf8JsonWriter)
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
