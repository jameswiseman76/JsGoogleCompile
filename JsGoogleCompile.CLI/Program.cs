﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="www.jameswiseman.com">
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
//     Program entry point for requesting JS Compilation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile.CLI
{
    using System;

    using log4net;
    using log4net.Config;

    /// <summary>
    /// The command line program entry class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The command line program entry point.
        /// </summary>
        /// <param name="args">
        /// Command line arguments.
        /// </param>
        public static void Main(string[] args)
        {
            try
            {
                var commandLineArguments = new CommandLineArguments(args, new CompilationLevelHelper());

                if (!commandLineArguments.AreValid)
                {
                    return;
                }

                const string CompilerUrl = @"http://closure-compiler.appspot.com/compile";
                XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

                var requestCompile = new RequestCompile(
                    commandLineArguments.FileName,
                    commandLineArguments.CompilationLevel,
                    CompilerUrl,
                    commandLineArguments.SuppressedWarnings);

                var compilerResults = requestCompile.Run();

                var errorCount = compilerResults.Errors == null ? 0 : compilerResults.Errors.Count;
                if (errorCount > 0)
                {
                    foreach (var compilerError in compilerResults.Errors)
                    {
                        Log.Info(string.Format("{0}({1}): ERROR ({2}) - {3}", commandLineArguments.FileName, compilerError.Lineno, compilerError.Type, compilerError.Error));
                        Log.Info(compilerError.Line.TrimStart());
                    }
                }

                var warningCount = compilerResults.Warnings == null ? 0 : compilerResults.Warnings.Count;
                if (warningCount > 0)
                {
                    foreach (var compilerWarning in compilerResults.Warnings)
                    {
                        Log.Info(string.Format("{0}({1}): WARNING  ({2}) - {3}", commandLineArguments.FileName, compilerWarning.Lineno, compilerWarning.Type, compilerWarning.Warning));
                        Log.Info(compilerWarning.Line.TrimStart());
                    }
                }

                Log.Info("----------------------------");
                Log.Info("Completed Scan");

                if (warningCount > 0 || errorCount > 0)
                {
                    Log.Info("Found " + errorCount + " Errors, " + warningCount + " Warnings");
                }
                else
                {
                    Log.Info("No Errors or Warnings Found!");
                }

                if (errorCount <= 0)
                {
                    Log.Info("----------------------------");
                    Log.Info("Code Emitted:");
                    Log.Info(compilerResults.CompiledCode);
                }
            }
            catch (Exception e)
            {
                Log.Info("An Error Occurred Running GoogleCC:");
                Log.Info(e.Message);
                Log.Info(e.StackTrace);
            }
        }
    }
}
