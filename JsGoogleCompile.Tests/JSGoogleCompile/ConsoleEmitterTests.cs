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

        ////[TestMethod]
        ////public void EmitWarnings_Logs_Message()
        ////{
        ////    var loggerMock = new Mock<ILog>();
        ////    ConsoleEmitter.SetLogger(loggerMock.Object);

        ////    var compilerResults = new CompilerResults
        ////                              {
        ////                                  Warnings = new List<CompilerError> { new CompilerError() },
        ////                              };
        ////    var emitter = new ConsoleEmitter();

        ////    emitter.EmitWarnings(compilerResults);
        ////}

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
