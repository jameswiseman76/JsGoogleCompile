namespace JsGoogleCompile.Tests
{
    using System;
    using System.IO;
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class CompilerOptionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Null_sourceFileReader()
        {
            var compiler = new CompilerOptions(null, Mock.Of<WebRequest>(), "A");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Null_webRequest()
        {
            var compiler = new CompilerOptions(Mock.Of<TextReader>(), null, "A");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Null_compilationLevel()
        {
            var compiler = new CompilerOptions(Mock.Of<TextReader>(), Mock.Of<WebRequest>(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Empty_compilationLevel()
        {
            var compiler = new CompilerOptions(Mock.Of<TextReader>(), Mock.Of<WebRequest>(), string.Empty);
        }
    }
}
