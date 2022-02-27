namespace InnoSetup.CodeGenerator
{
    internal class CSharpCallbackResult
    {
        public CSharpCallbackResult(string delegateSignature, string delegateVariable, string function)
        {
            DelegateSignature = delegateSignature;
            DelegateVariable = delegateVariable;
            Function = function;
        }

        public string DelegateSignature { get; }

        public string Function { get; }

        public string DelegateVariable { get; }
    }
}
