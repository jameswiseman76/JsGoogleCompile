namespace JsGoogleCompile
{
    /// <summary>
    /// Class repesenting warnings.
    /// </summary>
    public static class Warnings
    {
        /// <summary>
        /// JSC_BAD_DELETE_OPERAND warning.
        /// </summary>
        public const string JscBadDeleteOperand = "JSC_BAD_DELETE_OPERAND";

        /// <summary>
        /// The jsc bad type for bit operation.
        /// </summary>
        public const string JscBadTypeForBitOperation = "JSC_BAD_TYPE_FOR_BIT_OPERATION";

        /// <summary>
        /// The jsc constructor not callable.
        /// </summary>
        public const string JscConstructorNotCallable = "JSC_CONSTRUCTOR_NOT_CALLABLE";

        /// <summary>
        /// The jsc function masks variable.
        /// </summary>
        public const string JscFunctionMasksVariable = "JSC_FUNCTION_MASKS_VARIABLE";
        
        public const string JscInvalidFunctionDecl = "JSC_INVALID_FUNCTION_DECL";

        public const string JscNamespaceRedefined = "JSC_NAMESPACE_REDEFINED";
        public const string JscNotAConstructor = "JSC_NOT_A_CONSTRUCTOR";
        public const string JscNotFunctionType = "JSC_NOT_FUNCTION_TYPE";
        public const string JscRedeclaredVariable = "JSC_REDECLARED_VARIABLE";
        public const string JscReferenceBeforeDeclare = "JSC_REFERENCE_BEFORE_DECLARE";
        public const string JscSetWithoutRead = "JSC_SET_WITHOUT_READ";
        public const string JscSuspiciousSemicolon = "JSC_SUSPICIOUS_SEMICOLON";
        public const string JscTypeMismatch = "JSC_TYPE_MISMATCH";
        public const string JscUndefinedName = "JSC_UNDEFINED_NAME";
        public const string JscUndefinedVariable = "JSC_UNDEFINED_VARIABLE";
        public const string JscUnsafeNamespace = "JSC_UNSAFE_NAMESPACE";
        public const string JscUnsafeThis = "JSC_UNSAFE_THIS";
        public const string JscUsedGlobalThis = "JSC_USED_GLOBAL_THIS";
        public const string JscUselessCode = "JSC_USELESS_CODE";
        public const string JscVarArgsMustBeLast = "JSC_VAR_ARGS_MUST_BE_LAST";
        public const string JscWrongArgumentCount = "JSC_WRONG_ARGUMENT_COUNT";
    }
}
