using System.Collections.Generic;
using System.Threading;

namespace TddWorkshop.BankCodesProvider.RestService
{
    public class Provider : IBankCodesProvider
    {
        public IEnumerable<string> BankCodes()
        {
            Thread.Sleep(5000); // simulating rest call
            return new[] {"INGB", "ABNA", "DEUT"};
        }
    }
}