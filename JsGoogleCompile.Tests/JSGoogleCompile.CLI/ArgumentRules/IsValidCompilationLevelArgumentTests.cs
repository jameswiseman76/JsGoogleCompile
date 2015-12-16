namespace JsGoogleCompile.Tests
{
    using System;

    using JsGoogleCompile.CLI;
    using Moq;

    using Xunit;

    public class IsValidCompilationLevelArgumentTests
    {
        [Fact]
        public void Constructor_Guards_Null_commandLineArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new IsValidCompilationLevelArgument(null, Mock.Of<ICompilationLevelHelper>()));
        }

        [Fact]
        public void Constructor_Guards_Null_compilationLevelHelper()
        {
            Assert.Throws<ArgumentNullException>(() => new IsValidCompilationLevelArgument(Mock.Of<ICommandLineArguments>(), null));
        }

        [Fact]
        public void IsSatisfiedBy_Guards_Null_commandLineArguments()
        {
            var rule = new IsValidCompilationLevelArgument(Mock.Of<ICommandLineArguments>(), Mock.Of<ICompilationLevelHelper>());
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(null));
        }

        [Fact]
        public void Valid_Compilation_Switch_Is_Recognised_As_Being_Valid()
        {
            // Arrange
            const string ValidCompilationSwitchAttribute = "A";
            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.CompilationLevel = It.IsAny<string>()).Verifiable();

            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == ValidCompilationSwitchAttribute))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            var args = new[] { string.Format("/C{0}", ValidCompilationSwitchAttribute) };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.True(isValid);
            commandLineArguments.VerifySet(m => m.CompilationLevel = ValidCompilationSwitchAttribute);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == ValidCompilationSwitchAttribute)), Times.Once);
        }

        [Fact]
        public void Invalid_Compilation_Switch_Directive_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            const string InvalidCompilationSwitchDirective = "/R";
            var invalidCommandLineArgument = string.Format("{0}A", InvalidCompilationSwitchDirective);

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.False(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == "A")), Times.Never);
        }

        [Fact]
        public void Invalid_Compilation_Switch_Attribute_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            const string InvalidCompilationSwitchAttribute = "Z";
            var invalidCommandLineArgument = string.Format("/C{0}", InvalidCompilationSwitchAttribute);

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.False(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == InvalidCompilationSwitchAttribute)), Times.Once);
        }

        [Fact]
        public void Invalid_Compilation_Switch_Multi_Char_Attribute_Is_Recognised_As_Being_Invalid()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var compilationLevelHelper = new Mock<ICompilationLevelHelper>();
            compilationLevelHelper.Setup(m => m.IsValid(It.Is<string>(r => r == "A"))).Returns(true);

            var rule = new IsValidCompilationLevelArgument(
                commandLineArguments.Object,
                compilationLevelHelper.Object);

            const string InvalidCompilationSwitchMultiCharAttribute = "AB";
            var invalidCommandLineArgument = string.Format("/C{0}", InvalidCompilationSwitchMultiCharAttribute);

            var args = new[] { invalidCommandLineArgument };

            // Act
            var isValid = rule.IsSatisfiedBy(args);

            // Assert
            Assert.False(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => r == InvalidCompilationSwitchMultiCharAttribute)), Times.Once);
        }

        [Fact]
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
            Assert.False(isValid);
            compilationLevelHelper.Verify(m => m.IsValid(It.Is<string>(r => string.IsNullOrEmpty(r))), Times.Once);
        }
    }
}
