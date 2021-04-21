using System.Collections.Generic;
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

            var actual = new IbanValidatorNL(new TestBankCodesProvider()).IsValid(iban);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidBankCodeReturnsFalse()
        {
            var iban = "NL86INXB0002445588";
            var expected = false;

            var actual = new IbanValidatorNL(new TestBankCodesProvider()).IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void EmptyStringReturnsFalse()
        {
            var iban = "";
            var expected = false;

            var actual = new IbanValidatorNL(new TestBankCodesProvider()).IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void InvalidLengthReturnsFalse()
        {
            var iban = "NL86INGB000244558845678";
            var expected = false;

            var actual = new IbanValidatorNL(new TestBankCodesProvider()).IsValid(iban);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void InvalidBankCodeShouldBeFalse() => 
            new IbanValidatorNL(new TestBankCodesProvider())
                .IsValid("NL86YYYY0002445588")
                .Should()
                .BeFalse();

        
        private class TestBankCodesProvider : IBankCodesProvider
        {
            public IEnumerable<string> BankCodes() => 
                new[] { "INGB"};
        }
    }
}