namespace JsGoogleCompile
{
    using System.IO;
    using System.Net;
    using System.Web.Script.Serialization;
    
    public static class RequestCompile
    {
        public static CompilerResults Run(string fileName, string compilationLevel)
        {
            var inputStream = new StreamReader(fileName);
            var request = WebRequest.Create(@"http://closure-compiler.appspot.com/compile");
            var compilerOptions = new CompilerOptions(inputStream, request, compilationLevel);

            var compiler = new JavaScriptCompiler(compilerOptions);
            var responseFromServer = compiler.Compile();

            var deserializer = new ResultsDeserializer(new JavaScriptSerializer());
            return deserializer.DeserializeCompilerResults(responseFromServer);
        }
    }
}
