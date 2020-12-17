using System.Threading.Tasks;
using FluentAssertions;
using Rebus.Activation;
using Rebus.Config;
using Xunit;

namespace BankCode.Rebus.IntegrationTests
{
    public class BankCodeServiceTests
    {
        /// <summary>
        /// docker run -d --hostname my-rabbit --name some-rabbit -p 8080:15672 -p 5672:5672 rabbitmq:management-alpine
        /// </summary>
        [Fact]
        public async Task BankCodesUpdatedAfterMessageReceived()
        {
            // Arrange
            var service = new BankCodeService();

            using var activator = new BuiltinHandlerActivator();
            activator.Register(() => service);
            
            using var bus = Configure.With(activator)
                .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "test2"))
                .Start();

            await bus.Subscribe<NewBankCodes>();
            
            // Act
            await bus.Publish(new NewBankCodes { Codes = new[] { "ING" } });
            
            // Assert
            var result = await service.BankCodes();
            result.Should().BeEquivalentTo("ING");
        }
    }
}