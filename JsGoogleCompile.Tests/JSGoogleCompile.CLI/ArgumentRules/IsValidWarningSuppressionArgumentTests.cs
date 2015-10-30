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
            var expectedWarningsSuppressed = new[] { "ERROR" };
            var comamndLineSwitch= string.Format("/s{0}", string.Join(";", expectedWarningsSuppressed));

            var commandLineArguments = new Mock<ICommandLineArguments>();
            commandLineArguments.SetupSet(m => m.SuppressedWarnings = It.IsAny<IList<string>>()).Verifiable();

            var rule = new IsValidWarningSuppressionArgument(commandLineArguments.Object);

            // Act
            var isValid = rule.IsSatisfiedBy(new[] { comamndLineSwitch });

            // Assert
            Assert.IsTrue(isValid);

            commandLineArguments.VerifySet(m => m.SuppressedWarnings = It.Is<IList<string>>(l => l.SequenceEqual(expectedWarningsSuppressed)));
        }
    }
}
