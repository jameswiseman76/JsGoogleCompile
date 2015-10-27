namespace JsGoogleCompile.Tests
{
    using System;
    using System.Linq;

    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;

    [TestClass]
    public class IsValidWarningSuppressionArgumentTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Null_commandLineArguments()
        {
            var rule = new IsValidWarningSuppressionArgument(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsSatisfiedBy_Guards_Null_commandLineArguments()
        {
            var rule = new IsValidWarningSuppressionArgument(Mock.Of<ICommandLineArguments>());
            rule.IsSatisfiedBy(null);
        }

        [TestMethod]
        public void Single_Valid_Warning_Argument_Is_Recognised()
        {
            // Arrange
            var expectedWarningsSuppressed = new[] { "Error" };
            var expectedSwitch = string.Format("/s{0}", string.Join(";", expectedWarningsSuppressed));
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidWarningSuppressionArgument(Mock.Of<ICommandLineArguments>());
            commandLineArguments.SetupAllProperties();

            // Act
            var isValid = rule.IsSatisfiedBy(new[] { expectedSwitch });

            // Assert
            Assert.IsTrue(isValid);
            // todo: sort these out:
            // commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.Is<List<string>>(l => l.SequenceEqual(expectedWarningsSuppressed)));
            // commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.IsAny<List<string>>());
        }
    }
}
