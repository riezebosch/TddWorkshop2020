using System.Linq;

namespace TddWorkshop
{
    public static class IbanValidatorNL
    {
        public static bool IsValid(string iban)
        {
            return IsValidLength(iban) && 
                   IsValidBankCode(iban);
        }

        private static bool IsValidBankCode(string iban) =>
            new[] {"INGB", "ABNA", "DEUT"}.Contains(iban.BankCode());

        private static bool IsValidLength(string iban) => 
            iban.Length == 18;
    }
}