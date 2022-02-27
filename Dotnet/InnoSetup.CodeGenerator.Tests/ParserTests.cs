using System;
using NUnit.Framework;

namespace InnoSetup.CodeGenerator.Tests
{
    [TestFixture]
    public class ParserTests
    {
        private Parser sut;

        [SetUp]
        public void SetUp()
        {
            sut = new Parser();
        }

        [Test]
        public void CanParseSignature1()
        {
            // Arrange
            var signature = "procedure CurInstallProgressChanged(CurProgress, MaxProgress: Integer);";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("CurInstallProgressChanged", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(2, output.Parameters.Count);

            AssertParameter(output.Parameters[0], "CurProgress", typeof(int), false);
            AssertParameter(output.Parameters[1], "MaxProgress", typeof(int), false);
        }

        [Test]
        public void CanParseSignature2()
        {
            // Arrange
            var signature = "procedure CancelButtonClick(CurPageID: Integer; var Cancel, Confirm: Boolean);";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("CancelButtonClick", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(3, output.Parameters.Count);

            AssertParameter(output.Parameters[0], "CurPageID", typeof(int), false);
            AssertParameter(output.Parameters[1], "Cancel", typeof(bool), true);
            AssertParameter(output.Parameters[2], "Confirm", typeof(bool), true);
        }

        [Test]
        public void CanParseSignature3()
        {
            // Arrange
            var signature = "function InitializeSetup(): Boolean;";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("InitializeSetup", output.Name);
            Assert.AreEqual(typeof(bool), output.ReturnType);
            Assert.AreEqual(0, output.Parameters.Count);
        }

        [Test]
        public void CanParseSignature4()
        {
            // Arrange
            var signature = "procedure InitializeWizard();";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("InitializeWizard", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(0, output.Parameters.Count);
        }

        [Test]
        public void CanParseSignature5()
        {
            // Arrange
            var signature = "procedure Log(const S: String);";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Log", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(1, output.Parameters.Count);

            AssertParameter(output.Parameters[0], "S", typeof(string), false);
        }

        [Test]
        public void CanParseSignature6()
        {
            // Arrange
            var signature = "function ExpandConstant(const S: String): String;";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("ExpandConstant", output.Name);
            Assert.AreEqual(typeof(string), output.ReturnType);
            Assert.AreEqual(1, output.Parameters.Count);

            AssertParameter(output.Parameters[0], "S", typeof(string), false);
        }

        private void AssertParameter(Parameter parameter, string name, Type type, bool isOut)
        {
            Assert.AreEqual(isOut, parameter.Out);
            Assert.AreEqual(name, parameter.Name);
            Assert.AreEqual(type, parameter.Type);
        }
    }
}
