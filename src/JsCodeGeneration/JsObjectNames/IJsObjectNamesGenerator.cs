namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IJsObjectNamesGenerator
{
    string GenerateUniqueObjectName(string namePrefix, string initialName);
}
