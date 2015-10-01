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
    using System.Collections.Generic;

    /// <summary>
    /// The command line arguments.
    /// </summary>
    public class CommandLineArguments : ICommandLineArguments
    {
        /// <summary>
        /// The compilation level helper.
        /// </summary>
        private readonly ICompilationLevelHelper compilationLevelHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineArguments"/> class.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <param name="compilationLevelHelper">
        /// The compilation Level Helper.
        /// </param>
        public CommandLineArguments(IList<string> args, ICompilationLevelHelper compilationLevelHelper)
        {
            Guard.ArgumentNotNull(() => args, args);
            Guard.ArgumentNotNull(() => compilationLevelHelper, compilationLevelHelper);

            this.compilationLevelHelper = compilationLevelHelper;
            this.FromArgs(args);

            if (this.AreValid)
            {
                this.CompilationLevel = "A";
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current args are valid.
        /// </summary>
        public bool AreValid { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the compilation level.
        /// </summary>
        public string CompilationLevel { get; set; }

        /// <summary>
        /// Gets or sets the suppressed warnings.
        /// </summary>
        public List<string> SuppressedWarnings { get; set; }

        /// <summary>
        /// Emit usage instructions.
        /// </summary>
        private static void EmitUsageInstructions()
        {
            Console.WriteLine("JsGoogleCompile: Request a compile from the Google Closure Compiler service");
            Console.WriteLine("(http://closure-compiler.appspot.com/compile)");
            Console.WriteLine();
            Console.WriteLine("Usage:\tJsGoogleCompile.exe FileName [/c[attribute]] [/s[semi-colon separated list of warnings]]");
            Console.WriteLine("\tFileName:\tThe full filename and path of the file on disk to compress");
            Console.WriteLine("\t/c: \t\tSpecify compilation level. (If omitted 'advanced' is assumed)");
            Console.WriteLine("\t\t\t/cw: Whitespace only");
            Console.WriteLine("\t\t\t/cs: Simple optimisations");
            Console.WriteLine("\t\t\t/ca: Advanced optimisations");
            Console.WriteLine("\t/s: \t\tSpecify warnings to be suppressed");
            Console.WriteLine("\t\t\t/s[warning1, warning2]");
            Console.WriteLine("\tWarnings:\tJSC_BAD_DELETE_OPERAND");
            Console.WriteLine("\t\t\tJSC_BAD_TYPE_FOR_BIT_OPERATION");
            Console.WriteLine("\t\t\tJSC_CONSTRUCTOR_NOT_CALLABLE");
            Console.WriteLine("\t\t\tJSC_FUNCTION_MASKS_VARIABLE");
            Console.WriteLine("\t\t\tJSC_INVALID_FUNCTION_DECL");
            Console.WriteLine("\t\t\tJSC_NAMESPACE_REDEFINED");
            Console.WriteLine("\t\t\tJSC_NOT_A_CONSTRUCTOR");
            Console.WriteLine("\t\t\tJSC_NOT_FUNCTION_TYPE");
            Console.WriteLine("\t\t\tJSC_REDECLARED_VARIABLE");
            Console.WriteLine("\t\t\tJSC_REFERENCE_BEFORE_DECLARE");
            Console.WriteLine("\t\t\tJSC_SET_WITHOUT_READ");
            Console.WriteLine("\t\t\tJSC_SUSPICIOUS_SEMICOLON");
            Console.WriteLine("\t\t\tJSC_TYPE_MISMATCH");
            Console.WriteLine("\t\t\tJSC_UNDEFINED_NAME");
            Console.WriteLine("\t\t\tJSC_UNDEFINED_VARIABLE");
            Console.WriteLine("\t\t\tJSC_UNSAFE_NAMESPACE");
            Console.WriteLine("\t\t\tJSC_UNSAFE_THIS");
            Console.WriteLine("\t\t\tJSC_USED_GLOBAL_THIS");
            Console.WriteLine("\t\t\tJSC_USELESS_CODE");
            Console.WriteLine("\t\t\tJSC_VAR_ARGS_MUST_BE_LAST");
            Console.WriteLine("\t\t\tJSC_WRONG_ARGUMENT_COUNT");
            Console.WriteLine("Example usages:");
            Console.WriteLine("\tJsGoogleCompile.exe sample.js");
            Console.WriteLine("\tJsGoogleCompile.exe sample.js /ca");
            Console.WriteLine("\tJsGoogleCompile.exe sample.js /sJSC_BAD_TYPE_FOR_BIT_OPERATION");
            Console.WriteLine("\tJsGoogleCompile.exe sample.js /cw /sJSC_BAD_TYPE_FOR_BIT_OPERATION;JSC_UNSAFE_THIS");
        }

        /// <summary>
        /// The check usage instructions.
        /// </summary>
        /// <param name="arguments">
        /// The args.
        /// </param>
        private void FromArgs(IList<string> arguments)
        {
            var argumentRules = new ArgumentRules(this, this.compilationLevelHelper);
            this.AreValid = argumentRules.AnySatisfiedBy(arguments);

            if (!this.AreValid)
            {
                EmitUsageInstructions();
            }
        }
    }
}
