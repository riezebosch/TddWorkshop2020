using System.Linq;
using System.Threading.Tasks;

namespace TddWorkshop
{
    internal class IbanValidatorNL : IIbanValidator
    {
        private readonly IBankCodeProvider _bankCodeProvider;
        public IbanValidatorNL(IBankCodeProvider bankCodeProvider) => 
            _bankCodeProvider = bankCodeProvider;

        public async Task<bool> Check(string input)
        {
            return !string.IsNullOrEmpty(input)
                   && CheckControlCode(input)
                   && await CheckBankCode(input);
        }

        private async Task<bool> CheckBankCode(string input) => 
            (await _bankCodeProvider.BankCodes()).Contains(input.Substring(4, 4));

        private static bool CheckControlCode(string input) => 
            int.TryParse(input.Substring(2, 2), out _);
    }
}