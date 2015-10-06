namespace JsGoogleCompile.Tests
{
    using System;

    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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
    }
}
