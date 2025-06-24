using System.Buffers;
using System.Collections;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Serialization;

internal class ViewModelJsonSerializer : IViewModelJsonSerializer
{
    public async Task<string> Serialize(ViewModelJsNames viewModelSerializationData, object viewModel)
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

        SerializeObjectToJson(viewModelSerializationData, viewModel, writer);

        await writer.FlushAsync();

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }

    private static void SerializeObjectToJson(ViewModelJsNames viewModelSerializationData, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WriteStartObject();
        SerializePropertiesToJson(viewModelSerializationData, obj, utf8JsonWriter);
        utf8JsonWriter.WriteEndObject();
    }

    private static void SerializePropertiesToJson(ViewModelJsNames viewModelSerializationData, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        SerializeValuesToJson(viewModelSerializationData.ValueProperties, obj, utf8JsonWriter);
        SerializeCollectionsToJson(viewModelSerializationData.CollectionProperties, obj, utf8JsonWriter);
    }

    private static void SerializeValuesToJson(IEnumerable<ViewModelJsValueName> values, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var value in values)
        {
            SerializeValueToJson(value, obj, utf8JsonWriter);
        }
    }

    private static void SerializeValueToJson(ViewModelJsValueName value, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        var retValue = value.ValuePropertyValueDelegate.DynamicInvoke(obj);
        SerializeObjectValueToJson(value.ValuePropertyName, retValue, utf8JsonWriter);
    }

    private static void SerializeObjectValueToJson(JsonEncodedText propertyName, object? value, Utf8JsonWriter utf8JsonWriter)
    {
        if (value is null)
        {
            utf8JsonWriter.WriteNull(propertyName);
        }
        else
        {
            if (IsSimpleType(value.GetType()))
            {
                utf8JsonWriter.WriteString(propertyName, value.ToString());
            }
            else
            {
                //It will be handled when ToOwnMapping be implemented, in the meantime it returns an empty object here.
                utf8JsonWriter.WritePropertyName(propertyName);
                utf8JsonWriter.WriteStartObject();
                utf8JsonWriter.WriteEndObject();
            }
        }
    }

    private static bool IsSimpleType(Type type)
        => type.IsPrimitive ||
            type.IsEnum ||
            type == typeof(string) ||
            type == typeof(decimal) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(Guid) ||
            type == typeof(TimeSpan) ||
            type == typeof(DateOnly) ||
            type == typeof(TimeOnly) ||
            (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

    private static void SerializeCollectionsToJson(IEnumerable<ViewModelJsCollectionName> collections, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var col in collections)
        {
            SerializeCollectionToJson(col, obj, utf8JsonWriter);
        }
    }

    private static void SerializeCollectionToJson(ViewModelJsCollectionName collection, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WritePropertyName(collection.CollectionPropertyName);
        utf8JsonWriter.WriteStartArray();

        var collectionItems = GetTypedEnumerableObjectFromDelegateInvokation(collection.CollectionPropertyValueDelegate, obj);

        foreach (var child in collection.Children)
        {
            foreach (var item in collectionItems)
            {
                SerializeObjectToJson(child, item, utf8JsonWriter);
            }
        }

        utf8JsonWriter.WriteEndArray();
    }

    private static IEnumerable<object> GetTypedEnumerableObjectFromDelegateInvokation(Delegate del, object obj)
    {
        var enumerable = del.DynamicInvoke(obj) as IEnumerable;
        return enumerable?.Cast<object>() ?? [];
    }
}
