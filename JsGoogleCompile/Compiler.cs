using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

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

        public string CompileJsFile(string fileName, string compileLevel)
        {
            return CompileJsString(ReadJavaScriptFile(fileName), compileLevel);
        }

        public string CompileJsString(string javaScript, string compileLevel)
        {
            switch (compileLevel.ToUpper())
            {
                case "S":
                    compileLevel = "SIMPLE_OPTIMIZATIONS";
                    break;
                case "W":
                    compileLevel = "WHITESPACE_ONLY";
                    break;
                default:
                    compileLevel = "ADVANCED_OPTIMIZATIONS";
                    break;
            }

            var request = WebRequest.Create(@"http://closure-compiler.appspot.com/compile");
            request.Method = "POST";

            string postData = "output_format=json" +
                              "&output_info=compiled_code" +
                              "&output_info=warnings" +
                              "&output_info=errors" +
                              "&output_info=statistics" +
                              "&compilation_level=" + compileLevel + 
                              "&warning_level=verbose" +
                              "&output_file_name=default.js" +
                              "&js_code=" + javaScript;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            string responseFromServer = string.Empty;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        responseFromServer = reader.ReadToEnd();

                        reader.Close();
                        dataStream.Close();
                        response.Close();
                    }
                }
            }

            return responseFromServer;
        }
    }

}
