namespace JsGoogleCompile.Tests
{
    using System;
    using System.IO;
    using System.Net;

    using Moq;

    using Xunit;

    public class CompilerOptionsTests
    {
        [Fact]
        public void Constructor_Guards_Null_sourceFileReader()
        {
            Assert.Throws<ArgumentNullException>(() => new CompilerOptions(null, Mock.Of<WebRequest>(), "A"));
        }

        [Fact]
        public void Constructor_Guards_Null_webRequest()
        {
            Assert.Throws<ArgumentNullException>(() => new CompilerOptions(Mock.Of<TextReader>(), null, "A"));
        }

        [Fact]
        public void Constructor_Guards_Null_compilationLevel()
        {
            Assert.Throws<ArgumentNullException>(() => new CompilerOptions(Mock.Of<TextReader>(), Mock.Of<WebRequest>(), null));
        }

        [Fact]
        public void Constructor_Guards_Empty_compilationLevel()
        {
            Assert.Throws<ArgumentNullException>(() => new CompilerOptions(Mock.Of<TextReader>(), Mock.Of<WebRequest>(), string.Empty));
        }
    }
}
