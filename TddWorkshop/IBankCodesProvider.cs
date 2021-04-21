using System.Collections.Generic;

namespace TddWorkshop
{
    public interface IBankCodesProvider
    {
        IEnumerable<string> BankCodes();
    }
}