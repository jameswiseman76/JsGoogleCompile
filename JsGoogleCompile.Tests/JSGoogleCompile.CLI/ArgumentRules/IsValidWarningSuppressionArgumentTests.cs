namespace JsGoogleCompile.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JsGoogleCompile.CLI;
    using Moq;

    using Xunit;

    public class IsValidWarningSuppressionArgumentTests
    {
        [Fact]
        public void Constructor_Guards_Null_commandLineArguments()
        {
            Assert.Throws<ArgumentNullException>(() => new IsValidWarningSuppressionArgument(null));
        }

        [Fact]
        public void IsSatisfiedBy_Guards_Null_commandLineArguments()
        {
            var rule = new IsValidWarningSuppressionArgument(Mock.Of<ICommandLineArguments>());
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(null));
        }

        [Fact]
        public void Single_Valid_Warning_Argument_Is_Recognised()
        {
            // Arrange
            var expectedWarningsSuppressed = new[] { "ERROR" };
            var comamndLineSwitch = string.Format("/S{0}", string.Join(";", expectedWarningsSuppressed));

            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.SuppressedWarnings = It.IsAny<IList<string>>()).Verifiable();

            var rule = new IsValidWarningSuppressionArgument(commandLineArguments.Object);

            // Act
            var isValid = rule.IsSatisfiedBy(new[] { comamndLineSwitch });

            // Assert
            Assert.True(isValid);

            commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.Is<IList<string>>(l => l.SequenceEqual(expectedWarningsSuppressed)));
        }

        [Fact]
        public void Single_Valid_Warning_Argument_Is_Recognised_Case_Insensitive()
        {
            // Arrange
            var expectedWarningsSuppressed = new[] { "ERROR" };
            var comamndLineSwitch = string.Format("/s{0}", string.Join(";", expectedWarningsSuppressed));

            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.SuppressedWarnings = It.IsAny<IList<string>>()).Verifiable();

            var rule = new IsValidWarningSuppressionArgument(commandLineArguments.Object);

            // Act
            var isValid = rule.IsSatisfiedBy(new[] { comamndLineSwitch });

            // Assert
            Assert.True(isValid);

            commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.Is<IList<string>>(l => l.SequenceEqual(expectedWarningsSuppressed)));
        }

        [Fact]
        public void Valid_Switch_With_Empty_Argument_Is_Invalid()
        {
            // Arrange
            const string ComamndLineSwitch = "/S";

            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.SuppressedWarnings = It.IsAny<IList<string>>()).Verifiable();

            var rule = new IsValidWarningSuppressionArgument(commandLineArguments.Object);

            // Act
            var isValid = rule.IsSatisfiedBy(new[] { ComamndLineSwitch });

            // Assert
            Assert.False(isValid);

            commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.IsAny<IList<string>>(), Times.Never);
        }

        [Fact]
        public void Invalid_Switch_Is_Invalid()
        {
            // Arrange
            var invalidComamndLineSwitch = string.Format("/XXX");

            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.SuppressedWarnings = It.IsAny<IList<string>>()).Verifiable();

            var rule = new IsValidWarningSuppressionArgument(commandLineArguments.Object);

            // Act
            var isValid = rule.IsSatisfiedBy(new[] { invalidComamndLineSwitch });

            // Assert
            Assert.False(isValid);

            commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.IsAny<IList<string>>(), Times.Never);
        }
    }
}
