using System.Collections.Generic;
using System.Threading;

namespace TddWorkshop.BankCodesProvider.Database
{
    public class Provider : IBankCodesProvider
    {
        public IEnumerable<string> BankCodes()
        {
            Thread.Sleep(5000); // simulating database calls
            return new[] {"INGB", "ABNA", "DEUT"};
        }
    }
}