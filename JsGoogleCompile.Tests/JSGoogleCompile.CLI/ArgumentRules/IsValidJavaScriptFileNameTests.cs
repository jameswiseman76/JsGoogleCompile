using System;

namespace JsGoogleCompile.Tests
{
    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class IsValidJavaScriptFileNameTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Null_commandLineArguments()
        {
            var rule = new IsValidJavaScriptFileName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsSatisfiedBy_Guards_Null_commandLineArguments()
        {
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            rule.IsSatisfiedBy(null);
        }

        [TestMethod]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "JSFILE.JS" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsTrue(isSatisfiedBy);
        }

        [TestMethod]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name_As_One_Of_Many_Arguments()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "JSFILE.JS", "/CA" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsTrue(isSatisfiedBy);
        }

        [TestMethod]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name_As_One_Of_Many_Arguments_Regardless_Of_Position()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "/CA", "JSFILE.JS" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsTrue(isSatisfiedBy);
        }

        [TestMethod]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name_Regardless_Of_Captial_Casing()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "jsfile.js" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsTrue(isSatisfiedBy);
        }

        [TestMethod]
        public void Command_Line_Arguments_FileName_Is_Set()
        {
            // Arrange
            var expectedFileName = "JSFILE.JS";
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { expectedFileName };

            // Act
            rule.IsSatisfiedBy(args);

            // Assert
            commandLineArguments.VerifySet(m => m.FileName = expectedFileName);
        }

        [TestMethod]
        public void Rule_Is_Not_Satisfied_When_InValid_Js_File_Name_Is_Passed()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "JsFile.ps" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.IsFalse(isSatisfiedBy);
        }
    }
}
