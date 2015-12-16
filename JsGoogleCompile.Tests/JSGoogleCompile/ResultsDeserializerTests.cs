namespace JsGoogleCompile.Tests
{
    using System;
    using System.Web.Script.Serialization;

    using Xunit;

    public class ResultsDeserializerTests
    {
        [Fact]
        public void Constructor_Guards_serializer_Argument()
        {
            Assert.Throws<ArgumentNullException>(() => new ResultsDeserializer(null));
        }

        [Fact]
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
            Assert.Equal(0, deserializedResults.Statistics.CompileTime);
            Assert.Equal(20, deserializedResults.Statistics.CompressedGzipSize);
            Assert.Equal(0, deserializedResults.Statistics.CompressedSize);
            Assert.Equal(49, deserializedResults.Statistics.OriginalGzipSize);
            Assert.Equal(42, deserializedResults.Statistics.OriginalSize);
            Assert.Equal(ExpectedFileName, deserializedResults.OutputFilePath);
        }

        [Fact]
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
            Assert.Equal(1, deserializedResults.Errors.Count);
            Assert.Equal(ExpectedError, deserializedResults.Errors[0].Error);
            Assert.Equal(string.Empty, deserializedResults.Errors[0].Line);
            Assert.Equal(ExpectedFileName, deserializedResults.OutputFilePath);
        }
    }
}
