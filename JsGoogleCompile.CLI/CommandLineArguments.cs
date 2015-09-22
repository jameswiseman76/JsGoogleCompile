// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArguments.cs" company="www.jameswiseman.com">
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
//     Encapsultes command line arguments functionality
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace JsGoogleCompile.CLI
{
    using System;

    /// <summary>
    /// The command line arguments.
    /// </summary>
    public class CommandLineArguments
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineArguments"/> class.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public CommandLineArguments(string[] args)
        {
            this.FromArgs(args);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current args are valid.
        /// </summary>
        public bool AreValid { get; set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the compilation level.
        /// </summary>
        public string CompilationLevel { get; private set; }

        /// <summary>
        /// The check usage instructions.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private void FromArgs(string[] args)
        {
            if ((args.Length == 2) && (args[1].Substring(1, 1).ToUpper() == "C"))
            {
                this.FileName = args[0];
                this.CompilationLevel = args[1].Substring(2, 1);
                this.AreValid = true;
                return;
            }

            if ((args.Length == 1) && (args[0] != "/?"))
            {
                this.FileName = args[0];
                this.CompilationLevel = "a";
                this.AreValid = true;
                return;
            }

            EmitUsageInstructions();
            this.AreValid = false;
        }

        /// <summary>
        /// Emit usage instructions.
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
