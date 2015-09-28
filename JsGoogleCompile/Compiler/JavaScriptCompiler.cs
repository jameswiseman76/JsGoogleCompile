// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JavaScriptCompiler.cs" company="www.jameswiseman.com">
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
    using System.Text;

    /// <summary>
    /// The JavaScript Compiler.
    /// </summary>
    public class JavaScriptCompiler
    {
        /// <summary>
        /// The source file reader.
        /// </summary>
        private readonly CompilerOptions compilerOptions;

        /// <summary>
        /// The compilation level helper.
        /// </summary>
        private readonly ICompilationLevelHelper compilationLevelHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavaScriptCompiler"/> class.
        /// </summary>
        /// <param name="compilerOptions">
        /// The compiler options.
        /// </param>
        /// <param name="compilationLevelHelper">
        /// The compilation Level Helper.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when argument is null
        /// </exception>
        public JavaScriptCompiler(CompilerOptions compilerOptions, ICompilationLevelHelper compilationLevelHelper)
        {
            Guard.ArgumentNotNull(() => compilerOptions, compilerOptions);
            Guard.ArgumentNotNull(() => compilationLevelHelper, compilationLevelHelper);

            this.compilerOptions = compilerOptions;
            this.compilationLevelHelper = compilationLevelHelper;
        }

        /// <summary>
        /// Run the compilation from the given JavaScript file.
        /// </summary>
        /// <returns>
        /// The response string from the compiler
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when argument is null or empty
        /// </exception>
        public string Compile()
        {
            return this.CompileFromString(this.ReadFile(), this.compilerOptions.CompilationLevel);
        }

        /// <summary>
        /// The reads the source JavaScript file
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ReadFile()
        {
            return Uri.EscapeDataString(this.compilerOptions.SourceReader.ReadToEnd());
        }

        /// <summary>
        /// Compiles JavaScript form given string.
        /// </summary>
        /// <param name="javaScript">
        /// The JavaScript string to compile.
        /// </param>
        /// <param name="compilationLevel">
        /// The compilation level.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> string response ready for serialization.
        /// </returns>
        private string CompileFromString(string javaScript, string compilationLevel)
        {
            compilationLevel = this.compilationLevelHelper.From(compilationLevel);

            this.compilerOptions.WebRequest.Method = "POST";

            var postData = "output_format=json" +
                              "&output_info=compiled_code" +
                              "&output_info=warnings" +
                              "&output_info=errors" +
                              "&output_info=statistics" +
                              "&compilation_level=" + compilationLevel + 
                              "&warning_level=verbose" +
                              "&language=ECMASCRIPT3" +
                              "&js_code=" + javaScript;

            var byteArray = Encoding.UTF8.GetBytes(postData);

            this.compilerOptions.WebRequest.ContentType = "application/x-www-form-urlencoded";
            this.compilerOptions.WebRequest.ContentLength = byteArray.Length;

            using (var dataStream = this.compilerOptions.WebRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            string responseFromServer;
            using (var response = this.compilerOptions.WebRequest.GetResponse())
            {
                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = dataStream == null ? null : new StreamReader(dataStream))
                    {
                        responseFromServer = reader == null ? string.Empty : reader.ReadToEnd();
                    }
                }
            }

            return responseFromServer;
        }
    }
}
