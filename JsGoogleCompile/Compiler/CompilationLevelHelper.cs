// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompilationLevel.cs" company="www.jameswiseman.com">
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
//     Contains the compilation level constants
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace JsGoogleCompile
{
    using System.Collections.Generic;

    /// <summary>
    /// The compilation level.
    /// </summary>
    public class CompilationLevelHelper : ICompilationLevelHelper
    {
        /// <summary>
        /// Compiler setting string representing Simple Optimizations
        /// </summary>
        public const string SimpleOptimizations = "SIMPLE_OPTIMIZATIONS";

        /// <summary>
        /// Compiler setting string representing Whitespace only
        /// </summary>
        public const string WhitespaceOnly = "WHITESPACE_ONLY";

        /// <summary>
        /// Compiler setting string representing Advanced Optimizations
        /// </summary>
        public const string AdvancedOptimizations = "ADVANCED_OPTIMIZATIONS";

        /// <summary>
        /// The compilation level mapping.
        /// </summary>
        private readonly Dictionary<string, string> mapping = new Dictionary<string, string>
        {
            { "S", SimpleOptimizations }, 
            { "W", WhitespaceOnly }, 
            { "A", AdvancedOptimizations }, 
        };

        /// <summary>
        /// The from.
        /// </summary>
        /// <param name="shortCode">
        /// The short code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string From(string shortCode)
        {
            shortCode = shortCode.ToUpper();
            return this.IsValid(shortCode) ? this.mapping[shortCode] : AdvancedOptimizations;
        }

        /// <summary>
        /// Denotes if the given short code option is valid.
        /// </summary>
        /// <param name="shortCode">
        /// The short code representing the given option
        /// </param>
        /// <returns>
        /// true or false indicating validity
        /// </returns>
        public bool IsValid(string shortCode)
        {
            return this.mapping.ContainsKey(shortCode.ToUpper());
        }
    }
}
