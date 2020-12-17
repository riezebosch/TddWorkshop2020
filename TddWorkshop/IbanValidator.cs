using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TddWorkshop
{
    public class IbanValidator
    {
        private const int CountryCodeLength = 2;

        private readonly IDictionary<string, IIbanValidator> _validators;

        public IbanValidator(IBankCodeProvider bankCodeProvider)
        {
            _validators = new Dictionary<string, IIbanValidator>
            {
                ["BE"] = new IbanValidatorBE(),
                ["NL"] = new IbanValidatorNL(bankCodeProvider)
            };
        }
        
        public async Task<bool> IsValid(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            input = input.Replace(" ", "");
            if (input.Length < CountryCodeLength)
            {
                return false;
            }
            
            return _validators.TryGetValue(input.Substring(0, CountryCodeLength), out var validator) 
                   && await validator.Check(input);
        }
    }
}