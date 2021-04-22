using System;
using System.Collections.Generic;
using System.Linq;

namespace TddWorkshop
{
    public class IbanValidatorNL
    {
        private readonly IBankCodesProvider _provider;

        public IbanValidatorNL(IBankCodesProvider provider) => 
            _provider = provider;

        public bool IsValid(string iban)
        {
            if (iban == null) throw new ArgumentNullException(nameof(iban));
            
            return IsValidLength(iban) && 
                   IsValidBankCode(iban);
        }

        private bool IsValidBankCode(string iban) => 
            _provider.BankCodes().Contains(iban.BankCode());

        private static bool IsValidLength(string iban) => 
            iban.Length == 18;
    }
}