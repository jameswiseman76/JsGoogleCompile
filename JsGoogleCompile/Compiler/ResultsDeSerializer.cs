// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResultsDeserializer.cs" company="www.jameswiseman.com">
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
    using System.Collections.Generic;

    using System.Web.Script.Serialization;

    /// <summary>
    /// Takes results from the compiler and serializes them into a class
    /// </summary>
    public class ResultsDeserializer
    {
        /// <summary>
        /// The serializer.
        /// </summary>
        private readonly JavaScriptSerializer serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsDeserializer"/> class.
        /// </summary>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when argument is null
        /// </exception>
        public ResultsDeserializer(JavaScriptSerializer serializer)
        {
            Guard.ArgumentNotNull(() => serializer);

            this.serializer = serializer;
        }

        /// <summary>
        /// Deserialize from input
        /// </summary>
        /// <param name="input">
        /// The input to be deserialized
        /// </param>
        /// <returns>
        /// Deserialized <see cref="CompilerResults"/>.
        /// </returns>
        public CompilerResults DeserializeCompilerResults(string input)
        {
            CompilerResults results;

            try
            {
                results = this.serializer.Deserialize<CompilerResults>(input);
            }
            catch (ArgumentException exception)
            {
                results = this.CompilerResultsFromException(exception);
            }

            return results;
        }

        /// <summary>
        /// Return compiler results from an exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// The <see cref="CompilerResults"/>.
        /// </returns>
        private CompilerResults CompilerResultsFromException(Exception exception)
        {
            return new CompilerResults
            {
                Errors = new List<CompilerError>
                {
                    new CompilerError
                    {
                        Error = string.Format("Error reading compiler results : {0}", exception.Message),
                        Line = string.Empty,
                    }
                }
            };
        }
    }
}
