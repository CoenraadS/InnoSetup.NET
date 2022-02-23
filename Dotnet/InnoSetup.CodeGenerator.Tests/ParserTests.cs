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
            var description = "";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, description, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("CurInstallProgressChanged", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(2, output.Parameters.Count);

            Assert.False(output.Parameters[0].Out);
            Assert.AreEqual(typeof(int), output.Parameters[0].Type);
            Assert.AreEqual("CurProgress", output.Parameters[0].Name);

            Assert.False(output.Parameters[1].Out);
            Assert.AreEqual(typeof(int), output.Parameters[1].Type);
            Assert.AreEqual("MaxProgress", output.Parameters[1].Name);
        }

        [Test]
        public void CanParseSignature2()
        {
            // Arrange
            var signature = "procedure CancelButtonClick(CurPageID: Integer; var Cancel, Confirm: Boolean);";
            var description = "";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, description, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("CancelButtonClick", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(3, output.Parameters.Count);

            Assert.False(output.Parameters[0].Out);
            Assert.AreEqual(typeof(int), output.Parameters[0].Type);
            Assert.AreEqual("CurPageID", output.Parameters[0].Name);

            Assert.True(output.Parameters[1].Out);
            Assert.AreEqual(typeof(bool), output.Parameters[1].Type);
            Assert.AreEqual("Cancel", output.Parameters[1].Name);

            Assert.True(output.Parameters[2].Out);
            Assert.AreEqual(typeof(bool), output.Parameters[2].Type);
            Assert.AreEqual("Confirm", output.Parameters[2].Name);
        }

        [Test]
        public void CanParseSignature3()
        {
            // Arrange
            var signature = "function InitializeSetup(): Boolean;";
            var description = "";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, description, out var output);

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
            var description = "";

            // Act
            var result = sut.TryConvertPascalToCSharp(signature, description, out var output);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("InitializeWizard", output.Name);
            Assert.AreEqual(typeof(void), output.ReturnType);
            Assert.AreEqual(0, output.Parameters.Count);
        }
    }
}
