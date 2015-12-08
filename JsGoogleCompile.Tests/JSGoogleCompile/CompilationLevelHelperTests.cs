namespace JsGoogleCompile.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CompilationLevelHelperTests
    {
        [TestMethod]
        public void Code_For_Simple_Optimizations_Returns_Expected_Result()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("S");

            // Assert
            Assert.AreEqual("SIMPLE_OPTIMIZATIONS", code);
        }

        [TestMethod]
        public void Code_For_Simple_Optimizations_Returns_Expected_Result_Regardless_Of_Case()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("s");

            // Assert
            Assert.AreEqual("SIMPLE_OPTIMIZATIONS", code);
        }

        [TestMethod]
        public void Code_For_Whitespace_Only_Optimizations_Returns_Expected_Result()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("W");

            // Assert
            Assert.AreEqual("WHITESPACE_ONLY", code);
        }

        [TestMethod]
        public void Code_For_Advanced_Optimizations_Returns_Expected_Result()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("A");

            // Assert
            Assert.AreEqual("ADVANCED_OPTIMIZATIONS", code);
        }

        [TestMethod]
        public void Null_Code_Returns_Advanced_Optimizations_As_Default()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From(null);

            // Assert
            Assert.AreEqual("ADVANCED_OPTIMIZATIONS", code);
        }

        [TestMethod]
        public void Empty_Code_Returns_Advanced_Optimizations_As_Default()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From(string.Empty);

            // Assert
            Assert.AreEqual("ADVANCED_OPTIMIZATIONS", code);
        }

        [TestMethod]
        public void Unrecognised_Code_Returns_Advanced_Optimizations_As_Default()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("akjshdasjhgdkjashgd");

            // Assert
            Assert.AreEqual("ADVANCED_OPTIMIZATIONS", code);
        }

        [TestMethod]
        public void IsValid_Returns_True_For_Valid_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid("A");

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValid_Returns_False_For_Invalid_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid("asdasdas");

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValid_Returns_False_For_Null_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid(null);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValid_Returns_False_For_Empty_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid(string.Empty);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
