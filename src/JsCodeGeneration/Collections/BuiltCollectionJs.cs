﻿namespace Vitraux.JsCodeGeneration.Collections;

internal record class BuiltCollectionJs(string JsCode, IEnumerable<CollectionViewModelSerializationData> CollectionViewModelSerializationsData);
