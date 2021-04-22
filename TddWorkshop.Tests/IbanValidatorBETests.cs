using FluentAssertions;
using Xunit;

namespace TddWorkshop.Tests
{
    public class IbanValidatorBETests
    {
        [Fact]
        public void AllIbansAreValid()
        {
            // omwille van het voorbeeld een tweede validator die nog lang niet af is...
            var iban = "BE61310126985517";
            new IbanValidatorBE().IsValid().Should().BeTrue();
        }
    }
}