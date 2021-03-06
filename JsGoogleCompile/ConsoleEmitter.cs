﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleEmitter.cs" company="www.jameswiseman.com">
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
//     Implements the results writing for writing to the console.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile
{
    using log4net;

    /// <summary>
    /// Emit results to the console
    /// </summary>
    public class ConsoleEmitter : IResultsOutput
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static ILog log = LogManager.GetLogger(typeof(ConsoleEmitter));

        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="logger">The log.</param>
        public static void SetLogger(ILog logger)
        {
            log = logger;
        }

        /// <summary>
        /// Emits warnings.
        /// </summary>
        /// <param name="compilerResults">The compiler results.</param>
        public void EmitWarnings(ICompilerResults compilerResults)
        {
            Guard.ArgumentNotNull(() => compilerResults, compilerResults);

            var warningCount = compilerResults.Warnings == null ? 0 : compilerResults.Warnings.Count;

            if (warningCount <= 0)
            {
                return;
            }

            // ReSharper disable once PossibleNullReferenceException
            foreach (var compilerWarning in compilerResults.Warnings)
            {
                log.Info(
                    string.Format(
                        "{0}({1}): WARNING  ({2}) - {3}",
                        compilerResults.OutputFilePath,
                        compilerWarning.Lineno,
                        compilerWarning.Type,
                        compilerWarning.Warning));
                log.Info(compilerWarning.Line.TrimStart());
            }
        }

        /// <summary>
        /// Emits errors.
        /// </summary>
        /// <param name="compilerResults">The compiler results.</param>
        public void EmitErrors(ICompilerResults compilerResults)
        {
            Guard.ArgumentNotNull(() => compilerResults, compilerResults);

            var errorCount = compilerResults.Errors == null ? 0 : compilerResults.Errors.Count;

            if (errorCount <= 0)
            {
                return;
            }

            // ReSharper disable once PossibleNullReferenceException
            foreach (var compilerError in compilerResults.Errors)
            {
                log.Info(
                    string.Format(
                        "{0}({1}): ERROR ({2}) - {3}", 
                        compilerResults.OutputFilePath, 
                        compilerError.Lineno, 
                        compilerError.Type, 
                        compilerError.Error));
                log.Info(compilerError.Line.TrimStart());
            }
        }

        /// <summary>
        /// Emits a summary of the results.
        /// </summary>
        /// <param name="compilerResults">The compiler results.</param>
        public void EmitSummary(ICompilerResults compilerResults)
        {
            Guard.ArgumentNotNull(() => compilerResults, compilerResults);

            log.Info("----------------------------");

            var warningCount = compilerResults.Warnings == null ? 0 : compilerResults.Warnings.Count;
            var errorCount = compilerResults.Errors == null ? 0 : compilerResults.Errors.Count;

            if (warningCount > 0 || errorCount > 0)
            {
                log.Info("Found " + errorCount + " Errors, " + warningCount + " Warnings");
            }
            else
            {
                log.Info("No Errors or Warnings Found!");
            }

            if (errorCount <= 0)
            {
                log.Info("----------------------------");
                log.Info("Code Emitted:");
                log.Info(compilerResults.CompiledCode);
            }
        }
    }
}
