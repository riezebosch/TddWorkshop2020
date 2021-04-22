using System;
using FluentAssertions;
using TddWorkshop.BankCodesProvider.Database;
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
            new IbanValidatorNL(new Provider())
                .IsValid(iban)
                .Should()
                .BeTrue();
        }
    }
}