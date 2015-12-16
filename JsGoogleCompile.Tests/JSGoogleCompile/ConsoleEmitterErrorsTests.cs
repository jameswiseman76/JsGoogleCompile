namespace JsGoogleCompile.Tests
{
    using System;
    using System.Collections.Generic;

    using log4net;

    using Moq;

    using Xunit;

    public class ConsoleEmitterErrorsTests
    {
        [Fact]
        public void EmitErrors_Guards_Null_Compiler_Results()
        {
            Assert.Throws<ArgumentNullException>(() => (new ConsoleEmitter()).EmitErrors(null));
        }

        [Fact]
        public void EmitErrors_Logs_Message_Twice_With_Single_Error()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var expectedLineText = "expectedLineText" + Guid.NewGuid();
            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = new List<CompilerError>
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
            emitter.EmitErrors(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(m => m.Info(It.Is<string>(p => p == expectedLineText)), Times.Once);
        }

        [Fact]
        public void EmitErrors_Logs_Message_Four_Times_With_Two_Errors()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var expectedFirstLineText = "expectedLineText" + Guid.NewGuid();
            var expectedSecondLineText = "expectedLineText" + Guid.NewGuid();
            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = new List<CompilerError>
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
            emitter.EmitErrors(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Exactly(4));
            loggerMock.Verify(m => m.Info(It.Is<string>(p => p == expectedFirstLineText)), Times.Once);
            loggerMock.Verify(m => m.Info(It.Is<string>(p => p == expectedSecondLineText)), Times.Once);
        }

        [Fact]
        public void EmitErrors_Logs_Message_Once_With_Valid_Results_That_Has_Empty_Errors_Collection()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = new List<CompilerError>()    // our empty Errors collection
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitErrors(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void EmitErrors_Logs_Message_Once_With_Valid_Results_That_Has_Null_Errors_Collection()
        {
            // Arrange
            var loggerMock = new Mock<ILog>();
            ConsoleEmitter.SetLogger(loggerMock.Object);

            var compilerResults = new CompilerResults
            {
                OutputFilePath = string.Empty,
                Errors = null     // our null Errors collection
            };
            var emitter = new ConsoleEmitter();

            // Act
            emitter.EmitErrors(compilerResults);

            // Assert
            loggerMock.Verify(m => m.Info(It.IsAny<string>()), Times.Never);
        }
    }
}
