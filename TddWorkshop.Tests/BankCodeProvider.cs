namespace TddWorkshop.Tests
{
    internal class BankCodeProvider : IBankCodeProvider
    {
        public string[] BankCodes() => 
            new[] { "INGB", "RABO" };
    }
}