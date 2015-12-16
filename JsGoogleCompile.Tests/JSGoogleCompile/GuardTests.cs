namespace JsGoogleCompile.Tests
{
    using System;

    using Xunit;

    public class GuardTests
    {
        [Fact]
        public void ArgumentNotNull_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(() => argument, argument));
        }

        [Fact]
        public void ArgumentNotNull_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            string argument = null;
            var exception = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(() => argument, argument));
            Assert.Equal("argument", exception.ParamName);
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(() => argument, argument));
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            var argument = string.Empty;
            try
            {
                Guard.ArgumentNotNullOrEmpty(() => argument, argument);
            }
            catch (ArgumentNullException exception)
            {
                Assert.Equal("argument", exception.ParamName);
            }
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Empty()
        {
            string argument = null;
            Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(() => argument, argument));
        }

        [Fact]
        public void ArgumentNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Empty()
        {
            var argument = string.Empty;
            var exception = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNullOrEmpty(() => argument, argument));
            Assert.Equal("argument", exception.ParamName);
        }

        [Fact]
        public void ValueNotNull_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Assert.Throws<NullReferenceException>(() => Guard.ValueNotNull(() => argument, argument));
        }

        [Fact]
        public void ValueNotNull_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            string argument = null;
            var exception = Assert.Throws<NullReferenceException>(() => Guard.ValueNotNull(() => argument, argument));
            Assert.Equal("argument", exception.Message);
        }

        [Fact]
        public void ValueNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Null()
        {
            string argument = null;
            Assert.Throws<NullReferenceException>(() => Guard.ValueNotNullOrEmpty(() => argument, argument));
        }

        [Fact]
        public void ValueNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Null()
        {
            var argument = string.Empty;
            var exception = Assert.Throws<NullReferenceException>(() => Guard.ValueNotNullOrEmpty(() => argument, argument));
            Assert.Equal("argument", exception.Message);
        }

        [Fact]
        public void ValueNotNullOrEmpty_Throws_Null_When_Given_Argument_Is_Empty()
        {
            string argument = null;
            Assert.Throws<NullReferenceException>(() => Guard.ValueNotNullOrEmpty(() => argument, argument));
        }

        [Fact]
        public void ValueNotNullOrEmpty_Includes_Null_Argument_Name_In_Exception_When_Given_Argument_Is_Empty()
        {
            var argument = string.Empty;
            var exception = Assert.Throws<NullReferenceException>(() => Guard.ValueNotNullOrEmpty(() => argument, argument));
            Assert.Equal("argument", exception.Message);
        }
    }
}
