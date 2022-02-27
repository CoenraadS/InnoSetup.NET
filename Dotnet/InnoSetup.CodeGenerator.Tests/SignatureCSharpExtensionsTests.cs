using NUnit.Framework;

namespace InnoSetup.CodeGenerator.Tests
{
    [TestFixture]
    public class SignatureCSharpExtensionsTests
    {
        readonly Parser parser = new Parser();

        [Test]
        public void CanCreateLogCallback()
        {
            // Arrange
            var signature = FromPascal("procedure Log(const S: String);");

            // Act
            var result = signature.ToCSharpCallback();

            // Assert
            Assert.AreEqual("public delegate void LogDelegate([MarshalAs(UnmanagedType.LPWStr)] string S);", result.DelegateSignature);
            Assert.AreEqual("private static LogDelegate LogCallback;", result.DelegateVariable);
            Assert.AreEqual("[DllExport] public static void RegisterLogCallback(LogDelegate callback) => LogCallback = callback;", result.Function);
            Assert.AreEqual("public delegate void LogDelegate(string S);", result.PrettyDelegateSignature);
            Assert.AreEqual("Log = (string S) => LogCallback(S);", result.PrettyFunction);
        }


        [Test]
        public void CanExpandConstantCallback()
        {
            // Arrange
            var signature = FromPascal("function ExpandConstant(const S: String): String;");

            // Act
            var result = signature.ToCSharpCallback();


            // Assert
            Assert.AreEqual("public delegate void ExpandConstantDelegate([MarshalAs(UnmanagedType.LPWStr)] string S, [MarshalAs(UnmanagedType.BStr)] out string result);", result.DelegateSignature);
            Assert.AreEqual("private static ExpandConstantDelegate ExpandConstantCallback;", result.DelegateVariable);
            Assert.AreEqual("[DllExport] public static void RegisterExpandConstantCallback(ExpandConstantDelegate callback) => ExpandConstantCallback = callback;", result.Function);
            Assert.AreEqual("public delegate string ExpandConstantDelegate(string S);", result.PrettyDelegateSignature);
            Assert.AreEqual("ExpandConstant = (string S) => { ExpandConstantCallback(S, out var result); return result; }", result.PrettyFunction);

        }

        private Signature FromPascal(string pascal)
        {
            if (parser.TryConvertPascalToCSharp(pascal, out var signature))
            {
                return signature;
            }

            throw new System.Exception("Could not parse pascal signature");
        }
    }
}
