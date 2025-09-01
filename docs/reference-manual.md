# Vitraux Reference Manual

This manual explains the public surface and runtime model of Vitraux for .NET and front-end developers. It focuses on how mappings are declared in C#, how the library generates and invokes JavaScript to update the DOM, configuration options, caching, and change tracking.

---

## Concepts at a Glance

- ViewModel mapping: declare how a .NET ViewModel projects into HTML elements.
- Generated JS: at startup, Vitraux generates two JS functions per ViewModel on the fly: an initialize function and an update function.
- Element query strategies: control when/where DOM queries happen to resolve target elements.
- Change tracking: send full data or only changes on Update.
- Caching: reuse generated JS across sessions by version.
- Trimmable & AOT-ready (Blazor WebAssembly): safe to enable PublishTrimmed and AOT; no runtime codegen or reflective serializers.
- Loosely coupled HTML: your HTML stays plain; mapping targets are selected by IDs, selectors, templates, or URIs.

---

## Setup and Bootstrapping

Vitraux integrates via DI and a build step to prepare JS functions for your mapped ViewModels.

1) Add services and optional configuration

```csharp
_ = builder.Services
			.AddVitraux()                              // registers core services
			.AddConfiguration(() => new VitrauxConfiguration
			{
					UseShadowDom = true                    // optional; default: true
			})
			.AddModelConfiguration<MyViewModel, MyViewModelConfiguration>()
            .AddModelConfiguration<MyOtherViewModel, MyOtherViewModelConfiguration>(); //Add your viewmodels
```

2) Build Vitraux (generates/initializes JS functions)

```csharp
await host.Services.BuildVitraux();
```

3) Resolve an `IViewUpdater<TViewModel>` and call `Update` whenever you need to sync the UI

```csharp
var updater = host.Services.GetRequiredService<IViewUpdater<MyViewModel>>();
await updater.Update(vm);
```

### APIs involved

- `AddVitraux(this IServiceCollection)` registers internal subsystems.
- `AddConfiguration(Func<VitrauxConfiguration>)` or `AddDefaultConfiguration()` provides global options passed to the JS runtime at build time.
- `AddModelConfiguration<TViewModel, TConfiguration>()` wires a mapping and registers an updater and trackers for that ViewModel.
- `BuildVitraux(this IServiceProvider)` triggers code generation and initialization per registered ViewModel.

---

## ViewModel Mapping DSL

You declare mapping inside an `IModelConfiguration<TViewModel>` implementation via the provided fluent builder (`IModelMapper<TViewModel>`). Just an example:

```csharp
public class PetOwnerConfiguration : IModelConfiguration<PetOwner>
{
		public ConfigurationBehavior ConfigurationBehavior { get; } = new()
		{
				QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
				TrackChanges = true,
				VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("1.0")
		};

		public ModelMappingData ConfigureMapping(IModelMapper<PetOwner> model)
				=> model
						.MapValue(po => po.Name)                   // value mapping
								.ToElements.ById("petowner-name").ToContent
						.MapValue(po => po.HtmlComments)
								.ToElements.ByQuery(".comments").ToHtml
						.MapCollection(po => po.Pets)              // collection mapping
								.ToTables.ById("petowner-pets")
								.PopulatingRows.ToTBody(0)
										.FromTemplate("petowner-pet-row")
												.MapValue(pet => pet.Name)
														.ToElements.ByQuery("[data-id='pet-name']").ToContent
												.MapValue(pet => pet.DateOfBirth)
														.ToElements.ByQuery("[data-id='pet-date-of-birth']").ToContent
								.EndCollection
						.Data;
}
```

### Map values

- `MapValue<TValue>(Func<TViewModel, TValue>)` starts a value mapping chain.
  - As Vitraux doesn't bind properties the delegate signature is `Func<TViewModel, TValue>`. The first generic parameter is the source `TViewModel` (or `TItem` in inner collections) and the last is any type `TValue`. 
	So, the following `MapValue` would be valid for Vitraux although no one would do that: `MapValue<TValue>(pet => Int32.Parse(pet.DateOfBirth.ToString("yyyy")) + 2000)`
- `.ToElements.ById(string)` or `.ByQuery(string)` selects target element(s).
- `.ToContent` sets `textContent` via the built-in JS call.
- `.ToHtml` sets `innerHTML`: Injected html is a user responsability.
- `.ToAttribute(string attribute)` sets/toggles the given attribute:
  - If the mapped value is boolean: `true` adds the attribute (presence-based), `false` removes it. Ideal for boolean attributes like `disabled`, `checked`, `hidden`.
  - Otherwise: sets the attribute to the stringified value, e.g., `href`, `src`, `data-*`.

