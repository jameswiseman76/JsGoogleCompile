namespace JsGoogleCompile.Tests
{
    using System;
    using System.Collections.Generic;

    using log4net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ConsoleEmitterWarningsTests
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
        public void EmitWarnings_Logs_Message_Four_Times_With_Two_Warnings()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var expectedFirstLineText = "expectedLineText" + Guid.NewGuid();
            var expectedSecondLineText = "expectedLineText" + Guid.NewGuid();
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
                       Line = expectedFirstLineText
                    },
                    new CompilerError
                    {
                       Lineno = 0,
                       Type = string.Empty,
                       Error = string.Empty,
                       Line = expectedSecondLineText
                    }
                },
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitWarnings(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Exactly(4));
            loggerMock.Verify(m => m.Info(It.Is<string>(p => p == expectedFirstLineText)), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(p => p == expectedSecondLineText)), Times.Once);
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
    }
}
