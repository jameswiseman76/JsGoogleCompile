namespace JsGoogleCompile.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System.Web.Script.Serialization;

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

            var mockCompilerResponse =
                "{\"compiledCode\":\"\",\"statistics\":{\"originalSize\":42,\"originalGzipSize\":49,\"compressedSize\":0,\"compressedGzipSize\":20,\"compileTime\":0}}";

            // Act
            var deserializedResults = deserializer.DeserializeCompilerResults(mockCompilerResponse);

            // Assert
            Assert.AreEqual(0, deserializedResults.Statistics.CompileTime);
            Assert.AreEqual(20, deserializedResults.Statistics.CompressedGzipSize);
            Assert.AreEqual(0, deserializedResults.Statistics.CompressedSize);
            Assert.AreEqual(49, deserializedResults.Statistics.OriginalGzipSize);
            Assert.AreEqual(42, deserializedResults.Statistics.OriginalSize);
        }

        [TestMethod]
        public void Invalid_Compiler_Response_Is_Reported_In_Results_Error_Collection()
        {
            // Arrange
            var serializer = new JavaScriptSerializer();
            var deserializer = new ResultsDeserializer(serializer);
            var mockCompilerResponse = "This is invalid";
            var expectedError = "Error reading compiler results : Invalid JSON primitive: This.";
            
            // Act
            var deserializedResults = deserializer.DeserializeCompilerResults(mockCompilerResponse);

            // Assert
            Assert.AreEqual(1, deserializedResults.Errors.Count);
            Assert.AreEqual(expectedError, deserializedResults.Errors[0].Error);
            Assert.AreEqual(string.Empty, deserializedResults.Errors[0].Line);
        }
    }
}
