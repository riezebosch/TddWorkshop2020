namespace TddWorkshop
{
    internal static class IbanHelpers
    {
        public static string BankCode(this string iban) => 
            iban.Substring(4, 4);
    }
}