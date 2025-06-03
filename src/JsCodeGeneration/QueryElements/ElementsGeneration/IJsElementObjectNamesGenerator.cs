using Vitraux.Modeling.Data.Selectors;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal interface IJsElementObjectNamesGenerator
{
    public IEnumerable<JsObjectName> Generate(string namePrefix, IEnumerable<SelectorBase> selectors);
}
