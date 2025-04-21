using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal interface IJsObjectNamesGenerator
{
    public IEnumerable<JsObjectName> Generate(string namePrefix, IEnumerable<SelectorBase> selectors);
}
