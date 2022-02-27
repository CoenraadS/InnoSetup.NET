namespace InnoSetup.CodeGenerator
{
    internal record CSharpCallbackResult(
            string DelegateSignature,
            string DelegateVariable,
            string Function,
            string PrettyDelegateSignature,
            string PrettyFunction);
}