Advanced value targets:

- `.Insert.FromTemplate(string templateId)|FromUri(Uri uri).ToChildren.ByQuery(string).ToContent|ToHtml|ToAttribute(...)`
	- Insert a fragment (from a `<template>` or external URI) into matching target elements and then place the value into children matched by the provided selector.
- `.ToJsFunction(string functionName)`
	- Delegate rendering to your own JS function. Optionally pair with `.FromModule(Uri)` to import an ES module before invocation.
- `.ToOwnMapping`
	- Treat the value object type as another ViewModel; Vitraux invokes that ViewModel’s update function with the nested value.

### Map collections

- `MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>>)` starts a collection mapping chain with the item type. The parameter is a `Func<...>` and follow the same notes as `MapValue(...)` as above.

Targets:

- `.ToTables.ById(string)|ByQuery(string)` then `.PopulatingRows.ToTBody(intIndex).FromTemplate(string)|FromUri(Uri)`
	- Inserts one row per item using a row template/URI; for each row you can call `.MapValue(...)` or `.MapCollection(...)` relative to the item.
- `.ToContainerElements.ById(string)|ByQuery(string).FromTemplate(string)|FromUri(Uri)`
	- Populates non-table container(s) by appending a template per item and mapping values inside.
- `.ToJsFunction(string)` (with optional `.FromModule(Uri)`)
	- Hand off collection rendering to a custom JS function.

Inner mapping inside collections:

- Within item scope you can:
	- `MapValue(item => item.Prop).ToElements...`
	- `MapCollection(item => item.Children).ToTables...` or `.ToContainerElements...`
	- `.ToOwnMapping` to reuse an existing model configuration for the item type.
	- `.ToJsFunction(string)`... the same as `MapValue(...)`.

End Collection:

- `EndCollection` finish the collection scope to return the previous builder to continue mapping more properties.

End of chains:
- `IModelMapper<TViewModel>` expose the `.Data` property to return `ConfigureMapping(...)`.

### ⚠️ Important
Custom JS functions passed to `ToJsFunction(...)` are always awaited and must return a Promise.

---

## ConfigurationBehavior

`ConfigurationBehavior` lives in your `IModelConfiguration<TViewModel>` and controls runtime behavior.

- `QueryElementStrategy` (default: `OnlyOnceAtStart`)
	- `OnlyOnceAtStart`: Query and store DOM element references at initialization time only. Fastest updates, but requires HTML elements to exist at startup.
	- `OnlyOnceOnDemand`: Defer element declarations until the first update call that needs them. Useful when fragments are lazy-inserted.
	- `Always`: Resolve selectors every update. Safest for highly dynamic DOM, but slower.

- `TrackChanges` (default: false)
	- `false`: Serialize full ViewModel into JSON every `Update`. Simple and predictable.
	- `true`: Use shallow change tracking for top-level value and collection properties:
		- Values: compares simple values by `Equals`; complex objects track whole sub-objects when reference or simple members change. Nested object serialization leverages the child ViewModel’s mapping names.
		- Collections: compares sequence by order and equality; when different, the entire collection is sent.

- `VMUpdateFunctionCaching`
	- `NoCache` (default): Generate and initialize JS functions every run.
	- `ByVersion("x.y")`: Try to reuse previously cached JS functions in local storage identified by version; if not present, generate and cache them under that version. Bump the version to invalidate cache.

---

## IViewUpdater<TViewModel>

`IViewUpdater<TViewModel>.Update(TViewModel vm)` serializes the ViewModel (or changes) and invokes the generated update JS function for that ViewModel.

- Blazor WebAssembly interop has non-trivial overhead per call. Instead of wiring property-level bindings (e.g., INotifyPropertyChanged) that trigger many small interop calls, Vitraux batches the UI sync: one call per Update carrying the whole ViewModel (or its diff). The generated JS applies all changes in one pass.

**Pros**
- Fewer JS interop round-trips (typically one per update).
- Deterministic, atomic UI updates per call.
- No INotifyPropertyChanged or event wiring required.
- Works well with TrackChanges to minimize payload size.

**Cons**
- Updates are explicit; you must call Update when state changes.
- Large ViewModels without TrackChanges can increase payload size.
- No per-property “live binding”; partial updates require calling Update with the latest model (or rely on TrackChanges).

