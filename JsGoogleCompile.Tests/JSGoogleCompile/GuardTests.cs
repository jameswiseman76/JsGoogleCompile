namespace JsGoogleCompile.Tests.JSGoogleCompile
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GuardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNull_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Guard.ArgumentNotNull(() => argument, argument);
        }

        [TestMethod]
        public void ArgumentNotNull_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            string argument = null;
            try
            {
                Guard.ArgumentNotNull(() => argument, argument);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("argument", exception.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Guard.ArgumentNotNullOrEmpty(() => argument, argument);
        }

        [TestMethod]
        public void ArgumentNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            var argument = string.Empty;
            try
            {
                Guard.ArgumentNotNullOrEmpty(() => argument, argument);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("argument", exception.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Empty()
        {
            string argument = null;
            Guard.ArgumentNotNullOrEmpty(() => argument, argument);
        }

        [TestMethod]
        public void ArgumentNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Empty()
        {
            var argument = string.Empty;
            try
            {
                Guard.ArgumentNotNullOrEmpty(() => argument, argument);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("argument", exception.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValueNotNull_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Guard.ValueNotNull(() => argument, argument);
        }

        [TestMethod]
        public void ValueNotNull_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            string argument = null;
            try
            {
                Guard.ValueNotNull(() => argument, argument);
            }
            catch (NullReferenceException exception)
            {
                 Assert.AreEqual("argument", exception.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValueNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Guard.ValueNotNullOrEmpty(() => argument, argument);
        }

        [TestMethod]
        public void ValueNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            var argument = string.Empty;
            try
            {
                Guard.ValueNotNullOrEmpty(() => argument, argument);
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual("argument", exception.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValueNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Empty()
        {
            string argument = null;
            Guard.ValueNotNullOrEmpty(() => argument, argument);
        }

        [TestMethod]
        public void ValueNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Empty()
        {
            var argument = string.Empty;
            try
            {
                Guard.ValueNotNullOrEmpty(() => argument, argument);
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual("argument", exception.Message);
            }
        }
    }
}
