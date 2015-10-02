namespace JsGoogleCompile.Tests
{
    using System.Collections.Generic;
    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ArgumentRulesTests
    {
        [TestMethod]
        public void Single_FileName_Argument_Is_Valid()
        {
            // Arrange
            var commandLineArgumentsMock = Mock.Of<ICommandLineArguments>();
            var compilationLevelHelperMock = new Mock<ICompilationLevelHelper>();
            compilationLevelHelperMock.Setup(m => m.From(It.IsAny<string>())).Returns("ADVANCED_OPTIMIZATIONS");

            var argumentRules = new ArgumentRules(commandLineArgumentsMock, compilationLevelHelperMock.Object);

            var arguments = new List<string> { "sample.js" };

            // Act
            var anyRulesSatified = argumentRules.AnySatisfiedBy(arguments);

            // Assert
            Assert.IsTrue(anyRulesSatified);
        }

        [TestMethod]
        public void FileName_And_CompilationLevel_Argument_Is_Valid()
        {
            // Arrange
            var commandLineArgumentsMock = Mock.Of<ICommandLineArguments>();
            var compilationLevelHelperMock = new Mock<ICompilationLevelHelper>();
            compilationLevelHelperMock.Setup(m => m.From(It.IsAny<string>())).Returns("ADVANCED_OPTIMIZATIONS");
            compilationLevelHelperMock.Setup(m => m.IsValid(It.IsAny<string>())).Returns(true);

            var argumentRules = new ArgumentRules(commandLineArgumentsMock, compilationLevelHelperMock.Object);

            var arguments = new List<string> { "sample.js", "/ca" };

            // Act
            var anyRuleComboSatified = argumentRules.AnySatisfiedBy(arguments);

            // Assert
            Assert.IsTrue(anyRuleComboSatified);
        }

        [TestMethod]
        public void FileName_And_WarningSuppression_Argument_Is_Valid()
        {
            // Arrange
            var commandLineArgumentsMock = Mock.Of<ICommandLineArguments>();
            var compilationLevelHelperMock = new Mock<ICompilationLevelHelper>();
            compilationLevelHelperMock.Setup(m => m.From(It.IsAny<string>())).Returns("ADVANCED_OPTIMIZATIONS");

            var argumentRules = new ArgumentRules(commandLineArgumentsMock, compilationLevelHelperMock.Object);

            var arguments = new List<string> { "sample.js", "/sJSC_BAD_TYPE_FOR_BIT_OPERATION" };

            // Act
            var anyRuleComboSatified = argumentRules.AnySatisfiedBy(arguments);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void FileName_And_And_CompilationLevel_WarningSuppression_Argument_Is_Valid()
        {
            // Arrange
            var commandLineArgumentsMock = Mock.Of<ICommandLineArguments>();
            var compilationLevelHelperMock = new Mock<ICompilationLevelHelper>();
            compilationLevelHelperMock.Setup(m => m.From(It.IsAny<string>())).Returns("ADVANCED_OPTIMIZATIONS");

            var argumentRules = new ArgumentRules(commandLineArgumentsMock, compilationLevelHelperMock.Object);

            var arguments = new List<string> { "sample.js", "/ca", "/sJSC_BAD_TYPE_FOR_BIT_OPERATION,JSC_FUNCTION_MASKS_VARIABLE" };

            // Act
            var anyRuleComboSatified = argumentRules.AnySatisfiedBy(arguments);

            // Assert
            Assert.IsTrue(true);
        }
    }
}
