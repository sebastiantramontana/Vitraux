# Instructions for GitHub Copilot

These instructions guide AI code suggestions and human contributions so they stay consistent with Vitraux’s goals.

## 1. Project Domain (Scope)
Vitraux is a .NET 9 library that declaratively maps .NET ViewModels to standard HTML in WebAssembly (Blazor WASM runtime bootstrap only). It generates and executes JavaScript functions to perform efficient, incremental DOM updates without introducing a component model or binding engine like Razor Components.

## 2. Core Principles
- Separation of concerns: Backend logic (C#) vs Presentation (HTML/JS) must remain cleanly decoupled.
- Declarative mapping describes *what* updates; generated JS implements *how*.
- Minimum runtime surface: avoid hidden magic and large abstractions.
- Favor immutability (records for data models).
- Predictable, incremental DOM updates (no full re-renders unless mandatory).
- No dependency on Razor components, tag helpers, or server-side rendering.
- Opt-in Shadow DOM usage (configurable).
- Trimmable / AOT-friendly in Release builds.

## 3. Domain Vocabulary
| Term | Meaning |
| ---- | ------- |
| ViewModel (VM) | Immutable data instance passed from .NET to the browser. |
| Mapping | Declaration connecting VM members to HTML targets. |
| Initialization Function | Generated JS function that resolves & caches DOM/template references. |
| Update Function | Generated JS function that applies diffs / updates. |
| Query Element Strategy (QES) | Strategy controlling how DOM nodes are queried & cached. |
| Template / Fragment | HTML reusable block (native \<template\> or fetched fragment). |
| Value Mapper | Single property mapping (MapValue). |
| Collection Mapper | Repeated element mapping (MapCollection). |
| VMUpdateFunctionCaching | Strategy to reuse previously generated functions. |

## 4. Non‑Goals
- No server-side rendering.
- No Razor component tree, routing, or layout system.
- No virtual DOM or JS framework integration (React/Vue/Angular).
- No ORM-like patterns or persistence abstractions.
- No runtime code generation via eval / Function().

## 5. C# Code Style
- File-scoped namespaces.
- Use `sealed` for classes not intended for inheritance.
- Prefer `record` / `record class` for immutable data models.
- Private fields: `_camelCase`.
- Null validation: `ArgumentNullException.ThrowIfNull(arg);`
- Avoid `async void` (except event handlers not used here).
- Use `readonly struct` only for small immutable value types.
- Use `var` when type is obvious or improves brevity.
- Public APIs: document intent & extension points (XML doc).
- Keep methods focused (< ~40 lines ideally).
- Avoid reflection in hot paths; isolate any reflective bootstrap.
- Release build must remain trimmable: mark roots consciously.

## 6. JavaScript Generation & Runtime Guidelines
- Generated JS must:
  - Be ES modules only when explicitly needed (dynamic imports for user modules).
  - Avoid global pollution beyond the established `globalThis.vitraux` namespace.
  - Access pre-cached references when QES allows (avoid repeated `querySelector`).
  - Use pure functions where practical (no hidden state mutation).
  - Avoid introducing external frameworks or reactive wrappers.
- Variable naming inside generated functions:
  - Short internal tokens (`e0`, `c9`, `f2`) are acceptable (size + performance).
  - Do not rely on these tokens outside the generated scope.
- Avoid allocating intermediate arrays when iterables suffice.
- In loops: prefer classic `for` / `for...of` over LINQ-like patterns for hot paths.
- Dynamic imports permitted only for user-provided extension modules (e.g., custom callbacks).

## 7. Performance & Memory Guidelines
- Do not copy collections unnecessarily; pass spans/iterables when feasible.
- Avoid LINQ in tight loops (replace with explicit iteration).
- Batch DOM writes where possible (group related updates).
- Prefer textContent updates over innerHTML unless HTML injection is explicitly required.
- Keep mapping evaluation allocation-light (avoid closures capturing large objects).
- Ensure trimming/AOT warnings are addressed (no suppression without justification).

## 8. Security
- HTML insertion only where explicitly configured (e.g., `ToHtml`).
- Treat any mapped HTML as trusted only if validated at source.
- Never generate or suggest code using `eval`, `new Function`, or arbitrary string execution.
- Do not expose internal element cache keys externally.

## 9. Dependency Injection (DI)
- Only register what is used.
- `AddSingleton` for stateless services or caches.
- `AddScoped` when stateful per logical session (minimize this).
- No ambient service locators; rely on constructor injection.
- Avoid accidental service duplication (no duplicate registration of same concrete type w/o intent).

## 10. Mapping Design Rules
- Flat, readable chained API (fluent style).
- `MapValue` and `MapCollection` must remain orthogonal.
- Collection mapping:
  - Row/template population must not assume ordering stability unless documented.
  - Provide extension points for custom row initialization hooks.
- Templates:
  - Support both inline `<template>` and fetched external fragments.
  - Fetched fragments must be cached respecting strategy (OnlyOnce / ByVersion, etc.).

## 11. Update Function Semantics
- Only update DOM nodes when value changed AND value is valid.
- Null / undefined / empty rules:
  - If value invalid -> skip mutation (do not clear unless mapping states so).
- When executing custom user JS functions:
  - Catch & surface errors in a controlled way (avoid breaking entire update pipeline).
  - Execute after main DOM mutation for that segment unless dependency requires otherwise.

## 12. Generated File Conventions
- Header comment: “This file is generated by Vitraux. Do not edit manually.”
- Keep deterministic order for reproducibility (important for caching/versioning).
- Avoid environment-specific paths; use relative normalized paths.
- If code changes generation schema, increment internal generator version constant.

## 13. Caching Strategies
- `VMUpdateFunctionCaching.ByVersion("x.y")`:
  - Version string must uniquely reflect breaking mapping changes.
  - Copilot suggestions adding new mapping features must remind updating version when necessary.
- Provide future extension point for content hashing (not implemented yet).

## 14. Testing Guidelines
- Test name pattern: `MethodOrFeature_Scenario_ExpectedOutcome`.
- Deterministic: no random delays or sleeps.
- Prefer hand-written test doubles before using Moq.
- Cover:
  - Mapping chain validity.
  - DOM diff logic (simulated).
  - Template insertion & collection growth/shrink.
  - Caching branch (first run vs subsequent run).
  - Shadow DOM optional path.
- Avoid overfitting tests to internal variable names (`e0`, `c9`, etc.).

## 15. Build & Packaging
- Release:
  - Trimming enabled.
  - AOT compatibility targeted (validate no reflection regression).
- Keep public API surface minimal; mark internal helpers as `internal`.
- Ensure README + package description remain aligned (no stale examples).

## 16. Contribution Checklist (Human + AI)
When adding features:
1. Add or update tests.
2. Update `docs/reference-manual.md` if a new strategy / mapping capability is introduced.
3. Update `ARCHITECTURE.md` if a new layer or service type is added.
4. Consider performance impact (hot paths).
5. Evaluate trimming warnings.
6. If generator output schema changes → bump generator version.

## 17. Prompts (Examples for Copilot Chat)
Use these patterns to keep suggestions aligned:

- “Add a new QueryElementStrategy that revalidates cached nodes if an attribute marker changes.”
- “Refactor the JS generator to split DOM selection caching from update emission.”
- “Introduce hashing-based caching for generated update functions while retaining version fallback.”
- “Generate tests for collection diff behavior when items removed mid-sequence.”
- “Add Shadow DOM opt-out example to documentation.”

## 18. What to Avoid (Restated)
- Recommending Razor components or .razor files.
- Adding SPA router abstractions.
- Introducing global mutable singletons for convenience.
- Large external libraries (React, Angular, lit, etc.).
- Active Record or persistence layers.
- Unnecessary use of `dynamic` or reflection-based dispatch in hot code.

## 19. Error Handling & Observability (Future-Safe Guidance)
- Prefer returning structured errors or throwing explicit exceptions (C#).
- In JS, log through a centralized `globalThis.vitraux.log` abstraction (pluggable).
- Do not swallow errors silently unless documented.

## 20. File & Folder Conventions
- `JsCodeGeneration/` (core generators)
- `Mappings/` (model mapping configuration classes)
- `Templates/` (if embedded or external fragment samples)
- `docs/` (manual + architecture)
- `test/` (unit + manual generation fixtures)

## 21. Style Enforcement
Rely on `.editorconfig` + analyzers; suggestions should not contradict enforced styles:
- Warnings-as-errors.
- File-scoped namespaces.
- Prefer expressive naming over comments.

## 22. Extensibility Notes
When suggesting extensibility:
- Offer clean interfaces over flags explosion.
- Keep constructor parameter counts low (composition over parameter bloat).
- Prefer optional service extension via `IServiceCollection` extension methods.

## 23. Shadow DOM Considerations
- Suggestions must not assume Shadow DOM always enabled.
- Generated code should branch cleanly if host uses regular DOM.

## 24. Safety Checks
Copilot must highlight:
- Any mutability introduced into previously immutable structures.
- Any accidental reliance on execution ordering not guaranteed by spec.
- Introduction of allocation-heavy patterns in tight loops.

## 25. Decision Record (Optional Practice)
If major architectural change suggested: encourage adding concise ADR (docs/adr/XXXX-short-title.md) answering:
- Context
- Decision
- Alternatives
- Consequences

---

Maintainers & AI should treat this document as authoritative for style, architecture direction, and acceptable scope. Update it intentionally—avoid drift with README and reference manual.