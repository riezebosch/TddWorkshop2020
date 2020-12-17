using System.Linq;

namespace TddWorkshop
{
    internal class IbanValidatorNL : IIbanValidator
    {
        public bool Check(string input)
        {
            return !string.IsNullOrEmpty(input) 
                   && CheckCountryCode(input) 
                   && CheckControlCode(input)
                   && CheckBankCode(input);
        }

        private static bool CheckBankCode(string input) => 
            new[] { "INGB", "RABO" }.Contains(input.Substring(4, 4));

        private static bool CheckControlCode(string input) => 
            int.TryParse(input.Substring(2, 2), out _);

        private static bool CheckCountryCode(string input) => 
            input.StartsWith("NL");
    }
}