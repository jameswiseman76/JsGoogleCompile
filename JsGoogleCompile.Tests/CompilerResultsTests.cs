namespace JsGoogleCompile.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class CompilerResultsTests
    {
        [TestMethod]
        public void Test_That_We_Can_Filter_Out_A_Single_Warning()
        {
            var jscBadTypeForBitOperationWarning = new CompilerError { Warning = WarningCode.JscBadTypeForBitOperation };
            var jscConstructorNotCallableWarning = new CompilerError { Warning = WarningCode.JscConstructorNotCallable };

            var results = new CompilerResults
            {
                Warnings = new List<CompilerError>
                {
                    new CompilerError { Warning = WarningCode.JscBadDeleteOperand },
                    jscBadTypeForBitOperationWarning,
                    jscConstructorNotCallableWarning,
                }
            };

            var supressWarnings = new List<string>
            {
                WarningCode.JscBadDeleteOperand,
            };

            results.SupressWarningsFrom(supressWarnings);

            Assert.AreEqual(2, results.Warnings.Count);
            CollectionAssert.Contains(results.Warnings, jscBadTypeForBitOperationWarning);
            CollectionAssert.Contains(results.Warnings, jscConstructorNotCallableWarning);
        }

        [TestMethod]
        public void Test_That_We_Can_Filter_Out_Multiple_Warnings()
        {
            var jscBadTypeForBitOperationWarning = new CompilerError { Warning = WarningCode.JscBadTypeForBitOperation };
            var jscConstructorNotCallableWarning = new CompilerError { Warning = WarningCode.JscConstructorNotCallable };
            var jscBadDeleteOperandWarning = new CompilerError { Warning = WarningCode.JscBadDeleteOperand };
            var jscJscFunctionMasksVariableWarning = new CompilerError { Warning = WarningCode.JscFunctionMasksVariable };

            var results = new CompilerResults
            {
                Warnings = new List<CompilerError>
                {
                    jscBadDeleteOperandWarning,
                    jscBadTypeForBitOperationWarning,
                    jscConstructorNotCallableWarning,
                    jscJscFunctionMasksVariableWarning,
                }
            };

            var supressWarnings = new List<string>
            {
                WarningCode.JscBadDeleteOperand,
                WarningCode.JscConstructorNotCallable,
            };

            results.SupressWarningsFrom(supressWarnings);

            Assert.AreEqual(2, results.Warnings.Count);
            CollectionAssert.Contains(results.Warnings, jscBadTypeForBitOperationWarning);
            CollectionAssert.Contains(results.Warnings, jscJscFunctionMasksVariableWarning);
        }
    }
}
