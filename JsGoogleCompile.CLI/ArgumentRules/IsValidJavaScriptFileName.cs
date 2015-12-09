﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsValidJavaScriptFileName.cs" company="www.jameswiseman.com">
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
//     Argument Rule to check if this is a valid JavaScript file name
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile.CLI
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The is valid compilation level argument.
    /// </summary>
    public class IsValidJavaScriptFileName : IArgumentRule
    {
        /// <summary>
        /// The command line arguments.
        /// </summary>
        private readonly ICommandLineArguments commandLineArguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="IsValidJavaScriptFileName"/> class.
        /// </summary>
        /// <param name="commandLineArguments">
        /// The command line arguments.
        /// </param>
        public IsValidJavaScriptFileName(ICommandLineArguments commandLineArguments)
        {
            Guard.ArgumentNotNull(() => commandLineArguments, commandLineArguments);

            this.commandLineArguments = commandLineArguments;
        }

        /// <summary>
        /// Determines if the rule is satisfied by the given arguments
        /// </summary>
        /// <param name="arguments">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSatisfiedBy(IList<string> arguments)
        {
            Guard.ArgumentNotNull(() => arguments, arguments);

            return arguments.Any(this.IsValid);
        }

        /// <summary>
        /// Determine if the given file has a valid JavaScript file extension.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// True or false if this has a JavaScript file extension <see cref="bool"/>.
        /// </returns>
        private bool IsValid(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (Path.GetExtension(fileName).ToUpper() == ".JS")
            {
                this.commandLineArguments.FileName = fileName;
                return true;
            }

            return false;
        }
    }
}
