namespace JsGoogleCompile.Tests
{
    using System;
    using System.Web.Script.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResultsDeserializerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Guards_serializer_Argument()
        {
            new ResultsDeserializer(null);
        }

        [TestMethod]
        public void Valid_Compiler_Response_Is_Deserialized_Correctly()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();

            var deserializer = new ResultsDeserializer(serializer);

            const string MockCompilerResponse = "{\"compiledCode\":\"\",\"statistics\":{\"originalSize\":42,\"originalGzipSize\":49,\"compressedSize\":0,\"compressedGzipSize\":20,\"compileTime\":0}}";

            const string ExpectedFileName = "jsfile.js";

            // Act
            var deserializedResults = deserializer.DeserializeCompilerResults(MockCompilerResponse, ExpectedFileName);

            // Assert
            Assert.AreEqual(0, deserializedResults.Statistics.CompileTime);
            Assert.AreEqual(20, deserializedResults.Statistics.CompressedGzipSize);
            Assert.AreEqual(0, deserializedResults.Statistics.CompressedSize);
            Assert.AreEqual(49, deserializedResults.Statistics.OriginalGzipSize);
            Assert.AreEqual(42, deserializedResults.Statistics.OriginalSize);
            Assert.AreEqual(ExpectedFileName, deserializedResults.OutputFilePath);
        }

        [TestMethod]
        public void Invalid_Compiler_Response_Is_Reported_In_Results_Error_Collection()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var deserializer = new ResultsDeserializer(serializer);
            const string MockCompilerResponse = "This is invalid";
            const string ExpectedError = "Error reading compiler results : Invalid JSON primitive: This.";

            const string ExpectedFileName = "jsfile2.js";

            // Act
            var deserializedResults = deserializer.DeserializeCompilerResults(MockCompilerResponse, ExpectedFileName);

            // Assert
            Assert.AreEqual(1, deserializedResults.Errors.Count);
            Assert.AreEqual(ExpectedError, deserializedResults.Errors[0].Error);
            Assert.AreEqual(string.Empty, deserializedResults.Errors[0].Line);
            Assert.AreEqual(ExpectedFileName, deserializedResults.OutputFilePath);
        }
    }
}
