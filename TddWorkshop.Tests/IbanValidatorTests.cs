using System;
using FluentAssertions;
using Moq;
using NSubstitute;
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
            var result = new IbanValidator(new BankCodeProvider()).IsValid(input);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EmptyIbanReturnsFalse()
        {
            // Arrange
            var input = "";
            
            // Act
            var result = new IbanValidator(new BankCodeProvider()).IsValid(input);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void NullShouldThrowArgumentException()
        {
            var exception = Assert
                .Throws<ArgumentNullException>(() => new IbanValidator(new BankCodeProvider()).IsValid(null));
            
            Assert.Equal("input", exception.ParamName);
            exception
                .ParamName
                .Should()
                .Be("input");
        }
        
        [Fact]
        public void NullShouldThrowArgumentExceptionWithAssertionFramework()
        {
            Action act = () => new IbanValidator(new BankCodeProvider()).IsValid(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .Which
                .ParamName
                .Should()
                .Be("input");
        }
        
        [Fact]
        public void NoCheckCodeReturnsFalse() => 
            new IbanValidator(new BankCodeProvider())
                .IsValid("NLXX INGB 0002 4455 88")
                .Should()
                .BeFalse();
        
        [Fact]
        public void InvalidBankCodeReturnsFalse() => 
            new IbanValidator(new BankCodeProvider())
                .IsValid("NL86XXXX0002445588")
                .Should()
                .BeFalse();

        [InlineData("", false, "empty")]
        [InlineData("xxx", false, "obviously wrong")]
        [InlineData("NL11RABO0110099222", true, "gemeente amsterdam")]
        [Theory]
        public void ShouldAlsoWorkFor(string iban, bool expected, string because) => 
            new IbanValidator(new BankCodeProvider()).IsValid(iban).Should().Be(expected, because);

        [Fact]
        public void ValidBelgianIbanShouldReturnTrue() => 
            new IbanValidator(new BankCodeProvider()).IsValid("BE57 6792 0060 9235").Should().BeTrue();
        
        [Fact]
        public void GivenMockTellingBankCodeDoesNotExist_WhenValidating_ThenReturnFalse()
        {
            var provider = Substitute.For<IBankCodeProvider>();
            provider.BankCodes().Returns(new string[0]);
            
            new IbanValidator(provider)
                .IsValid("NL11RABO0110099222")
                .Should()
                .BeFalse();
        }
        
        [Fact]
        public void GivenMoqTellingBankCodeDoesNotExist_WhenValidating_ThenReturnFalse()
        {
            var provider = new Mock<IBankCodeProvider>();
            provider
                .Setup(x => x.BankCodes())
                .Returns(new string[0]);
            
            new IbanValidator(provider.Object)
                .IsValid("NL11RABO0110099222")
                .Should()
                .BeFalse();
        }
    }
}
