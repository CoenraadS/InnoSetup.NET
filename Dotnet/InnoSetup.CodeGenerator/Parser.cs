// 0. Generate Event Functions https://jrsoftware.org/ishelp/index.php?topic=scriptevents
// 1. Generate SupportFunctions (with Exception Handeling) https://jrsoftware.org/ishelp/index.php?topic=scriptfunctions
// 2. Reverse generate EntryPoints?
// 3. Generate classes containing the callbacks?

using System.Diagnostics.CodeAnalysis;
namespace InnoSetup.CodeGenerator;

internal class Parser
{
    bool TryGetType(string pascalType, [NotNullWhen(returnValue: true)] out Type? type)
    {
        if (pascalType == "")
        {
            type = typeof(void);
            return true;
        }

        if (pascalType == "Boolean")
        {
            type = typeof(bool);
            return true;
        }

        if (pascalType == "Integer")
        {
            type = typeof(int);
            return true;
        }

        if (pascalType == "String")
        {
            type = typeof(string);
            return true;
        }

        if (pascalType == "Longword")
        {
            type = typeof(uint);
            return true;
        }

        type = null;
        return false;
    }

    public bool TryConvertPascalToCSharp(string signature, [NotNullWhen(returnValue: true)] out Signature? output)
    {
        // e.g. function NextButtonClick(CurPageID: Integer): Boolean;

        var openSplit = signature.Split('(');

        var functionAndName = openSplit[0].Split(' '); // function NextButtonClick
        var functionName = functionAndName[1]; // NextButtonClick

        var closeSplit = openSplit[1].Split(')');

        var rawParamString = closeSplit[0].Split(';', StringSplitOptions.RemoveEmptyEntries); // [CurPageID: Integer]
        var rawReturnType = closeSplit[1].TrimStart("; ").TrimStart(": ").TrimEnd(';'); // Boolean

        if (!TryGetType(rawReturnType, out var returnType))
        {
            output = null;
            return false;
        }

        var parameters = new List<Parameter>();
        foreach (var rawParamGroup in rawParamString)
        {
            // e.g. var Cancel, Confirm: Boolean

            // [var Cancel, Confirm: Boolean]
            var rawIndividualParams = rawParamGroup.Split(", ", StringSplitOptions.TrimEntries).ToList(); ;

            // [Confirm, Boolean]
            var lastParamSplit = SplitType(rawIndividualParams[rawIndividualParams.Count - 1]);

            if (!TryGetType(lastParamSplit.Type, out var groupType))
            {
                output = null;
                return false;
            }

            rawIndividualParams[rawIndividualParams.Count - 1] = lastParamSplit.FirstPart;

            var firstParamSplit = ParseFirstToken(rawIndividualParams[0]);

            rawIndividualParams[0] = firstParamSplit.LastPart;

            parameters.AddRange(
              rawIndividualParams.Select(e =>
              new Parameter(
                  name: e,
                  type: groupType,
                  @out: firstParamSplit.IsVar
              )));
        }

        output = new Signature(functionName, returnType, parameters);
        return true;
    }

    private (bool IsVar, string LastPart) ParseFirstToken(string parameter)
    {
        var isVar = false;
        if (parameter.StartsWith("var "))
        {
            parameter = parameter.Remove(0, 4);
            isVar = true;
        }

        if (parameter.StartsWith("const "))
        {
            parameter = parameter.Remove(0, 6);
        }

        return (isVar, parameter);
    }

    private (string FirstPart, string Type) SplitType(string parameter)
    {
        var split = parameter.Split(": ", StringSplitOptions.TrimEntries);

        return (split[0], split[1]);
    }
}
