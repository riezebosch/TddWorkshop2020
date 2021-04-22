using System.Collections.Generic;
using Moq;
using Xunit;

namespace TddWorkshop.Tests
{
    public class MockingDemo
    {
        [Fact]
        public void MetDeEchteImplementatieDusLangzaam()
        {
            var validator = new IbanValidatorNL(new DatabaseBankCodesProvider());
            Assert.True(validator.IsValid("NL86INGB0002445588"));
        }
        
        [Fact]
        public void HandRolledMock()
        {
            var validator = new IbanValidatorNL(new HandRolledMock());
            Assert.True(validator.IsValid("NL86INGB0002445588"));
        }
        
        [Fact]
        public void HandRolledMockControleOpAanroep()
        {
            // Arrange
            var provider = new HandRolledMock();
            var validator = new IbanValidatorNL(provider);
            
            // Act
            validator.IsValid("NL86INGB0002445588");
            
            // Assert
            Assert.True(provider.IsValidAanroep);
        }

        [Fact]
        public void Moq()
        {
            var provider = new Mock<IBankCodesProvider>();
            provider
                .Setup(x => x.BankCodes())
                .Returns(new[] {"XXXX"});
            
            var validator = new IbanValidatorNL(provider.Object);
            
            Assert.True(validator.IsValid("NL86XXXX0002445588"));
        }
        
        [Fact]
        public void MoqAanroep()
        {
            // Arrange
            var provider = new Mock<IBankCodesProvider>();
            var validator = new IbanValidatorNL(provider.Object);
            
            // Act
            validator.IsValid("NL86INGB0002445588");
            
            // Assert
            provider.Verify(x => x.BankCodes(), Times.Exactly(1));
        }
    }

    public class HandRolledMock : IBankCodesProvider
    {
        public IEnumerable<string> BankCodes()
        {
            IsValidAanroep = true;
            return new[] {"INGB"};
        }

        public bool IsValidAanroep { get; set; }
    }
}