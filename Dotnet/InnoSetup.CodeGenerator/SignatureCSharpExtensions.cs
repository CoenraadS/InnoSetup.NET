namespace InnoSetup.CodeGenerator
{
    internal static class SignatureCSharpExtensions
    {
        public static CSharpCallbackResult ToCSharpCallback(this Signature signature)
        {
            var delegateType = $"{signature.Name}Delegate";
            var delegateSignature = $"public delegate {ShortHandType(signature.ReturnType)} {delegateType}({ToParametersString(signature.Parameters)});";
            var delegateVariable = $"private static {delegateType} {signature.Name}Callback;";
            var function = $"[DllExport] public static void Register{signature.Name}Callback({delegateType} callback) => {signature.Name}Callback = callback;";

            return new CSharpCallbackResult(delegateSignature, delegateVariable, function);
        }

        private static string ToParametersString(IReadOnlyList<Parameter> parameters)
        {
            return string.Join(", ", parameters.Select(ToParameterString));
        }

        private static string ToParameterString(Parameter parameter)
        {
            return $"{GetAttribute(parameter)}{(parameter.Out ? " out" : "")} {ShortHandType(parameter.Type)} {parameter.Name}";
        }

        private static string GetAttribute(Parameter parameter)
        {
            if (parameter.Type != typeof(string))
            {
                return string.Empty;
            }

            var marshalType = parameter.Out ? "BStr" : "LPWStr";
            return $"[MarshalAs(UnmanagedType.{marshalType})]";
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
