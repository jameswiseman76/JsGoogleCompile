namespace JsGoogleCompile.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using Xunit;

    public class CompilerResultsTests
    {
        [Fact]
        public void Test_That_SupressWarningsFrom_Filters_Out_A_Single_Warning()
        {
            // Arrange
            var jscBadTypeForBitOperationWarning = new CompilerError { Type = WarningCode.JscBadTypeForBitOperation };
            var jscConstructorNotCallableWarning = new CompilerError { Type = WarningCode.JscConstructorNotCallable };

            var results = new CompilerResults
            {
                Warnings = new List<CompilerError>
                {
                    new CompilerError { Type = WarningCode.JscBadDeleteOperand },
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
            Assert.Equal(2, results.Warnings.Count);
            Assert.True(results.Warnings.Any(w => w == jscBadTypeForBitOperationWarning));
            Assert.True(results.Warnings.Any(w => w == jscConstructorNotCallableWarning));
        }

        [Fact]
        public void Test_That_SupressWarningsFrom_Filters_Out_Multiple_Warnings()
        {
            // Arrange
            var jscBadTypeForBitOperationWarning = new CompilerError { Type = WarningCode.JscBadTypeForBitOperation };
            var jscConstructorNotCallableWarning = new CompilerError { Type = WarningCode.JscConstructorNotCallable };
            var jscBadDeleteOperandWarning = new CompilerError { Type = WarningCode.JscBadDeleteOperand };
            var jscJscFunctionMasksVariableWarning = new CompilerError { Type = WarningCode.JscFunctionMasksVariable };

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
            Assert.Equal(2, results.Warnings.Count);
            Assert.True(results.Warnings.Any(w => w == jscBadTypeForBitOperationWarning));
            Assert.True(results.Warnings.Any(w => w == jscJscFunctionMasksVariableWarning));
        }

        [Fact]
        public void Test_That_SupressWarningsFrom_Works_When_Specified_Filters_Do_Not_Exist()
        {
            // Arrange
            var jscBadTypeForBitOperationWarning = new CompilerError { Type = WarningCode.JscBadTypeForBitOperation };
            var jscJscFunctionMasksVariableWarning = new CompilerError { Type = WarningCode.JscFunctionMasksVariable };

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
            Assert.Equal(2, results.Warnings.Count);
            Assert.True(results.Warnings.Any(w => w == jscBadTypeForBitOperationWarning));
            Assert.True(results.Warnings.Any(w => w == jscJscFunctionMasksVariableWarning));
        }

        [Fact]
        public void Test_That_SupressWarningsFrom_Doesnt_Suppress_Any_Warnings_When_Passed_A_Null_Suppression_List()
        {
            // Arrange
            var jscBadTypeForBitOperationWarning = new CompilerError { Type = WarningCode.JscBadTypeForBitOperation };
            var jscJscFunctionMasksVariableWarning = new CompilerError { Type = WarningCode.JscFunctionMasksVariable };

            var results = new CompilerResults
            {
                Warnings = new List<CompilerError>
                {
                    jscBadTypeForBitOperationWarning,
                    jscJscFunctionMasksVariableWarning,
                }
            };

            // Act
            results.SupressWarningsFrom(null);

            // Assert
            Assert.Equal(2, results.Warnings.Count);
            Assert.True(results.Warnings.Any(w => w == jscBadTypeForBitOperationWarning));
            Assert.True(results.Warnings.Any(w => w == jscJscFunctionMasksVariableWarning));
        }

        [Fact]
        public void Test_That_Emit_Function_Emits_Warnings_Errors_And_Summary_Once_With_A_Single_Emitter()
        {
            // Arrange
            var results = new CompilerResults();
            var emitterMock = new Mock<IResultsOutput>();
            var emitters = new List<IResultsOutput> { emitterMock.Object };

            // Act
            results.Emit(emitters);

            // Assert
            emitterMock.Verify(m => m.EmitErrors(It.Is<CompilerResults>(cr => cr.Equals(results))), Times.Once);
            emitterMock.Verify(m => m.EmitWarnings(It.Is<CompilerResults>(cr => cr.Equals(results))), Times.Once);
            emitterMock.Verify(m => m.EmitSummary(It.Is<CompilerResults>(cr => cr.Equals(results))), Times.Once);
        }

        [Fact]
        public void Test_That_Emit_Function_Emits_Warnings_Errors_And_Summary_Twice_With_Two_Emitters()
        {
            // Arrange
            var results = new CompilerResults();
            var emitterMock = new Mock<IResultsOutput>();
            var emitters = new List<IResultsOutput> { emitterMock.Object, emitterMock.Object };

            // Act
            results.Emit(emitters);

            // Assert
            emitterMock.Verify(m => m.EmitErrors(It.Is<CompilerResults>(cr => cr.Equals(results))), Times.Exactly(2));
            emitterMock.Verify(m => m.EmitWarnings(It.Is<CompilerResults>(cr => cr.Equals(results))), Times.Exactly(2));
            emitterMock.Verify(m => m.EmitSummary(It.Is<CompilerResults>(cr => cr.Equals(results))), Times.Exactly(2));
        }
    }
}
