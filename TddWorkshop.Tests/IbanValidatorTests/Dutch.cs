using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NSubstitute;
using Xunit;

namespace TddWorkshop.Tests
{
    public class Dutch
    {
        [Fact]
        public async Task ValidIbanReturnsTrue()
        {
            // Arrange
            var input = "NL86 INGB 0002 4455 88";
            
            // Act
            var result = await new IbanValidator(new BankCodeProvider()).IsValid(input);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task EmptyIbanReturnsFalse()
        {
            // Arrange
            var input = "";
            
            // Act
            var result = await new IbanValidator(new BankCodeProvider()).IsValid(input);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public async Task NullShouldThrowArgumentException()
        {
            var exception = await Assert
                .ThrowsAsync<ArgumentNullException>(() => new IbanValidator(new BankCodeProvider()).IsValid(null));
            
            Assert.Equal("input", exception.ParamName);
            exception
                .ParamName
                .Should()
                .Be("input");
        }
        
        [Fact]
        public void NullShouldThrowArgumentExceptionWithAssertionFramework()
        {
            Func<Task> act = async () => await new IbanValidator(new BankCodeProvider()).IsValid(null);
            act.Should()
                .Throw<ArgumentNullException>()
                .Which
                .ParamName
                .Should()
                .Be("input");
        }
        
        [Fact]
        public async Task NoCheckCodeReturnsFalse()
        {
            var result = await new IbanValidator(new BankCodeProvider())
                .IsValid("NLXX INGB 0002 4455 88");
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task InvalidBankCodeReturnsFalse()
        {
            var result = await new IbanValidator(new BankCodeProvider())
                .IsValid("NL86XXXX0002445588");
            result
                .Should()
                .BeFalse();
        }

        [InlineData("", false, "empty")]
        [InlineData("xxx", false, "obviously wrong")]
        [InlineData("NL11RABO0110099222", true, "gemeente amsterdam")]
        [Theory]
        public async Task ShouldAlsoWorkFor(string iban, bool expected, string because)
        {
            var result = await new IbanValidator(new BankCodeProvider()).IsValid(iban);
            result.Should().Be(expected, because);
        }

        [Fact]
        public async Task ValidBelgianIbanShouldReturnTrue()
        {
            var result = await new IbanValidator(new BankCodeProvider()).IsValid("BE57 6792 0060 9235");
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GivenMockTellingBankCodeDoesNotExist_WhenValidating_ThenReturnFalse()
        {
            var provider = Substitute.For<IBankCodeProvider>();
            provider.BankCodes().Returns(new string[0]);

            var result = await new IbanValidator(provider)
                .IsValid("NL11RABO0110099222");
            result
                .Should()
                .BeFalse();
        }
        
        [Fact]
        public async Task GivenMoqTellingBankCodeDoesNotExist_WhenValidating_ThenReturnFalse()
        {
            var provider = new Mock<IBankCodeProvider>();
            provider
                .Setup(x => x.BankCodes())
                .ReturnsAsync(new string[0]);

            var result = await new IbanValidator(provider.Object)
                .IsValid("NL11RABO0110099222");
            result
                .Should()
                .BeFalse();
        }
    }
}
