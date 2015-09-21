namespace JsGoogleCompile.Tests
{
    using System;
    using System.Net;
    using System.IO;
    using Moq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    class ResultsSerializerTests
    {
        [TestMethod]
        // [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_Null_sourceFileReader()
        {
            // var compiler = new JavaScriptCompiler(null, Mock.Of<WebRequest>());
        }
    }
}
