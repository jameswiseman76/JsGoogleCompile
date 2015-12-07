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
    }
}
