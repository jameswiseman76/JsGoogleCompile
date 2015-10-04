namespace JsGoogleCompile.Tests.JSGoogleCompile.CLI.ArgumentRules
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using JsGoogleCompile.CLI;
    using Moq;

    [TestClass]
    public class IsValidJavaScriptFileNameTests
    {
        [TestMethod]
        public void Vailid_Js_File_Is_Recognised()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new string[] { "JsFile.js" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsTrue(isSatisfiedBy);
        }

        [TestMethod]
        public void Command_Line_Arguments_FileName_Is_Set()
        {
            // Arrange
            var expectedFileName = "JsFile.js";
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new string[] { expectedFileName };

            // Act
            rule.IsSatisfiedBy(args);

            // Assert
            commandLineArguments.VerifySet(m => m.FileName = expectedFileName);
        }
    }
}
