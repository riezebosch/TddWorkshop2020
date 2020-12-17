using System.Threading.Tasks;
using Rebus.Handlers;
using TddWorkshop;

namespace BankCode.Rebus
{
    public class BankCodeService : IHandleMessages<NewBankCodes>, IBankCodeProvider
    {
        private readonly TaskCompletionSource<string[]> _message = new TaskCompletionSource<string[]>();
        
        public async Task Handle(NewBankCodes message) => 
            _message.SetResult(message.Codes);

        public Task<string[]> BankCodes() => _message.Task;
    }
}