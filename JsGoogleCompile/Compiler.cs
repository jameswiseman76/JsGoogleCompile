// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Compiler.cs" company="www.jameswiseman.com">
// This license governs use of the accompanying software. If you use the software, you
// accept this license. If you do not accept the license, do not use the software.
//
// 1. Definitions
// The terms "reproduce," "reproduction," "derivative works," and "distribution" have the
// same meaning here as under U.S. copyright law.
// A "contribution" is the original software, or any additions or changes to the software.
// A "contributor" is any person that distributes its contribution under this license.
// "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2. Grant of Rights
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//
// 3. Conditions and Limitations
// (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
// (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// </copyright>
// <summary>
//     Calls out to the Google Closure Compiler
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The compiler.
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// The read java script file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ReadJavaScriptFile(string fileName)
        {
            var javascriptStreamReader = new StreamReader(fileName);
            return Uri.EscapeDataString(javascriptStreamReader.ReadToEnd());
        }

        /// <summary>
        /// The compile JavaScript file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="compileLevel">
        /// The compile level.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CompileJavaScriptFile(string fileName, string compileLevel)
        {
            return this.CompileJavaScriptString(this.ReadJavaScriptFile(fileName), compileLevel);
        }

        /// <summary>
        /// The compile JavaScript string.
        /// </summary>
        /// <param name="javaScript">
        /// The java script.
        /// </param>
        /// <param name="compileLevel">
        /// The compile level.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CompileJavaScriptString(string javaScript, string compileLevel)
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

            var postData = "output_format=json" +
                              "&output_info=compiled_code" +
                              "&output_info=warnings" +
                              "&output_info=errors" +
                              "&output_info=statistics" +
                              "&compilation_level=" + compileLevel + 
                              "&warning_level=verbose" +
                              "&output_file_name=default.js" +
                              "&js_code=" + javaScript;

            var byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            var responseFromServer = string.Empty;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            using (var response = request.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    if (dataStream != null)
                    {
                        using (var reader = new StreamReader(dataStream))
                        {
                            responseFromServer = reader.ReadToEnd();
                        }
                    }
                }
            }

            return responseFromServer;
        }
    }
}
