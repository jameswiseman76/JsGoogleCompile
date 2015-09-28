using System.Text;
using Moq;

namespace JsGoogleCompile.Tests
{
    using System;
    using System.IO;

    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandLineArgumentsTest
    {
        [TestMethod]
        public void Command_Line_Arguments_Are_Invalid_When_Empty()
        {
            // Act
            var cla = new CommandLineArguments(new string[0], new CompilationLevelHelper());

            // Assert
            Assert.IsFalse(cla.AreValid);
        }

        [TestMethod]
        public void Usage_Instructions_Are_Emitted_As_Expected()
        {
            using (var sw = new StringWriter())
            {
                // Arrange
                Console.SetOut(sw);
                var expected = GetExpectedUseageInstructions();

                // Act
                var cla = new CommandLineArguments(new string[0], new CompilationLevelHelper());

                // Assert
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void Request_For_Usage_Instructions_Emits_Usage_Instructions()
        {
            using (var sw = new StringWriter())
            {
                // Arrange
                Console.SetOut(sw);
                var expected = GetExpectedUseageInstructions();

                // Act
                var cla = new CommandLineArguments(new[] { "/?" }, new CompilationLevelHelper());

                // Assert
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void File_Name_Passed_As_Single_Argument_Is_Recognised()
        {
            // Arrange
            var expectedFileName = "SomeFile.js";

            // Act
            var cu = new CommandLineArguments(new[] { expectedFileName }, new CompilationLevelHelper());

            // Assert
            Assert.AreEqual(expectedFileName, cu.FileName);
            Assert.IsTrue(cu.AreValid);
        }

        [TestMethod]
        public void File_Name_And_CompilationLevel_Passed_As_Arguments_Are_Recognised()
        {
            // Arrange
            var expectedFileName = "SomeFile.js";
            var expectedCompilationLevel = "W";
            var compilationArgument = string.Format("/C{0}", expectedCompilationLevel);
            var args = new[] { expectedFileName, compilationArgument };

            // Act
            var cu = new CommandLineArguments(args, new CompilationLevelHelper());

            // Assert
            Assert.AreEqual(args[0], cu.FileName);
            Assert.AreEqual(expectedCompilationLevel, cu.CompilationLevel);
            Assert.IsTrue(cu.AreValid);
        }

        [TestMethod]
        public void Invalid_CompilationLevel_Passed_As_Arguments_Is_Caught()
        {
            // Arrange
            var expectedFileName = "SomeFile.js";
            var expectedCompilationLevel = Guid.NewGuid().ToString();
            var compilationArgument = string.Format("/C{0}", expectedCompilationLevel);
            var args = new[] { expectedFileName, compilationArgument };

            var compilationLevelHelperMock = new Mock<ICompilationLevelHelper>();
            compilationLevelHelperMock.Setup(m => m.IsValid(It.IsAny<string>())).Returns(false);

            // Act
            var cla = new CommandLineArguments(args, compilationLevelHelperMock.Object);

            // Assert
            Assert.IsFalse(cla.AreValid);
            compilationLevelHelperMock.Verify(m => m.IsValid(It.Is<string>(p => p == expectedCompilationLevel)), Times.Once);
        }

        [TestMethod]
        public void Expected_Js_File_Extension()
        {
            var ex = Path.GetExtension("test.hs");
            var ex3 = Path.GetExtension("test.");
            var ex2 = Path.GetExtension("test");
        }

        private static string GetExpectedUseageInstructions()
        {
            var expected = new StringBuilder();
            expected.AppendFormat("JsGoogleCompile: Request a compile from the Google Closure Compiler service{0}", Environment.NewLine);
            expected.AppendFormat("(http://closure-compiler.appspot.com/compile){0}", Environment.NewLine);
            expected.AppendFormat("{0}", Environment.NewLine);
            expected.AppendFormat("Usage:\t\tJsGoogleCompile.exe FileName [/c[attribute]]{0}", Environment.NewLine);
            expected.AppendFormat("FileName:\tThe full filename and path of the file on disk to compress{0}", Environment.NewLine);
            expected.AppendFormat("/c: \t\tSpecify compilation level. (If omitted 'advanced' is assumed){0}", Environment.NewLine);
            expected.AppendFormat("attribute: \t w: Whitespace only{0}", Environment.NewLine);
            expected.AppendFormat("\t\t s: Simple optimisations{0}", Environment.NewLine);
            expected.AppendFormat("\t\t a: Advianced optimisations{0}", Environment.NewLine);

            return expected.ToString();
        }
    }
}