Pipeline:

1) Select changes tracker based on `TrackChanges`.
2) Produce an encoded structure containing value properties and collection properties aligned to generated JS names.
3) Serialize to JSON with a tuned `Utf8JsonWriter` for speed.
4) Invoke the generated update view JS function.

Error handling notes:

- Passing `null` to `Update` is a no-op.
- If a selector resolves zero elements, the specific handler is a no-op for that value/collection (no exception).

---

## The Vitraux JavaScript runtime: vitraux-\<version\>-min.js (let’s just refer to it as vitraux.js)

**Why you must include it**
- The C# side (BuildVitraux and Update) calls into a well-known JS API exposed on globalThis.vitraux. If vitraux-\<version\>-min.js is not loaded, those interop calls fail and no DOM updates can run.
- The file hosts the runtime that stores your generated functions, performs DOM operations, manages caching, and bridges .NET \<-\> JS.

### ⚠️ Important
Ensure that vitraux-\<version\>-min.js matches with Vitraux Nuget's package version!

**Recommended script tags**
- Load the runtime before the Blazor autostart or before your app first calls BuildVitraux/Update:
```html
<script src="js/vitraux-<version>-min.js"></script>
<script src="_framework/blazor.webassembly.js" autostart="false"></script>
```

**Load order and hosting notes**
- Must be loaded before the first interop to globalThis.vitraux:
  - If you call BuildVitraux during Program.Main, ensure vitraux-\<version\>-min.js is present before Blazor starts or at least before the first Update. Placing it in \<head\> is safe.
  - With autostart="false", you typically call Blazor.start() from your page script; vitraux-\<version\>-min.js should be loaded earlier in the page so the .NET code finds globalThis.vitraux when it initializes.
- The runtime uses modern JS (Promises and dynamic import). For .ToJsFunction().FromModule(...), the browser must support ES module dynamic import.
- Versioning: the ByVersion("x.y") cache path relies on the runtime’s versioned registry to reuse compiled functions across app sessions. Bump the version whenever you change mappings so vitraux.js knows to discard the old functions.
- Security: ToHtml writes innerHTML as-is. Sanitize untrusted content on the .NET side or use a custom JS target that sanitizes before inserting. Sanitization is user responsability.

## Generated JavaScript and Interop

At build time per ViewModel type, Vitraux:

1) Computes a unique ViewModel key.
2) Generates two JS function sources:
	 - Initialize JS: declares and optionally stores element references according to the selected `QueryElementStrategy`.
	 - Update JS: orchestrates value placements and collection updates, returning a resolved Promise.
3) Initializes functions in the browser depending on caching:
	 - `NoCache`: `globalThis.vitraux.updating.vmFunctions.initializeNonCachedViewFunctions(vmKey, initJs, updateJs)`
	 - `ByVersion`: try `tryInitializeViewFunctionsFromCacheByVersion(vmKey, version)`, else `initializeNewViewFunctionsToCacheByVersion(vmKey, version, initJs, updateJs)`

During `Update`, C# calls the in-process JS function:

- `executeUpdateViewFunctionFromJson(vmKey, json)` which parses JSON and calls the cached update function for that ViewModel key.

### Short example (autogenerated update JS, shortened)
```javascript
...
const n0_c0_e16 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.inner-nav-antiparasitics');
const n0_c0_c17 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/row-antiparasitics.html');

if(globalThis.vitraux.updating.utils.isValueValid(item.v0)) {
    globalThis.vitraux.updating.dom.setElementsContent(n0_c0_e10, item.v0);
    globalThis.vitraux.updating.dom.setElementsAttribute(n0_c0_e11, 'href', item.v0);
    globalThis.vitraux.updating.dom.setElementsAttribute(n0_c0_e12, 'href', item.v0);
}
...
```

### Built-in DOM operations (invoked by generated JS)

Generated code calls into `globalThis.vitraux.updating.dom.*` helpers:

- `setElementsContent(elements, value)` – set `textContent` on a collection of elements or a single element.
- `setElementsHtml(elements, html)` – set `innerHTML`.
- `setElementsAttribute(elements, name, value)` – set attribute on each element.
- `updateCollectionByPopulatingElements(appendToElements, elementToInsert, updateCallback, collection)` – generic list rendering; clones a fragment and calls `updateCallback(parentElement, item)` for each item.
- `updateTable(tables, tbodyIndex, rowTemplate, updateCallback, collection)` – table row rendering specialized for `<table>`.

