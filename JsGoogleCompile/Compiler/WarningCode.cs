// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WarningCode.cs" company="www.jameswiseman.com">
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
//     Warning code constants
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace JsGoogleCompile
{
    using System.Collections.Generic;

    /// <summary>
    /// Class representing warning codes
    /// </summary>
    public static class WarningCode
    {
        /// <summary>
        /// JSC_BAD_DELETE_OPERAND warning.
        /// </summary>
        public const string JscBadDeleteOperand = "JSC_BAD_DELETE_OPERAND";

        /// <summary>
        /// JSC_BAD_TYPE_FOR_BIT_OPERATION warning.
        /// </summary>
        public const string JscBadTypeForBitOperation = "JSC_BAD_TYPE_FOR_BIT_OPERATION";

        /// <summary>
        /// JSC_CONSTRUCTOR_NOT_CALLABLE warning.
        /// </summary>
        public const string JscConstructorNotCallable = "JSC_CONSTRUCTOR_NOT_CALLABLE";

        /// <summary>
        /// JSC_FUNCTION_MASKS_VARIABLE warning.
        /// </summary>
        public const string JscFunctionMasksVariable = "JSC_FUNCTION_MASKS_VARIABLE";

        /// <summary>
        /// JSC_INVALID_FUNCTION_DECL warning.
        /// </summary>
        public const string JscInvalidFunctionDecl = "JSC_INVALID_FUNCTION_DECL";

        /// <summary>
        /// JSC_NAMESPACE_REDEFINED warning.
        /// </summary>
        public const string JscNamespaceRedefined = "JSC_NAMESPACE_REDEFINED";

        /// <summary>
        /// JSC_NOT_A_CONSTRUCTOR warning.
        /// </summary>
        public const string JscNotAConstructor = "JSC_NOT_A_CONSTRUCTOR";

        /// <summary>
        /// JSC_BAD_DELETE_OPERAND warning.
        /// </summary>
        public const string JscNotFunctionType = "JSC_NOT_FUNCTION_TYPE";

        /// <summary>
        /// JSC_REDECLARED_VARIABLE warning.
        /// </summary>
        public const string JscRedeclaredVariable = "JSC_REDECLARED_VARIABLE";

        /// <summary>
        /// JSC_REFERENCE_BEFORE_DECLARE warning.
        /// </summary>
        public const string JscReferenceBeforeDeclare = "JSC_REFERENCE_BEFORE_DECLARE";

        /// <summary>
        /// JSC_SET_WITHOUT_READ warning.
        /// </summary>
        public const string JscSetWithoutRead = "JSC_SET_WITHOUT_READ";

        /// <summary>
        /// JSC_SUSPICIOUS_SEMICOLON warning.
        /// </summary>
        public const string JscSuspiciousSemicolon = "JSC_SUSPICIOUS_SEMICOLON";

        /// <summary>
        /// JSC_TYPE_MISMATCH warning.
        /// </summary>
        public const string JscTypeMismatch = "JSC_TYPE_MISMATCH";

        /// <summary>
        /// JSC_UNDEFINED_NAME warning.
        /// </summary>
        public const string JscUndefinedName = "JSC_UNDEFINED_NAME";

        /// <summary>
        /// JSC_UNDEFINED_VARIABLE warning.
        /// </summary>
        public const string JscUndefinedVariable = "JSC_UNDEFINED_VARIABLE";

        /// <summary>
        /// JSC_UNSAFE_NAMESPACE warning.
        /// </summary>
        public const string JscUnsafeNamespace = "JSC_UNSAFE_NAMESPACE";

        /// <summary>
        /// JSC_UNSAFE_THIS warning.
        /// </summary>
        public const string JscUnsafeThis = "JSC_UNSAFE_THIS";

        /// <summary>
        /// JSC_USED_GLOBAL_THIS warning.
        /// </summary>
        public const string JscUsedGlobalThis = "JSC_USED_GLOBAL_THIS";

        /// <summary>
        /// JSC_USELESS_CODE warning.
        /// </summary>
        public const string JscUselessCode = "JSC_USELESS_CODE";

        /// <summary>
        /// JSC_VAR_ARGS_MUST_BE_LAST warning.
        /// </summary>
        public const string JscVarArgsMustBeLast = "JSC_VAR_ARGS_MUST_BE_LAST";

        /// <summary>
        /// JSC_WRONG_ARGUMENT_COUNT warning.
        /// </summary>
        public const string JscWrongArgumentCount = "JSC_WRONG_ARGUMENT_COUNT";

        /// <summary>
        /// Collection of all warnings.
        /// </summary>
        private static readonly IList<string> All = new List<string>
        {
            JscBadDeleteOperand,
            JscBadTypeForBitOperation,
            JscWrongArgumentCount,
            JscFunctionMasksVariable,
            JscInvalidFunctionDecl,
            JscNamespaceRedefined,
            JscNotAConstructor,
            JscNotFunctionType,
            JscRedeclaredVariable,
            JscReferenceBeforeDeclare,
            JscSetWithoutRead,
            JscSuspiciousSemicolon,
            JscTypeMismatch,
            JscUndefinedName,
            JscUndefinedVariable,
            JscUnsafeNamespace,
            JscUnsafeThis,
            JscUsedGlobalThis,
            JscUselessCode,
            JscVarArgsMustBeLast,
            JscWrongArgumentCount,
        };

        /// <summary>
        /// Gets a collection of all Warnings.
        /// </summary>
        public static IList<string> AllWarningCodes
        {
            get { return All; }
        }
    }
}
