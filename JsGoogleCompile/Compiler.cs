using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JsGoogleCompile
{
    public class Compiler
    {
        public string ReadJavaScriptFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            var jsFileContents = sr.ReadToEnd();
            return Uri.EscapeDataString(jsFileContents);
        }

        public string CompileJsFile(string fileName)
        {
            return CompileJsString(ReadJavaScriptFile(fileName));
        }

        public string CompileJsString(string javaScript)
        {
            var request = WebRequest.Create(@"http://closure-compiler.appspot.com/compile");
            request.Method = "POST";

            string postData = "output_format=json" +
                              "&output_info=compiled_code" +
                              "&output_info=warnings" +
                              "&output_info=errors" +
                              "&output_info=statistics" +
                              "&compilation_level=ADVANCED_OPTIMIZATIONS" +
                              "&warning_level=verbose" +
                              "&output_file_name=default.js" +
                              "&js_code=" + javaScript;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }
    }

}