### Custom JavaScript targets

Value or collection mappings can call a custom function:

- `.ToJsFunction("myLib.renderItem")` generates `await myLib.renderItem(obj)`.
- `.FromModule(new Uri("/js/my-module.js", UriKind.Relative))` imports as an ES module before invocation:
	- Generates `const mN = await import('/js/my-module.js'); await mN.renderItem(obj);`

### Own mapping (composition)

- `.ToOwnMapping` tells Vitraux to route the sub-object to the update function of its own ViewModel type, using the existing mapping and JS code for that type. The generated JS uses `executeUpdateViewFunction(vmKey, obj)` to delegate.

---

## Element Query Strategies in Depth

The strategy impacts two places:

- Initialization JS: how/when element variables are declared and resolved.
- Update JS: whether the query lines are emitted per update.

Behavior summary:

- OnlyOnceAtStart
	- Initialization code queries elements (IDs, selectors, templates/URIs) and stores references accessible to update functions.
	- Updates only set content/HTML/attributes and perform insertions using stored references.

- OnlyOnceOnDemand
	- Initialization emits deferred declarations; update code ensures first-use resolution and then reuses references.

- Always
	- Update code includes queries every call, ensuring elements are re-resolved (robust with dynamic DOM, slower).

Choose the least dynamic strategy that fits your page for performance.

---

## Data Serialization and Change Tracking

Vitraux maps C# property accessors to generated JS names and serializes as a flat JSON object with nested objects/arrays matching your mapping.

- Simple values: bool/number/string/DateTime/Guid, etc., are written as JSON primitives.
- Complex values: serialized as nested objects, using the child mapping’s generated names.
- Collections: serialized as arrays of mapped item objects.

When `TrackChanges = true`:

- Values are compared to previous state; only changed values are sent. Complex objects are treated atomically at the first level (if the reference or any shallow member differs, the object subtree is sent).
- Collections are compared by order and equality; if different, the whole collection is sent. Item-level diffing is not performed.

When `TrackChanges = false`:

- The full ViewModel is serialized every time.

Note: Name encoding uses `JsonEncodedText` to keep property names safe for transport.

---

## Mapping Reference (Fluent API)

Root mapper: `IModelMapper<TViewModel>`

- `IRootValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> selector)`
- `IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> selector)`
- `ModelMappingData Data { get; }` (result of the fluent mapping)

Value mapping chain:

- `.ToElements` → `ById(string)` | `ByQuery(string)` → `ToContent` | `ToHtml` | `ToAttribute(string)`
- Optional insertion for values: `.Insert.FromTemplate(string)|FromUri(Uri).ToChildren.ByQuery(string).ToContent|ToHtml|ToAttribute(...)`
- Alternative targets:
	- `.ToJsFunction(string)` [optional: `.FromModule(Uri)`]
	- `.ToOwnMapping`

Collection mapping chain:

- `.ToTables.ById(string)|ByQuery(string).PopulatingRows.ToTBody(int).FromTemplate(string)|FromUri(Uri)`
- `.ToContainerElements.ById(string)|ByQuery(string).FromTemplate(string)|FromUri(Uri)`
- Alternative target: `.ToJsFunction(string)` [optional: `.FromModule(Uri)`]
- Inside collection item mapping: `MapValue(...)`, `MapCollection(...)`, `.ToOwnMapping`, `.ToJsFunction(string)` [optional: `.FromModule(Uri)`]

---

## Configuration Reference

- `VitrauxConfiguration`
	- `bool UseShadowDom` (default: true). Affects whether Vitraux attaches to a shadow root in its internal helpers. Choose based on your styling/encapsulation needs.

- `ConfigurationBehavior`
	- `QueryElementStrategy QueryElementStrategy` – see strategies above.
	- `bool TrackChanges` – enable shallow change tracking.
	- `VMUpdateFunctionCaching VMUpdateFunctionCaching` – `NoCache` or `ByVersion(string)`.

---

## Execution Model and Lifecycle

Per ViewModel type during `BuildVitraux()`:

1) Mapping collection: Vitraux calls your `ConfigureMapping` to get `ModelMappingData`.
2) JS name generation: converts model data to strongly typed JS name structures (values, collections, element objects).
3) JS code generation: produces initialization and update source strings.
4) Caching and JS initialization:
	 - NoCache: initialize functions directly.
	 - ByVersion: try from cache else install new version into cache.
