using System.Linq;

namespace TddWorkshop
{
    internal class IbanValidatorNL : IIbanValidator
    {
        private readonly IBankCodeProvider _bankCodeProvider;
        public IbanValidatorNL(IBankCodeProvider bankCodeProvider) => 
            _bankCodeProvider = bankCodeProvider;

        public bool Check(string input)
        {
            return !string.IsNullOrEmpty(input)
                   && CheckControlCode(input)
                   && CheckBankCode(input);
        }

        private bool CheckBankCode(string input) => 
            _bankCodeProvider.BankCodes().Contains(input.Substring(4, 4));

        private static bool CheckControlCode(string input) => 
            int.TryParse(input.Substring(2, 2), out _);
    }
}