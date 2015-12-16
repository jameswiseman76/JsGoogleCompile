namespace JsGoogleCompile.Tests
{
    using System;
    using System.IO;
    using System.Net;

    using Moq;

    using Xunit;

    public class JavaScriptCompilerTests
    {
        [Fact]
        public void Constructor_Guards_Null_compilerOptions()
        {
            Assert.Throws<ArgumentNullException>(() => new JavaScriptCompiler(null, new CompilationLevelHelper()));
        }

        [Fact]
        public void Constructor_Guards_Null_CompilationLevelHelper()
        {
            Assert.Throws<ArgumentNullException>(
                () => new JavaScriptCompiler(
                    new CompilerOptions(Mock.Of<TextReader>(), Mock.Of<WebRequest>(), "A"), 
                    null));
        }

        [Fact]
        public void Test_That_Compiler_Returns_Response_As_Expected()
        {
            // Arrange
            const string ExpectedCompiledJavaScript = "{\"compiledCode\":\"alert(2);var a\\u003d(void 0).a.value,b\\u003d\\\"\\\";if(\\\"\\\"\\u003d\\u003da||-1\\u003d\\u003da.indexOf(\\\"@\\\"))b\\u003d\\\"please do something\\\";alert(\\\"\\\"\\u003d\\u003db);\",\"warnings\":[{\"type\":\"JSC_POSSIBLE_INEXISTENT_PROPERTY\",\"file\":\"Input_0\",\"lineno\":8,\"charno\":22,\"warning\":\"Property something never defined on frm\",\"line\":\"    var somevar \\u003d frm.something.value;\"},{\"type\":\"JSC_WRONG_ARGUMENT_COUNT\",\"file\":\"Input_0\",\"lineno\":19,\"charno\":6,\"warning\":\"Function dosomething: called with 0 argument(s). Function requires at least 1 argument(s) and no more than 1 argument(s).\",\"line\":\"alert(dosomething());\"}],\"statistics\":{\"originalSize\":372,\"originalGzipSize\":219,\"compressedSize\":103,\"compressedGzipSize\":113,\"compileTime\":0},\"outputFilePath\":\"/code/jsc533f6c7203b8d05105e273ac53810976/default.js\"}";

            // request and response data that will be used by the mock webrequest
            var requestStreamMock = new Mock<Stream>();
            var webResponseMock = new Mock<WebResponse>();
            var responseStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ExpectedCompiledJavaScript));
            webResponseMock.Setup(m => m.GetResponseStream()).Returns(responseStream);

            // Mock of the webrequest tht will make the call to GCC
            var webRequestMock = new Mock<WebRequest>();
            webRequestMock.Setup(m => m.GetRequestStream()).Returns(requestStreamMock.Object);
            webRequestMock.Setup(m => m.GetResponse()).Returns(webResponseMock.Object);

            // Mock of file that will be read by the compiler
            var textReaderMock = new Mock<TextReader>();
            textReaderMock.Setup(m => m.ReadToEnd()).Returns("var x = 0;");

            var compilerOptions = new CompilerOptions(textReaderMock.Object, webRequestMock.Object, "A");
            var compiler = new JavaScriptCompiler(compilerOptions, new CompilationLevelHelper());

            // Act
            var actualCompiledJavaScript = compiler.Compile();

            Assert.Equal(ExpectedCompiledJavaScript, actualCompiledJavaScript);
        }
    }
}
