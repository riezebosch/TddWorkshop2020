using System;
using FluentAssertions;
using Xunit;

namespace TddWorkshop.IntegrationTests
{
    public class UnitTest1
    {
        
        [Theory]
        [InlineData("NL25ABNA0477256600")]
        [InlineData("NL31DEUT0319810577")]
        public void ValidIbanFromOtherBanks(string iban)
        {
            new IbanValidatorNL(new DatabaseBankCodesProvider())
                .IsValid(iban)
                .Should()
                .BeTrue();
        }
    }
}