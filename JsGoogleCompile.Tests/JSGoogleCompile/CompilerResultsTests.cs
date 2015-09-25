﻿namespace JsGoogleCompile.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class CompilerResultsTests
    {
        [TestMethod]
        public void Test_That_SupressWarningsFrom_Filters_Out_A_Single_Warning()
        {
            // Arrange
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

            // Act
            results.SupressWarningsFrom(supressWarnings);

            // Assert
            Assert.AreEqual(2, results.Warnings.Count);
            CollectionAssert.Contains(results.Warnings, jscBadTypeForBitOperationWarning);
            CollectionAssert.Contains(results.Warnings, jscConstructorNotCallableWarning);
        }

        [TestMethod]
        public void Test_That_SupressWarningsFrom_Filters_Out_Multiple_Warnings()
        {
            // Arrange
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

            // Act
            results.SupressWarningsFrom(supressWarnings);

            // Assert
            Assert.AreEqual(2, results.Warnings.Count);
            CollectionAssert.Contains(results.Warnings, jscBadTypeForBitOperationWarning);
            CollectionAssert.Contains(results.Warnings, jscJscFunctionMasksVariableWarning);
        }

        [TestMethod]
        public void Test_That_SupressWarningsFrom_Works_When_Specified_Filteres_Do_Not_Exist()
        {
            // Arrange
            var jscBadTypeForBitOperationWarning = new CompilerError { Warning = WarningCode.JscBadTypeForBitOperation };
            var jscJscFunctionMasksVariableWarning = new CompilerError { Warning = WarningCode.JscFunctionMasksVariable };

            var results = new CompilerResults
            {
                Warnings = new List<CompilerError>
                {
                    jscBadTypeForBitOperationWarning,
                    jscJscFunctionMasksVariableWarning,
                }
            };

            var supressWarnings = new List<string>
            {
                WarningCode.JscBadDeleteOperand,
                WarningCode.JscConstructorNotCallable,
            };

            // Act
            results.SupressWarningsFrom(supressWarnings);

            // Assert
            Assert.AreEqual(2, results.Warnings.Count);
            CollectionAssert.Contains(results.Warnings, jscBadTypeForBitOperationWarning);
            CollectionAssert.Contains(results.Warnings, jscJscFunctionMasksVariableWarning);
        }
    }
}
