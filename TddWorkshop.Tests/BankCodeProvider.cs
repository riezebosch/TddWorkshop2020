using System.Threading.Tasks;

namespace TddWorkshop.Tests
{
    internal class BankCodeProvider : IBankCodeProvider
    {
        public Task<string[]> BankCodes() => 
            Task.FromResult(new[] { "INGB", "RABO" });
    }
}