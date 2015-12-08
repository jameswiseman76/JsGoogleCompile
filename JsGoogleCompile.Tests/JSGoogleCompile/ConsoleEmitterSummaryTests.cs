namespace JsGoogleCompile.Tests
{
    using System;
    using System.Collections.Generic;

    using log4net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ConsoleEmitterSummaryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmitSummary_Guards_Null_Compiler_Results()
        {
            (new ConsoleEmitter()).EmitSummary(null);
        }

        [TestMethod]
        public void EmitSummary_Logs_Expected_Messages_With_Valid_Results_That_Has_Empty_Errors_And_Warnings()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);
            var expectedCompiledCode = "CompiledCode" + Guid.NewGuid();

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = new List<CompilerError>(),   
                Warnings = new List<CompilerError>(),    
                CompiledCode = expectedCompiledCode,
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitSummary(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "----------------------------")), Times.Exactly(2));
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "No Errors or Warnings Found!")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "Code Emitted:")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == expectedCompiledCode)), Times.Once);
        }

        [TestMethod]
        public void EmitSummary_Logs_Expected_Messages_With_Valid_Results_That_Has_Empty_Errors_And_Null_Warnings()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);
            var expectedCompiledCode = "CompiledCode" + Guid.NewGuid();

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = new List<CompilerError>(),    
                Warnings = null,    
                CompiledCode = expectedCompiledCode,
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitSummary(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "----------------------------")), Times.Exactly(2));
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "No Errors or Warnings Found!")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "Code Emitted:")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == expectedCompiledCode)), Times.Once);
        }

        [TestMethod]
        public void EmitSummary_Logs_Expected_Messages_With_Valid_Results_That_Has_Null_Errors_And_Empty_Warnings()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);
            var expectedCompiledCode = "CompiledCode" + Guid.NewGuid();

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = null,    
                Warnings = new List<CompilerError>(),    
                CompiledCode = expectedCompiledCode,
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitSummary(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "----------------------------")), Times.Exactly(2));
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "No Errors or Warnings Found!")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "Code Emitted:")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == expectedCompiledCode)), Times.Once);
        }

        [TestMethod]
        public void EmitSummary_Logs_Expected_Messages_With_Valid_Results_That_Has_A_Warning_But_No_Errors()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);
            var expectedCompiledCode = "CompiledCode" + Guid.NewGuid();

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = null,
                Warnings = new List<CompilerError>
                               {
                                    new CompilerError
                                    {
                                       Lineno = 0,
                                       Type = string.Empty,
                                       Error = string.Empty,
                                       Line = string.Empty,
                                    }
                               },
                CompiledCode = expectedCompiledCode,
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitSummary(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "----------------------------")), Times.Exactly(2));
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "Code Emitted:")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == expectedCompiledCode)), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s.Contains("1 Warnings"))), Times.Once);
        }

        [TestMethod]
        public void EmitSummary_Logs_Expected_Messages_With_Valid_Results_That_Has_No_Warnings_And_One_Error()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);
            var expectedCompiledCode = "CompiledCode" + Guid.NewGuid();

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Warnings = null,
                Errors = new List<CompilerError>
                               {
                                    new CompilerError
                                    {
                                       Lineno = 0,
                                       Type = string.Empty,
                                       Error = string.Empty,
                                       Line = string.Empty,
                                    }
                               },
                CompiledCode = expectedCompiledCode,
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitSummary(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s == "----------------------------")), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(s => s.Contains("1 Errors"))), Times.Once);
        }
    }
}
