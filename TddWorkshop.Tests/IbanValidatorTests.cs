using System;
using FluentAssertions;
using Xunit;

namespace TddWorkshop.Tests
{
    public class IbanValidatorTests
    {
        [Fact]
        public void ValidIbanReturnsTrue()
        {
            // Arrange
            var input = "NL86 INGB 0002 4455 88";
            
            // Act
            var result = IbanValidator.IsValid(input);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EmptyIbanReturnsFalse()
        {
            // Arrange
            var input = "";
            
            // Act
            var result = IbanValidator.IsValid(input);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void NullShouldThrowArgumentException()
        {
            var exception = Assert
                .Throws<ArgumentNullException>(() => IbanValidator.IsValid(null));
            
            Assert.Equal("input", exception.ParamName);
            exception
                .ParamName
                .Should()
                .Be("input");
        }
        
        [Fact]
        public void NullShouldThrowArgumentExceptionWithAssertionFramework()
        {
            Action act = () => IbanValidator.IsValid(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .Which
                .ParamName
                .Should()
                .Be("input");
        }

        [InlineData("", false, "empty")]
        [InlineData("xxx", false, "obviously wrong")]
        [InlineData("NL11RABO0110099222", true, "gemeente amsterdam")]
        [Theory]
        public void ShouldAlsoWorkFor(string iban, bool expected, string because) => 
            IbanValidator.IsValid(iban).Should().Be(expected, because);
    }
}
