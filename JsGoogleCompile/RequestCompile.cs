// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestCompile.cs" company="www.jameswiseman.com">
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
//     The interface for calling into the compiler API
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web.Script.Serialization;

    using log4net;

    /// <summary>
    /// The class for requesting compilation.
    /// </summary>
    public class RequestCompile
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(RequestCompile));

        /// <summary>
        /// The file name.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// The compilation level.
        /// </summary>
        private readonly string compilationLevel;

        /// <summary>
        /// The compiler url.
        /// </summary>
        private readonly string compilerUrl;

        /// <summary>
        /// The suppressed warnings.
        /// </summary>
        private readonly IList<string> suppressedWarnings;

        /// <summary>
        /// The results output emitters
        /// </summary>
        private readonly IList<IResultsOutput> resultsEmitters;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestCompile" /> class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="compilationLevel">The compilation level.</param>
        /// <param name="compilerUrl">The compiler url.</param>
        /// <param name="suppressedWarnings">The suppressed warnings.</param>
        /// <param name="resultsEmitters">The results output emitters.</param>
        public RequestCompile(
            string fileName,
            string compilationLevel,
            string compilerUrl,
            IList<string> suppressedWarnings = null,
            IList<IResultsOutput> resultsEmitters = null)
        {
            Guard.ArgumentNotNullOrEmpty(() => fileName, fileName);
            Guard.ArgumentNotNullOrEmpty(() => compilerUrl, compilerUrl);

            this.fileName = fileName;
            this.compilationLevel = compilationLevel;
            this.compilerUrl = compilerUrl;
            this.suppressedWarnings = suppressedWarnings;
            this.resultsEmitters = this.SetupLocalDefaultEmitterIfNull(resultsEmitters);
        }

        /// <summary>
        /// Run the compilation
        /// </summary>
        /// <returns>
        /// The <see cref="CompilerResults" />.
        /// </returns>
        public ICompilerResults Run()
        {
            Log.Info(string.Format("Requesting compile of {0} from {1}...", this.fileName, this.compilerUrl));
            Log.Info(string.Empty);

            // Setup compiler dependencies
            var inputStream = new StreamReader(this.fileName);
            var request = WebRequest.Create(this.compilerUrl);
            var compilerOptions = new CompilerOptions(inputStream, request, this.compilationLevel);
            var compilationLevelHelper = new CompilationLevelHelper();

            var compiler = new JavaScriptCompiler(compilerOptions, compilationLevelHelper);
            var responseFromServer = compiler.Compile();

            var deserializer = new ResultsDeserializer(new JavaScriptSerializer());
            var compilerResults = deserializer.DeserializeCompilerResults(responseFromServer, this.fileName);

            compilerResults.SupressWarningsFrom(this.suppressedWarnings);
            compilerResults.Emit(this.resultsEmitters);

            return compilerResults;
        }

        /// <summary>
        /// Setups the local default emitter if the given emitter is null.
        /// </summary>
        /// <param name="resultsEmitters">The results emitters.</param>
        /// <returns>Non null list of emitters</returns>
        private IList<IResultsOutput> SetupLocalDefaultEmitterIfNull(IList<IResultsOutput> resultsEmitters)
        {
            return resultsEmitters ?? new List<IResultsOutput> { new ConsoleEmitter() };
        }
    }
}
