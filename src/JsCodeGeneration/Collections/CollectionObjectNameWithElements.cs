﻿namespace Vitraux.JsCodeGeneration.Collections;

internal record class CollectionObjectNameWithElements(string Name, IEnumerable<JsCollectionElementObjectPairNames> AssociatedElementNames);
