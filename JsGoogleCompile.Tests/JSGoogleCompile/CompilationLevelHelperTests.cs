namespace JsGoogleCompile.Tests
{
    using Xunit;

    public class CompilationLevelHelperTests
    {
        [Fact]
        public void Code_For_Simple_Optimizations_Returns_Expected_Result()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("S");

            // Assert
            Assert.Equal("SIMPLE_OPTIMIZATIONS", code);
        }

        [Fact]
        public void Code_For_Simple_Optimizations_Returns_Expected_Result_Regardless_Of_Case()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("s");

            // Assert
            Assert.Equal("SIMPLE_OPTIMIZATIONS", code);
        }

        [Fact]
        public void Code_For_Whitespace_Only_Optimizations_Returns_Expected_Result()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("W");

            // Assert
            Assert.Equal("WHITESPACE_ONLY", code);
        }

        [Fact]
        public void Code_For_Advanced_Optimizations_Returns_Expected_Result()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("A");

            // Assert
            Assert.Equal("ADVANCED_OPTIMIZATIONS", code);
        }

        [Fact]
        public void Null_Code_Returns_Advanced_Optimizations_As_Default()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From(null);

            // Assert
            Assert.Equal("ADVANCED_OPTIMIZATIONS", code);
        }

        [Fact]
        public void Empty_Code_Returns_Advanced_Optimizations_As_Default()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From(string.Empty);

            // Assert
            Assert.Equal("ADVANCED_OPTIMIZATIONS", code);
        }

        [Fact]
        public void Unrecognised_Code_Returns_Advanced_Optimizations_As_Default()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var code = helper.From("akjshdasjhgdkjashgd");

            // Assert
            Assert.Equal("ADVANCED_OPTIMIZATIONS", code);
        }

        [Fact]
        public void IsValid_Returns_True_For_Valid_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid("A");

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void IsValid_Returns_False_For_Invalid_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid("asdasdas");

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_Returns_False_For_Null_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid(null);

            // Assert
            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_Returns_False_For_Empty_Code()
        {
            // Arrange
            var helper = new CompilationLevelHelper();

            // Act
            var isValid = helper.IsValid(string.Empty);

            // Assert
            Assert.False(isValid);
        }
    }
}
