namespace JsGoogleCompile.Tests
{
    using System;

    using JsGoogleCompile.CLI;
    using Moq;

    using Xunit;

    public class IsValidJavaScriptFileNameTests
    {
        [Fact]
        public void Constructor_Guards_Null_commandLineArguments()
        {
           Assert.Throws<ArgumentNullException>(() => new IsValidJavaScriptFileName(null));
        }

        [Fact]
        public void IsSatisfiedBy_Guards_Null_commandLineArguments()
        {
            var rule = new IsValidJavaScriptFileName(Mock.Of<ICommandLineArguments>());
            Assert.Throws<ArgumentNullException>(() => rule.IsSatisfiedBy(null));
        }

        [Fact]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "JSFILE.JS" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.True(isSatisfiedBy);
        }

        [Fact]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name_As_One_Of_Many_Arguments()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "JSFILE.JS", "/CA" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.True(isSatisfiedBy);
        }

        [Fact]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name_As_One_Of_Many_Arguments_Regardless_Of_Position()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "/CA", "JSFILE.JS" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.True(isSatisfiedBy);
        }

        [Fact]
        public void Rule_Is_Satisfied_By_Valid_Js_File_Name_Regardless_Of_Captial_Casing()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "jsfile.js" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.True(isSatisfiedBy);
        }

        [Fact]
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

        [Fact]
        public void Rule_Is_Not_Satisfied_When_Invalid_Js_File_Name_Is_Passed()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { "JsFile.ps" };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.False(isSatisfiedBy);
        }

        [Fact]
        public void Rule_Is_Not_Satisfied_sWhen_Empty_Js_File_Name_Is_Passed()
        {
            // Arrange
            var commandLineArguments = new Mock<ICommandLineArguments>();
            var rule = new IsValidJavaScriptFileName(commandLineArguments.Object);
            var args = new[] { string.Empty };

            // Act
            var isSatisfiedBy = rule.IsSatisfiedBy(args);

            // Assert
            Assert.False(isSatisfiedBy);
        }
    }
}
