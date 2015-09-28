using System.Text;

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
            var cu = new CommandLineArguments(new string[0]);

            // Assert
            Assert.IsFalse(cu.AreValid);
        }

        [TestMethod]
        public void Usage_Instructions_Are_Emitted_As_Expected()
        {
            using (var sw = new StringWriter())
            {
                // Arrange
                Console.SetOut(sw);

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

                // Act
                var cu = new CommandLineArguments(new string[0]);

                // Assert
                Assert.AreEqual(expected.ToString(), sw.ToString());
            }
        }
    }
}
