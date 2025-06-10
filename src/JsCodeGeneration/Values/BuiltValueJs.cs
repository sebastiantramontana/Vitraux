namespace Vitraux.JsCodeGeneration.Values;

internal record class BuiltValueJs(string JsCode, IEnumerable<ValueViewModelSerializationData> ValueViewModelSerializationsData);
