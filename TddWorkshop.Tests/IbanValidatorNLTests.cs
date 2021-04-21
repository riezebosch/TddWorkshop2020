using FluentAssertions;
using Xunit;

namespace TddWorkshop.Tests
{

    public class IbanValidatorNLTests
    {
        [Fact]
        public void ValidIbanTest()
        {
            var iban = "NL86INGB0002445588";
            var expected = true;

            var actual = IbanValidatorNL.IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void InvalidBankCodeReturnsFalse()
        {
            var iban = "NL86INXB0002445588";
            var expected = false;

            var actual = IbanValidatorNL.IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void EmptyStringReturnsFalse()
        {
            var iban = "";
            var expected = false;

            var actual = IbanValidatorNL.IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void InvalidLengthReturnsFalse()
        {
            var iban = "NL86INGB000244558845678";
            var expected = false;

            var actual = IbanValidatorNL.IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void InvalidBankCodeShouldBeFalse() => 
            IbanValidatorNL
                .IsValid("NL86YYYY0002445588")
                .Should()
                .BeFalse();

        [Theory]
        [InlineData("NL25ABNA0477256600")]
        [InlineData("NL31DEUT0319810577")]
        public void ValidIbanFromOtherBanks(string iban)
        {
            IbanValidatorNL
                .IsValid(iban)
                .Should()
                .BeTrue();
        }
    }
}