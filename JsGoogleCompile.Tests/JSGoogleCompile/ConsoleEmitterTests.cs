namespace JsGoogleCompile.Tests
{
    using System;
    using System.Collections.Generic;

    using log4net;
    using log4net.Repository.Hierarchy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ConsoleEmitterTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmitWarnings_Guards_Null_Compiler_Results()
        {
            (new ConsoleEmitter()).EmitWarnings(null);
        }

        [TestMethod]
        public void EmitWarnings_Logs_Message_Twice_With_Single_Warning()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var expectedLineText = "expectedLineText" + Guid.NewGuid();
            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Warnings = new List<CompilerError>
                {
                    new CompilerError
                    {
                       Lineno = 0,
                       Type = string.Empty,
                       Error = string.Empty,
                       Line = expectedLineText
                    }
                },
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitWarnings(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(m => m.Info(It.Is<string>(p => p == expectedLineText)), Times.Once);
        }

        [TestMethod]
        public void EmitWarnings_Logs_Message_Once_With_Valid_Results_That_Has_Empty_Warnings_Collection()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Warnings = new List<CompilerError>()    // our empty warnings collection
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitWarnings(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void EmitWarnings_Logs_Message_Once_With_Valid_Results_That_Has_Null_Warnings_Collection()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Warnings = null     // our null warnings collection
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitWarnings(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmitErrors_Guards_Null_Compiler_Results()
        {
            (new ConsoleEmitter()).EmitErrors(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmitSummary_Guards_Null_Compiler_Results()
        {
            (new ConsoleEmitter()).EmitSummary(null);
        }
    }
}
