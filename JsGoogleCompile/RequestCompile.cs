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
//     Program entry point for requesting JS Compilation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile
{
    using System;
    using System.Web.Script.Serialization;

    /// <summary>
    /// The request compile.
    /// </summary>
    public class RequestCompile
    {
        /// <summary>
        /// The file name.
        /// </summary>
        private static string fileName = string.Empty;

        /// <summary>
        /// The compile level.
        /// </summary>
        private static string compileLevel = string.Empty;

        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            try
            {
                var compiler = new Compiler();

                if (!CheckUsageInstructions(args))
                {
                    return;
                }

                Console.WriteLine("Requesting compile from http://closure-compiler.appspot.com/compile...");
                Console.WriteLine();

                fileName = args[0];

                var responseFromServer = compiler.CompileJavaScriptFile(fileName, compileLevel);

                var jss = new JavaScriptSerializer();
                var cr = jss.Deserialize<CompilerResults>(responseFromServer);

                // cr.errors.c
                int errorCount = 0, warningCount = 0;
                if (cr.Errors != null)
                {
                    errorCount = cr.Errors.Count;
                    if (errorCount > 0)
                    {
                        foreach (var ce in cr.Errors)
                        {
                            Console.WriteLine(fileName + "(" + ce.Lineno + "): ERROR " + ce.Error);
                            Console.WriteLine(ce.Line.TrimStart());
                        }
                    }
                }

                if (cr.Warnings != null)
                {
                    warningCount = cr.Warnings.Count;
                    if (warningCount > 0)
                    {
                        foreach (var cw in cr.Warnings)
                        {
                            Console.WriteLine(fileName + "(" + cw.Lineno + "): WARNING " + cw.Warning);
                            Console.WriteLine(cw.Line.TrimStart());
                        }
                    }
                }

                Console.WriteLine("----------------------------");
                Console.WriteLine("Completed Scan");

                if (warningCount > 0 || errorCount > 0)
                {
                    Console.WriteLine("Found " + errorCount + " Errors, " + warningCount + " Warnings");
                }
                else
                {
                    Console.WriteLine("No Errors or Warnings Found!");
                }

                if (errorCount <= 0)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Code Emitted:");
                    Console.WriteLine(cr.CompiledCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error Occurred Running GoogleCC:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// The check usage instructions.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckUsageInstructions(string[] args)
        {
            if ((args.Length == 2) && (args[1].Substring(1, 1).ToUpper() == "C"))
            {
                fileName = args[0];
                compileLevel = args[1].Substring(2, 1);
                return true;
            }

            if ((args.Length == 1) && (args[0] != "/?"))
            {
                compileLevel = "a";
                fileName = args[0];
                return true;
            }

            EmitUsageInstructions();
            return false;
        }

        /// <summary>
        /// The emit usage instructions.
        /// </summary>
        private static void EmitUsageInstructions()
        {
            Console.WriteLine("JsGoogleCompile: Request a compile from the Google Closure Compiler service");
            Console.WriteLine("(http://closure-compiler.appspot.com/compile)");
            Console.WriteLine();
            Console.WriteLine("Usage:\t\tJsGoogleCompile.exe FileName [/c[attribute]]");
            Console.WriteLine("FileName:\tThe full filename and path of the file on disk to compress");
            Console.WriteLine("/c: \t\tSpecify compilation level. (If omitted 'advanced' is assumed)");
            Console.WriteLine("attribute: \t w: Whitespace only");
            Console.WriteLine("\t\t s: Simple optimisations");
            Console.WriteLine("\t\t a: Advianced optimisations");
        }
    }
}
