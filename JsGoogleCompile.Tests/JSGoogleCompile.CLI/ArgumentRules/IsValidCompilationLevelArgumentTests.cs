namespace JsGoogleCompile.Tests
{
    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class IsValidCompilationLevelArgumentTests
    {
        [TestMethod]
        public void Valid_Compilation_Switch_Is_Recognised_As_Being_Valid()
        {
            // Arrange
            var validCompilationSwitchAttribute = "A";
            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.CompilationLevel = It.IsAny<string>()).Verifiable();

            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == validCompilationSwitchAttribute))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            var args = new[] { string.Format("/C{0}", validCompilationSwitchAttribute) };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsTrue(isValid);
            commandLineArguments.VerifySet(m => m.CompilationLevel = validCompilationSwitchAttribute);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == validCompilationSwitchAttribute)), Times.Once);
        }

        [TestMethod]
        public void Invalid_Compilation_Switch_Directive_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            var invalidCompilationSwitchDirective = "/R";
            var invalidCommandLineArgument = string.Format("{0}A", invalidCompilationSwitchDirective);

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsFalse(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == "A")), Times.Never);
        }

        [TestMethod]
        public void Invalid_Compilation_Switch_Attribute_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            var invalidCompilationSwitchAttribute = "Z";
            var invalidCommandLineArgument = string.Format("/C{0}", invalidCompilationSwitchAttribute);

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsFalse(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == invalidCompilationSwitchAttribute)), Times.Once);
        }

        [TestMethod]
        public void Invalid_Compilation_Switch_Multi_Char_Attribute_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            var invalidCompilationSwitchMultiCharAttribute = "AB";
            var invalidCommandLineArgument = string.Format("/C{0}", invalidCompilationSwitchMultiCharAttribute);

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsFalse(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == invalidCompilationSwitchMultiCharAttribute)), Times.Once);
        }

        [TestMethod]
        public void Invalid_Compilation_Switch_No_Char_Attribute_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            var invalidCommandLineArgument = string.Format("/C");

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsFalse(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => string.IsNullOrEmpty(r))), Times.Once);
        }
    }
}