5) Names cache stored: `IViewModelJsNamesCacheGeneric<TViewModel>` keeps the ViewModel key and generated names to align serialized JSON with JS.

At runtime on `Update(vm)`:

1) Choose tracker (no-changes vs shallow-changes).
2) Produce encoded values/collections.
3) Serialize to JSON.
4) Call JS `executeUpdateViewFunctionFromJson` with the ViewModel key and JSON.

---

## Publishing, Trimming, and AOT (WebAssembly)

Vitraux is trimming-friendly and compatible with Blazor WebAssembly AOT.

Why it works
- No Reflection.Emit or runtime code generation.
- Minimal reflection (expression analysis only); no member activation by string.
- JSON payloads are written via Utf8JsonWriter from your mapping (no reflective serializers).
- JS interop uses static entry points.

---

## Custom JavaScript Integration

You can integrate existing JS utilities without rewriting them as components:

- Values: `.ToJsFunction("yourNamespace.setAvatar")` will receive the selected value object as the single argument.
- Collections: `.ToJsFunction("yourList.render")` receives the array for the mapped collection.
- ES Modules: chain `.FromModule(new Uri("/js/list.js", UriKind.Relative))` to dynamic-import before invoking.

Contract expectations:

- Custom functions are awaited (must return a Promise). Errors bubble to the caller update.
- When used on collections, you fully own DOM updates; Vitraux won’t auto-insert templates for that target.

---

## Tips, Patterns, and Edge Cases

- Prefer `OnlyOnceAtStart` for static pages. Switch to `OnlyOnceOnDemand` when elements originate from templates/URIs created by earlier updates. Use `Always` only if the DOM structure is highly dynamic between updates.
- Table rendering: ensure your template `<tr>` matches the expected structure and use `ToTBody(index)` of the target `<table>` if there are multiple `<tbody>` elements.
- Nested ViewModels: `.ToOwnMapping` composes mappings and reuses the child ViewModel’s generated update function for consistency.
- Collections diffing: with `TrackChanges = true`, any order/content change resends the entire collection. For fine-grained diffing, use a custom JS target.

---

## Minimal End-to-End Example (Recap)

```csharp
// Program.cs
_ = builder.Services
			.AddVitraux()
			.AddConfiguration(() => new VitrauxConfiguration { UseShadowDom = true })
			.AddModelConfiguration<PetOwner, PetOwnerConfiguration>();

await host.Services.BuildVitraux();

var updater = host.Services.GetRequiredService<IViewUpdater<PetOwner>>();
await updater.Update(new PetOwner { /* ... */ });
```

```csharp
// PetOwnerConfiguration.cs
public class PetOwnerConfiguration : IModelConfiguration<PetOwner>
{
		public ConfigurationBehavior ConfigurationBehavior { get; } = new()
		{
				QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
				TrackChanges = true,
				VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("1.0")
		};

		public ModelMappingData ConfigureMapping(IModelMapper<PetOwner> m)
				=> m
						.MapValue(x => x.Name).ToElements.ById("petowner-name").ToContent
						.MapCollection(x => x.Pets)
								.ToTables.ById("petowner-pets").PopulatingRows.ToTBody(0)
										.FromTemplate("petowner-pet-row")
												.MapValue(p => p.Name).ToElements.ByQuery("[data-id='pet-name']").ToContent
												.MapValue(p => p.DateOfBirth).ToElements.ByQuery("[data-id='pet-date-of-birth']").ToContent
								.EndCollection
						.Data;
}
```

The HTML only needs IDs/selectors and templates; Vitraux handles the rest.

---

## Versioning and Deployment

- When using `VMUpdateFunctionCaching.ByVersion`, bump the version whenever you change mappings or upgrade Vitraux so clients fetch fresh JS functions.
- The generated JS is transient in memory; the caching mechanism relies on the client’s JS runtime (via the Vitraux JS bootstrap) to store/reuse functions by version.

---

## Troubleshooting

- Elements not updating: verify selectors/IDs exist for the chosen `QueryElementStrategy`. For `OnlyOnceAtStart`, elements must exist at initialization; consider `OnlyOnceOnDemand`.
- No visible rows: ensure the template id/URI is correct and the fragment contains the expected child selectors used by your item mappings.
- Custom JS not called: confirm the function name is global or imported via `FromModule`. Functions must be callable and always return a Promise.
- Shadow DOM: if your app does not use Shadow DOM, set `UseShadowDom = false` and ensure your selectors match the light DOM.
