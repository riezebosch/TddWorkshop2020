using System;
using System.Collections.Generic;

namespace TddWorkshop
{
    public static class IbanValidator
    {
        private const int CountryCodeLength = 2;

        private static readonly IDictionary<string, IIbanValidator> Validators = new Dictionary<string, IIbanValidator>
        {
            ["BE"] = new IbanValidatorBE(),
            ["NL"] = new IbanValidatorNL(new BankCodeProvider())
        };
        
        public static bool IsValid(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            input = input.Replace(" ", "");
            if (input.Length < CountryCodeLength)
            {
                return false;
            }
            
            return Validators.TryGetValue(input.Substring(0, CountryCodeLength), out var validator) 
                   && validator.Check(input);
        }
    }
}