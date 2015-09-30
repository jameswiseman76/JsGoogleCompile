namespace JsGoogleCompile.Tests
{
    using System;
    using System.IO;
    using System.Text;

    using JsGoogleCompile.CLI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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
            compilationLevelHelperMock.Verify(m => m.IsValid(It.Is<string>(p => p == expectedCompilationLevel)), Times.Exactly(3));
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
            expected.AppendFormat("Usage:\tJsGoogleCompile.exe FileName [/c[attribute]] [/s[comma seprated list of warnings]]{0}", Environment.NewLine);
            expected.AppendFormat("\tFileName:\tThe full filename and path of the file on disk to compress{0}", Environment.NewLine);
            expected.AppendFormat("\t/c: \t\tSpecify compilation level. (If omitted 'advanced' is assumed){0}", Environment.NewLine);
            expected.AppendFormat("\t\t\t/cw: Whitespace only{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\t/cs: Simple optimisations{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\t/ca: Advanced optimisations{0}", Environment.NewLine);
            expected.AppendFormat("\t/s: \t\tSpecify warnings to be suppressed{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\t/s[warning1, warning2]{0}", Environment.NewLine);
            expected.AppendFormat("\tWarnings:\tJSC_BAD_DELETE_OPERAND{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_BAD_TYPE_FOR_BIT_OPERATION{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_CONSTRUCTOR_NOT_CALLABLE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_FUNCTION_MASKS_VARIABLE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_INVALID_FUNCTION_DECL{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_NAMESPACE_REDEFINED{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_NOT_A_CONSTRUCTOR{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_NOT_FUNCTION_TYPE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_REDECLARED_VARIABLE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_REFERENCE_BEFORE_DECLARE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_SET_WITHOUT_READ{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_SUSPICIOUS_SEMICOLON{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_TYPE_MISMATCH{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_UNDEFINED_NAME{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_UNDEFINED_VARIABLE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_UNSAFE_NAMESPACE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_UNSAFE_THIS{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_USED_GLOBAL_THIS{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_USELESS_CODE{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_VAR_ARGS_MUST_BE_LAST{0}", Environment.NewLine);
            expected.AppendFormat("\t\t\tJSC_WRONG_ARGUMENT_COUNT{0}", Environment.NewLine);
            expected.AppendFormat("Example usages:{0}", Environment.NewLine);
            expected.AppendFormat("\tJsGoogleCompile.exe sample.js{0}", Environment.NewLine);
            expected.AppendFormat("\tJsGoogleCompile.exe sample.js /ca{0}", Environment.NewLine);
            expected.AppendFormat("\tJsGoogleCompile.exe sample.js /sJSC_BAD_TYPE_FOR_BIT_OPERATION{0}", Environment.NewLine);
            expected.AppendFormat("\tJsGoogleCompile.exe sample.js /cw /sJSC_BAD_TYPE_FOR_BIT_OPERATION,JSC_UNSAFE_THIS{0}", Environment.NewLine); 
            
            return expected.ToString();
        }
    }
}
