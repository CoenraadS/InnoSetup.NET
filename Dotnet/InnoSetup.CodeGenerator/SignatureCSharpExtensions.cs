namespace InnoSetup.CodeGenerator
{
    internal static class SignatureCSharpExtensions
    {
        public const string StringResultVariableName = "result";

        public static CSharpCallbackResult ToCSharpCallback(this Signature signature)
        {
            var name = signature.Name;
            var exportSignature = signature;
            var originalSignature = signature;
            var returnLastOutParam = false;
            if (exportSignature.ReturnType == typeof(string))
            {
                exportSignature = new Signature(signature.Name, typeof(void), signature.Parameters.Append(new Parameter(StringResultVariableName, typeof(string), true)).ToList());
                returnLastOutParam = true;
            }

            var delegateType = $"{name}Delegate";
            var delegateSignature = CreateDelegateSignature(exportSignature, delegateType, true);
            var delegateVariable = $"private static {delegateType} {name}Callback;";
            var function = $"[DllExport] public static void Register{name}Callback({delegateType} callback) => {name}Callback = callback;";

            var prettyDelegateSignature = CreateDelegateSignature(originalSignature, delegateType, false);

            var binding = CreateDelegateBinding(returnLastOutParam ? exportSignature : signature, name, returnLastOutParam);

            return new CSharpCallbackResult(delegateSignature, delegateVariable, function, prettyDelegateSignature, binding);
        }

        private static string CreateDelegateBinding(Signature signature, string name, bool returnLastOutParam)
        {
            string binding;
            if (!returnLastOutParam)
            {
                var lambaParams = paramsWithTypes(signature.Parameters);
                var lambaParamsWithoutType = paramsWithoutType(signature.Parameters);
                binding = $"{name} = ({lambaParams}) => {name}Callback({lambaParamsWithoutType});";
            }
            else
            {
                var lambaParams = paramsWithTypes(signature.Parameters.SkipLast(1));
                var lambaParamsWithoutType = paramsWithoutType(signature.Parameters.SkipLast(1));
                binding = $"{name} = ({lambaParams}) => {{ {name}Callback({lambaParamsWithoutType}, out var {StringResultVariableName}); return {StringResultVariableName}; }}";
            }

            return binding;
        }

        private static string paramsWithoutType(IEnumerable<Parameter> parameters)
        {
            return string.Join(", ", parameters.Select(param =>
            {
                return $"{(param.Out ? "out " : "")}{param.Name}";
            }));
        }

        private static string paramsWithTypes(IEnumerable<Parameter> parameters)
        {
            return string.Join(", ", parameters.Select(param =>
            {
                return $"{(param.Out ? "out " : "")}{ShortHandType(param.Type)} {param.Name}";
            }));
        }

        private static string CreateDelegateSignature(Signature signature, string delegateType, bool marshal)
        {
            return $"public delegate {ShortHandType(signature.ReturnType)} {delegateType}({ToParametersString(signature.Parameters, marshal)});";
        }

        private static string ToParametersString(IReadOnlyList<Parameter> parameters, bool marshal)
        {
            return string.Join(", ", parameters.Select(e => ToParameterString(e, marshal)));
        }

        private static string ToParameterString(Parameter parameter, bool marshal)
        {
            var prepend = "";
            if (marshal && TryGetMarshal(parameter, out prepend))
            {
                prepend += ' ';
            }
            return $"{prepend}{(parameter.Out ? "out " : "")}{ShortHandType(parameter.Type)} {parameter.Name}";
        }

        private static bool TryGetMarshal(Parameter parameter, out string marshal)
        {
            if (parameter.Type != typeof(string))
            {
                marshal = "";
                return false;
            }

            var marshalType = parameter.Out ? "BStr" : "LPWStr";
            marshal = $"[MarshalAs(UnmanagedType.{marshalType})]";
            return true;
        }

        private static string ShortHandType(Type type)
        {
            var typeString = type.ToString();
            if (typeString.StartsWith("System."))
            {
                return typeString.Remove(0, 7).ToLower();
            }

            return typeString;
        }
    }
}
