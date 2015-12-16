namespace JsGoogleCompile.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;

    public class WarningCodeTests
    {
        [Fact]
        public void Ensure_List_Of_All_Warnings_Codes_Is_As_Expected()
        {
            var allCodes = new List<string>
            {
                WarningCode.JscBadDeleteOperand,
                WarningCode.JscBadTypeForBitOperation,
                WarningCode.JscWrongArgumentCount,
                WarningCode.JscFunctionMasksVariable,
                WarningCode.JscInvalidFunctionDecl,
                WarningCode.JscNamespaceRedefined,
                WarningCode.JscNotAConstructor,
                WarningCode.JscNotFunctionType,
                WarningCode.JscRedeclaredVariable,
                WarningCode.JscReferenceBeforeDeclare,
                WarningCode.JscSetWithoutRead,
                WarningCode.JscSuspiciousSemicolon,
                WarningCode.JscTypeMismatch,
                WarningCode.JscUndefinedName,
                WarningCode.JscUndefinedVariable,
                WarningCode.JscUnsafeNamespace,
                WarningCode.JscUnsafeThis,
                WarningCode.JscUsedGlobalThis,
                WarningCode.JscUselessCode,
                WarningCode.JscVarArgsMustBeLast,
                WarningCode.JscWrongArgumentCount,
            };

            Assert.Equal(allCodes, WarningCode.AllWarningCodes.ToList());
        }
    }
}
